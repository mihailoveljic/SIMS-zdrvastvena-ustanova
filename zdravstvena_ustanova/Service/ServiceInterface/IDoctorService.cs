﻿using zdravstvena_ustanova.Model;

namespace zdravstvena_ustanova.Service.ServiceInterface;

public interface IDoctorService : IService<Doctor>
{
    Doctor GetDoctorByShift(int hour);
    Doctor GetDoctorByNameSurname(string nameSurname);
}