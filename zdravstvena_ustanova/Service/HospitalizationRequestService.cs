﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using zdravstvena_ustanova.Repository;
using Model;
using Repository;
using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service
{
    public class HospitalizationRequestService
    {
        private readonly HospitalizationRequestRepository _hospitalizationRequestRepository;

        public HospitalizationRequestService(HospitalizationRequestRepository hospitalizationRequestRepository)
        {
            _hospitalizationRequestRepository = hospitalizationRequestRepository;
        }

        public IEnumerable<HospitalizationRequest> GetAll()
        {
            return _hospitalizationRequestRepository.GetAll();
        }
        public HospitalizationRequest GetById(long id)
        {
            return _hospitalizationRequestRepository.GetById(id);
        }

        public HospitalizationRequest Create(HospitalizationRequest hospitalizationRequest)
        {
            return _hospitalizationRequestRepository.Create(hospitalizationRequest);
        }
        public bool Update(HospitalizationRequest hospitalizationRequest)
        {
            return _hospitalizationRequestRepository.Update(hospitalizationRequest);
        }
        public bool Delete(long id)
        {
            return _hospitalizationRequestRepository.Delete(id);
        }
    }
}