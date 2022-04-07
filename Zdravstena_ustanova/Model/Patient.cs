using System;

namespace Model
{
    public class Patient : Person
    {
        public int InsuranceNumber { get; set; }
        public Enums.BloodType BloodType { get; set; }
        public Enums.EmploymentStatus EmploymentStatus { get; set; }

        public Patient(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, long accountId, int insuranceNumber, Enums.BloodType bloodType, Enums.EmploymentStatus employmentStatus) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, account, accountId)
        {
            InsuranceNumber = insuranceNumber;
            BloodType = bloodType;
            EmploymentStatus = employmentStatus;
        }

        public Patient(string name, string surname, string phoneNumber, string email, DateTime dateOfBirth, Address address, Account account, long accountId, int insuranceNumber, Enums.BloodType bloodType, Enums.EmploymentStatus employmentStatus) : base(name, surname, phoneNumber, email, dateOfBirth, address, account, accountId)
        {
            InsuranceNumber = insuranceNumber;
            BloodType = bloodType;
            EmploymentStatus = employmentStatus;
        }

        public Patient(string name, string surname, long id, string phoneNumber, string email, DateTime dateOfBirth, Address address, long accountId, int insuranceNumber, Enums.BloodType bloodType, Enums.EmploymentStatus employmentStatus) : base(name, surname, id, phoneNumber, email, dateOfBirth, address, accountId)
        {
            InsuranceNumber = insuranceNumber;
            BloodType = bloodType;
            EmploymentStatus = employmentStatus;
        }
    }
}