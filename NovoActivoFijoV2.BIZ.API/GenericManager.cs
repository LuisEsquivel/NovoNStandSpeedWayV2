

using COMMON.Entidades;
using COMMON.Interfaces;
using System.Collections.Generic;

namespace BIZ.API
{
    public abstract class GenericManager<T> : IGenericManager<T> where T : BaseDTO
    {
        protected readonly RestService<T> restService;
        public GenericManager()
        {
#if DEBUG
            restService = new RestService<T>(CoreResources.BaseUri);
#else
            restService = new RestService<T>(RecursosBIZAPI.APIBaseAdressRelease);
#endif
        }
        public string Error { get; protected set; }

        public IEnumerable<T> ObtenerTodos => restService.ObtenerTodos();

        public T Actualizar(T entity) => restService.Actualizar(entity);

        public T BuscarPorId(string id) => restService.BuscarPorId(id);

        public bool Eliminar(string id) => restService.Eliminar(id);

        public T Insertar(T entity) => restService.Insertar(entity);
    }
}
