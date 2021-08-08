using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prescriptions.DTO.Responses;
using Prescriptions.Models;
using Prescriptions.Repositories.Interfaces;

namespace Prescriptions.Repositories.Implementations
{
    public class PrescriptionDbRepository : IPrescriptionDbRepository
    {
        private readonly MedicamentsDbContext _context;
        
        public PrescriptionDbRepository(MedicamentsDbContext context)
        {
            _context = context;
        }
        
        public async Task<PrescriptionResponseDto> GetPrescription(int id)
        {
            var prescription = await _context.Prescriptions
                .Include(x => x.IdDoctorNavigation)
                .Include(x => x.IdPatientNavigation)
                .Include(x => x.PrescriptionMedicaments)
                .ThenInclude(x => x.IdMedicamentNavigation)
                .SingleOrDefaultAsync(x => x.IdPrescription.Equals(id));

            if (prescription == null)
            {
                throw new ArgumentException();
            }

            return new PrescriptionResponseDto()
            {
                Date = prescription.Date,
                DueDate = prescription.DueDate,
                PatientResponseDto = new PatientResponseDto()
                {
                    FirstName = prescription.IdPatientNavigation.FirstName,
                    LastName = prescription.IdPatientNavigation.LastName,
                    Birthdate = prescription.IdPatientNavigation.Birthdate
                },
                DoctorResponseDto = new DoctorResponseDto()
                {
                    FirstName = prescription.IdDoctorNavigation.FirstName,
                    LastName = prescription.IdDoctorNavigation.LastName,
                    Email = prescription.IdDoctorNavigation.Email
                },
                Medicaments = prescription.PrescriptionMedicaments.Select(x=>
                    new
                    {
                        Dose = x.Dose,
                        Details = x.Details,
                        Med = x.IdMedicamentNavigation
                    }).Select(x=>new MedicamentResponseDto()
                {
                    Name = x.Med.Name,
                    Description = x.Med.Description,
                    Type = x.Med.Type,
                    Dose = x.Dose,
                    Details = x.Details
                })
            };
        }
    }
}