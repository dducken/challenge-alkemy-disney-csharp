using System.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Cors;
using Models;
using Resultados;
using Comandos;
using Data;

namespace Controllers
{
    [ApiController]
    [EnableCors("DisneyAdmin")]
    public class UsuarioController : ControllerBase
    {
       
         private readonly Context db = new Context();

        public UsuarioController()
        {
          
         
        }

       
        [HttpGet]
        [Route("/get/users")]
        public ActionResult<ResultadoAPI> Get()
        {
            var resultado = new ResultadoAPI();
            try
            {
               
              resultado.Ok = true;
              resultado.Return = db.Usuarios.ToList();
              
              
              return resultado;
              
            }
            catch(Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 2;
                resultado.Error = "Error al encontrar usuarios";

                return resultado;
            }
        }


        [HttpPost]
        [Route("/auth/login")]
        public ActionResult<ResultadoAPI> Login([FromBody] ComandoUsuarioLogin comando)
        {
            var resultado = new ResultadoAPI();
            var email = comando.Email.Trim();
            var password = comando.Password;
            try
            {
              var usu = db.Usuarios.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
              if(usu != null)
              {

                  db.Entry(usu).Reference(x => x.rol).Load(); 

                  resultado.Ok = true;
                  resultado.Return = usu;
              }
              else
              {
                  resultado.Ok = false;
                  resultado.Error = "Usuario o contraseña incorrectos!";
              }
           
              
              return resultado;
              
            }
            catch(Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 3;
                resultado.Error = "Error al conectarse, intente nuevamente!" + ex.Message;

                return resultado;
            }
        }

        [HttpPost]
        [Route("/auth/register")]
        public ActionResult<ResultadoAPI> Register([FromBody] ComandoUsuarioRegister comando)
        {
            var resultado = new ResultadoAPI();

            if(comando.Email.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese email!";
                return resultado;
            }

            if(comando.Password.Equals(""))
            {
                resultado.Ok = false;
                resultado.Error = "Ingrese contraseña!";
                return resultado;
            }

            var user = new Usuario();
            user.Email = comando.Email;
            user.Password = comando.Password;
            user.idRol = comando.idRol;
         
            db.Usuarios.Add(user);
            db.SaveChanges();
           
            resultado.Ok = true;
            resultado.Return = db.Usuarios.ToList();

            return resultado;
            
        }

     


    }
}