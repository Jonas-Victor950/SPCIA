using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.GetEntities.SPCIA;
using BRCSystem.ClassLibrary.Helpers;
using BRCSystem.ClassLibrary.POSStation.Entities;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SPCIA.API.Models;
using System.Collections;
using System.ComponentModel;
using static SPCIA.API.Services.ChartControlService;

namespace SPCIA.API.Services
{
    public interface IChartControlService
    {
        public ChartControlResponse GetByFilter(ChartControlFilterRequest request);
        public Boolean? CheckForAlarm(int machineId, int processId, List<int> CharacteristicId);
        public List<ChartControlResponse> GetLimitHistory(ChartControlFilterRequest request);

    }

    public class ChartControlService(DataContext context) : IChartControlService
    {
        private readonly DataContext _context = context;

        public ChartControlResponse GetByFilter(ChartControlFilterRequest request)
        {
            var query = _context.ProcessSamplings
                .Include(x => x.Samples).ThenInclude(s => s.Inputs)
                .Include(x => x.Samples).ThenInclude(s => s.Characteristic)
                .Where(x => x.MachineId == request.MachineId && x.ProcessId == request.ProcessId
                && x.Samples.Any(s => s.CharacteristicId == request.CharacteristicId));

            Characteristic characteristic = _context.Characteristics.Where(c => c.Id == request.CharacteristicId).SingleOrDefault();

            if (request.Classifiers != null && request.Classifiers.Count > 0) query = query.Where(x => request.Classifiers.Contains(x.Classifier));

            if (request.SampleSize > 0)
            {
                if (query.Count() > request.SampleSize && (query.Count() - request.SampleSize) >= 1)
                {
                    query = query.Skip(Math.Max(0, query.Count() - (int)request.SampleSize));
                }
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(cl => cl.SampleDate >= request.StartDate);
            }
            else
            {
                query = query.Where(cl => cl.SampleDate >= DateTime.UtcNow.AddDays(-30));
            }

            if (request.EndDate.HasValue)
            {
                query = query.Where(cl => cl.SampleDate <= request.EndDate);
            }
            else
            {
                query = query.Where(cl => cl.SampleDate <= DateTime.UtcNow);
            }

            var result = query.ToList();

            if (result.Count() == 0) return null;

            foreach (var res in result)
            {
                if (res.Samples is not null && res.Samples.Count > 0)
                {
                    res.Samples = res.Samples.Where(r => r.CharacteristicId == request.CharacteristicId).ToList();
                }
            }

            List<Samples> listsSamples = new List<Samples>();

            int averageSampleSize = 0;
            List<int> sampleSizes = new List<int>();

            foreach (ProcessSampling ps in result)
            {
                foreach (Samples samples in ps.Samples)
                {
                    listsSamples.Add(samples);
                    sampleSizes.Add(samples.Inputs.Count());
                }                
            }

            if (listsSamples.Count < 2)
            {
                return null;
            }

            for(int i = 0; i < sampleSizes.Count(); i++){
                averageSampleSize = averageSampleSize + sampleSizes[i];
            }

            averageSampleSize = averageSampleSize / sampleSizes.Count();

            if(averageSampleSize < 2){
                averageSampleSize = 5;
            }

            double a2 = _context.TableParametersSPC.Where(tps => tps.N <= averageSampleSize).OrderByDescending(tps => tps.N).FirstOrDefault().A2;
            double d2 = _context.TableParametersSPC.Where(tps => tps.N <= averageSampleSize).OrderByDescending(tps => tps.N).FirstOrDefault().D2;
            double d4 = _context.TableParametersSPC.Where(tps => tps.N <= averageSampleSize).OrderByDescending(tps => tps.N).FirstOrDefault().D4;

            ControlLimit controlLimit =
                _context.ControlLimits.Include(x => x.Limits).Where(
                        x => x.ProcessId == request.ProcessId &&
                        x.MachineId == request.MachineId &&
                        x.Active == true)
                    .First();
            Limit limit = controlLimit.Limits.Where(x => x.CharacteristicId == request.CharacteristicId).First();

            var calculation = new LimitCalculationResponse(listsSamples, characteristic, a2, d2, d4, limit);

            List<DateTime> timestamp = [];
            List<double> xbar = [];
            List<double> r = [];
            List<Classifier> classifiers = [];

            foreach (var samples in calculation.lists)
            {
                xbar.Add(samples.Xbar);
                r.Add(samples.R);
            }

            foreach (var res in result)
            {
                if (res.SampleDate is not null)
                {
                    timestamp.Add((DateTime)res.SampleDate);
                    classifiers.Add(res.Classifier);
                }
            }

            ChartControlResponse response = new()
            {
                Cpk = calculation.cpk,
                Ppk = calculation.ppk,
                Clr = limit.CLr,
                Clx = limit.CLx,
                Lclx = limit.LCLx,
                Lclr = limit.LCLr,
                Uclr = limit.UCLr,
                Uclx = limit.UCLx,
                Timestamp = timestamp,
                Xbar = xbar,
                R = r,
                Classifiers = classifiers,
                Rcolors = calculation.pointColorsRbar is not null ? calculation.pointColorsRbar : [],
                Xbarcolors = calculation.pointColorsXBar is not null ? calculation.pointColorsXBar : [],
            };

            return response;
        }

