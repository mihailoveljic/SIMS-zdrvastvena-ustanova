﻿using zdravstvena_ustanova.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using zdravstvena_ustanova.Repository.RepositoryInterface;
using zdravstvena_ustanova.Service.ServiceInterface;

namespace zdravstvena_ustanova.Service
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseRepository _diseaseRepository;

        public DiseaseService(IDiseaseRepository diseaseRepository)
        {
            _diseaseRepository = diseaseRepository;
        }

        public IEnumerable<Disease> GetAll()
        {
            return _diseaseRepository.GetAll();
        }

        public Disease FindDiseaseById(IEnumerable<Disease> diseases, long id)
        {
            return diseases.SingleOrDefault(disease => disease.Id == id);
        }

        public Disease Create(Disease disease)
        {
            return _diseaseRepository.Create(disease);
        }
        public bool Update(Disease disease)
        {
            return _diseaseRepository.Update(disease);
        }
        public bool Delete(long diseaseId)
        {
            return _diseaseRepository.Delete(diseaseId);
        }

        public Disease Get(long id)
        {
            throw new NotImplementedException();
        }
    }
}