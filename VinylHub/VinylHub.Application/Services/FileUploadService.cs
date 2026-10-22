using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VinylHub.Application.Services
{
    public class FileUploadService
    {
        public void UploadFile(Stream input, string filePath)
        {
            using var stream = new FileStream(filePath, FileMode.Create);
            input.CopyTo(stream);
        }
    }
}
