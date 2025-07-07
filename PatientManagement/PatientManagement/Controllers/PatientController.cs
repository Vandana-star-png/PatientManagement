using Microsoft.AspNetCore.Mvc;
using PatientManagement.Models;
using PatientManagement.Repository;
using Microsoft.AspNetCore.JsonPatch;

namespace PatientManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var patients = await _patientRepository.GetAllPatientsAsync();

            if (!patients.Any() || patients == null)
            {
                return NotFound("No Patients found");
            }

            return Ok(patients);
        }

        [HttpGet("{id}", Name = "GetPatientById")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            if(id <= 0)
            {
                return BadRequest("Patient ID must be positive number");
            }

            var patient = await _patientRepository.GetPatientByIdAsync(id);

            if (patient == null)
            {
                return NotFound($"Patient with ID {id} not found");
            }

            return Ok(patient);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody]PatientRequest patientRequest)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Required patient data");
            }
            else if (await _patientRepository.IsPatientExistsAsync(patientRequest.Email))
            {
                return Conflict("patient with this email already exists");
            }
            else if (await _patientRepository.IsNumberExistsAsync(patientRequest.ContactNumber))
            {
                return Conflict("patient with this contact number already exists");
            }

            var id = await _patientRepository.AddPatientAsync(patientRequest);

            var patient = await _patientRepository.GetPatientByIdAsync(id);

            return CreatedAtRoute("GetPatientById", new { id = id }, patient);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] PatientRequest patientRequest)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Patient data is required");
            }
            else if (await _patientRepository.IsPatientExistsAsync(patientRequest.Email))
            {
                return Conflict("patient with this email already exists");
            }
            else if (await _patientRepository.IsNumberExistsAsync(patientRequest.ContactNumber))
            {
                return Conflict("patient with this contact number already exists");
            }

            var existingPatient = await _patientRepository.GetPatientByIdAsync(id);

            if (existingPatient == null)
            {
                return BadRequest($"Patient with ID {id} not found");
            }

            var updatedPatient = await _patientRepository.UpdatePatientAsync(existingPatient, patientRequest);

            return Ok(updatedPatient);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatchAsync([FromBody] JsonPatchDocument patientRequest, [FromRoute] int id)
        {
            if (patientRequest == null)
            {
                return BadRequest("Patch document is required");
            }

            var existingPatient = await _patientRepository.GetPatientByIdAsync(id);
            if (existingPatient == null)
            {
                return NotFound($"Patient with ID {id} not found");
            }

            var isUpdated = await _patientRepository.UpdatePatientPatchAsync(existingPatient, patientRequest);
            if (!isUpdated)
            {
                return StatusCode(500, "An error occurred while saving the updated patient data");
            }
            return Ok("Patient data updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            var existingPatient = await _patientRepository.GetPatientByIdAsync(id);
            if (existingPatient == null)
            {
                return BadRequest($"Patient with ID {id} not found");
            }

            var isDeleted = await _patientRepository.DeletePatientAsync(existingPatient);
            if (!isDeleted)
            {
                return BadRequest($"Failed to delete patient with ID {id}");
            }
            return Ok($"Patient with ID {id} deleted successfully");
        }
    }
}
