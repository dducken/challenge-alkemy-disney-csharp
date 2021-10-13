using System;
using System.ComponentModel.DataAnnotations;

namespace Comandos.Personaje
{
    public class ComandoUpdatePersonaje
    {
        //===================================
       [Required(ErrorMessage="Ingrese el id del personaje.")]
        public int IdPersonaje {get; set;}
        //===================================
        
       [Required(ErrorMessage="Ingrese el nombre.")]
       [MinLength(4, ErrorMessage ="Escriba al menos 5 caracteres.")]
       [MaxLength(25, ErrorMessage ="Escriba un maximo de 25 caracteres.")]
        public string Nombre {get; set;}
        //===================================

       [Required(ErrorMessage="Ingrese la historia.")]
       [MinLength(10, ErrorMessage ="Escriba al menos 11 caracteres.")]
       [MaxLength(200, ErrorMessage ="Escriba un maximo de 200 caracteres.")]
        public string Historia {get; set;}
        //===================================
       [Required(ErrorMessage="Ingrese el id de la pelicula.")]
        public int IdPelicula {get; set;}
    
    }
}