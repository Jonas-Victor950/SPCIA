using BRCSystem.ClassLibrary.Data;
using BRCSystem.ClassLibrary.EnrollmentSystem.Entities;

namespace BRCSystem.ClassLibrary.Helpers
{
    public interface IFileModelService
    {
        FileModel? GetById(int id);
        FileModel Save(FileModel model);
        void RemoveFiles(List<int> fileIds);
        string GetFileNameById(int id);
    }
    public class FileModelService : IFileModelService
    {
        private readonly DataContext _context;

        public FileModelService(DataContext context)
        {
            _context = context;
        }

        public FileModel? GetById(int id) => _context.FileModels.SingleOrDefault(s => s.Id == id);

        public void RemoveFiles(List<int> fileIds)
        {
            foreach (var id in fileIds)
            {
                var model = _context.FileModels.Find(id);
                if (model != null) _context.RemoveRange(model);
            }
            _context.SaveChanges();
        }

        public FileModel Save(FileModel model)
        {
            if (model.Id == null) _context.FileModels.AddRange(model);
            else _context.FileModels.UpdateRange(model);
            _context.SaveChanges();

            return model;
        }

        public string GetFileNameById(int id)
        {
            var model = _context.FileModels.Find(id);

            return model.Name + model.Extension;
        }
    }
}