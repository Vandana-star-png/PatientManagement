using PatientManagement.Data;
using PatientManagement.Models;

namespace PatientManagement.Repository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientsAsync();

        Task<Patient> GetPatientByIdAsync(int id);

        Task<int> AddPatientAsync(PatientRequest patientRequest);

        Task<bool> IsPatientExistsAsync(string email);

        Task<bool> IsNumberExistsAsync(string contactNumber);
    }
}