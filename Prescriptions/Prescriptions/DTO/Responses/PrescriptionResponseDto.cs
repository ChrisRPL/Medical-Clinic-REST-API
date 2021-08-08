using System;
using System.Collections.Generic;

namespace Prescriptions.DTO.Responses
{
    public class PrescriptionResponseDto
    {
        public DateTime Date { get; set; }
        public DateTime DueDate { get; set; }
        public PatientResponseDto PatientResponseDto { get; set; }
        public DoctorResponseDto DoctorResponseDto { get; set; }
        public IEnumerable<MedicamentResponseDto> Medicaments { get; set; }
    }
}