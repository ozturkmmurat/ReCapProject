using System;
using System.Collections.Generic;
using System.Text;

namespace Core.CrossCuttingConcers.Caching
{
    public interface ICacheManager  // Bu interface diğer alternatif Caching yöntemlerinin İnterfaci olacak
    {
        // Buradaki Key value Cache çalışması Key value şeklinde olmasından 
        // Key Adres olarak düşünebiliriz Örneğin  Business.Concrete.GetBy(id) 
        //id burada value diğeride adresi keyi yani
        T Get<T>(string key); // Generic olma sebebi farklı çeşit Get Metodu olduğu için yani 
        // Liste şeklinde Get ve Tek bir veriyi getiren Get olduğu için 
        object Get(string key);  // Generic olmayan versiyonu get ile aynı farklı çeşit yazımı 
        void Add(string key, object value, int duration);  // Duration Cache'de ne kadar duracağını belirtiyor 
        bool IsAdd(string key); // Önbellek de böyle bir veri var mı yok ise Veritabanından al ve ekle 
        void Remove(string key); // Cache sil  Keyine göre 
        void RemoveByPattern(string pattern); // Cache'i Metod adına göre sil 
        //Adına göre silme sebebimiz Metodun birden fazla parametresi olabilir farklı çeşit 
    }
}
