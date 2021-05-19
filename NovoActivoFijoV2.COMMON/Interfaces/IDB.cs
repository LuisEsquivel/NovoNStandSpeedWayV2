namespace COMMON.Interfaces
{
    using System;

    /// <summary>
    /// Interface para implemtar la conexión a la base de datos
    /// </summary>
    public interface IDB : IDisposable
        {
            /// <summary>
            /// Obtiene el mensaje cuando se genera un error con la tracción de la BD.
            /// </summary>
            string Error { get; }
            /// <summary>
            /// Ejecuta un comando SQL sobre la base de datos (Insert, Update, Delete)
            /// </summary>
            /// <param name="command">Comando SQL a ejecutar</param>
            /// <returns>El número de filas afectdas, -1 cuando ha ocurrido un error.</returns>
            int Comando(string command);
            /// <summary>
            /// Ejecuta una consulta SQL sobre la base de datos (Select)
            /// </summary>
            /// <param name="query">Consulta SQL</param>
            /// <returns>Registro resultantes de la consulta</returns>
            object Consulta(string query);
        }
}
