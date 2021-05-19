

using COMMON.Entidades;
using Microsoft.AspNetCore.Mvc;
using NovoNStandSpeedWayV2.WEB.Helpers;
using WEB.ApiServices;

namespace NovoNStandSpeedWayV2.WEB.Controllers
{

    public class LoginController : Controller
    {


        public Generals g;

        public LoginController()
        {
            g = new Generals();
        }
        // GET: Login


        public ActionResult Index()
        {

            if (g.GetCookieValue(g.CockieName) != null)
            {
                return Redirect("/Home/Index");
            }
            else
            {
                return View();
            }

        }


        public ActionResult Logout()
        {
            if (g.GetCookieValue(g.CockieName) != null)
            {
                Response.Cookies.Delete(g.CockieName);
            }

            return Redirect("../Login/Index");
        }


        public JsonResult Access(Usuarios user)
        {
            var res = Services.Login<Usuarios>(user);

            if (res != null)
            {
                user = (Usuarios)res;

                if (user.EstaActivo)
                {

                    ////account not verified
                    //if (user.CuentaVerificadaBit == false)
                    //{

                    //    //send email code verification
                    //    var random = new Random();
                    //    user.CodigoDeVerificacionVar = random.Next(0, 999999).ToString();
                    //    if (g.SendEmailSMTP(user.Usuario, user.CodigoDeVerificacionVar))
                    //    {
                    //        Services.Update<Usuarios>(user);
                    //        g.CreateCookie(user.Usuario);
                    //        return Json("/Registrarse/VerificarCuenta");
                    //    }

                    //}


                    var value = user.Id.ToString();
                    g.CreateCookie(value);
                    return Json(user.Nombre);

                }


            }



            return Json(0);

        }





    }


}