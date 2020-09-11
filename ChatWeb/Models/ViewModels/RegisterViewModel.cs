using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ChatWeb.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name="Nombre")]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Ciudad")]
        public string City { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        [Compare("Password")]
        [Display(Name = "Confirmar Contraseña")]
        public string Password2 { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
    }
}