using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Helpers.FileHelper
{
    public interface IFileHelper
    {
        string Upload(IFormFile formFile,string root);
        string Update(IFormFile formFile,string filePath, string root);
        void Delete(string filePath);
    }
}
