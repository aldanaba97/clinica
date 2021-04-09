using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Turnos.Models;

namespace Turnos.ViewModel
{
    public class ListadoenVista
    {
        public List<listado> listadoParaM { get; set;  }
        public List <turno> turnero { get; set; }
    }
}