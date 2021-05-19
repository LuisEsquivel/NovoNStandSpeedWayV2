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
                list = Services.Get<Ubicaciones>("ubicaciones").Where(p => p.EstaActivo == true).Select(
                    x => new
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


        //public JsonResult DropDownCentroDeCostos()
        //{
        //    object list;
        //    try
        //    {
        //        list = services.Get<CentroCosto>("centrodecostos").Where(p => p.ActivoBit == true).Select(
        //            x => new
        //            {
        //                IID = x.CentroCostosIdVar,
        //                NOMBRE = x.DescripcionVar,
        //                x.ActivoBit 
        //            })
        //            .Where(x => x.ActivoBit == true)
        //            .OrderBy(p => p.NOMBRE).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        public string DropDownFormaAdquisicion()
        {
            object list;
            try
            {
                list = Services.Get<FormaAdquisicion>("formaadquisicion").Where(p => p.EstaActivo == true).Select(
                    x => new
                    {
                        IID = x.Id,
                        NOMBRE = x.Descripcion,
                        x.EstaActivo 
                    })
                    .Where(x => x.EstaActivo == true)
                    .OrderBy(p => p.NOMBRE).ToList();
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