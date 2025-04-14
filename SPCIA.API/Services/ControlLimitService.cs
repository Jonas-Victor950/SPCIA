using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.Helpers;
using Microsoft.EntityFrameworkCore;
using SPCIA.API.Models;
using BRCSystem.ClassLibrary.GetEntities.SPCIA;
using System.Reflection.Metadata.Ecma335;
using Cuemon;

namespace SPCIA.API.Services
{
    public interface IControlLimitService
    {
        ControlLimit? GetById(int id);
        List<ControlLimit>? GetAll();
        ControlLimit Save(ControlLimit model);
        List<ControlLimit> GetAllByProcessId(int processId);
        Limit? getCalculatedLimits(int machineId, int processId, int characteristicId);
        Limit? getCalculatedLimitsByCpk(int machineId, int processId, int characteristicId, double cpk);
    }
    public class ControlLimitService(DataContext context) : IControlLimitService
    {
        private readonly DataContext _context = context;

        public List<ControlLimit>? GetAll()
        {
            return _context.ControlLimits
               .Include(x => x.Limits).ThenInclude(l => l.Characteristic)
               .Include(x => x.Process).ThenInclude(p => p.Characteristics)
               .Include(x => x.Process).ThenInclude(p => p.Station)
               .Include(x => x.Machine).ThenInclude(y => y.Stations)
               .Include(x => x.User)
               .ToList();
        }

        public ControlLimit? GetById(int id) =>
            _context.ControlLimits
                .Include(x => x.Limits).ThenInclude(l => l.Characteristic)
                .Include(x => x.Process).ThenInclude(p => p.Characteristics)
                .Include(x => x.Process).ThenInclude(p => p.Station)
                .Include(x => x.Machine).ThenInclude(y => y.Stations)
                .Include(x => x.User)
                .SingleOrDefault(x => x.Id == id);

        public ControlLimit Save(ControlLimit model)
        {
            if (model.CreationDate == null)
            {
                model.CreationDate = DateTime.UtcNow;
            }
            _context.Limits.AttachRange(model.Limits);

            foreach (Limit limit in model.Limits)
            {
                if(limit.SetDate == null)
                {
                    limit.SetDate = DateTime.UtcNow;    
                }

                if(limit.SetById is not null)
                {
                    limit.SetById = model.UserId;
                }
            }

            _context.AddRange(model);

            List<ControlLimit> controlLimits = _context.ControlLimits
               .Include(x => x.Limits).ThenInclude(l => l.Characteristic)
               .Include(x => x.Process).ThenInclude(p => p.Characteristics)
               .Include(x => x.Process).ThenInclude(p => p.Station)
               .Include(x => x.Machine).ThenInclude(y => y.Stations)
               .Include(x => x.User)
               .Where(x => x.MachineId == model.MachineId && x.ProcessId == model.ProcessId && x.Active)
               .ToList();


            foreach (ControlLimit cl in controlLimits)
            {
                if (cl.Active)
                {
                    cl.Active = false;
                }
            }

           
            _context.SaveChanges();

            return model;
        }

        public List<ControlLimit> GetAllByProcessId(int processId) => _context.ControlLimits
                .Include(x => x.Limits).ThenInclude(l => l.Characteristic)
                .Include(x => x.Process).ThenInclude(p => p.Characteristics)
                .Include(x => x.User)
                .Where(x => x.ProcessId == processId)
                .ToList().Select(x => { x.User.PasswordHash = null; x.User.MFACode = null ; return x; }).ToList();

