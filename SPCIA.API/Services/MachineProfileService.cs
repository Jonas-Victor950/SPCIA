using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.Helpers;
using Microsoft.EntityFrameworkCore;

namespace SPCIA.API.Services
{

    public interface IMachineProfileService
    {
        public void Delete(int id);
        MachineProfile? GetById(int id);
        List<MachineProfile> GetAll();
        MachineProfile Save(MachineProfile model);
        List<MachineProfile> GetByMachineId(int machineId);
    }

    public class MachineProfileService : IMachineProfileService
    {
        private readonly DataContext _context;

        public MachineProfileService(DataContext dbContext)
        {
            this._context = dbContext;
        }

        public MachineProfile Save(MachineProfile model)
        {
            _context.Machines.AttachRange(model.Machines);

            if (model.Id == null) _context.AddRange(model);
            else
            {
                var machineProfile = _context.MachineProfiles
                                        .Include(x => x.MachineProfileParameters)
                                        .Include(x => x.Machines)
                                        .SingleOrDefault(x => x.Id == model.Id);

                if (machineProfile == null) throw new NotFoundException($"MachineProfile with id {model.Id} not found!");

                machineProfile.Machines.Clear();
                machineProfile.MachineProfileParameters.Clear();
                _context.UpdateRange(machineProfile);
                _context.SaveChanges();
                _context.ChangeTracker.Clear();

                _context.UpdateRange(model);
            }
            _context.SaveChanges();

            return model;
        }

        public void Delete(int id)
        {
            var model = _context.MachineProfiles.Find(id);

            if (model == null) throw new NotFoundException($"MachineProfiles with id {id} not found!");

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

        public MachineProfile? GetById(int id)
        {
            var returnedValue = _context.MachineProfiles
                .Include(x => x.MachineProfileParameters).ThenInclude(y => y.UnitMeasurement)
                .Include(x => x.Machines).ThenInclude(y => y.Stations)
                .SingleOrDefault(x => x.Id == id);
            return returnedValue;
        }

        public List<MachineProfile> GetAll()
        {
            return _context.MachineProfiles
                .Include(x => x.MachineProfileParameters).ThenInclude(y => y.UnitMeasurement)
                .Include(x => x.Machines).ThenInclude(y => y.Stations)
                .Where(x => x.Active)
                .ToList();
        }

        public List<MachineProfile> GetByMachineId(int machineId)
        {
            return _context.MachineProfiles
                .Include(x => x.MachineProfileParameters).ThenInclude(y => y.UnitMeasurement)
                .Include(x => x.Machines).ThenInclude(y => y.Stations)
                .Where(x => x.Machines.Any(machine => machine.Id == machineId) && x.Active)
                .ToList();
        }        
    }
}
