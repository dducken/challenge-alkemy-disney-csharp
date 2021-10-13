using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Usuario
    {
       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdUsuario {get; set;}
       
        public string Email {get; set;}
       
        public string Password {get; set;}
      
        public int idRol{get;set;}   
        [ForeignKey("idRol")]      

        public Rol rol {get; set;} 
      

        public Usuario()
        {}

        
    }
}