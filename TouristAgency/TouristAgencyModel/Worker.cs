using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyModel
{
    public class Worker
    {
        public int Id { get; set; }

        [Required]
        public string WorkerFIO { get; set; }

        [Required]
        public string WorkerLogin { get; set; }

        [Required]
        public string WorkerPassword { get; set; }

        [ForeignKey("WorkerId")]
        public virtual List<Order> Orders { get; set; }

    }
}
