using System;
using System.Collections;
using System.Collections.Generic;
using Prescriptions.Models;

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