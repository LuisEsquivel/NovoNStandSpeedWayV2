namespace COMMON.Interfaces
{
    using System.Collections.Generic;
    using COMMON.Entidades;
    public interface IGenericRepository<T> where T : BaseDTO
    {
        string Error { get; }
        T Create(T entity);
        IEnumerable<T> Read { get; }
        T Update(T entity);
        bool Delete(string id);
        IEnumerable<T> Query(string querySql);
        T SearchById(string id);
        IEnumerable<T> InsertMany(IEnumerable<T> list);
        IEnumerable<T> UpdateMany(IEnumerable<T> list);
        IEnumerable<T> DeleteMany(IEnumerable<T> list);
    }
}