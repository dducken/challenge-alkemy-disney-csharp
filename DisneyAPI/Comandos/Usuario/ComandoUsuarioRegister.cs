using System;
using System.ComponentModel.DataAnnotations;

namespace Comandos.Usuario
{
    public class ComandoUsuarioRegister
    {
        [Required(ErrorMessage = "Ingrese la fecha de creacion.")]
        [EmailAddress(ErrorMessage ="Escriba un correo valido")]
        public string Email {get; set;}

        [Required(ErrorMessage = "Ingrese un password.")]
        [DataType(DataType.Password)]
        public string Password {get; set;}

        [Required(ErrorMessage = "Ingrese la id del rol.")]
        [Range(1, 2, ErrorMessage ="su id debe ser 1 o 2")]
        public int idRol{get;set;}   
    }
}