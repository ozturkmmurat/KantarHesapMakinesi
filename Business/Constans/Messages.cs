using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constans
{
    public static class Messages
    {
        public static string GetByAll = " Veriler başarıyla listelendi";
        public static string GetByIdMessage = "Veri başarıyla listelendi";
        public static string GetByAllDefault = " Veri bulunamadı";
        public static string DataAdded = " Veri başarıyla eklendi";
        public static string UnDataAdded = " Veri eklenemedi.";
        public static string DataDeleted = " Veri başarıyla silindi";
        public static string UnDataDeleted = " Veri silinemedi";
        public static string DataUpdate = " Veri başarıyla güncellendi";
        public static string UnDataUpdate = " Veri güncellenmedi";
        public static string DataRuleFail = " Veri belirtilen kurallara uymuyor";
        public static string GetByClaim = "Kullanıcının yetkileri listelendi";
        public static string AuthorizationDenied = "Yetkiniz yok.";
        public static string CurrentMail = "Bu mail adresine sahip bir kullanıcı mevcut.";
        public static string LoginCheck = "Email ve şifreyi kontrol ediniz";
        public static string UserCheck = "Böyle bir kullanıcı bulunamadı.";
        public static string PasswordResetCode = "Şifre sıfırlama kodu email adresinize gönderilmiştir";
        public static string FailedPasswordResetCode = "Şifre sıfırlama kodu yanlış.";
        public static string FailedEmailCheck = "Böyle bir mail adresi bulunmamaktadır.";
        public static string FailedCodeCheck = "Kod Hatalı";
        public static string CheckStatusError = "Çok fazla hatalı kod girişi yaptınız.";
        public static string ResetPasswordLink = "Şifre sıfırlama linki mail adresinize gönderilmiştir.";
        public static string FindFailedUser = "Kullanıcı bulunamadı";
        public static string SuccessUserPasswordReset = "Şifreniz başarıyla güncellendi";
        public static string UnSuccessUserPasswordReset = "Şifreniz güncellenemedi";
        public static string CodeHasExpired = "Kodun süresi dolmuş. Lütfen tekrardan işlemleri gerçekleştiriniz";
        public static string CodeCheck = "Bir hata oluştu lütfen kodu kontrol edin veya tekrar şifre isteğinde bulununuz";
        public static string CheckSlicer = "Bir slicer mevcut.";
        public static string CheckEmail = "Email adresi bulunamadı";
    }
}
