using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SAMS.WebUI.Models.Forms
{
    public class LoginForm
    {
        [Required(ErrorMessage = "Zəhmət olmasa, istifadəçi adı daxil edin.")]
        public string txtUserLogin { get; set; }
        [Required(ErrorMessage = "Zəhmət olmasa, şifrəni daxil edin.")]
        [DataType(DataType.Password, ErrorMessage = "Zəhmət olmasa şifrəni düzgün formatda daxil edin.")]
        public string txtPassword { get; set; }


    }
}