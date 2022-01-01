using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class ProfileChange
    {
        public string Id { get; set; }
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
        [DisplayName("Adınız...")]
        [EmailAddress(ErrorMessage ="Geçersiz Email adresi...!")]
        public string Email { get; set; }

    }
}