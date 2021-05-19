namespace NovoActivoFijoV2.DAL.MsSQL
{
    using System;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using COMMON.Interfaces;
    using global::DAL.MsSQL.CoreResource;


    public class DB : IDB
    {
        private static SqlConnection conexion;

        public string Error { get; private set; }

        private DB()
        {
            try
            {
                conexion = new SqlConnection(CoreResources.ConectionStringLocal); //solo para depuración 
                //conexion = new SqlConnection(Core.CoreResources.ConectionStringLocal); //Cadena de conexión solo en producción

                conexion.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        private static IDB db;

        public static IDB GetInstance
        {
            get
            {
                if (db == null)
                {
                    db = new DB();
                }
                return db;
            }
        }

        public int Comando(string command)
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
                Debug.Print($"============>{command}");
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

        public object Consulta(string query)
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                }
                Debug.Print($"============>{query}");
                SqlCommand cmd = new SqlCommand(query, conexion);
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

        private bool isDisposed;

        public void Dispose()
        {
            if (!isDisposed)
            {
                isDisposed = true;
                GC.SuppressFinalize(this);
            }
        }

        ~DB()
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
