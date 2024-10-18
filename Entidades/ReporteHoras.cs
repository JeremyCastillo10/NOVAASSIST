using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NOVAASSIST.Entidades
{
    public class ReporteHoras
    {
        [NotMapped]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Cedula { get; set; }
        public string Genero { get; set; }
        public double SalarioPorHora { get; set; }
        public double TotalMes { get; set; }
        public double HorasTrabajadasMes { get; set; }
    }
}
