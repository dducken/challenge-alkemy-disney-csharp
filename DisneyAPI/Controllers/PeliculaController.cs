
using System;
using System.Linq;
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
    public class PeliculaController : ControllerBase
    {
        private readonly Context db = new Context();
        private readonly ILogger<PeliculaController> _logger;

        public PeliculaController(ILogger<PeliculaController> logger)
        {
            _logger = logger;

        }

     // ========================================================= //
     // ===========================GET PELICULAS============================== //

        [HttpGet]
        [Route("/movies")]
        public ActionResult<ResultadoAPI> Get()
        {
                var resultado = new ResultadoAPI();
           try
           {
     
                resultado.Ok = true;
                resultado.Return = db.Peliculas.ToList();
                return resultado;
           }
           catch (System.Exception ex)
           {
               
               resultado.Ok = false;
                resultado.CodigoError = 1;
                resultado.Error = "no hay peliculas - " + ex.Message;

                return resultado;
           }
        }
      // ========================================================= //
     // ===========================GET PELICULAS NOMBRE============================== //
        [HttpGet]
        [Route("/movies/name")]
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
                resultado.Error = "Pelicula no encontrada - " + ex.Message;

                return resultado;
            }
        }
         // ========================================================= //
     // ===========================GET PELICULAS GENERO============================== //
        [HttpGet]
        [Route("/movies/genre")]
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
                resultado.Error = "Pelicula no encontrada - " + ex.Message;

                return resultado;
            }
        }
     // ========================================================= //
     // ===========================POST MOVIES============================== //
        [HttpPost]
        [Route("movies/create")]
        public ActionResult<ResultadoAPI> postPelicula([FromBody]ComandoCrearPelicula comando)
        {
            var resultado = new ResultadoAPI();

            if(comando.Titulo.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese titulo";
                return resultado;
            }

            if(comando.IdGenero.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese id de pelicula";
                return resultado;
            }

            var pel = new Pelicula();  
            pel.Titulo = comando.Titulo;
            pel.fechaCreacion = comando.fechaCreacion;
            pel.IdGenero = comando.IdGenero;
            


         
            db.Peliculas.Add(pel);
            db.SaveChanges();
           
            resultado.Ok = true;
            resultado.Return = db.Peliculas.ToList();
            db.Entry(pel).Reference(x => x.genero).Load(); 


            return resultado;
        }
        
// ========================================================= //
     // ===========================UPDATE MOVIES============================== //
        [HttpPut]
        [Route("/movies/update")]
        public ActionResult<ResultadoAPI> UpdatePelicula([FromBody]ComandoUpdatePelicula comando)
        {
            var resultado = new ResultadoAPI();

           
            if(comando.Titulo.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese titulo";
                return resultado;
            }

            if(comando.IdPelicula.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese id de pelicula";
                return resultado;
            }

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
// ========================================================= //
     // ===========================DELETE MOVIES============================== //
        [HttpDelete]
        [Route("/movies/delete")]
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
