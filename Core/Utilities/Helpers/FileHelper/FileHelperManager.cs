using Core.Helpers.FileHelper;
using Core.Helpers.GuidHelper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Core.Utilities.Helpers
{
    public class FileHelperManager : IFileHelper
    {
        public void Delete(string filePath) // Dosyamızın tam adı 
        { 
            if (File.Exists(filePath)) //Belirtilen klasör yolu var mı yok mu kontrol et
            {
                File.Delete(filePath); // Var ise belirtilen dosyayı sil 
            }
        }

        public string Update(IFormFile file,string filePath, string root)  //
        {
            if (File.Exists(filePath)) // Belirtilen dosya adı var mı kontrol et 
            {
                File.Delete(filePath); // Var ise onu sil
            }
            return Upload(file,root); //  Upload metodu ile yeni bir dosya oluşturmak için file ile dosya adı ve uzantısını oluşturmak için kullanıyoruz
                                     // Root ise uzantısı yani yolu için
        }
        public string Upload(IFormFile file, string root) //file dosyayla ilgili bilgileri edinmemizi sağlar root yol oluyor
        {
            if(file.Length > 0) // Dosya uzunluğunu kontrol ediyor dosya gönderilmiş mi gönderilmemiş diye 
            {
                if (!Directory.Exists(root)) // Belirtilen klasör yolunun varolup olmadığıyla ilgili boolean değer döndürür örneğin 
                                             //   C:\Users gönderdiğimizi düşünelim böyle bir klasör yolu var ise 
                                             // True değer gönderir  
                {
                    Directory.CreateDirectory(root);  // Belirtilen dizin yolunda verilen ada göre klasör oluşturur	
                                                      //Directory.CreateDirectory("C:\newfolder"); Örneğin bu kodda C: Dizini altında newfolder
                                                      // Adında yeni bir klasör oluşturmasını söylüyoruz 

                }
                string extension = Path.GetExtension(file.Name); // Dosya uzantısını elde ediyoruz .jpg mi .txt mi .png mi 
                string guid = GuidHelper.CreateGuid(); // Guidhelper classında  CreateGuid metoduyla benzersiz bir ada sahip dosya adı oluşturuyoruz
                string filePath = guid + extension;  // Dosya adı ile uzantısını birleştirip filepath oluşturuyoruz böyle ortaya örneğin 
                                                    // Şöyle bir şey çıkıyor  kedifotografi.jpg  kedifotografi  guid yani benzeri olmayan ad  extension ise uzantısı

                using (FileStream fileStream = File.Create(root + filePath))  // Burada belirtilen adreste dosya oluşturuluyor ve fileStream atanıyor 
                {
                    file.CopyTo(fileStream); // Parametre olarak verdiğimiz file dosyasının   nereye kopyalanacağını söylüyoruz 
                                             // Yukarıda filePath de  dosyadı ile uzantısını birleştirmiştik bunu da fileStream atadık
                    fileStream.Flush();  // Ara bellekten siler tam olarak ne yaptığını anlamadım baktığım kaynaklarda 
                    return filePath; // Dosyamızın adını geri gönderiyoruz sql için 
                }
            }
            return null;
        }
    }
}
