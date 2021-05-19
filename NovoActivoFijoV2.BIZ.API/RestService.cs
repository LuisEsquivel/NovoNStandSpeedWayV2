
using COMMON.Entidades;
using COMMON.Modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BIZ.API
{
    public class RestService<T> where T : BaseDTO
    {
        private readonly HttpClient client;

        public string Error { get; private set; }

        //Uri: https://www.novovending.com/api/TipoUsuarios
        //UriBase: https://www.novovending.com/
        //uri: api/TipoUsuarios
        public RestService(string uriBase)
        {
            client = new HttpClient() { BaseAddress = new Uri(uriBase) };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.MaxResponseContentBufferSize = 256000000;
        }
        #region Token
        public void ObtenerToken(string app, string key)
        {
            try
            {
                string token = ObtenerTokenAsync(new AuthModel()
                {
                    NombreApp = app,
                    Key = key
                }).Result;
                Parametros.Token = token.Replace("\"", "");
                //Parametros.Token = token.Replace("\"","");

            }
            catch (Exception ex)
            {
                Error = ex.Message;
            }
        }

        private async Task<string> ObtenerTokenAsync(AuthModel model)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("api/Auth/Login", content).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                string respuesta = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return respuesta;
            }
            else
            {
                throw new Exception("Credenciales invalidas");
            }
        }
        #endregion
        private async Task<IEnumerable<T>> ObtenerTodosAsync()
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Parametros.Token);
            HttpResponseMessage responseMessage = await client.GetAsync($"api/{typeof(T).Name}").ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<IEnumerable<T>>(await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            else
            {
                Error = "Error al comunicarse con el WebService";
                return null;
            }
        }

        public IEnumerable<T> ObtenerTodos()
        {
            try
            {
                return ObtenerTodosAsync().Result;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        private async Task<T> BuscarPorIdAsync(string id)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Parametros.Token);
            HttpResponseMessage httpResponseMessage = await client.GetAsync($"api/{typeof(T).Name}/{id}").ConfigureAwait(false);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            else
            {
                Error = "Error al comunicarse con el WebService";
                return null;
            }
        }

        public T BuscarPorId(string id)
        {
            try
            {
                return BuscarPorIdAsync(id).Result;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        private async Task<T> InsertarAsync(T entidad)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Parametros.Token);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await client.PostAsync($"api/{typeof(T).Name}", content).ConfigureAwait(false);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            else
            {
                Error = "Error al comunicarse con el WebService";
                return null;
            }
        }
        public T Insertar(T entidad)
        {
            try
            {
                return InsertarAsync(entidad).Result;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        private async Task<T> ActualizarAsync(T entidad)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Parametros.Token);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");
            HttpResponseMessage httpResponseMessage = await client.PutAsync($"api/{typeof(T).Name}", content).ConfigureAwait(false);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<T>(await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            else
            {
                Error = "Error al comunicarse con el WebService";
                return null;
            }
        }

        public T Actualizar(T entidad)
        {
            try
            {
                return ActualizarAsync(entidad).Result;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }

        private async Task<bool> EliminarAsync(string id)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Parametros.Token);
            HttpResponseMessage httpResponseMessage = await client.DeleteAsync($"api/{typeof(T).Name}/{id}").ConfigureAwait(false);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Error = "";
                return JsonConvert.DeserializeObject<bool>(await httpResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            else
            {
                Error = "Error al comunicarse con el WebService";
                return false;
            }
        }

        public bool Eliminar(string id)
        {
            try
            {
                return EliminarAsync(id).Result;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }

        private async Task<string> ObtenerVistaAsync(string uriVista)
        {
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Parametros.Token);
            HttpResponseMessage responseMessage = await client.GetAsync($"api/{uriVista}").ConfigureAwait(false);
            if (responseMessage.IsSuccessStatusCode)
            {
                Error = "";
                return await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            }
            else
            {
                Error = "Error al comunicarse con el WebService";
                return null;
            }
        }

        public string ObtenerVista(string uriVista)
        {
            try
            {
                return ObtenerVistaAsync(uriVista).Result;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }
    }
}
