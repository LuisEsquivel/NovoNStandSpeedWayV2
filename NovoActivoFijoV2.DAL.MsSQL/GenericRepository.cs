namespace DAL.MsSQL
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Data.SqlClient;
    using System.Reflection;
    using System.Linq;
    using FluentValidation;
    using FluentValidation.Results;
    using NovoActivoFijoV2.DAL.MsSQL;
    using COMMON.Interfaces;
    using COMMON.Entidades;

    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDTO
    {
        private readonly IDB db;
        private readonly AbstractValidator<T> validator;

        public GenericRepository(AbstractValidator<T> validator)
        {
            this.validator = validator;
            db = DB.GetInstance;
        }

        public string Error { get; private set; }

        public IEnumerable<T> Read
        {
            get
            {
                try
                {
                    var datos = Query($"Select * from {typeof(T).Name}");
                    Error = "";
                    return datos;
                }
                catch (Exception ex)
                {
                    Debug.Print($"==========> Error: {ex.Message}");
                    Error = "";
                    return null;
                }
            }
        }

        public T Create(T entity)
        {
            string id = Guid.NewGuid().ToString();
            entity.Id = id;
            entity.FechaAlta = DateTime.Now;
            entity.FechaModificacion = DateTime.Now;

            ValidationResult validationResult = validator.Validate(entity);
            if (validationResult.IsValid)
            {
                string sql1 = $"INSERT INTO {typeof(T).Name} (";
                string sql2 = ") VAlUES (";
                string sql3 = ");";
                var campos = typeof(T).GetProperties();
                T dato = (T)Activator.CreateInstance(typeof(T));
                Type tTipo = typeof(T);
                for (int i = campos.Length - 1; i >= 0; i--)
                {
                    var propiedad = tTipo.GetProperty(campos[i].Name);
                    var valor = propiedad.GetValue(entity);
                    if (valor == null)
                    {
                        continue;
                    }
                    sql1 += " " + campos[i].Name;
                    switch (propiedad.PropertyType.Name)
                    {
                        case "String":
                            sql2 += "'" + valor + "'";
                            break;
                        case "DateTime":
                            //2021-02-05 05:51:25
                            DateTime dateTime = (DateTime)valor;
                            sql2 += $"'{dateTime.Month}-{dateTime.Day}-{dateTime.Year} {dateTime.Hour}:{dateTime.Minute}:{dateTime.Second}'";
                            break;
                        case "Boolean":
                            sql2 += (bool)valor ? "1" : "0";
                            break;
                        default:
                            sql2 += " " + valor;
                            break;
                    }
                    if (i != 0)
                    {
                        sql1 += ", ";
                        sql2 += ", ";
                    }
                }
                if (db.Comando(sql1 + sql2 + sql3) == 1)
                {
                    Error = "";
                    return SearchById(id);
                }
                else
                {
                    Error = $"Error al construir el SQL: {db.Error}";
                    return null;
                }
            }
            else
            {
                Error = "Error de validación: ";
                foreach (var item in validationResult.Errors)
                {
                    Error += item.ErrorMessage + ". ";
                }
                return null;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                int r = db.Comando($"DELETE FROM {typeof(T).Name} WHERE Id='{id}';");
                if (r == 1)
                {
                    Error = "";
                    return true;
                }
                else
                {
                    Error = db.Error;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Error = ex.Message + ": " + db.Error;
                return false;
            }
        }

        public IEnumerable<T> DeleteMany(IEnumerable<T> list)
        {
            try
            {
                string error = "";
                List<T> rechazados = new List<T>();
                foreach (var item in list)
                {
                    if (Delete(item.Id))
                    {
                        rechazados.Add(item);
                        error += Error + "; ";
                    }
                }
                Error = error;
                return rechazados;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }
        /// <summary>
        /// Inserta varios elementos a la tabla
        /// </summary>
        /// <param name="list">Lista de elementos a insertar</param>
        /// <returns>Lista de elementos que NO fueron insertados</returns>
        public IEnumerable<T> InsertMany(IEnumerable<T> list)
        {
            try
            {
                string error = "";
                List<T> rechazados = new List<T>();
                foreach (var item in list)
                {
                    if (Create(item) == null)
                    {
                        rechazados.Add(item);
                        error += Error + "; ";
                    }
                }
                Error = error;
                return rechazados;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }

        }

        public IEnumerable<T> Query(string querySql)
        {
            try
            {
                SqlDataReader dataReader = (SqlDataReader)db.Consulta(querySql);
                List<T> datos = new List<T>();
                var campos = typeof(T).GetProperties();
                T dato;
                Type tTipo = typeof(T);
                while (dataReader.Read())
                {
                    dato = (T)Activator.CreateInstance(typeof(T));
                    for (int i = 0; i < campos.Length; i++)
                    {
                        int indice = campos.ToList().FindIndex(columna => columna.Name == dataReader.GetName(i));
                        PropertyInfo property = tTipo.GetProperty(campos[indice].Name);
                        if (dataReader[i] != DBNull.Value)
                        {
                            property.SetValue(dato, dataReader[i]);
                        }
                        else
                        {
                            property.SetValue(dato, null);
                        }
                    }
                    datos.Add(dato);
                }
                dataReader.Close();
                Error = "";
                return datos;
            }
            catch (Exception ex)
            {
                Error = ex.Message + ": " + db.Error;
                return null;
            }
        }

        public T SearchById(string id)
        {
            try
            {
                return Query($"SELECT * FROM {typeof(T).Name} WHERE Id='{id}';").SingleOrDefault();
            }
            catch (Exception ex)
            {
                Error = ex.Message + ": " + db.Error;
                return null;
            }
        }

        public T Update(T entity)
        {
            entity.FechaModificacion = DateTime.Now;
            ValidationResult validationResult = validator.Validate(entity);
            if (validationResult.IsValid)
            {
                string sql1 = $"UPDATE {typeof(T).Name} SET ";
                string sql2 = $" WHERE Id='{entity.Id}'";
                string sql = "";
                var campos = typeof(T).GetProperties();
                T dato = (T)Activator.CreateInstance(typeof(T));
                Type tTipo = typeof(T);
                for (int i = 0; i <= campos.Length - 1; i++)
                {
                    if (campos[i].Name == "Id")
                    {
                        continue;
                    }
                    var propidad = tTipo.GetProperty(campos[i].Name);
                    var valor = propidad.GetValue(entity);
                    if (valor != null)
                    {
                        sql += propidad.Name + "=";
                        switch (propidad.PropertyType.Name)
                        {
                            case "String":
                                sql += "'" + valor + "'";
                                break;
                            case "DateTime":
                                //2021-02-05 05:51:25
                                DateTime dateTime = (DateTime)valor;
                                sql += $"'{dateTime.Month}-{dateTime.Day}-{dateTime.Year} {dateTime.Hour}:{dateTime.Minute}:{dateTime.Second}'";
                                break;
                            case "Boolean":
                                sql += (bool)valor ? "1" : "0";
                                break;
                            default:
                                sql += " " + valor;
                                break;
                        }
                        if (i != campos.Length - 1)
                        {
                            sql += ",";
                        }

                        sql1 += sql;
                        sql = "";
                    }

                }
                char ultimo = sql1.Last();
                if (ultimo == ',')
                {
                    sql1 = sql1.Substring(0, sql1.Length - 1);
                }
                if (db.Comando(sql1 + " " + sql2) == 1)
                {
                    Error = "";
                    return SearchById(entity.Id);
                }
                else
                {
                    Error = $"Error al contruir el SQL: {db.Error}";
                    return null;
                }
            }
            else
            {
                Error = "Error de validación: ";
                foreach (var item in validationResult.Errors)
                {
                    Error += item.ErrorMessage + ". ";
                }
                return null;
            }
        }

        public IEnumerable<T> UpdateMany(IEnumerable<T> list)
        {
            try
            {
                string error = "";
                List<T> rechazados = new List<T>();
                foreach (var item in list)
                {
                    if (Update(item) == null)
                    {
                        rechazados.Add(item);
                        error += Error + "; ";
                    }
                }
                Error = error;
                return rechazados;
            }
            catch (Exception ex)
            {
                Error = ex.Message;
                return null;
            }
        }
    }
}