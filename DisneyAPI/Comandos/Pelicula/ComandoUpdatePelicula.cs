using System;
using System.ComponentModel.DataAnnotations;

namespace Comandos.Pelicula
{
    public class ComandoUpdatePelicula
    {
        //===================================

        [Required(ErrorMessage = "Ingrese la id de la pelicula.")]
        public int IdPelicula { get; set; }
        //===================================

        [Required(ErrorMessage = "Ingrese el titulo.")]
        [MinLength(4, ErrorMessage = "Escriba al menos 5 caracteres.")]
        [MaxLength(25, ErrorMessage = "Escriba un maximo de 25 caracteres.")]
        public string Titulo { get; set; }
        //===================================

        [Required(ErrorMessage = "Ingrese la fecha de creacion.")]
        [DataType(DataType.Date)]
        public DateTime fechaCreacion { get; set; }
        //===================================

        [Required(ErrorMessage="Ingrese una calificacion")]
        [Range(1, 5, ErrorMessage = "Ingrese un numero del 1 al 5")]

         public int Calificacion {get; set;}
        //===================================

        [Required(ErrorMessage = "Ingrese la id del genero.")]
        [Range(0, 3, ErrorMessage = "Ingrese un id entre 1 y 2")]

        public int IdGenero { get; set; }
        //===================================



    }
}