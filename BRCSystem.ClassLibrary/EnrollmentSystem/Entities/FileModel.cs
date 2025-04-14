using Microsoft.AspNetCore.Http;

namespace BRCSystem.ClassLibrary.EnrollmentSystem.Entities
{
    public class FileModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string UploadedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public byte[] Data { get; set; }

        public FileModel()
        {

        }

        public FileModel(IFormFile file, string Description, string UploadedBy = "")
        {
            var fileName = Path.GetFileNameWithoutExtension(file.FileName);
            var extension = Path.GetExtension(file.FileName);

            CreatedOn = DateTime.UtcNow;
            FileType = file.ContentType;
            Extension = extension;
            Name = fileName;
            this.Description = Description;
            this.UploadedBy = UploadedBy;

            using (var dataStream = new MemoryStream())
            {
                file.CopyTo(dataStream);
                Data = dataStream.ToArray();
            }
        }


    }
}