        public Boolean? CheckForAlarm(int machineId, int ProcessId, List<int> CharacteristicId)
        {
            try{
                var query = _context.ProcessSamplings
                .Include(x => x.Samples).ThenInclude(s => s.Inputs)
                .Include(x => x.Samples).ThenInclude(s => s.Characteristic)
                .Where(x => x.MachineId == machineId && x.ProcessId == ProcessId);  

                if(query.Count() == 0){
                    return false;
                }

                foreach(ProcessSampling ps in query){
                    if(ps.Samples != null && ps.Samples.Any()){
                        foreach(Samples sample in ps.Samples){
                            if(sample.Inputs == null || !sample.Inputs.Any()){
                                return false;
                            }
                        }
                    } else {
                        return false;
                    }
                }

            } catch(Exception){
                return false;
            }

            foreach(int charId in CharacteristicId){
                ChartControlFilterRequest request = new ChartControlFilterRequest();
                request.CharacteristicId = charId;
                request.ProcessId = ProcessId;
                request.MachineId = machineId;
                request.SampleSize = 10;

                List<ChartControlResponse> result = GetLimitHistory(request);

                if (result.Count() > 0) {
                    ChartControlResponse ccr = result.Last();

                    if (ccr.Xbarcolors.Count() > 0 && ccr.Xbarcolors.Last().Equals("Red") || ccr.Rcolors.Count() > 0 && ccr.Rcolors.Last().Equals("Red"))
                    {
                        return true;
                    }
                }
            }
           
            return false;
        }
    
