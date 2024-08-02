using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Comun
{
    public class Repositorio<T> where T : class
    {
        DbContext db;
        protected DbSet<T> entidad;
        string[] incluir;
        public Repositorio(DbContext db)
        {
            this.db = db;
            entidad = this.db.Set<T>();
            incluir = null;
        }

        public Repositorio(DbContext db, string[] incluir)
        {
            this.db = db;
            entidad = this.db.Set<T>();
            this.incluir = incluir;
        }

        public T Crear(T objeto)
        {
            return entidad.Add(objeto);
        }

        public IEnumerable<T> CrearMuchos(IEnumerable<T> objeto)
        {
            return entidad.AddRange(objeto);
        }

        public T Obtener(int id)
        {
            return entidad.Find(id);
        }

        public List<T> Todos()
        {
            IQueryable<T> query = entidad.AsQueryable<T>();
            query = IncluirPropiedadesNavegacion(query);
            return query.ToList();
        }

        public IQueryable<T> TodosQueryable()
        {
            IQueryable<T> query = entidad.AsQueryable<T>();
            query = IncluirPropiedadesNavegacion(query);
            return query;
        }

        public List<T> Algunos(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = entidad.AsQueryable<T>();
            query = IncluirPropiedadesNavegacion(query);
            return query.Where(predicate).ToList();
        }

        public List<T> Algunos(Expression<Func<T, bool>> predicate, int cantidad)
        {
            IQueryable<T> query = entidad.AsQueryable<T>();
            query = IncluirPropiedadesNavegacion(query);
            return query.Where(predicate).Take(cantidad).ToList();
        }

        public IQueryable<T> AlgunosQueryable(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> query = entidad.AsQueryable<T>();
            query = IncluirPropiedadesNavegacion(query);
            return query.Where(predicate);
        }

        public IQueryable<T> AlgunosQueryable(Expression<Func<T, bool>> predicate, int cantidad)
        {
            IQueryable<T> query = entidad.AsQueryable<T>();
            query = IncluirPropiedadesNavegacion(query);
            return query.Where(predicate).Take(cantidad);
        }

        public IQueryable<T> AlgunosQueryable(string command)
        {
            IQueryable<T> query = db.Database.SqlQuery<T>(command).AsQueryable();
            query = IncluirPropiedadesNavegacion(query);
            return query;
        }

        public IQueryable<T> AlgunosQueryable(string command, params object[] parametros)
        {
            return db.Database.SqlQuery<T>(command, parametros).AsQueryable();
        }

        public T Modificar(T objeto)
        {
            T dato = entidad.Attach(objeto);
            db.Entry<T>(objeto).State = EntityState.Modified;
            return dato;
        }

        public T Eliminar(T objeto)
        {
            return entidad.Remove(objeto);
        }

        public IEnumerable<T> EliminarMuchos(IEnumerable<T> objeto)
        {
            return entidad.RemoveRange(objeto);
        }

        private IQueryable<T> IncluirPropiedadesNavegacion(IQueryable<T> query)
        {
            if (incluir != null)
            {
                foreach (string navprop in incluir)
                {
                    query = query.Include(navprop);
                }
            }

            return query;
        }

        public bool Existe(Expression<Func<T, bool>> predicate)
        {
            return entidad.Any(predicate);
        }

        public void ModificarMuchos(IEnumerable<T> objeto)
        {
            objeto.ToList().ForEach(o =>
            {
                T dato = entidad.Attach(o);
                db.Entry<T>(o).State = EntityState.Modified;
            });
        }
    }
}
