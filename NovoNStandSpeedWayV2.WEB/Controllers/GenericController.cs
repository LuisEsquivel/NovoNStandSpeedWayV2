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
using System.Data;

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
            var lst = Services.Get<T>().ToList();

            if (lst == null || lst.Count() == 0) return "{}";

            return JsonConvert.SerializeObject(Data(lst));

        }



        [HttpPost]
        public virtual string GetById(string Id)
        {
            var lst = Services.GetById<T>(Id).ToList();

            if (lst == null || lst.Count() == 0) return "{}";

            return JsonConvert.SerializeObject(Data(lst));
        }



        [HttpPost]
        public string Add(T o, string IsActive, IFormFile Imagen,  bool llevaImagen = true)
        {
            o.EstaActivo = IsActive != null ? Convert.ToBoolean(IsActive) : false;

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
                    var objeto = Services.GetById<T>(o.Id).FirstOrDefault();
                    o.UsuarioRegistroID = objeto.UsuarioRegistroID;
                    o.FechaAlta = objeto.FechaAlta;
                    o.UsuarioModificaID = "1";
                    var rowAdded = Data( Services.Update<T>(o) );
                    if (Responses.StatusCode == StatusCodes.Status200OK) return JsonConvert.SerializeObject(rowAdded);
                    else return Responses.Error;
                }


                //Insert
                o.UsuarioRegistroID = "1";
                o.UsuarioModificaID = "1";
                var rowModified = Data(Services.Insert<T>(o));
                if (Responses.StatusCode == StatusCodes.Status200OK) return JsonConvert.SerializeObject(rowModified);
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


        public DataTable Data(List<T> lst)
        {
            DataTable dt = new DataTable();
            DataColumn column;
            int numberRow = 0;
            string columnName = "";

            foreach (var item in lst)
            {
                dt.Rows.Add(dt.NewRow());

                foreach (var prop in item.GetType().GetProperties())
                {

                    columnName = prop.Name;
                    if (columnName == "EstaActivo") columnName = "ActivoBit";


                    if (!dt.Columns.Contains(columnName))
                    {
                        column = new DataColumn();
                        column.ColumnName = columnName;
                        dt.Columns.Add(column);
                    }


                    if (columnName == "FechaAlta") dt.Rows[numberRow][columnName] = Convert.ToDateTime(prop.GetValue(item).ToString()).ToShortDateString();
                    else if (columnName == "ActivoBit") dt.Rows[numberRow][columnName] = (bool)prop.GetValue(item) == true ? "SI" : "NO";
                    else dt.Rows[numberRow][columnName] = prop.GetValue(item);

                }

                numberRow++;

            }


            return dt;

        }



    }
}
