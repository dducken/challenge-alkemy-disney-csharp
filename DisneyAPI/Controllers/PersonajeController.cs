using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Resultados;
using Microsoft.AspNetCore.Cors;
using Data;
using Comandos;


namespace Controllers
{
    [ApiController]
    [EnableCors("DisneyAdmin")]
    public class PersonajeController : ControllerBase
    {
        private readonly Context db = new Context();
        private readonly ILogger<PersonajeController> _logger;

        public PersonajeController(ILogger<PersonajeController> logger)
        {
            _logger = logger;

        }

     // ========================================================= //
     // ===========================LISTADO DE PERSONAJES============================== //

        [HttpGet]
        [Route("/characters")]
        public ActionResult<ResultadoAPI> Get()
        {
           var resultado = new ResultadoAPI();
            var lista = db.Personajes.ToList();
           
           try
           {
               foreach(var per in db.Personajes.ToList()){
                resultado.Return += "Nombre: " + per.Nombre + " Edad: " + per.Edad + "// ";
            }

           resultado.Ok = true;
           return resultado;
           }
           catch (Exception ex)
           {
               
              resultado.Ok = false;
              resultado.Error = "Error " + ex.Message;

              return resultado;
           }
           
        }
     
     // ========================================================= //
     // ===========================POST CHARACTERS============================== //
        [HttpPost]
        [Route("/characters/create")]
        public ActionResult<ResultadoAPI> postCharacter([FromBody]ComandoCrearPersonaje comando)
        {
            var resultado = new ResultadoAPI();

            if(comando.Nombre.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese el nombre";
                return resultado;
            }

            if(comando.Edad.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese la edad";
                return resultado;
            }

            if(comando.Peso.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese el peso del personaje";
                return resultado;
            }

            if(comando.Historia.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese una historia";
                return resultado;
            }

            if(comando.idPelicula.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese pelicula/serie";
                return resultado;
            }

            var per = new Personaje();  
            per.Nombre = comando.Nombre;
            per.Edad = comando.Edad;
            per.Peso = comando.Peso;
            per.Historia = comando.Historia;
            per.idPelicula = comando.idPelicula;

             

         
            db.Personajes.Add(per);
            db.SaveChanges();
           
            resultado.Ok = true;
            resultado.Return = db.Personajes.ToList();

            db.Entry(per).Reference(x => x.pelicula).Load(); 
            return resultado;
        }
        // ========================================================= //
     // ===========================UPDATE CHARACTERS============================== //

        [HttpPut]
        [Route("/characters/update")]
        public ActionResult<ResultadoAPI> UpdateCharacter([FromBody]ComandoUpdatePersonaje comando)
        {
            var resultado = new ResultadoAPI();

           
            try
            {
                 if(comando.Nombre.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese nombre";
                return resultado;
            }

            if(comando.IdPelicula.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese ID de pelicula";
                return resultado;
            }

            var per = db.Personajes.Where(c =>c.IdPersonaje == comando.IdPersonaje).FirstOrDefault();
            if(per != null)
            {
                
                per.Nombre = comando.Nombre;
                per.Historia = comando.Historia;
                per.idPelicula = comando.IdPelicula;


                db.Personajes.Update(per);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Personajes.ToList();;

            db.Entry(per).Reference(x => x.pelicula).Load(); 
            return resultado;
            }
            catch (System.Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Personaje no encontrado" + ex.Message;

                return resultado;
                
            }
        }
// ========================================================= //
     // ===========================DELETE CHARACTERS============================== //
        [HttpDelete]
        [Route("/characters/delete")]
        public ActionResult<ResultadoAPI> DeleteCharacter(int id)
        {
            var resultado = new ResultadoAPI();
            try
            {
                   
                var per = db.Personajes.Where(c => c.IdPersonaje == id).FirstOrDefault();
                db.Personajes.Remove(per);
                db.SaveChanges();

                resultado.Ok = true;
                resultado.Return = db.Personajes.ToList();

                db.Entry(per).Reference(x => x.pelicula).Load(); 
                return resultado;
            }
            catch (System.Exception ex)
            {  
                resultado.Ok = false;
                resultado.Error = "Personaje no encontrado" + ex.Message;

                return resultado;
            }
        }
      // ========================================================= //
     // ===========================GET CHARACTERS DETAILS============================== //
        [HttpGet]
        [Route("/characters/details")]
        public ActionResult<ResultadoAPI> getCharactersInfo()
        {
            var resultado = new ResultadoAPI();
            try
            {
                resultado.Return = db.Personajes.ToList();
                resultado.Ok = true;

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.Error = "Error" + ex.Message;

                return resultado;
            }

        }
       // ========================================================= //
     // ===========================GET CHARACTERS NAME============================== //
        [HttpGet]
        [Route("/characters/Name")]
        public ActionResult<ResultadoAPI> GetByName([FromQuery] string Nombre)
        {
            var resultado = new ResultadoAPI();
            try
            {     
              var per =  db.Personajes.Where(c => c.Nombre == Nombre).FirstOrDefault();

              resultado.Ok = true;
              resultado.Return = per;
              
              db.Entry(per).Reference(x => x.pelicula).Load(); 
              return resultado;
              
            }
            catch(Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Personaje no encontrado - " + ex.Message;

                return resultado;
            }
        }

         // ========================================================= //
     // ===========================GET CHARACTERS AGE============================== //
        [HttpGet]
        [Route("/characters/Age")]
        public ActionResult<ResultadoAPI> GetByAge([FromQuery] int Edad)
        {
            var resultado = new ResultadoAPI();
            
            try
            {     
              var per =  db.Personajes.Where(c => c.Edad == Edad).FirstOrDefault();

              resultado.Ok = true;
              resultado.Return = per;
              
              db.Entry(per).Reference(x => x.pelicula).Load(); 
              return resultado;
              
            }
            catch(Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Personaje no encontrado - " + ex.Message;

                return resultado;
            }
        }
              // ========================================================= //
     // ===========================GET CHARACTERS MOVIE============================== //
        [HttpGet]
        [Route("/characters/Movie")]
        public ActionResult<ResultadoAPI> GetByPelicula([FromQuery] int idPelicula)
        {
            var resultado = new ResultadoAPI();
            
            try
            {     
              var per =  db.Personajes.Where(c => c.idPelicula == idPelicula).FirstOrDefault();

              resultado.Ok = true;
              resultado.Return = per;
              
              db.Entry(per).Reference(x => x.pelicula).Load(); 
              return resultado;
              
            }
            catch(Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Personaje no encontrado - " + ex.Message;

                return resultado;
            }
        }

    }
}
