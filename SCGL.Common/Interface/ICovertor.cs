﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lazarus.Common.Interface
{
    public interface ICovertor<T>
    {
        T ToModel();
    }
}