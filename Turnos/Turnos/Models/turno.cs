using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;



namespace Turnos.Models
{
    public class turno
    {
        public int id { get; set; }
        [Required]
public string nombre { get; set; }
        [Range(1, 100)]
        [Required(ErrorMessage = "Por favor, introduce la edad")]
        public int edad { get; set; }
        
        [Required(ErrorMessage = "Por favor, introduce la fecha")]
        public DateTime fecha { get; set;  }
        [Required(ErrorMessage = "Por favor, seleccione una prestacion")]
        public int presta { get; set; }
        public string Descripcion { get; set; }
    }
}