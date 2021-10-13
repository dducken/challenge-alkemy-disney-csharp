using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Comandos.Personaje{
    
    public class ComandoCrearPersonaje{

         
        //===================================
        [Required(ErrorMessage = "Ingrese el nombre.")]
        [MinLength(4, ErrorMessage = "Escriba al menos 5 caracteres.")]
        [MaxLength(25, ErrorMessage = "Escriba un maximo de 25 caracteres.")]
        public string Nombre { get; set; }

        //===================================
        [Required(ErrorMessage = "Ingrese una imagen.")]
        public string Imagen { get; set; }
        //===================================

        [Required(ErrorMessage = "Ingrese la edad.")]
        [Range(10, 90, ErrorMessage = "Ingrese una edad valida")]
        public int Edad { get; set; }
        //===================================

        [Required(ErrorMessage = "Ingrese el peso.")]
        public float Peso { get; set; }
        //===================================

        [Required(ErrorMessage = "Ingrese la historia.")]
        [MinLength(10, ErrorMessage = "Escriba al menos 11 caracteres.")]
        [MaxLength(200, ErrorMessage = "Escriba un maximo de 200 caracteres.")]
        public string Historia { get; set; }
        //===================================
 
        [Required(ErrorMessage = "Ingrese la id de la pelicula.")]
        public int idPelicula {get; set;}
        

    }
}