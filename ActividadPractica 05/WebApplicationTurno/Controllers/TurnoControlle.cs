using ActividadPractica_05.Models;
using ActividadPractica_05.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace WebApplicationTurno.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoControlle : ControllerBase
    {
        private readonly ITurnoRepositorio _turnoRepositorio;
        public TurnoControlle(ITurnoRepositorio turnoRepositorio)
        {
            _turnoRepositorio = turnoRepositorio;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_turnoRepositorio.ObtenerTurnos());
        }
        [HttpPost]
        public IActionResult Post([FromBody] T_TURNO turno)
        {
            
            DateTime FechaMinima = DateTime.Now.AddDays(1);
            DateTime FechaMaxima = DateTime.Now.AddDays(45);
            
            if (DateTime.TryParse(turno.fecha, out DateTime fecha))
            {
                if(fecha >= FechaMinima && fecha <= FechaMaxima)
                {
                    var TurnoExistenstes = _turnoRepositorio.ObtenerTurnos();

                    if (TurnoExistenstes.Any(t => t.fecha == turno.fecha && t.hora == turno.hora))
                    {
                        return BadRequest("Ya existe un turno con la misma fecha y hora");
                    }

                    var servicios = new HashSet<int>();
                    foreach (var detalle in turno.Detalles)
                    {
                        if (!servicios.Add(detalle.id_servicio))
                        {
                            return BadRequest("No se pueden repetir los servicios");
                        }
                    }
                    if (turno.Detalles.Count == 0)
                    {
                        return BadRequest("El turno debe tener al menos un detalle");
                    }
                    if (_turnoRepositorio.AgregarTurno(turno))
                    {
                        return Ok("Se Agrego correctamente"+ " " +turno);
                    }
                    else
                    {
                        return BadRequest("Error al agregar el turno");
                    }
                }
                else
                {
                    return BadRequest("La fecha debe ser mayor a la fecha actual y menor a 45 dias");
                }
            }
            else
            {
                return BadRequest("La fecha no es valida");
            }
           

        }


    }
}
