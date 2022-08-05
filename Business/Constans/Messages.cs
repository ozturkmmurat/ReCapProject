using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Business.Constans
{
    public static class Messages
    {
        public static string GetByAll = " Veriler başarıyla listelendi";       
        public static string GetByIdMessage = "Veri başarıyla listelendi";
        public static string GetByAllDefault = " Veri bulunamadı";
        public static string DataAdded = " Veri başarıyla eklendi";
        public static string DataDeleted = " Veri başarıyla silindi";
        public static string DataUpdate = " Veri başarıyla güncellendi";
        public static string DataRuleFail = " Veri belirtilen kurallara uymuyor";
        public static string CheckIfCarImageLimit = "Bir araç için 5'den fazla resim yükleyemezsiniz";
        public static string GetByClaim = "Kullanıcının yetkileri listelendi";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string GetRentalDetailsDto = "Kira detayları listelendi";
        public static string CurrentMail = "Bu mail adresine sahip bir kullanıcı mevcut.";
    }
}
