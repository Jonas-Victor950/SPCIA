using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.Helpers;
using Microsoft.EntityFrameworkCore;
using SPCIA.API.Models;
using Microsoft.IdentityModel.Tokens;

namespace SPCIA.API.Services
{
    public interface IProcessSamplingService
    {
        public void Delete(int id);
        ProcessSampling? GetById(int id);
        List<ProcessSampling>? GetByFilter(ProcessSamplingFilterRequest filter);
        ProcessSampling Save(ProcessSampling model);
    }
    public class ProcessSamplingService(DataContext context) : IProcessSamplingService
    {
        private readonly DataContext _context = context;

        public void Delete(int id)
        {
            var model = _context.ProcessSamplings.Find(id);

            if (model == null) throw new NotFoundException($"ProcessSamplings with id {id} not found!");

            _context.RemoveRange(model!);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception)
            {

                model.Active = false;
                _context.UpdateRange(model);
                _context.SaveChanges();
            }
        }

        public List<ProcessSampling>? GetByFilter(ProcessSamplingFilterRequest filter)
        {
            if (filter == null)
            {
                var resultAll = _context.ProcessSamplings
                                        .Include(x => x.Machine)
                                        .Include(x => x.Operator)
                                        .Where(x => x.Active)
                                        .ToList();

                return resultAll?.Select(x => { x.Operator.PasswordHash = null; x.Operator.MFACode = null; return x; }).ToList();
            }

            var query = _context.ProcessSamplings.AsQueryable();

            if (filter.MachineId is not null)
            {
                query = query.Where(x => x.MachineId == filter.MachineId);
            }
            if (filter.ProcessId is not null)
            {              
                query = query.Where(x => x.ProcessId == filter.ProcessId);             
            }
            if (filter.Classifier is not null)
            {
                query = query.Where(x => x.Classifier == filter.Classifier);             
            }
            if (filter.ProductLotId is not null)
            {
                query = query.Where(x => x.ProductLotId == filter.ProductLotId);
            }
            if (filter.StationId is not null)
            {
                query = query.Where(x => x.Process.StationId == filter.StationId);                
            }

            var result = query
                .Include(x => x.Machine)
                .Include(x => x.Process).ThenInclude(p => p.Characteristics)
                .Include(x => x.Process).ThenInclude(p => p.Station)                
                .Include(x => x.ProductLot)
                .Include(x => x.Samples).ThenInclude(s => s.Inputs)
                .Include(x => x.Operator)
                .Where(x => x.Active)
                .ToList();
            return result?.Select(x => { x.Operator.PasswordHash = null; x.Operator.MFACode = null; return x; }).ToList();
        }

        public ProcessSampling? GetById(int id) => _context.ProcessSamplings
            .Include(x => x.Machine)
            .Include(x => x.Process).ThenInclude(p => p.Station)
            .Include(x => x.Process).ThenInclude(p => p.Characteristics)
            .Include(x => x.ProductLot)
            .Include(x => x.Samples).ThenInclude(s => s.Inputs)
            .SingleOrDefault(x => x.Id == id); 

        public ProcessSampling Save(ProcessSampling model)
        {
            if (model.SampleDate == null)
            {
                model.SampleDate = DateTime.UtcNow;
            }

            if (model.Classifier.Equals(Classifier.Requal))
            {
                if (model.RootCause.IsNullOrEmpty() || model.Who.IsNullOrEmpty() 
                    || model.CorrectiveAction.IsNullOrEmpty())
                {
                    throw new AppException("Requal fields must not be null!");
                }
            }
            if (model.Id is null) _context.AddRange(model);
            else _context.UpdateRange(model);

            _context.SaveChanges();

            return model;
        }
    }
}
