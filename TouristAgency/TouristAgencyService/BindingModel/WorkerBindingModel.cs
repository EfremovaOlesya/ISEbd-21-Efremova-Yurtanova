﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristAgencyService.BindingModel
{
    public class WorkerBindingModel
    {
        public int Id { get; set; }

        public string WorkerFIO { get; set; }

        public string WorkerLogin { get; set; }

        public string WorkerPassword { get; set; }
    }
}
