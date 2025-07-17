using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Data;
using PatientManagement.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace PatientManagement.Repository
{
    public class PatientRepository : IPatientRepository
    {
        private readonly PatientDbContext _context;
        private readonly IMapper _mapper;

        public PatientRepository(PatientDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<Patient>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients.ToListAsync();
            return patients;
        }

        public async Task<Patient> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            return patient;
        }

        public async Task<Patient> AddPatientAsync(PatientRequest patientRequest)
        {
            var patient = _mapper.Map<Patient>(patientRequest);
            patient.CreatedDate = DateTime.Now;

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<Patient> UpdatePatientAsync(Patient patient, PatientRequest patientRequest)
        {
            _mapper.Map(patientRequest, patient);

            patient.UpdatedDate = DateTime.Now; 

            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdatePatientPatchAsync(Patient patient, JsonPatchDocument patientRequest)
        {
            patient.UpdatedDate = DateTime.Now;

            patientRequest.ApplyTo(patient);

            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> DeletePatientAsync(Patient patient)
        {
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<bool> IsPatientExistsAsync(string email)
        {
            var isEmailExists = await _context.Patients.AnyAsync(e => e.Email == email);
            return isEmailExists;
        }

        public async Task<bool> IsNumberExistsAsync(string contactNumber)
        {
            var isContactNumberExists = await _context.Patients.AnyAsync(e => e.ContactNumber == contactNumber);
            return isContactNumberExists;
        }
    }
}
