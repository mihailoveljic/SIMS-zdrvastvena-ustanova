﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Service;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Controller
{
    public class AllergensController
    {
        private readonly IAllergensService _allergenService;

        public AllergensController(IAllergensService allergensService)
        {
            _allergenService = allergensService;
        }

        public IEnumerable<Allergens> GetAll()
        {
            return _allergenService.GetAll();
        }
        public Allergens GetById(long id)
        {
            return _allergenService.Get(id);
        }
        public Allergens Create(Allergens allergen)
        {
            return _allergenService.Create(allergen);
        }
        public bool Update(Allergens allergen)
        {
            return _allergenService.Update(allergen);
        }
        public bool Delete(long allergenId)
        {
            return _allergenService.Delete(allergenId);
        }
    }
}
