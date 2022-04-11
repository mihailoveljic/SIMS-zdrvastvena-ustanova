using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Model.Enums;

namespace Service
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepository;
        private PatientService _patientService;
        private DoctorService _doctorService;
        private SecretaryService _secretaryService;
        private ManagerService _managerService;

        public AccountService(AccountRepository accountsRepository, PatientService patientService, DoctorService doctorService, SecretaryService secretaryService, ManagerService managerService)
        {
            _accountRepository = accountsRepository;
            _patientService = patientService;
            _doctorService = doctorService;
            _secretaryService = secretaryService;
            _managerService = managerService;
        }

        public Account Create(Account account)
        {
            return _accountRepository.Create(account);
            
        }

        public bool Delete(long accountId)
        {
            return _accountRepository.Delete(accountId);
        }

        public bool Update(Account account) 
        { 
               return _accountRepository.Update(account);
        }

        public Account GetById(long id)
        {
            var patients = _patientService.GetAll();
            var doctors = _doctorService.GetAll();
            var managers = _managerService.GetAll();
            var secretaries = _secretaryService.GetAll();
            var account = _accountRepository.GetById(id);
            
            BindPersonWithAccount(patients, doctors, managers, secretaries, account);
            return account;
        }

        public IEnumerable<Account> GetAll()
        {
            var patients = _patientService.GetAll();
            var doctors = _doctorService.GetAll();
            var managers = _managerService.GetAll();
            var secretaries = _secretaryService.GetAll();
            var accounts = _accountRepository.GetAll();
            foreach (Account acc in accounts)
            {
                BindPersonWithAccount(patients, doctors, managers, secretaries, acc);
            }
            return accounts;
        }

        public void BindPersonWithAccount(IEnumerable<Patient> patients, IEnumerable<Doctor> doctors, IEnumerable<Manager> managers, IEnumerable<Secretary> secretaries, Account account)
        {
            switch(account.AccountType)
            {
                case AccountType.PATIENT:
                    Patient patient = FindPatientById(patients, account.Person.Id);
                    account.Person = patient;
                    break;
                case AccountType.DOCTOR:
                    Doctor doctor = FindDoctorById(doctors, account.Person.Id);
                    account.Person = doctor;
                    break;
                case AccountType.MANAGER:
                    Manager manager = FindManagerById(managers, account.Person.Id);
                    account.Person = manager;
                    break;
                case AccountType.SECRETARY:
                    Secretary secretary = FindSecretaryById(secretaries, account.Person.Id);
                    account.Person = secretary;
                    break;
                default:
                    account.Person = null;
                    break;
            }
        }
        private Patient FindPatientById(IEnumerable<Patient> patients, long patientId)
        {
            return patients.SingleOrDefault(patient => patient.Id == patientId);
        }

        private Doctor FindDoctorById(IEnumerable<Doctor> doctors, long doctorId)
        {
            return doctors.SingleOrDefault(doctor => doctor.Id == doctorId);
        }

        private Manager FindManagerById(IEnumerable<Manager> managers, long managerId)
        {
            return managers.SingleOrDefault(manager => manager.Id == managerId);
        }
        private Secretary FindSecretaryById(IEnumerable<Secretary> secrearies, long secretaryId)
        {
            return secrearies.SingleOrDefault(secretary => secretary.Id == secretaryId);
        }


    }
}