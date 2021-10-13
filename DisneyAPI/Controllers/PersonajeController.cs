using System.Net;
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
using Microsoft.AspNetCore.Authorization;
using Comandos.Personaje;

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
     // ===========================READ CHARACTER============================== //

        [HttpGet]
        [Route("/characters")]
        [AllowAnonymous]
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
              resultado.Error = "Error se requiere autorizacion " + ex.Message;

              return resultado;
           }
           
        }
     
     // ===========================CREATE CHARACTER============================== //
        [HttpPost]
        [Route("/characters/create")]
        [Authorize]
        public ActionResult<ResultadoAPI> postCharacter([FromBody]ComandoCrearPersonaje comando)
        {
            var resultado = new ResultadoAPI();

            var per = new Personaje();  
            per.Nombre = comando.Nombre;
            per.Imagen = comando.Imagen;
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
     // ===========================UPDATE CHARACTER============================== //

        [HttpPut]
        [Route("/characters/update")]
        [Authorize]

        public ActionResult<ResultadoAPI> UpdateCharacter([FromBody]ComandoUpdatePersonaje comando)
        {
            var resultado = new ResultadoAPI();

            try
            {

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
     // ===========================DELETE CHARACTERS============================== //
        [HttpDelete]
        [Route("/characters/delete")]
        [Authorize]
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
     // ===========================GET CHARACTERS DETAILS============================== //
        [HttpGet]
        [Route("/characters/details")]
        [AllowAnonymous]
        public ActionResult<ResultadoAPI> getCharactersInfo()
        {
            var resultado = new ResultadoAPI();
            try
            {
                foreach (Personaje p in db.Personajes.ToList())
                {
                   var per = db.Personajes.FirstOrDefault(x => x.IdPersonaje.Equals(p.IdPersonaje));
                   db.Entry(per).Reference(x => x.pelicula).Load();
                }

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
     // ===========================GET CHARACTER BY NAME============================== //
        [HttpGet]
        [Route("/characters/Name")]
        [AllowAnonymous]
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
                resultado.Error = "Personaje no encontrado! " + ex.Message;

                return resultado;
            }
        }

     // ===========================GET CHARACTER BY AGE============================== //
        [HttpGet]
        [Route("/characters/Age")]
        [AllowAnonymous]
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
                resultado.CodigoError = 404;
                resultado.Error = "Personaje no encontrado! " + ex.Message;

                return resultado;
            }
        }
     // ===========================GET CHARACTER BY MOVIE============================== //
        [HttpGet]
        [Route("/characters/Movie")]
        [AllowAnonymous]
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
                resultado.CodigoError = 404;
                resultado.Error = "Personaje no encontrado! " + ex.Message;

                return resultado;
            }
        }

    }
}