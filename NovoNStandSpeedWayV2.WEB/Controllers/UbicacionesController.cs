using COMMON.Entidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NovoNStandSpeedWayV2.WEB.Helpers;
using System;
using System.Linq;
using WEB.ApiServices;

namespace NovoNStandSpeedWayV2.WEB.Controllers
{

    //[Authentication]
    public class UbicacionesController : GenericController<Ubicaciones>
    {
     
        public Generals g;

        public UbicacionesController()
        {
            g = new Generals();
        }


        public ActionResult Index()
        {
            return View();
        }


        //public object Get()
        //{
        //    object o;

        //    try
        //    {
        //        o = Services.Get<Ubicaciones>().Select(

        //            x => new
        //            {
        //                x.Id,
        //                x.Descripcion,
        //                IsActiveBit = x.EstaActivo != false ? "SI" : "NO",
        //                FechaAlta = Convert.ToDateTime(x.FechaAlta).ToShortDateString()
        //            }

        //            ).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return o;
        //}


        //public override string List()
        //{
        //    object o;

        //    try
        //    {
        //        o = Services.Get<Ubicaciones>().Select(

        //            x => new
        //            {
        //                x.Id,
        //                x.Descripcion,
        //                IsActiveBit = x.EstaActivo != false ? "SI" : "NO",
        //                FechaAlta = Convert.ToDateTime(x.FechaAlta).ToShortDateString()
        //            }

        //            ).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return JsonConvert.SerializeObject(o);
        //}



        //[HttpPost]
        //public string GetByID(string id)
        //{
        //    object o;

        //    try
        //    {
        //        o = Services.GetById<Ubicaciones>(id).Select(

        //            x => new
        //            {
        //                x.Id,
        //                x.Descripcion,
        //                x.EstaActivo,
        //                FechaAlta = Convert.ToDateTime(x.FechaAlta).ToShortDateString()
        //            }

        //            ).Where(x => x.Id == id).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return JsonConvert.SerializeObject(o);
        //}





    }

}