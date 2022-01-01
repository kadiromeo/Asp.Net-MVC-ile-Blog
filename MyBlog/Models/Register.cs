using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Register
    {
        [Required]
        [DisplayName("Adınız...")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Soyadınız...")]
        public string Surname { get; set; }
        [Required]
        [DisplayName("Kullanıcı adınız...")]
        public string userName { get; set; }
        [Required]
        [DisplayName("Email adresiniz...")]
        [EmailAddress(ErrorMessage = "uyumsuz email")]
        public string Email { get; set; }
        [Required]
        [DisplayName("Şifreniz...")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Tekrar Şifreniz...")]
        [Compare("Password",ErrorMessage = "Şifreleriniz eşleşmemektedir...!")]
        public string Repassword { get; set; }
    }
}