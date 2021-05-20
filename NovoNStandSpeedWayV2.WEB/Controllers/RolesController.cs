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
    public class RolesController : GenericController<Roles>
    {

        public Generals g;

        public RolesController()
        {
            g = new Generals();
        }


        public IActionResult Index()
        {
            return View();
        }


        //public object Get()
        //{
        //    object o;

        //    try
        //    {
        //        var roles = Services.Get<Roles>();
        //        o = (from x in roles
        //             where x.EstaActivo == true
        //             select new
        //             {
        //                 x.Id,
        //                 x.Descripcion,
        //                 IsActiveBit = x.EstaActivo != false ? "SI" : "NO",
        //                 FechaAlta = Convert.ToDateTime(x.FechaAlta).ToShortDateString()
        //             }).ToList();
                

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
        //        var roles = Services.Get<Roles>();
        //        o = (from x in roles
        //             where x.EstaActivo == true
        //             select new
        //             {
        //                 x.Id,
        //                 x.Descripcion,
        //                 IsActiveBit = x.EstaActivo != false ? "SI" : "NO",
        //                 FechaAlta = Convert.ToDateTime(x.FechaAlta).ToShortDateString()
        //             }).ToList();


        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return JsonConvert.SerializeObject(o);
        //}



        //[HttpPost]
        //public override string GetById(string id)
        //{
        //    object o;

        //    try
        //    {
        //        var roles = Services.Get<Roles>();
        //        o = (from a in roles
        //             where a.Id == id
        //             select new
        //             {
        //                 a.Id,
        //                 a.Descripcion,
        //                 ActivoBit = a.EstaActivo != false ? "SI" : "NO",
        //                 FechaAlta = Convert.ToDateTime(a.FechaAlta).ToShortDateString()
        //             }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return JsonConvert.SerializeObject(o);
        //}


    }

}


