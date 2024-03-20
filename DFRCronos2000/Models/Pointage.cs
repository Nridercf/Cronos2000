using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFRCronos2000.Models
{
    internal class Pointage
    {
        public int IdPointage { get; set; }
        public int IdUtil { get; set; }
        public DateTime DateHeureArrivee { get; set; }
        public DateTime DateHeureSortie { get; set; }
    }
}
