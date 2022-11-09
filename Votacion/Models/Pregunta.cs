using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votacion.Models
{
    public class Pregunta
    {
        public string Descripcion { get; set; } = "";
        public string Respuesta1 { get; set; } = "";
        public string Respuesta2 { get; set; } = "";
        public string Respuesta3 { get; set; } = "";
    }
}
