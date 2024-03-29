﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class RenovationTypeController
    {
        private readonly IRenovationTypeService _renovationTypeService;

        public RenovationTypeController(IRenovationTypeService renovationTypeService)
        {
            _renovationTypeService = renovationTypeService;
        }

        public IEnumerable<RenovationType> GetAll()
        {
            return _renovationTypeService.GetAll();
        }

        public RenovationType Create(RenovationType renovationType)
        {
            return _renovationTypeService.Create(renovationType);
        }
        public bool Update(RenovationType renovationType)
        {
            return _renovationTypeService.Update(renovationType);
        }
        public bool Delete(long renovationTypeId)
        {
            return _renovationTypeService.Delete(renovationTypeId);
        }
    }
}
