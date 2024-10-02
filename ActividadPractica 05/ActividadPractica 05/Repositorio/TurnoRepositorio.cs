using ActividadPractica_05.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadPractica_05.Repositorio
{
    public interface ITurnoRepositorio
    {
        bool AgregarTurno(T_TURNO turno);
        bool ModificarTurno(T_TURNO turno);
        bool EliminarTurno(int id);
        List<T_TURNO> ObtenerTurnos();
    }

    public class TurnoRepositorio : ITurnoRepositorio
    {
        private readonly db_turnosContext _context;
        public TurnoRepositorio(db_turnosContext db_TurnosContext)
        {
            _context = db_TurnosContext;
        }

        public bool AgregarTurno(T_TURNO turno)
        {
            _context.T_TURNOs.Add(turno);
            _context.SaveChanges();
            return true;
        }

        public bool EliminarTurno(int id)
        {
            _context.T_TURNOs.Remove(_context.T_TURNOs.Find(id));
            _context.SaveChanges();
            return true;
        }

        public bool ModificarTurno(T_TURNO turno)
        {
            _context.T_TURNOs.Update(turno);
            _context.SaveChanges();
            return true;
        }

        public List<T_TURNO> ObtenerTurnos()
        {
            return _context.T_TURNOs.ToList();
        }
    }
}