        public List<ChartControlResponse> GetLimitHistory(ChartControlFilterRequest request)
        {
            var query = _context.ProcessSamplings
                .Include(x => x.Samples).ThenInclude(s => s.Inputs)
                .Include(x => x.Samples).ThenInclude(s => s.Characteristic)
                .Where(x => x.MachineId == request.MachineId && x.ProcessId == request.ProcessId
                && x.Samples.Any(s => s.CharacteristicId == request.CharacteristicId));

            Characteristic characteristic = _context.Characteristics.Where(c => c.Id == request.CharacteristicId).SingleOrDefault();

            if (request.Classifiers != null && request.Classifiers.Count > 0) query = query.Where(x => request.Classifiers.Contains(x.Classifier));

            if (request.SampleSize > 0)
            {
                if (query.Count() > request.SampleSize && (query.Count() - request.SampleSize) >= 1)
                {
                    query = query.Skip(Math.Max(0, query.Count() - (int)request.SampleSize));
                }
            }

            if (request.StartDate.HasValue)
            {
                query = query.Where(cl => cl.SampleDate >= request.StartDate);            

                if(!request.StartDate.Equals(request.EndDate)){
                    if (request.EndDate.HasValue)
                    {
                        query = query.Where(cl => cl.SampleDate.Value.Date <= request.EndDate.Value.Date);
                    }
                }
            }

            var result = query.ToList();

            if (result.Count() == 0){
                return null;
            } 

            foreach (var res in result)
            {
                if (res.Samples is not null && res.Samples.Count > 0)
                {
                    res.Samples = res.Samples.Where(r => r.CharacteristicId == request.CharacteristicId).ToList();
                }
            }            

            List<ControlLimit> controlLimits = _context.ControlLimits
                .Include(x => x.Limits)
                .Where(x => x.ProcessId == request.ProcessId && x.MachineId == request.MachineId)
                .ToList();

            List<Limit> limits = new List<Limit>();

            //Isso será utilizado para quando não existir limites cadastrados
            limits.Add(new Limit { SetDate = DateTime.UtcNow.AddYears(-999) });

            foreach (ControlLimit cl in controlLimits){
                for (int i = 0; i < cl.Limits.Count(); i++)
                {
                    Limit l = cl.Limits[i];
                    if (l.CharacteristicId == request.CharacteristicId){
                        /*if(request.StartDate.HasValue && request.EndDate.HasValue){
                            if(i+1 < limits.Count()){
                                if(l.SetDate >= request.StartDate && limits[i+1].SetDate <= request.EndDate){
                                    limits.Add(l);
                                }
                            } else {
                                if(l.SetDate <= request.StartDate){
                                    limits.Add(l);
                                }
                            }
                        } else{*/
                            limits.Add(l);
                       //}
                    }
                }
            }

            List<ChartControlResponse> response = new List<ChartControlResponse>();
            
            
            for(int l = 0; l < limits.Count(); l++){
                List<Samples> listsSamples = new List<Samples>();

                int averageSampleSize = 0;
                List<int> sampleSizes = new List<int>();
                List<Classifier> classifiers = new List<Classifier>();
                List<DateTime> timestamp = new List<DateTime>();

                foreach (ProcessSampling ps in result)
                {
                    if(l+1 < limits.Count()){
                        if(limits[l].SetDate <= ps.SampleDate && ps.SampleDate < limits[l+1].SetDate){
                            if(ps.Samples.Count() > 0){
                                timestamp.Add((DateTime)ps.SampleDate);
                                classifiers.Add(ps.Classifier);
                            }
                            
                            for (int indexSamples = ps.Samples.Count() - 1; indexSamples >= 0; indexSamples--){
                                listsSamples.Add(ps.Samples[indexSamples]);
                                sampleSizes.Add(ps.Samples[indexSamples].Inputs.Count());
                                ps.Samples.RemoveAt(indexSamples);
                            }                          
                        }      
                    } else{
                        if(limits[l].SetDate.Value.Date <= ps.SampleDate.Value.Date){
                            if(ps.Samples.Count() > 0){
                                timestamp.Add((DateTime)ps.SampleDate);
                                classifiers.Add(ps.Classifier);
                            }
                            for (int index = ps.Samples.Count() - 1; index >= 0; index--){
                                listsSamples.Add(ps.Samples[index]);
                                sampleSizes.Add(ps.Samples[index].Inputs.Count());
                                ps.Samples.RemoveAt(index);
                            } 
                        }       
                    }      
                }                

                if(sampleSizes.Count() > 0){
                    for(int i = 0; i < sampleSizes.Count(); i++){
                    averageSampleSize = averageSampleSize + sampleSizes[i];
                    }
                    averageSampleSize = averageSampleSize / sampleSizes.Count();
                }                

                if(averageSampleSize < 2){
                    averageSampleSize = 5;
                }

                double a2 = _context.TableParametersSPC.Where(tps => tps.N <= averageSampleSize).OrderByDescending(tps => tps.N).FirstOrDefault().A2;
                double d2 = _context.TableParametersSPC.Where(tps => tps.N <= averageSampleSize).OrderByDescending(tps => tps.N).FirstOrDefault().D2;
                double d4 = _context.TableParametersSPC.Where(tps => tps.N <= averageSampleSize).OrderByDescending(tps => tps.N).FirstOrDefault().D4;
                    
                var calculation = new LimitCalculationResponse(listsSamples, characteristic, a2, d2, d4, limits[l]);
                
                List<double> xbar = [];
                List<double> r = [];

                foreach (var samples in calculation.lists)
                {
                    xbar.Add(samples.Xbar);
                    r.Add(samples.R);
                }

                ChartControlResponse chartControlGraph = new()
                {
                    Cpk = calculation.cpk,
                    Ppk = calculation.ppk,
                    Clr = limits[l].CLr,
                    Clx = limits[l].CLx,
                    Lclx = limits[l].LCLx,
                    Lclr = limits[l].LCLr,
                    Uclr = limits[l].UCLr,
                    Uclx = limits[l].UCLx,
                    Timestamp = timestamp,
                    Xbar = xbar,
                    R = r,
                    Classifiers = classifiers,
                    Rcolors = calculation.pointColorsRbar is not null ? calculation.pointColorsRbar : [],
                    Xbarcolors = calculation.pointColorsXBar is not null ? calculation.pointColorsXBar : [],
                    HasLimit = true,
                };

                var hasLimit = limits[l].Id is null;

                if (hasLimit)
                {
                    chartControlGraph.HasLimit = false;
                    chartControlGraph.Rcolors = chartControlGraph.Rcolors.Select(x => { return "Blue"; }).ToList();
                    chartControlGraph.Xbarcolors = chartControlGraph.Xbarcolors.Select(x => { return "Blue"; }).ToList();
                    chartControlGraph.Ppk = 0;
                    chartControlGraph.Ppk = 0;
                    chartControlGraph.Lclx = null;
                    chartControlGraph.Lclr = null;
                }

                response.Add(chartControlGraph);
            }
            
            return response;
        }    
    }
}
