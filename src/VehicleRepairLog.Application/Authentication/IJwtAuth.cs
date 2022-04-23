﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleRepairLog.Infrastructure.Entities;

namespace VehicleRepairLog.Application.Authentication
{
    public interface IJwtAuth
    {
        string GenerateToken(User user);
    }
}
