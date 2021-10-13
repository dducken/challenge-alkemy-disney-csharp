using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    
    public class Rol
    {
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       [Key]
        public int IdRol {get; set;}

        public string Nombre {get; set;}
        
        public string Descripcion {get; set;}
    }
}