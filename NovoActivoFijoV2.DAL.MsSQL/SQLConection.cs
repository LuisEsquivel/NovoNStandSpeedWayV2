
namespace DAL.MsSQL
{
    using global::DAL.MsSQL.CoreResource;
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    public class SQLConection
    {
        private readonly SqlConnection conexion;
        public string Error { get; private set; }
        public SQLConection()
        {
            //conexion = new SqlConnection(Core.CoreResources.ConectionString);
            conexion = new SqlConnection(CoreResources.ConectionStringLocal);
            Conectar();
        }

        private bool Conectar()
        {
            try
            {
                conexion.Open();
                Error = "";
                return true;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return false;
            }
        }
        /// <summary>
        /// Ejecuta un comando SQL sobre la base de datos (Insert, Update, Delete)
        /// </summary>
        /// <param name="command">Comando SQL a ejecutar</param>
        /// <returns>El número de filas afectadas, -1 cuando ha opcurrido un error.</returns>
        public int Comando(string command)
        {
            try
            {
                Debug.Print($"====>{command}");
                SqlCommand cmd = new SqlCommand(command, conexion);
                int r = cmd.ExecuteNonQuery();
                Error = "";
                return r;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return -1;
            }
        }
        /// <summary>
        /// Ejecuta una consulta SQL sobre la base de datos (Select)
        /// </summary>
        /// <param name="consulta">Consulta SQL a ejecutar</param>
        /// <returns>Registros resultantes de la consulta.</returns>
        public SqlDataReader Consulta(string consulta)
        {
            try
            {
                Debug.Print($"====>{consulta}");
                SqlCommand cmd = new SqlCommand(consulta, conexion);
                SqlDataReader dataReader = cmd.ExecuteReader();
                Error = "";
                return dataReader;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// Cierra la conexión con la BD.
        /// </summary>
        ~SQLConection()
        {
            if (conexion.State == System.Data.ConnectionState.Open)
            {
                try
                {
                    conexion.Close();
                }
                catch (Exception ex)
                {
                    Error = ex.Message;
                }
            }
        }
    }
}
