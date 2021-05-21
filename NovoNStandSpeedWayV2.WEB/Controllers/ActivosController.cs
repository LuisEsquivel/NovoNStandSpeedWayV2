using COMMON.Entidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WEB.ApiServices;

namespace NovoNStandSpeedWayV2.WEB.Controllers
{

    //[Authentication]
    public class ActivosController : GenericController<Activos>
    {


        public HomeController hc;

        public ActivosController()
        {

            hc = new HomeController();
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
        //        o = Services.Get<Activo>("activos").Select(

        //            x => new
        //            {
        //                x.ActivoIdInt ,
        //                x.DescripcionVar,
        //                x.EstadoActivoVar,
        //                FechaAdquisicion = Convert.ToDateTime(x.FechaAdquisicionDate).ToShortDateString()
        //            }

        //            ).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return o;
        //}


        //public JsonResult Listar()
        //{
        //    object o;

        //    try
        //    {
        //        var a = Services.Get<Activos>();

        //        o = (from x in a
        //             where x.EstaActivo == true
        //             select new
        //             {
        //                 x.ActivoIdInt,
        //                 x.DescripcionVar,
        //                 x.EstadoActivoVar,
        //                 FechaAdquisicion = Convert.ToDateTime(x.FechaAdquisicionDate).ToShortDateString()
        //             }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return JsonConvert.SerializeObject(o);
        //}



        //[HttpPost]
        //public JsonResult GetByID(int id)
        //{
        //    object o;

        //    try
        //    {
        //    o = services.Get<Activo>("activos").Select

        //    (
        //         x => new
        //         {
        //             x.ActivoIdInt,
        //             x.CentroCostosIdVar,
        //             x.EdificioVar,
        //             x.NoSerieVar,
        //             x.BarcodeVar,
        //             x.UbicacionIdVar,
        //             x.EstadoActivoVar,
        //             x.FormaAdquisicionIdInt,
        //             FechaAdquisicionDate = x.FechaAdquisicionDate.ToString("yyyy-MM-dd"),
        //             x.DocumentoVar,
        //             x.CostoDec,
        //             x.EpcVar,
        //             x.DescripcionVar,
        //             x.IdentificadorActivoVar,
        //             x.MarcaVar,
        //             x.ModeloVar,
        //             x.PisoVar,
        //             x.DepAcumuladaVar,
        //             x.ValorenLibrosVar,
        //             x.AdicionalVar,
        //             x.Adicional2Var,
        //             x.Adicional3Var,
        //             x.Adicional4Var,
        //             x.Adicional5Var,
        //             x.Adicional6Var,
        //             x.Adicional7Var,
        //             x.Adicional8Var,
        //             x.Adicional9Var,
        //             x.Adicional10Var,
        //             x.Adicional11Dec,
        //             x.Adicional12Dec,
        //             x.Adicional13Dec,
        //             x.Adicional14Dec,
        //             Adicional15Date = x.Adicional15Date.ToString("yyyy-MM-dd"),
        //             Adicional16Date = x.Adicional16Date.ToString("yyyy-MM-dd"),
        //         }
        //        ).ToList().Where(a => a.ActivoIdInt  == id);
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }


        //    return Json(o, JsonRequestBehavior.AllowGet);
        //}





    }
}
