using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.SPCIA.Entities;
using BRCSystem.ClassLibrary.Helpers;

namespace SPCIA.API.Services
{
    public interface IUnitMeasurementService
    {
        public List<UnitMeasurement> GetAll();
        public UnitMeasurement? GetById(int id);
        public UnitMeasurement Save(UnitMeasurement model);
        public void Delete(int id);
    }

    public class UnitMeasurementService : IUnitMeasurementService
    {
        private readonly DataContext _context;

        public UnitMeasurementService(DataContext context) => _context = context;


        public void Delete(int id)
        {
            var model = _context.UnitMeasurements.Find(id);

            if (model == null) throw new NotFoundException($"UnitMeasurements with id {id} not found!");

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

        public List<UnitMeasurement> GetAll() => _context.UnitMeasurements.Where(x => x.Active).ToList();

        public UnitMeasurement? GetById(int id) => _context.UnitMeasurements.Find(id);

        public UnitMeasurement Save(UnitMeasurement model)
        {
            if (model.Id == null) _context.AddRange(model);
            else _context.UpdateRange(model);
            _context.SaveChanges();
            
            return model;
        }
    }
}
