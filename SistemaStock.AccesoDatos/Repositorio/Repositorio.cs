using Microsoft.EntityFrameworkCore;
using SistemaStock.AccesoDatos.Data;
using SistemaStock.AccesoDatos.Repositorio.IRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SistemaStock.AccesoDatos.Repositorio
{

        public class Repositorio<T> : IRepositorio<T> where T : class
        {
            private readonly ApplicationDbContext _db;
            internal DbSet<T> dbSet;

            public Repositorio(ApplicationDbContext db) //seteando al objeto y poniendo como una prop de dbset
            {
                _db = db;
                this.dbSet = _db.Set<T>();
            }
            public async Task Add(T entidad)
            {
                await dbSet.AddAsync(entidad); //insertar en la tabla
            }

            public async Task<T> Get(int id)
            {
                return await dbSet.FindAsync(id); //select * from 
            }

            public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filtro = null, Func<IQueryable<T>,
                IOrderedQueryable<T>> orderBy = null, string incluirPropiedades = null, bool isTracking = true)
            {
                IQueryable<T> query = dbSet;
                if (filtro != null)
                {
                    query = query.Where(filtro);
                }
                if (incluirPropiedades != null)
                {
                    foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(incluirProp); //incluira categoria y marca al producto
                    }
                }
                if (orderBy != null)
                {
                    query = orderBy(query);
                }
                if (!isTracking)
                {
                    query = query.AsNoTracking();
                }

                return await query.ToListAsync();
            }

            public async Task<T> GetFirst(Expression<Func<T, bool>> filtro = null,
                string incluirPropiedades = null, bool isTracking = true)
            {
                IQueryable<T> query = dbSet;
                if (filtro != null)
                {
                    query = query.Where(filtro);
                }
                if (incluirPropiedades != null)
                {
                    foreach (var incluirProp in incluirPropiedades.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(incluirProp); //incluira categoria y marca al producto
                    }
                }

                if (!isTracking)
                {
                    query = query.AsNoTracking();
                }

                return await query.FirstOrDefaultAsync();
            }

            public void Remove(T entidad)
            {
                dbSet.Remove(entidad);
            }

            public void RemoveByRange(IEnumerable<T> entidad)
            {
                dbSet.RemoveRange(entidad);
            }
        }
    }

