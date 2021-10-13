using System.ComponentModel.DataAnnotations;

namespace Comandos.Usuario
{
    public class ComandoUsuarioLogin
    {
        //===================================

        [Required(ErrorMessage = "Ingrese su email.")]
        [EmailAddress(ErrorMessage ="Escriba un correo valido")]
        public string Email {get; set;}
        //===================================

        [Required(ErrorMessage = "Ingrese un password.")]
        [DataType(DataType.Password)]
        public string Password {get; set;}
        //===================================

    }
}