using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Resultados;
using Microsoft.AspNetCore.Cors;
using Data;
using Comandos.Pelicula;
using Microsoft.AspNetCore.Authorization;

namespace Controllers
{
    [ApiController]
    [EnableCors("DisneyAdmin")]
    public class PeliculaController : ControllerBase
    {
        private readonly Context db = new Context();
        private readonly ILogger<PeliculaController> _logger;
        public PeliculaController(ILogger<PeliculaController> logger)
        {
            _logger = logger;

        }

     // ===========================READ MOVIES============================== //

        [HttpGet]
        [Route("/movies")]
        [AllowAnonymous]
        public ActionResult<ResultadoAPI> Get()
        {
                var resultado = new ResultadoAPI();
           try
           {
               foreach (Pelicula p in db.Peliculas.ToList())
                {
                   var pel = db.Peliculas.FirstOrDefault(x => x.IdPelicula.Equals(p.IdPelicula));
                   db.Entry(pel).Reference(x => x.genero).Load();
                }

                resultado.Ok = true;
                resultado.Return = db.Peliculas.ToList();
                return resultado;
           }
           catch (System.Exception ex)
           {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "no se encontraron peliculas " + ex.Message;

                return resultado;
           }
        }
     // ===========================GET PELICULAS BY TITULO============================== //
        [HttpGet]
        [Route("/movies/name")]
        [AllowAnonymous]
        public ActionResult<ResultadoAPI> GetByTitulo([FromQuery]string Titulo)
        {
            var resultado = new ResultadoAPI();
            try
            {     
              var pel =  db.Peliculas.Where(c => c.Titulo == Titulo).FirstOrDefault();

              resultado.Ok = true;
              resultado.Return = pel;
              
              db.Entry(pel).Reference(x => x.genero).Load(); 
              return resultado;

              
            }
            catch(Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Pelicula no encontrada! " + ex.Message;

                return resultado;
            }
        }
     // ===========================GET PELICULAS BY GENERO============================== //
        [HttpGet]
        [Route("/movies/genre")]
        [AllowAnonymous]
        public ActionResult<ResultadoAPI> GetByGenero([FromQuery]int genero)
        {
            var resultado = new ResultadoAPI();
            try
            {     
              var pel =  db.Peliculas.Where(c => c.IdGenero == genero).FirstOrDefault();

              resultado.Ok = true;
              resultado.Return = pel;
              db.Entry(pel).Reference(x => x.genero).Load(); 
              
              return resultado;
              
            }
            catch(Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "Pelicula no encontrada! " + ex.Message;

                return resultado;
            }
        }
     // ===========================CREATE MOVIE============================== //
        [HttpPost]
        [Route("movies/create")]
        [Authorize]
        public ActionResult<ResultadoAPI> postPelicula([FromBody]ComandoCrearPelicula comando)
        {
            var resultado = new ResultadoAPI();

            var pel = new Pelicula();  
            pel.Titulo = comando.Titulo;
            pel.fechaCreacion = comando.fechaCreacion;
            pel.Calificacion = comando.Calificacion;
            pel.IdGenero = comando.IdGenero;
            
            db.Peliculas.Add(pel);
            db.SaveChanges();
           
            resultado.Ok = true;
            resultado.Return = db.Peliculas.ToList();
            db.Entry(pel).Reference(x => x.genero).Load(); 

            return resultado;
        }
        
     // ===========================UPDATE MOVIE============================== //
        [HttpPut]
        [Route("/movies/update")]
        [Authorize]
        public ActionResult<ResultadoAPI> UpdatePelicula([FromBody]ComandoUpdatePelicula comando)
        {
            var resultado = new ResultadoAPI();

            var pel = db.Peliculas.Where(c =>c.IdPelicula == comando.IdPelicula).FirstOrDefault();
            if(pel != null)
            {
                pel.Titulo = comando.Titulo;
                pel.fechaCreacion = comando.fechaCreacion;
                pel.IdGenero = comando.IdGenero;

                db.Peliculas.Update(pel);
                db.SaveChanges();
            }

            resultado.Ok = true;
            resultado.Return = db.Peliculas.ToList();
            db.Entry(pel).Reference(x => x.genero).Load(); 

            return resultado;
        }
     // ===========================DELETE MOVIES============================== //
        [HttpDelete]
        [Route("/movies/delete")]
        [Authorize]
        public ActionResult<ResultadoAPI> DeleteMovie(int id)
        {
            var resultado = new ResultadoAPI();
            try
            {  
                var pel = db.Peliculas.Where(c => c.IdPelicula == id).FirstOrDefault();
                db.Peliculas.Remove(pel);
                db.SaveChanges();

                resultado.Ok = true;
                resultado.Return = db.Peliculas.ToList();
                return resultado;
            }
            catch (System.Exception ex)
            {  
                resultado.Ok = false;
                resultado.Error = "Pelicula no encontrada" + ex.Message;

                return resultado;
            }
        }


    }
}
