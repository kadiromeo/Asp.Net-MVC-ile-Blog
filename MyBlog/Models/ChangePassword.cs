using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class ChangePassword
    {
        [Required]
        [DisplayName("Eski Şifrenizi Giriniz...")]
        public string OldPassword { get; set; }
        [Required]
        [StringLength(50,MinimumLength =6,ErrorMessage ="Şifreniz en azn 6 karakter olmalı...!")]
        [DisplayName("Yeni Şifrenizi Giriniz...")]

        public string NewPassword { get; set; }
        [Required]
        [Compare("NewPassword",ErrorMessage ="Şifreler Uyuşmamaktadır...!")]
        [DisplayName("Tekrar Yeni Şifrenizi Giriniz...")]

        public string ConNewPassword { get; set; }
    }
}