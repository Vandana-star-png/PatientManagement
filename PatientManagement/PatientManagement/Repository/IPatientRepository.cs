using PatientManagement.Data;
using PatientManagement.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace PatientManagement.Repository
{
    public interface IPatientRepository
    {
        Task<List<Patient>> GetAllPatientsAsync();

        Task<Patient> GetPatientByIdAsync(int id);

        Task<Patient> AddPatientAsync(PatientRequest patientRequest);

        Task<Patient> UpdatePatientAsync(Patient patient, PatientRequest patientRequest);

        Task<Patient> UpdatePatientPatchAsync(Patient patient, JsonPatchDocument patientRequest);

        Task<Patient> DeletePatientAsync(Patient patient);

        Task<bool> IsPatientExistsAsync(string email);

        Task<bool> IsNumberExistsAsync(string contactNumber);
    }
}