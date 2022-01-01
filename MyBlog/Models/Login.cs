using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Login
    {
        [Required]
        [DisplayName("Kullanıcı adınız...")]
        public string userName { get; set; }
        [Required]
        [DisplayName("Şifreniz...")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}