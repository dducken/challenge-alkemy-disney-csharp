using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Models;
using Resultados;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Comandos.Usuario;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Controllers
{
    [ApiController]
    [EnableCors("DisneyAdmin")]
    public class UsuarioController : ControllerBase
    {
        private readonly Context db = new Context();
        private readonly IConfiguration _configuration;
        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        // ===========================READ USERS============================== //
        [HttpGet]
        [Route("/get/users")]
        [Authorize]

        public ActionResult<ResultadoAPI> Get()
        {
            var resultado = new ResultadoAPI();

            try
            {
               foreach (Usuario usu in db.Usuarios.ToList())
                {
                   var user = db.Usuarios.FirstOrDefault(x => x.IdUsuario.Equals(usu.IdUsuario));
                   db.Entry(user).Reference(x => x.rol).Load();
                }

                resultado.Ok = true;
                resultado.Return = db.Usuarios.ToList();

                return resultado;
            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 404;
                resultado.Error = "Error al encontrar usuarios " + ex;

                return resultado;
            }
        }

        // ===========================LOGIN============================== //

        [HttpPost]
        [Route("/auth/login")]
        [AllowAnonymous]
        public ActionResult<ResultadoAPI> Login([FromBody] ComandoUsuarioLogin comando)
        {
            var resultado = new ResultadoAPI();
            var email = comando.Email.Trim();
            var password = comando.Password;
            try
            {
                var usu = db.Usuarios.FirstOrDefault(x => x.Email.Equals(email) && x.Password.Equals(password));
                if (usu != null)
                {
                    // Leemos el secret_key desde nuestro appseting
                    var secretKey = _configuration.GetValue<string>("SecretKey");
                    var key = Encoding.ASCII.GetBytes(secretKey);

                    // Creamos los claims (pertenencias, características) del usuario
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                     {
                        new Claim(ClaimTypes.NameIdentifier, usu.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Email, usu.Email)
                     }),
                        // Nuestro token va a durar una hora
                        Expires = DateTime.UtcNow.AddHours(1),
                        // Credenciales para generar el token usando nuestro secretykey y el algoritmo hash 256
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var tokenHandler = new JwtSecurityTokenHandler();
                    var createdToken = tokenHandler.CreateToken(tokenDescriptor);

                    db.Entry(usu).Reference(x => x.rol).Load();

                    resultado.Ok = true;
                    resultado.Return = tokenHandler.WriteToken(createdToken);
                }
                else{
                    resultado.Ok = false;
                    resultado.Error = "Usuario o contraseña incorrectos!";
                }

                return resultado;

            }
            catch (Exception ex)
            {
                resultado.Ok = false;
                resultado.CodigoError = 3;
                resultado.Error = "Error al conectarse, intente nuevamente! " + ex.Message;

                return resultado;
            }
        }
        // ===========================REGISTER============================== //

        [HttpPost]
        [Route("/auth/register")]
        [AllowAnonymous]
        public ActionResult<ResultadoAPI> Register([FromBody] ComandoUsuarioRegister comando)
        {
            var resultado = new ResultadoAPI();

            var user = new Usuario();
            user.Email = comando.Email;
            user.Password = comando.Password;
            user.idRol = comando.idRol;

            db.Usuarios.Add(user);
            db.SaveChanges();

            resultado.Ok = true;
            resultado.Return = "Registrado con éxito! Email: " + user.Email + " Password: " + user.Password;

  
            var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("alxfaml@gmail.com", "Alex");
            var subject = "Correo de bienvenida";
            var to = new EmailAddress(user.Email, user.Email);
            var plainTextContent = "gracias por registrarte!";
            var htmlContent = "<strong>Gracias por registrarte, bienvenido a Alkemy!</strong>" +
            "<img src=https://media-exp1.licdn.com/dms/image/C4E1BAQEDDjuh9HQchg/company-background_10000/0/1610631110628?e=2159024400&v=beta&t=00JMFny1Y6JiSd8rpPDIfJ_6vNH6NhtCK_yban1zy3c style= width:300px height: 300px> ";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response =  client.SendEmailAsync(msg);
        

            return resultado;
         
        }
        // static async Task Execute(){
        //     var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
        //     var client = new SendGridClient(apiKey);
        //     var from = new EmailAddress("alxfaml@gmail.com", "Alex");
        //     var subject = "Correo de bienvenida";
        //     var to = new EmailAddress("test@example.com", "Example User");
        //     var plainTextContent = "gracias por registrarte!";
        //     var htmlContent = "<strong>Se ha registrado correctamente!</strong>";
        //     var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        //     var response = await client.SendEmailAsync(msg);
        // }


    }
}