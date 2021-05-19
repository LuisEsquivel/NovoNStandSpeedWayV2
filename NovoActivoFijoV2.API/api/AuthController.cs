using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;


namespace API.api
{
    using COMMON.Entidades;
    using COMMON.Modelos;
    using COMMON.Interfaces.Catalogos;
    using BIZ;

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration configuracion;
        private readonly IAppsManager appsManager;
        public AuthController(IConfiguration configuration)
        {
            this.configuracion = configuration;
            appsManager = FabricManager.AppsManager();
        }
        [HttpPost("Login")]
        public ActionResult<string> Login([FromBody] AuthModel model)
        {
            try
            {
                if (model != null)
                {
                    Apps app = appsManager.ObtenerTodos.SingleOrDefault(a => a.NombreApp == model.NombreApp && a.Clave == model.Key);
                    if (app != null)
                    {
                        var secretKey = configuracion.GetValue<string>("SecretKey");
                        var key = Encoding.ASCII.GetBytes(secretKey);
                        List<Claim> claims = new List<Claim>()
                        {
                            new Claim(ClaimTypes.NameIdentifier,app.Id),
                            new Claim(ClaimTypes.Name,app.NombreApp)
                        };
                        var tokenDescriptor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(claims),
                            Expires = DateTime.UtcNow.AddDays(1),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var token = tokenHandler.CreateToken(tokenDescriptor);
                        return Ok(tokenHandler.WriteToken(token));
                    }
                    else
                    {
                        return Forbid();
                    }
                }
                else
                {
                    return Forbid();
                }
            }
            catch (Exception)
            {
                return BadRequest("Error al generar el token");
            }
        }
    }
}

