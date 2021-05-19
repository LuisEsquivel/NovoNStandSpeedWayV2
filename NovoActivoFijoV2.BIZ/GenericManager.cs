namespace BIZ
{

    using System.Collections.Generic;
    using COMMON.Entidades;
    using COMMON.Interfaces;
    public abstract class GenericManager<T> : IGenericManager<T> where T : BaseDTO
    {
         protected IGenericRepository<T> repository;

        public GenericManager(IGenericRepository<T> repository)
        {
            this.repository = repository;
        }


        public string Error => repository.Error;

        public IEnumerable<T> ObtenerTodos => repository.Read;

        public T Actualizar(T entity)
        {
            return repository.Update(entity);
        }

        public T BuscarPorId(string id)
        {
            return repository.SearchById(id);        }

        public bool Eliminar(string id)
        {
            return repository.Delete(id);
        }

        public T Insertar(T entity)
        {
          return  repository.Create(entity);
        }
    }
}
