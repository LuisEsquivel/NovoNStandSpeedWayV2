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
    public class CentroDeCostosController : GenericController<CentroCostos>
    { 


        public ActionResult Index()
        {
            return View();
        }


        public string Get()
        {
            object list;

            try
            {
                var centroConstos = Services.Get<CentroCostos>();


                 list = (from c in centroConstos
                        select new
                        {
                            c.Id,
                            c.Descripcion,
                            ActivoBit = c.EstaActivo != false ? "SI" : "NO",
                            FechaAlta = Convert.ToDateTime(c.FechaAlta).ToShortDateString()
                        }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return JsonConvert.SerializeObject(list);
        }


        public string Listar()
        {
            return Get();
        }





    }
}