using Microsoft.AspNetCore.Mvc;
using PatientManagement.Models;
using PatientManagement.Repository;

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
    }
}
