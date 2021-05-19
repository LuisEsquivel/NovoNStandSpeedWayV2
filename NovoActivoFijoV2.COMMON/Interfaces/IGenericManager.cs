namespace COMMON.Interfaces
{   
   using System.Collections.Generic;
   using COMMON.Entidades;
    public interface IGenericManager<T> where T: BaseDTO
    {
        string Error { get; }
        T Insertar(T entity);
        IEnumerable<T> ObtenerTodos { get; }
        T Actualizar(T entity);
        bool Eliminar(string id);
        T BuscarPorId(string id);       
    }
}
