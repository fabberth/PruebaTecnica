using CRUD.DbContext;
using CRUD.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace CRUD.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly ApplicationDbContext dbContext;
        public AutorRepository(ApplicationDbContext _dbContext)
        {
            dbContext = _dbContext;
        }

        /// <summary>
        /// Método que crea un autor.
        /// </summary>
        public void Add(Autor autor)
        {
            dbContext.Autores.Add(autor);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Método que elimina un autor.
        /// </summary>
        public void Delete(Autor autor)
        {
            dbContext.Autores.Remove(autor);
            dbContext.SaveChanges();
        }

        /// <summary>
        /// Método que obtiene  todos los autores.
        /// </summary>
        public IQueryable<Autor> FindAll()
        {
            return dbContext.Autores.AsQueryable();
        }

        /// <summary>
        /// Método que obtiene autor por id.
        /// </summary>
        /// <param name="id">El identificador único del autor.</param>
        public Autor Get(int id)
        {
            return FindAll().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Método que actualiza un autor.
        /// </summary>
        public void Update(Autor autor)
        {
            var existingAutor = Get(autor.Id);
            dbContext.Entry(existingAutor).State = EntityState.Detached;
            dbContext.Entry(autor).State = EntityState.Modified;
            dbContext.SaveChanges();
        }
    }
}
