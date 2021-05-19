

using COMMON.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using WEB.ApiServices;

namespace NovoNStandSpeedWayV2.WEB.Helpers
{
    public class Generals : Controller
    {

        public string CockieName = "UserIdNovoSpeedWay";
        public string key = "ABCDEFGHIJKLMÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz";




        public bool SendEmailSMTP(string CuentaDeCorreo, string CodigoVerificacion)
        {

            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            try
            {

                String Body = @"<br></br>";

                Body += @"<center><div style=""background-color:#F5F5F5;""><br/><img src=""https://novosys.com.mx/Img/NOVO_logosmall-1.png"" width=""320"" height=""150""></div></center>";

                Body += @"<center>TU CÓDIGO DE VERIFICACIÓN ES<center/>";

                Body += @"<br></br>";

                Body += @"<center style=""font-size:40px !important;"">" + CodigoVerificacion + "<center/>";

                mail.From = new MailAddress("informacion.novosys@gmail.com");

                mail.To.Add(CuentaDeCorreo);

                mail.Subject = "Verificación de tu cuenta Novosys :)";
                mail.IsBodyHtml = true;
                mail.Body = Body;

                SmtpServer.Port = 587;

                SmtpServer.EnableSsl = true;

                //Godaddy configuration
                //SmtpServer.Host = "relay-hosting.secureserver.net";
                //SmtpServer.Host = "smtpout.secureserver.net";
                SmtpServer.Host = "smtp.gmail.com";

                SmtpServer.UseDefaultCredentials = false;

                //Godaddy configurarion No Credentials
                SmtpServer.Credentials = new System.Net.NetworkCredential("informacion.novosys@gmail.com", "Nov2010?");

                SmtpServer.Send(mail);

                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }


        //we get the id of the current user 
        public string UserId()
        {
            return GetCookieValue(CockieName);
        }


        //we get the bit value of the user to know if the user is administrator
        public bool IsAdmin()
        {
            if (GetCookieValue(CockieName) == null) return false;
            int UsuarioIdInt = 0;
            int.TryParse(GetCookieValue(CockieName), out UsuarioIdInt);

            if (UsuarioIdInt > 0)
            {
                return Services.Get<Usuarios>().Where(x => x.Id.ToString() == GetCookieValue(CockieName)).FirstOrDefault().EsAdmin;
            }

            return false;
        }



        //we get the value of the cookie
        public string GetCookieValue(string CookieName)
        {

            string value = null;
            var allCoockies = Request?.Cookies;


            if (allCoockies != null)
            {
                if (Request.Cookies.ContainsKey(CookieName))
                {
                    value = Request.Cookies[CookieName].ToString();
                }

            }

            if (value == null) return null;
            return Decrypt(value);
        }




        //if exist the cookie then we delete and after that we create a new cookie for add value in the browser
        public void CreateCookie(string value)
        {

            if (Request.Cookies[CockieName] != null)
            {
                if (Request.Cookies[CockieName].ToString().Trim().Length > 0)
                {
                    Response.Cookies.Delete(CockieName);
                }

            }

            CookieOptions co = new CookieOptions();
            co.Expires = DateTime.Now.AddMonths(1);
            HttpContext.Response.Cookies.Append(CockieName, value, co);
        }




        //first we encrypt the value of the coockie and finally we convert to base64 the value encrypted
        public string Encrypt(string value)
        {
            //we get one key aceptable (24 bits) for TripleDESCryptoServiceProvider
            byte[] slt = Encoding.UTF8.GetBytes(key);
            var pdb = new Rfc2898DeriveBytes(key, slt);
            var keyAceptable = pdb.GetBytes(24);

            //we use the algoritm TripleDESCryptoServiceProvider()
            var tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyAceptable;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            //we instance ICryptoTransform and we use the algoritm CreateEncryptor()
            ICryptoTransform cTransform = tdes.CreateEncryptor();

            //we encrypt the array bytes
            var BytesValue = Encoding.UTF8.GetBytes(value);
            byte[] ArrayResultado = cTransform.TransformFinalBlock(BytesValue, 0, BytesValue.Length);

            tdes.Clear();

            //we return the value of array in Base64String
            return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
        }





        //we receive the value en base64, aftter that we convert the value to an array of bytes 
        //with Convert.FromBase64String abnd finally we decrypt that array of bytes with TransformFinalBlock
        //and we return this value as string 
        public string Decrypt(string value)
        {

            byte[] slt = Encoding.UTF8.GetBytes(key);
            var pdb = new Rfc2898DeriveBytes(key, slt);
            var keyAceptable = pdb.GetBytes(24);

            var tdes = new TripleDESCryptoServiceProvider();

            tdes.Key = keyAceptable;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();

            var BytesValue = Convert.FromBase64String(value);
            byte[] resultArray = cTransform.TransformFinalBlock(BytesValue, 0, BytesValue.Length);

            tdes.Clear();

            //we return the result in string form
            return Encoding.UTF8.GetString(resultArray);
        }


    }
}
