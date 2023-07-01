using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.AccesoDatos.Repositorio.IRepositorio
{
    public interface IRepositorio<T> where T : class
    {
        Task<T> Get(int id);
        Task<IEnumerable<T>> GetAll(
            Expression<Func<T, bool>> filtro = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string incluirPropiedades = null, //enlaces con otros objetos
            bool isTracking = true  //Cuando se pase en false  es cuando trabajamos con un objeto y lo queremos modificar

            );
        Task<T> GetFirst(
            Expression<Func<T, bool>> filtro = null,
            string incluirPropiedades = null, //enlaces con otros objetos
            bool isTracking = true  //Cuando se pase en false  es cuando trabajamos con un objeto y lo queremos modificar)
            );

        Task Add(T entidad);
        void Remove(T entidad);
        void RemoveByRange(IEnumerable<T> entidad);
    }
}
