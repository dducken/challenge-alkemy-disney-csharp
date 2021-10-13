using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Models{
    public class Personaje{

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        [Key]
        public int IdPersonaje {get; set;}

        public string Imagen { get; set; }
    
        public string Nombre { get; set; }
         
        public int Edad { get; set; }
       
        public float Peso { get; set; }
    
        public string Historia { get; set; }
        //===================================
 
        public int idPelicula {get; set;}
        [ForeignKey("idPelicula")]
        
        public Pelicula pelicula {get; set;}


        public Personaje(){

        }

    }
}