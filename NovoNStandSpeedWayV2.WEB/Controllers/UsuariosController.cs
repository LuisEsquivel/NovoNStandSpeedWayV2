
using COMMON.Entidades;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NovoNStandSpeedWayV2.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using WEB.ApiServices;


namespace NovoNStandSpeedWayV2.WEB.Controllers
{
    //[Authentication]
    public class UsuariosController : GenericController<Usuarios>
    {


        public HomeController hc;
        public Generals g;

        public UsuariosController()
        {
            hc = new HomeController();
            g = new Generals();
        }


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MiPerfil()
        {
            return View();
        }


        public string  ListaMiPerfil()
        {
            var MyUser = DeserializarLista<Usuarios>()
                .Where(x => x.Id == g.UserId())
                .Select(
                   u => new
                   {
                       u.Id,
                       u.Nombre,
                       u.Usuario,
                       u.EstaActivo,
                       u.FechaAlta
                   }
                ).ToList();
            return JsonConvert.SerializeObject(MyUser);
        }


        public List<T> DeserializarLista<T>()
        {

            List<T> lista = null;

            var result = Get();
            var json = JsonConvert.SerializeObject(result);
            lista = JsonConvert.DeserializeObject<List<T>>(json.ToString());

            return lista;

        }

  


        public object Get(bool VengoDeMiPerfil = false)
        {
            object o;

            try
            {
                var usuarios = Services.Get<Usuarios>();


                if (g.IsAdmin() == false || VengoDeMiPerfil)
                {
                    usuarios = usuarios.Where(x => x.Id == g.UserId() ).ToList();
                }

                var roles = Services.Get<Roles>();

                o = (from u in usuarios
                     join r in roles on u.Id equals r.Id 
                     into usu
                     from us in usu.DefaultIfEmpty()
                     select new
                     {
                         u.Id,
                         u.Nombre,
                         u.Usuario,
                         DescripcionVar = us?.Descripcion ?? "--SELECCIONE--",
                         IsActiveBit = u.EstaActivo != false ? "SI" : "NO",
                         FechaAlta = Convert.ToDateTime(u.FechaAlta).ToShortDateString()
                     }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return o;
        }


        public string Listar()
        {
            object list;

            try
            {

                var user = Services.Get<Usuarios>().Where(x => x.Id == g.UserId());

                var usuarios = Services.Get<Usuarios>();

                if (g.IsAdmin() == false)
                {
                    usuarios = usuarios.Where(x => x.Id == g.UserId()).ToList();
                }

                var roles = Services.Get<Roles>();

                list = (from u in usuarios
                        join r in roles on u.Id equals r.Id
                        into usu
                        from us in usu.DefaultIfEmpty()
                        select new
                        {
                            u.Id,
                            u.Nombre,
                            u.Usuario,
                            DescripcionVar =  us?.Descripcion ?? "--SELECCIONE--" ,
                            IsActiveBit = u.EstaActivo != false ? "SI" : "NO",
                            FechaAlta = Convert.ToDateTime(u.FechaAlta).ToShortDateString()
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
                var usuarios = Services.GetById<Usuarios>(id);
                o = (from u in usuarios
                     where u.Id == id
                     select new
                     {
                         u.Id,
                         u.Nombre,
                         u.RolID,
                         u.EstaActivo, 
                         u.EsAdmin,
                         u.Password,
                         u.Usuario,
                     }).ToList();
            }
            catch (Exception)
            {
                return null;
            }

            return JsonConvert.SerializeObject(o);
        }



        //[HttpPost]
        //public JsonResult Add(UsuarioAddOrUpdate o, string IsActive, string EsAdmin, bool VengoDeMiPerfil = false)
        //{
        //    o.ActivoBit = IsActive != null ? Convert.ToBoolean(IsActive) : false;
        //    o.EsAdminBit = EsAdmin != null ? Convert.ToBoolean(EsAdmin) : false;
        //    UsuarioAddOrUpdate result = null;



        //    try
        //    {
        //        if (o.UsuarioIdInt == 0)
        //        {

        //            var existe = services.Get<Usuario>("usuarios").
        //                         Where(
        //                         x => x.UsuarioVar == o.UsuarioVar
        //                         ).FirstOrDefault();

        //            if (existe != null)
        //            {
        //                var message = "Ya Existe un registro con estos campos: "
        //                             + Environment.NewLine
        //                             + "Correo Electrónico"
        //                             + Environment.NewLine
        //                             + "Verifique";

        //                return Json(new { message });

        //            }

        //            // ADD
        //            o.UsuarioRegIdInt = g.UserId();
        //            result = apiServices.Save<UsuarioAddOrUpdate>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UsuariosController, "Add", o);

        //        }

        //        else
        //        {

        //            //UPDATE
        //            var existe = services.Get<UsuarioAddOrUpdate>("usuarios").
        //                        Where(
        //                        x => x.UsuarioVar == o.UsuarioVar
        //                        &&
        //                        x.UsuarioIdInt != o.UsuarioIdInt
        //                        ).FirstOrDefault();

        //            if (existe != null)
        //            {
        //                var message = "Ya Existe un registro con estos campos: "
        //                             + Environment.NewLine
        //                             + "Correo Electrónico"
        //                             + Environment.NewLine
        //                             + "Verifique";

        //                return Json(new { message });

        //            }



        //            if (g.IsAdmin())
        //            {
        //                var u = services.Get<UsuarioAddOrUpdate>("usuarios").Where(x => x.UsuarioIdInt == o.UsuarioIdInt).FirstOrDefault();
        //                u.RolIdInt = o.RolIdInt;
        //                u.UsuarioIdModInt = g.UserId();
        //                if(VengoDeMiPerfil)  u.NombreVar = o.NombreVar;
        //                u.EsAdminBit = o.EsAdminBit;
        //                result = apiServices.Save<UsuarioAddOrUpdate>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UsuariosController, "Update", u);
        //            }


        //            if (g.IsAdmin() == false)
        //            {
        //                var u = services.Get<UsuarioAddOrUpdate>("usuarios").Where(x => x.UsuarioIdInt == o.UsuarioIdInt).FirstOrDefault();
        //                o.RolIdInt = u.RolIdInt;
        //                o.UsuarioIdModInt = g.UserId();
        //                o.UsuarioVar = u.UsuarioVar;
        //                result = apiServices.Save<UsuarioAddOrUpdate>(CoreResources.CoreResources.UrlBase, CoreResources.CoreResources.Prefix, CoreResources.CoreResources.UsuariosController, "Update", o);
        //            }


        //        }

        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }

        //    if (result == null) return null;


        //    return Json(new { message = Get(VengoDeMiPerfil) });
        //}


        #region DROPDOWN
        public string listarRoles()
        {
            object list;
            try
            {
                list = Services.Get<Roles>().Where(p => p.EstaActivo == true).Select(
                    x => new
                    {
                        IID = x.Id,
                        NOMBRE = x.Descripcion
                    }).OrderBy(p => p.NOMBRE).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }

            return JsonConvert.SerializeObject(list);
        }
        #endregion

    }
}

 
   