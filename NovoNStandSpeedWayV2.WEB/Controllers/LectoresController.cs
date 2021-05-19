using Microsoft.AspNetCore.Mvc;
using NovoNStandSpeedWayV2.WEB.Helpers;


namespace NovoNStandSpeedWayV2.WEB.Controllers
{
    //[Authentication]
    public class LectoresController : Controller
    {

        public Generals g;

        public LectoresController()
        {
            g = new Generals();
        }

        // GET: Lectores
        public ActionResult Index()
        {
            return View();
        }


        //public object Get()
        //{
        //    object list;

        //    try
        //    {
        //        var lectores = Services.Get<Lectore>("lectores");

        //        list = (from l in lectores
        //                select new
        //                {
        //                    l.LectorIdInt,
        //                    l.DescripcionVar,
        //                    l.DireccionVar,
        //                    l.ModeloVar,
        //                    FechaAlta = Convert.ToDateTime(l.FechaAltaDate).ToShortDateString()
        //                }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}


        //public JsonResult Listar()
        //{
        //    object list;

        //    try
        //    {
        //        var lectores = services.Get<Lectore>("lectores");

        //        list = (from l in lectores
        //                select new
        //                {
        //                    l.LectorIdInt,
        //                    l.DescripcionVar,
        //                    l.DireccionVar,
        //                    l.ModeloVar,
        //                    FechaAlta = Convert.ToDateTime(l.FechaAltaDate).ToShortDateString()
        //                }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return Json(list, JsonRequestBehavior.AllowGet);
        //}



        //[HttpPost]
        //public JsonResult GetByID(int id)
        //{
        //    object o;

        //    try
        //    {
        //        var lectores = Services.Get<Lectore>("lectores");
        //        o = (from l in lectores
        //             where l.LectorIdInt == id
        //             select new
        //             {
        //                 l.LectorIdInt,
        //                 l.DescripcionVar,
        //                 l.DireccionVar,
        //                 l.ModeloVar
        //             }).ToList();
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    return Json(o, JsonRequestBehavior.AllowGet);
        //}



    }
}