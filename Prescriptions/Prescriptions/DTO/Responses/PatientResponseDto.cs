using System;

namespace Prescriptions.DTO.Responses
{
    public class PatientResponseDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
    }
}