        public Limit? getCalculatedLimits(int machineId, int processId, int characteristicId)
        {
            var query = _context.ProcessSamplings
                .Include(x => x.Samples).ThenInclude(s => s.Inputs)
                .Include(x => x.Samples).ThenInclude(s => s.Characteristic)
                .Where(x => x.MachineId == machineId && x.ProcessId == processId
                && x.Samples.Any(s => s.CharacteristicId == characteristicId));

            Characteristic characteristic = _context.Characteristics.Where(c => c.Id == characteristicId).SingleOrDefault();

            var result = query.ToList();

            if (result.Count == 0)
            {
                return null;
            }

            foreach (var res in result)
            {
                if (res.Samples is not null && res.Samples.Count > 0)
                {
                    res.Samples = res.Samples.Where(r => r.CharacteristicId == characteristicId).ToList();
                }
            }

            List<Samples> lists = new List<Samples>();
            int averageSampleSize = 0;
            List<int> sampleSizes = new List<int>();

            foreach (ProcessSampling ps in result)
            {
                foreach(Samples sample in ps.Samples)
                {
                    lists.Add(sample);
                    sampleSizes.Add(sample.Inputs.Count());
                }   
            }

            if (lists.Count < 2)
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

            Limit limit = new Limit();
            limit.UCLr = 1;
            limit.UCLx = 1;
            limit.LCLx = 1;

            var calculation = new LimitCalculationResponse(lists, characteristic, a2, d2, d4, limit);

            limit.Characteristic = characteristic;
            limit.CharacteristicId = characteristicId;
            limit.UCLr = LimitCalculate.calculateUCLr(calculation.rBar, d4);     
            limit.UCLx = LimitCalculate.calculateUCLx(calculation.doubleXBar, calculation.rBar, a2);
            limit.LCLx = LimitCalculate.calculateLCLx(calculation.doubleXBar, calculation.rBar, a2);
            limit.CLr = LimitCalculate.calculateCLr(calculation.rBar);
            limit.CLx = LimitCalculate.calculateCLx(calculation.doubleXBar);
            limit.SetDate = DateTime.UtcNow;

            return limit;
        }

        public Limit? getCalculatedLimitsByCpk(int machineId, int processId, int characteristicId, double cpk)
        {
            var query = _context.ProcessSamplings
                .Include(x => x.Samples).ThenInclude(s => s.Inputs)
                .Include(x => x.Samples).ThenInclude(s => s.Characteristic)
                .Where(x => x.MachineId == machineId && x.ProcessId == processId
                && x.Samples.Any(s => s.CharacteristicId == characteristicId));

            Characteristic characteristic = _context.Characteristics.Where(c => c.Id == characteristicId).SingleOrDefault();

            var result = query.ToList();

            if (result.Count == 0) return null;

            foreach (var res in result)
            {
                if (res.Samples is not null && res.Samples.Count > 0)
                {
                    res.Samples = res.Samples.Where(r => r.CharacteristicId == characteristicId).ToList();
                }
            }

            List<Samples> lists = new List<Samples>();
            int averageSampleSize = 0;
            List<int> sampleSizes = new List<int>();


            foreach (ProcessSampling ps in result)
            {
                foreach (Samples sample in ps.Samples)
                {
                    lists.Add(sample);
                    sampleSizes.Add(sample.Inputs.Count());
                }
            }

            if (lists.Count == 0)
            {
                throw new ResultIsEmpty();
            }

            var inputQtd = 0;

            foreach(Samples samples in lists)
            {
                inputQtd = inputQtd + samples.Inputs.Count;
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

            Limit limit = new Limit();
            limit.UCLr = 1;
            limit.UCLx = 1;
            limit.LCLx = 1;

            var calculation = new LimitCalculationResponse(lists, characteristic, a2, d2, d4, limit);
            double stdDeviation = LimitCalculate.calculateStandardDeviationByCpk(characteristic.USL, characteristic.LSL, calculation.doubleXBar, cpk);
            limit.Characteristic = characteristic;
            limit.CharacteristicId = characteristicId;
            limit.UCLr = LimitCalculate.calculateUCLr(calculation.rBar, d4);
            limit.UCLx = LimitCalculate.calculateUCLxByCpk(characteristic.USL, cpk, inputQtd, stdDeviation);
            limit.LCLx = LimitCalculate.calculateLCLxByCpk(characteristic.LSL, cpk, inputQtd, stdDeviation);
            limit.CLr = LimitCalculate.calculateCLr(calculation.rBar);
            limit.CLx = LimitCalculate.calculateCLxByCpk(limit.UCLx, limit.LCLx);
            limit.SetDate = DateTime.UtcNow;           
            return limit;
        }
    }
}
