using CRUD.Entities;

namespace CRUD.Repositories
{
    public interface IAutorRepository
    {
        /// <summary>
        /// Método que obtiene  todos los autores.
        /// </summary>
        IQueryable<Autor> FindAll();

        /// <summary>
        /// Método que obtiene autor por id.
        /// </summary>
        /// <param name="id">El identificador único del autor.</param>
        Autor Get(int id);

        /// <summary>
        /// Método que crea un autor.
        /// </summary>
        void Add(Autor autor);

        /// <summary>
        /// Método que actualiza un autor.
        /// </summary>
        void Update(Autor autor);

        /// <summary>
        /// Método que elimina un autor.
        /// </summary>
        void Delete(Autor autor);
    }
}
