using CRUD.Entities;
using CRUD.Repositories;
using CRUD.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace CRUD.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AutoresController : ControllerBase
    {
        private readonly IAutorRepository AutorRepository;
        public AutoresController(IAutorRepository autorRepository)
        {
            AutorRepository = autorRepository;
        }

        [HttpGet("GetById")]
        public IActionResult GetById(string id)
        {
            if(!int.TryParse(id, out int rId))
                return NotFound(new { Message = "parámetro \"id\" debe ser número entero" });

            var autor = AutorRepository.Get(rId);

            if(autor == null)
                return NotFound(new { Message = $"Autor no encontrado con el id: {rId}" });

            return Ok(autor);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var autorList = new List<Autor>();

            try
            {
                var list = AutorRepository.FindAll();

                if (list != null)
                {
                    autorList = list.ToList();
                }
            }
            catch (Exception e)
            {
            }

            return Ok(autorList);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Autor autor)
        {
            try
            {
                if (autor == null)
                {
                    return Ok(new ResponseData(false, $"Cuerpo de la petición no encontrada"));
                }

                if (AutorRepository.Get(autor.Id) != null)
                {
                    return Ok(new ResponseData(false, $"Parámetro \"id\" no disponible"));
                }

                if (string.IsNullOrEmpty(autor.Nombre))
                {
                    return Ok(new ResponseData(false, $"Parametro \"Nombre\" es obligatorio"));
                }

                if (autor.Nombre.Length > 100)
                {
                    return Ok(new ResponseData(false, $"Parametro \"Nombre\" supero el tamaño maximo (100)"));
                }

                DateTime minDate = new DateTime(1753, 1, 1);
                DateTime maxDate = new DateTime(9999, 12, 31);

                if (minDate >= autor.FechaNacimiento)
                {
                    return Ok(new ResponseData(false, $"Parametro \"FechaNacimiento\" no es correcto (menor)"));
                }

                if (maxDate <= autor.FechaNacimiento)
                {
                    return Ok(new ResponseData(false, $"Parametro \"FechaNacimiento\" no es correcto (mayor)"));
                }

                AutorRepository.Add(autor);

                return Ok(new ResponseData(true, $"Autor guardado correctamente"));

            }
            catch (Exception e)
            {
                return Ok(new ResponseData(false, $"{e.Message}"));
            }

        }

        [HttpPut("Update")]
        public IActionResult Update([FromBody] Autor autor)
        {
            try
            {
                if (autor == null)
                {
                    return Ok(new ResponseData(false, $"Cuerpo de la petición no encontrada"));
                }

                var autorDb = AutorRepository.Get(autor.Id);

                if (autorDb == null)
                {
                    return Ok(new ResponseData(false, $"No se encontró registro para actualizar"));
                }

                autorDb.Nombre = string.IsNullOrEmpty(autor.Nombre) ? autorDb.Nombre : autor.Nombre;
                autorDb.FechaNacimiento = autor.FechaNacimiento;

                if (string.IsNullOrEmpty(autor.Nombre))
                {
                    return Ok(new ResponseData(false, $"Parametro \"Nombre\" es obligatorio"));
                }

                if (autor.Nombre.Length > 100)
                {
                    return Ok(new ResponseData(false, $"Parametro \"Nombre\" supero el tamaño maximo (100)"));
                }

                DateTime minDate = new DateTime(1753, 1, 1);
                DateTime maxDate = new DateTime(9999, 12, 31);

                if (minDate >= autor.FechaNacimiento)
                {
                    return Ok(new ResponseData(false, $"Parametro \"FechaNacimiento\" no es correcto (menor)"));
                }

                if (maxDate <= autor.FechaNacimiento)
                {
                    return Ok(new ResponseData(false, $"Parametro \"FechaNacimiento\" no es correcto (mayor)"));
                }

                AutorRepository.Update(autor);

                return Ok(new ResponseData(true, $"Autor actualizado correctamente"));

            }
            catch (Exception e)
            {
                return Ok(new ResponseData(false, $"{e.Message}"));
            }
        }

        [HttpDelete("Delete")]
        public IActionResult Delete(string id)
        {
            if (!int.TryParse(id, out int rId))
                return NotFound(new { Message = "parámetro \"id\" debe ser número entero" });

            var autor = AutorRepository.Get(rId);

            if (autor == null)
                return NotFound(new { Message = $"Autor no encontrado con el id: {rId}" });

            try
            {
                AutorRepository.Delete(autor);
                return Ok(new ResponseData(true, $"Autor eliminado correctamente"));
            }
            catch (Exception e)
            {
                // Es posible que No se elimine porque tiene Libros asociados.
                return Ok(new ResponseData(false, $"Verifique si tiene libros asociados, {e.Message}"));
            }
        }
    }
}
