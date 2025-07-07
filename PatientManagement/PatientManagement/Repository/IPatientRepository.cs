using PatientManagement.Data;
using PatientManagement.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace PatientManagement.Repository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientsAsync();

        Task<Patient> GetPatientByIdAsync(int id);

        Task<int> AddPatientAsync(PatientRequest patientRequest);

        Task<Patient> UpdatePatientAsync(Patient patient, PatientRequest patientRequest);

        Task<bool> UpdatePatientPatchAsync(Patient patient, JsonPatchDocument patientRequest);

        Task<bool> DeletePatientAsync(Patient patient);

        Task<bool> IsPatientExistsAsync(string email);

        Task<bool> IsNumberExistsAsync(string contactNumber);
    }
}