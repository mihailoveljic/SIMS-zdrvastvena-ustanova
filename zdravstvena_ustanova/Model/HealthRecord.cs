using Model.Enums;
using System;
using System.Collections.Generic;

namespace Model
{
   public class HealthRecord
   {
        public long Id { get; set; }
        public int InsuranceNumber { get; set; }
        public BloodType BloodType { get; set; }
        public EmploymentStatus EmploymentStatus { get; set; }

        public List<Allergens> Allergens { get; set; }

        public List<Anamnesis> Anamnesis { get; set; }

        public List<LabAnalysisRecord> LabAnalysisRecord { get; set; }

        public List<HospitalizationRecord> HospitalizationRecord { get; set; }

        public List<PrescribedMedicine> PrescribedMedicine { get; set; }
        public List<PatientDisease> PatientDisease { get; set; }
        public List<PatientVaccination> PatientVaccination { get; set; }

        public Patient Patient { get; set; }

        public HealthRecord(long id, int insuranceNumber, BloodType bloodType, EmploymentStatus employmentStatus, Patient patient)
        {
            Id = id;
            InsuranceNumber = insuranceNumber;
            BloodType = bloodType;
            EmploymentStatus = employmentStatus;
            Allergens = new List<Allergens>();
            Anamnesis = new List<Anamnesis>();
            LabAnalysisRecord = new List<LabAnalysisRecord>();
            HospitalizationRecord = new List<HospitalizationRecord>();
            PrescribedMedicine = new List<PrescribedMedicine>();
            PatientDisease = new List<PatientDisease>();
            PatientVaccination = new List<PatientVaccination>();
            Patient = patient;
        }
    }
}