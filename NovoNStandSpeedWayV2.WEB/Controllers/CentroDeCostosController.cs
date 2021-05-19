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
        public Generals g;
     

        public CentroDeCostosController()
        {
            g = new Generals();
        }

        // GET: CentroCosto
        public ActionResult Index()
        {
            return View();
        }


        public object Get()
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

            return list;
        }


        public string Listar()
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



        [HttpPost]
        public string GetByID(string id)
        {
            object o;

            try
            {
                var CentroDeCostos = Services.GetById<CentroCostos>(id);
                o = (from c in CentroDeCostos
                     where c.Id == id
                     select new
                     {
                         c.Id,
                         c.Descripcion,
                         c.EstaActivo,

                     }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return JsonConvert.SerializeObject(o);
        }



 
    }
}