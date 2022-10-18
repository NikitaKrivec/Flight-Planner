﻿using FlightPlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightPlanner.Core.Validations
{
    public interface IAirportValidator
    {
        bool IsValid(Airport airport);
    }
}
