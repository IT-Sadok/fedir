using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace booking_imitation_n_layer.BussinesLogic.Models.Domain
{
    internal class Room
    {
        public int Id { get; set; }
        public List<DateOnly> BookedDates { get; set; } = new List<DateOnly>();
    }
}
