﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Processors.Validators
{
    public interface IValidator
    {
        bool IsValid(object value);
    }
}
