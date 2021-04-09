using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Turnos.ViewModel;

namespace Turnos.Models
{
    public class ReporteVM
    {
        public List<montoabonado> montoA { get; set; }
        public List<Cantxturno> canT { get; set; }
    }
}