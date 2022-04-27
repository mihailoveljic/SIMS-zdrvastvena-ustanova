﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service;
using Model;

namespace Controller
{
    public class RenovationAppointmentController
    {
        private readonly RenovationAppointmentService _renovationAppointmentService;

        public RenovationAppointmentController(RenovationAppointmentService renovationAppointmentService)
        {
            _renovationAppointmentService = renovationAppointmentService;
        }

        public IEnumerable<RenovationAppointment> GetAll()
        {
            return _renovationAppointmentService.GetAll();
        }
        public RenovationAppointment GetById(long id)
        {
            return _renovationAppointmentService.GetById(id);
        }
        public IEnumerable<RenovationAppointment> GetFromToDates(DateTime start, DateTime end)
        {
            return _renovationAppointmentService.GetFromToDates(start, end);
        }
        public IEnumerable<RenovationAppointment> GetIfContainsDateForRoom(DateTime date, long roomId)
        {
            return _renovationAppointmentService.GetIfContainsDateForRoom(date, roomId);
        }
        public IEnumerable<RenovationAppointment> GetIfContainsDate(DateTime date)
        {
            return _renovationAppointmentService.GetIfContainsDate(date);
        }
        public RenovationAppointment Create(RenovationAppointment renovationAppointment)
        {
            return _renovationAppointmentService.Create(renovationAppointment);
        }
        public bool Update(RenovationAppointment renovationAppointment)
        {
            return _renovationAppointmentService.Update(renovationAppointment);
        }
        public bool Delete(long renovationAppointmentId)
        {
            return _renovationAppointmentService.Delete(renovationAppointmentId);
        }
    }
}