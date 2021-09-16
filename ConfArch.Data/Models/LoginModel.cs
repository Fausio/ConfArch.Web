using System;
using System.Collections.Generic;
using System.Text;

namespace ConfArch.Data.Models
{
   public class LoginModel
    {
        public string UserName   { get; set; }
        public string PassWord { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}
