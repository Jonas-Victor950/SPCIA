using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.Helpers;
using Microsoft.EntityFrameworkCore;
using BRCSystem.ClassLibrary.POSStation.Entities;
using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;
using System.Linq;
using System;

namespace SPCIA.API.Services
{
    public interface IProcessService
    {
        public void Delete(int id);
        Process? GetById(int id);
        List<Process> GetAll();
        List<Process> GetByStationId(int stationId);
        Process Save(Process model);
        List<ProductLot> GetLotsById(int id, int machineId);
        bool AlreadExistType(string name, int stationId);
    }

    public class ProcessService : IProcessService
    {
        private readonly DataContext _context;

        public ProcessService(DataContext dbContext)
        {
            this._context = dbContext;
        }

        public Process Save(Process model)
        {
            if (model.Products is not null && model.Products!.Count > 0)
            {
                var productIds = model.Products!.Select(x => x.Id).ToList();
                var productCodes = model.Products!.Select(x => x.ProductCode).ToList();
                var products = _context.Products.AsNoTracking().Where(x => productCodes.Contains(x.ProductCode) && !productIds.Contains(x.Id)).ToList();

                model.Products!.AddRange(products);
            }
           
            if (model.Id == null)
            {
                _context.Products.AttachRange(model.Products);
                _context.AddRange(model);
            }
            else
            {
                var process = _context.Processes
                                        .Include(x => x.Characteristics)
                                        .Include(x => x.Station)
                                        .Include(x => x.Products)
                                        .SingleOrDefault(x => x.Id == model.Id);

                if (process == null) throw new NotFoundException($"Process with id {model.Id} not found!");

                process.Characteristics.Clear();
                process.Products.Clear();
                _context.UpdateRange(process);
                _context.SaveChanges();
                _context.ChangeTracker.Clear();

                _context.UpdateRange(model);
            }
            _context.SaveChanges();

            return model;
        }

        public void Delete(int id)
        {
            var model = _context.Processes.Find(id);

            if (model == null) throw new NotFoundException($"Process with id {id} not found!");
             
            List<ControlLimit> cl = _context.ControlLimits.Where(cl => cl.ProcessId == id).ToList();

            if(cl.Count() > 0){
                throw new CannotDelete($"Cannot delete processes with dependencies.");
            }

            try
            {
                _context.RemoveRange(model!);
                _context.SaveChanges();
            }
            catch (Exception)
            {
                model.Active = false;
                _context.UpdateRange(model!);
                _context.SaveChanges();
            }
        }

        public Process? GetById(int id)
        {
            var process = _context.Processes
                .Include(x => x.Characteristics).ThenInclude(y => y.UnitMeasurement)
                .Include(x => x.Products).ThenInclude(p => p.ProductType)
                .Include(x => x.Station)
                .SingleOrDefault(x => x.Id == id);

            process.AbleUpdate = !_context.ProcessSamplings.Any(x => x.ProcessId == id);

            return process;
        }
           

        public List<Process> GetAll()
        {
            var processes = _context.Processes
                .Include(x => x.Characteristics).ThenInclude(y => y.UnitMeasurement)
                .Include(x => x.Products).ThenInclude(p => p.ProductType)
                .Include(x => x.Station)
                .Where(x => x.Active)
                .ToList()
                .Select(x => {
                    x.Products = x.Products.GroupBy(p => p.ProductCode)
                                                        .Select(g => g.OrderByDescending(p => p.Id).FirstOrDefault()).ToList();
                    return x; 
                }).ToList();

            foreach (var process in processes)
            {
                process.AbleUpdate = !_context.ProcessSamplings.Any(y => y.ProcessId == process.Id);
                process.Products = process.Products.Select(x => { x.User = null; return x; }).ToList();
            }
            //processes.ForEach(x => x.AbleUpdate = !_context.ProcessSamplings.Any(y => y.ProcessId == x.Id));
            return processes;
        }

        public List<Process> GetByStationId(int stationId)
        {
            return _context.Processes
                .Include(x => x.Characteristics).ThenInclude(y => y.UnitMeasurement)
                .Include(x => x.Products).ThenInclude(p => p.ProductType)
                .Include(x => x.Station)
                .Where(x => x.StationId == stationId && x.Active)
                .ToList();
        }

        public List<ProductLot> GetLotsById(int processId, int machineId)
        {
            var productIds = _context.Processes
                                .Include(x => x.Products)
                                    .ThenInclude(x => x.ProductType)            
                                .Include(x => x.Station).ThenInclude(s => s.Machines)
                                .SingleOrDefault(x => x.Id == processId)?.Products.Select(x => x.Id);

            List<ProductLot> lots = _context.ProductLots.Where(x => productIds.Contains(x.ProductId) && x.Active
            && x.LotStatus == LotStatus.OnGoing)
                .Include((p) => p.LotStepDatas)
                .OrderByDescending(x => x.DateCreated).ToList();

            List<ProductLot> returnLots = new List<ProductLot>();

            foreach(ProductLot lot in lots)
            {
                foreach(LotStepData stepData in lot.LotStepDatas)
                {
                    if(stepData.MachineId == machineId && !returnLots.Contains(lot))
                    {
                        returnLots.Add(lot);
                    }
                }
            }

            return returnLots;
        }

        public bool AlreadExistType(string name, int stationId) => 
            _context.Processes.Any(x => x.ProcessName.Replace(" ", String.Empty).ToUpper() == name.Replace(" ", String.Empty).ToUpper() && x.Active && x.Station.Id == stationId);
    }
}
