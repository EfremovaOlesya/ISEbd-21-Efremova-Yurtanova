using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyService.BindingModel
{
    public class ClientBindingModel
    {
        public int Id { get; set; }

        public string ClientFIO { get; set; }

        public string ClientLogin { get; set; }

        public string ClientPassword { get; set; }
    }
}
