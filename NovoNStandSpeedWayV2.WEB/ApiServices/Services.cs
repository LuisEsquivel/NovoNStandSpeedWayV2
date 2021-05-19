

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NovoNStandSpeedWayV2.WEB.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;


namespace WEB.ApiServices
{


    public static class Services
    {

        public static string url = "";
        public static StringContent data = null;
        public static HttpClient client;
        public static Uri uri;
        public static HttpResponseMessage responseMessage;

        public static object SetStringContent<T>(T model, string Action)
        {
            url = $"{CoreResources.UrlBase}{ CoreResources.Prefix}/{typeof(T).Name.ToLower()}/{Action}";
            var json = JsonConvert.SerializeObject(model);
            data = new StringContent(json, Encoding.UTF8, "application/json");
            client = new HttpClient();
            uri = new Uri(url);

            return true;
        }

        public static object SetResponse<T>()
        {
            object model = null;

            if (responseMessage.IsSuccessStatusCode)
            {
                var readTask = responseMessage.Content.ReadAsStringAsync().Result;
                JObject jsonn = JObject.Parse(readTask.ToString());
                model = JsonConvert.DeserializeObject<List<T>>(jsonn["Model"].ToString());
            }

            return model;
        }



        public static void SetResponse()
        {

            if (responseMessage.IsSuccessStatusCode)
            {
                var readTask = responseMessage.Content.ReadAsStringAsync().Result;
                JObject jsonn = JObject.Parse(readTask.ToString());
            }

        }


        public static List<T> Get<T>(object model = null,
                                     string id = "")
        {

            List<T> objeto = null;
            HttpContent content = null;


            var method = "/get";
            if (id.Trim().Length > 0) method = "/getbyid/" + id;


            if (model != null)
            {
                method = $"{"/getbyvalues/"}";
                var json = JsonConvert.SerializeObject(model);
                content = new StringContent(json, Encoding.UTF8, "application/json");
            }


            var url = CoreResources.UrlBase + CoreResources.Prefix + "/" + typeof(T).Name + method;


            try
            {

                using (var client = new HttpClient())
                {

                    var uri = new Uri(Path.Combine(url));


                    if (model != null) responseMessage  = client.PostAsync(uri, content).Result;
                    else responseMessage = client.GetAsync(uri).Result;

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        string readTask = responseMessage.Content.ReadAsStringAsync().Result;
                        JObject json = JObject.Parse(readTask.ToString());
                        objeto = JsonConvert.DeserializeObject<List<T>>(json["Model"].ToString());
                    }
                    else
                    {
                        objeto = null;
                    }
                }


                return objeto;

            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return objeto;
            }

        }






        public static object Login<T>(T model)
        {

            object objeto = null;
            var url = CoreResources.UrlBase + CoreResources.Prefix + "/usuarios/login";

            try
            {

                using (var client = new HttpClient())
                {

                    var uri = new Uri(Path.Combine(url));
                    var json = JsonConvert.SerializeObject(model);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var responseTask = client.PostAsync(uri, content).Result;

                    if (responseTask.IsSuccessStatusCode)
                    {
                        string readTask = responseTask.Content.ReadAsStringAsync().Result;
                        JObject jsonn = JObject.Parse(readTask.ToString());
                        objeto = JsonConvert.DeserializeObject<T>(jsonn["Model"].ToString());

                    }
                    else
                    {

                        objeto = null;

                    }
                }


                return objeto;

            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return objeto;
            }

        }


        public static List<T> GetById<T>(string id)
        {

            try
            {
                url = $"{CoreResources.UrlBase}{CoreResources.Prefix}/{typeof(T).Name.ToLower()}/getbyid/{id}";
                uri = new Uri(url);
                client = new HttpClient();
                var json = JsonConvert.SerializeObject(id);
                data = new StringContent(json, Encoding.UTF8, "application/json");
                responseMessage = client.PostAsync(uri, data).Result;
                return (List<T>)SetResponse<T>();

            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return null;
            }


        }

        public static List<T> GetCurrentRow<T>(string id)
        {

            try
            {

                url = $"{CoreResources.UrlBase}{CoreResources.Prefix}/{typeof(T).Name.ToLower()}/getcurrentrow/{id}";
                uri = new Uri(url);
                client = new HttpClient();
                var json = JsonConvert.SerializeObject(id);
                data = new StringContent(json, Encoding.UTF8, "application/json");
                responseMessage = client.PostAsync(uri, data).Result;
                var list = (List<T>)SetResponse<T>();
                return list;
            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
                return null;
            }

        }



        public static void Insert<T>(T model)
        {
            try
            {
                SetStringContent<T>(model, "Post");
                responseMessage = client.PostAsync(uri, data).Result;
                SetResponse<T>();
            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
            }

        }


        public static void Update<T>(T model)
        {

            try
            {
                SetStringContent<T>(model, "Put");
                responseMessage = client.PutAsync(uri, data).Result;
                SetResponse<T>();
            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
            }

        }


        public static void Delete<T>(string id)
        {
            try
            {
                url = $"{CoreResources.UrlBase}{CoreResources.Prefix}/{typeof(T).Name.ToLower()}/delete/{id}";
                uri = new Uri(url);
                client = new HttpClient();
                var json = JsonConvert.SerializeObject(id);
                data = new StringContent(id, Encoding.UTF8, "application/json");
                responseMessage = client.PostAsync(uri, data).Result;
                SetResponse();
            }
            catch (Exception ex)
            {
                Responses.StatusCode = (int)responseMessage.StatusCode;
                Responses.Error = ex.ToString();
            }

        }


    }

}




