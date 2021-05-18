using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CursoMVC.Models.User
{
    public class RegisterViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El Nombre solo debe contener letras")]
        public string Nombre { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El Apellido solo debe contener letras")]
        public string Apellido { get; set; }

        [Required]
        [RegularExpression("^[0-9]*$", ErrorMessage = "La Cedula solo debe contener numeros")]
        public string Cedula { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Correo invalido")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "La Contraseña debe contener almenos 5 caracteres")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Confirmar contraseña")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Las Contraseñas no coinciden")]
        public string PasswordConfirm { get; set; }
    }
}