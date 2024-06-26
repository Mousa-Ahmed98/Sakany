﻿using Sakany.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakany.Application.Interfaces
{
    public interface IGovernorateRepository
    {
        public List<Governorate> GetAll();
        public Governorate GetById(int governorateId);
    }
}
