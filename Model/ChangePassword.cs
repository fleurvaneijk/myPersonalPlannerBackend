﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyPersonalPlannerBackend.Model
{
    public class ChangePassword
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public ChangePassword()
        {

        }
    }
}
