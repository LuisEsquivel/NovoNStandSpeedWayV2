

using COMMON.Entidades;
using Microsoft.AspNetCore.Mvc;
using NovoNStandSpeedWayV2.WEB.Helpers;
using System;
using System.Linq;
using WEB.ApiServices;

namespace NovoNStandSpeedWayV2.WEB.Controllers

{
    public class RegistrarseController : Controller
    {

        public HomeController hc;
        public Generals g;

        public RegistrarseController()
        {
            hc = new HomeController();
            g = new Generals();
        }

        // GET: Registrarse
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult VerificarCuenta()
        {
            return View();
        }


        //olvidé la contraseña
        public ActionResult OlvideContraseña()
        {
            return View();
        }


        //actualizar la actualizar
        public ActionResult ActualizarContraseña()
        {
            return View();
        }



        //Enviar Código De Verificación
        public JsonResult EnviarCodigoDeverificacion(string UsuarioVar)
        {

            object result = null;

            var usuario = Services.Get<Usuarios>("usuarios").Where(x => x.Usuario == UsuarioVar).FirstOrDefault();
            if (usuario == null)
            {
                return Json("No se encontró cuenta de Novo asociada a este correo: " + UsuarioVar + Environment.NewLine + "Verifica que el correo está bien escrito y vuelve a intentarlo");
            }

            if (usuario.Id != "")
            {
                var random = new Random();
                //usuario. = random.Next(0, 999999).ToString();
                usuario.Password = "XdXd";

                //if (g.SendEmailSMTP(usuario.Usuario, usuario.CodigoDeVerificacionVar) == false)
                //{
                //    return Json(0);
                //}

                Services.Update<Usuarios>(usuario);

                if (Response.StatusCode != 200)
                {
                    g.CreateCookie(UsuarioVar);
                    return Json(1);
                }
            }

            return Json(0);
        }




        //public JsonResult UpdatePassword(Usuarios o, string ConfirmPassword)
        //{

        //    if (o == null) return Json(-1);

        //    if (o.Password != ConfirmPassword) return Json(0);

        //    var UsuarioVar = g.GetCookieValue(g.CockieName);
        //    var usuario = services.Get<Usuario>("usuarios").Where(x => x.UsuarioVar == UsuarioVar).FirstOrDefault();
        //    usuario.Password = o.Password;


        //    if (o.CodigoDeVerificacionVar != usuario.CodigoDeVerificacionVar) return (Json(2));

        //    Services.Update<Usuarios>(usuario);

        //    if (result == null) return Json(-1);



        //    g.CreateCookie(result.UsuarioIdInt.ToString());
        //    return Json(1);

        //}



        public JsonResult Add(Usuarios o, string IsActive = "true", bool GoogleAccount = false)
        {
            o.EstaActivo = IsActive != null ? Convert.ToBoolean(IsActive) : false;
            Usuarios result = null;


            // es cuenta de google
            if (GoogleAccount)
            {

                Services.Get<Usuarios>("usuarios").Where(x => x.Usuario == o.Usuario).FirstOrDefault();

                if (result == null)
                {

                    o.EstaActivo = true;

                    //agregamos el email a la BD
                    Services.Insert<Usuarios>(o);


                    //validamos la cuenta
                    o.Id = result.Id;
                    //o.CuentaVerificadaBit = true;
                    //result = Services.Insert<Usuarios>(o);

                }


                if (result != null)
                {
                    g.CreateCookie(result.Id.ToString());
                    return Json(1);
                }


                return Json(0);
            }



            try
            {
                if (o.Id == "")
                {

                    var existe = Services.Get<Usuarios>("usuarios").
                                 Where(
                                 x => x.Usuario == o.Usuario
                                 ).FirstOrDefault();

                    if (existe != null)
                    {
                        var message = "Ya Existe un registro con estos campos: "
                                     + Environment.NewLine
                                     + o.Usuario
                                     + Environment.NewLine
                                     + "Verifique";

                        return Json(new { message });

                    }

                    // ADD
                    o.FechaAlta = DateTime.Now;

                    var random = new Random();
                    //o.CodigoDeVerificacionVar = random.Next(0, 999999).ToString();

                    //if (g.SendEmailSMTP(o.Usuario , o.CodigoDeVerificacionVar) == false)
                    //{
                    //    return Json(0);
                    //}

                    //result = Services.Insert<Usuarios>(o);

                }

            }
            catch (Exception)
            {
                return Json(0);
            }

            if (result == null) return Json(0);


            //for verified account
            g.CreateCookie(o.Usuario);
            return Json(1);
        }



        public JsonResult ValidarCuenta(string CodigoVerificacionVar)
        {

            object result = null;

            try
            {

                var UsuarioVar = g.GetCookieValue(g.CockieName);
                var usuario = Services.Get<Usuarios>("usuarios").Where(x => x.Usuario == UsuarioVar).FirstOrDefault();

                //if (CodigoVerificacionVar == usuario.CodigoDeVerificacionVar)
                //{

                //    //usuario.CuentaVerificadaBit = true;
                //    Services.Update<Usuarios>(usuario);

                //    if (result != null)
                //    {
                //        g.CreateCookie(usuario.Usuario.ToString());
                //        return Json(1);
                //    }


                //}

            }
            catch (Exception)
            {
                return null;
            }

            return Json(0);

        }

    }


}