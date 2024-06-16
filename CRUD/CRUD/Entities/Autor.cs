using System.ComponentModel.DataAnnotations;

namespace CRUD.Entities
{
    public class Autor
    {
        [Key]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }

    }
}
