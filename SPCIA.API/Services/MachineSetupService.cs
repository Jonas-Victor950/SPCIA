using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.Helpers;
using Microsoft.EntityFrameworkCore;

namespace SPCIA.API.Services
{
    public interface IMachineSetupService
    {
        public void Delete(int id);
        MachineSetup? GetById(int id);
        List<MachineSetup> GetAll();
        MachineSetup Save(MachineSetup model);
        List<MachineSetup> GetByMachineId(int machineId);
    }
    public class MachineSetupService : IMachineSetupService
    {
        private readonly DataContext _context;

        public MachineSetupService(DataContext dbContext)
        {
            this._context = dbContext;
        }

        public MachineSetup Save(MachineSetup model)        {
            if(model.DateTime == null) { 
                model.DateTime = DateTime.UtcNow; 
            }
            if (model.Id == null) _context.AddRange(model);
            else
            {
                var machineSetup = _context.MachineSetups
                                        .Include(x => x.MachineSetupValues)
                                        .SingleOrDefault(x => x.Id == model.Id);

                if (machineSetup == null) throw new NotFoundException($"MachineSetup with id {model.Id} not found!");

                machineSetup.MachineSetupValues.Clear();
                _context.UpdateRange(machineSetup);
                _context.SaveChanges();
                _context.ChangeTracker.Clear();

                _context.UpdateRange(model);
            }
            _context.SaveChanges();

            return model;
        }

        public void Delete(int id)
        {
            var model = _context.MachineSetups.Find(id);

            if (model == null) throw new NotFoundException($"MachineSetups with id {id} not found!");

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

        public MachineSetup? GetById(int id) =>         
            _context.MachineSetups
                .Include(x => x.MachineSetupValues).ThenInclude(y => y.MachineProfileParameter)
                .Include(x => x.Machine).Where(x => x.Machine.Id == x.MachineId)
                .SingleOrDefault(x => x.Id == id);
         

        public List<MachineSetup> GetAll()
        {
            return _context.MachineSetups
                .Include(x => x.MachineSetupValues).ThenInclude(y => y.MachineProfileParameter)
                .Include(x => x.Machine).ThenInclude(y => y.Stations)
                .Where(x => x.Active)
                .ToList();
        }

        public List<MachineSetup> GetByMachineId(int machineId)
        {
            return _context.MachineSetups
                .Include(x => x.MachineSetupValues).ThenInclude(y => y.MachineProfileParameter)
                .Include(x => x.Machine).ThenInclude(y => y.Stations)
                .Where(x => x.MachineId == machineId && x.Active).ToList();
        }
    }
}
