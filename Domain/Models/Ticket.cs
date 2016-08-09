using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
   public class Ticket 
    {
        public int Id { get; set; }
        public string FestivalName { get; set; }
        public int? Duration { get; set; }
    }
}
