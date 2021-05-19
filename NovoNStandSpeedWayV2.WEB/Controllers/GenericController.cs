using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IO;
using WEB.ApiServices;
using NovoNStandSpeedWayV2.WEB.Helpers;
using COMMON.Entidades;
using System.Collections.Generic;

namespace NovoNStandSpeedWayV2.WEB.Controllers
{
    public class GenericController<T> : Controller where T : BaseDTO
    {

        ~GenericController()
        {
            Responses.StatusCode = 0;
            Responses.Error = "";
        }

        public ActionResult Inicio()
        {
            return View();
        }

        public virtual string List()
        {
            return JsonConvert.SerializeObject(Services.Get<T>().ToList());
        }

        public T objeto(T o)
        {
            return Services.Get<T>().FirstOrDefault();
        }



        [HttpPost]
        public virtual string GetById(string Id)
        {
            return JsonConvert.SerializeObject(Services.GetById<T>(Id).ToList());
        }

        public string GetCurrentRow(string id)
        {
            return JsonConvert.SerializeObject(Services.GetCurrentRow<T>(id).ToList());
        }

        [HttpPost]
        public string Add(T o, IFormFile Imagen, bool llevaImagen = true)
        {


            try
            {

                Type t = typeof(T);
                var Image = t.GetProperty("Image");
                string base64 = null;

                //Image
                if (Imagen != null && Imagen.Length > 0 && llevaImagen)
                {
                    using MemoryStream ms = new MemoryStream();
                    Imagen.CopyTo(ms);
                    base64 = Convert.ToBase64String(ms.ToArray());
                    if (!base64.Contains("data:image/jpeg;base64,")) base64 = "data:image/jpeg;base64," + base64;
                }

                if (Image != null) Image.SetValue(o, base64);

                //Update
                if (o.Id != null)
                {
                    o.Id = "1";
                    o.FechaAlta = DateTime.Now;
                    Services.Update<T>(o);
                    if (Responses.StatusCode == StatusCodes.Status200OK) return GetCurrentRow(o.Id);
                    else return Responses.Error;
                }


                //Insert
                o.Id = Guid.NewGuid().ToString();
                o.FechaAlta = DateTime.Now;
                o.UsuarioRegistroID = "1";
                o.UsuarioModificaID = "1";
                Services.Insert<T>(o);
                if (Responses.StatusCode == StatusCodes.Status200OK) return GetCurrentRow(o.Id);
                else return Responses.Error;
            }
            catch
            {
                return Responses.Error;
            }


        }



        public string Delete(T o)
        {
            Services.Delete<T>(o.Id);
            if (Responses.StatusCode == StatusCodes.Status200OK) return "deleted";

            return null;
        }




    }
}
