namespace PatientManagement.Data
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string ContactNumber { get; set; }
        public float Weight { get; set; }
        public float Height { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string MedicalComments { get; set; }
        public bool AnyMedicationsTaking { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
