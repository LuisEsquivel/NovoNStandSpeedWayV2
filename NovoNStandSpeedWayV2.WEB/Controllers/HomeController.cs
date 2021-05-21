using COMMON.Entidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Linq;
using WEB.ApiServices;

namespace NovoNStandSpeedWayV2.WEB.Controllers
{

    //[Authentication]
    public class HomeController : Controller
    {

 

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }



        #region DropDowns

        public string DropDownUbicacion()
        {
            object list;
            try
            {
                var ubicaciones = Services.Get<Ubicaciones>();

                list = (from x in ubicaciones
                        where x.EstaActivo == true
                        select new
                        {
                            IID = x.Id,
                            NOMBRE = x.Descripcion
                        }).OrderBy(p => p.NOMBRE).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return JsonConvert.SerializeObject(list);
        }


        public string DropDownCentroDeCostos()
        {
            object list;
            try
            {
                var cCostos = Services.Get<CentroCostos>();

                list = (from x in cCostos
                        where x.EstaActivo == true
                        select new
                        {
                            IID = x.Id,
                            NOMBRE = x.Descripcion
                        }).OrderBy(p => p.NOMBRE).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return JsonConvert.SerializeObject(list);
        }


        public string DropDownFormaAdquisicion()
        {
            object list;
            try
            {
                var formaAdquisicion = Services.Get<FormaAdquisicion>();

                list = (from x in formaAdquisicion
                        where x.EstaActivo == true
                        select new
                        {
                            IID = x.Id,
                            NOMBRE = x.Descripcion
                        }).OrderBy(p => p.NOMBRE).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return JsonConvert.SerializeObject(list);
        }


       
        #endregion
    }
}