﻿using System;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Model;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class MedicationTypeService : IMedicationTypeService
    {
        private readonly IMedicationTypeRepository _medicationTypeRepository;

        public MedicationTypeService(IMedicationTypeRepository medicationTypeRepository)
        {
            _medicationTypeRepository = medicationTypeRepository;
        }

        public IEnumerable<MedicationType> GetAll()
        {
            return _medicationTypeRepository.GetAll();
        }

        public MedicationType FindIngredientById(IEnumerable<MedicationType> medicationTypes, long id)
        {
            return medicationTypes.SingleOrDefault(medicationType => medicationType.Id == id);
        }

        public MedicationType Create(MedicationType medicationType)
        {
            return _medicationTypeRepository.Create(medicationType);
        }
        public bool Update(MedicationType medicationType)
        {
            return _medicationTypeRepository.Update(medicationType);
        }
        public bool Delete(long medicationId)
        {
            return _medicationTypeRepository.Delete(medicationId);
        }

        public MedicationType Get(long id)
        {
            throw new NotImplementedException();
        }
    }
}
