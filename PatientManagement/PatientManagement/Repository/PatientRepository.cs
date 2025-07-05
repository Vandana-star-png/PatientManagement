using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Data;
using PatientManagement.Models;

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

        public async Task<int> AddPatientAsync(PatientRequest patientRequest)
        {
            var patient = _mapper.Map<Patient>(patientRequest);
            patient.CreatedDate = DateTime.Now;

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return patient.Id;
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
