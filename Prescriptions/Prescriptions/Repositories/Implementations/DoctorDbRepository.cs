using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Prescriptions.Models;
using Prescriptions.Repositories.Interfaces;

namespace Prescriptions.Repositories.Implementations
{
    public class DoctorDbRepository : IDoctorDbRepository
    {
        private readonly MedicamentsDbContext _context;

        public DoctorDbRepository(MedicamentsDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Doctor>> GetDoctorsFromDbAsync()
        {
            var res = await _context.Doctors.ToListAsync();
            return res;
        }

        public async Task<Doctor> AddDoctorAsync(Doctor doctor)
        {
            var idExists = _context.Doctors.Any(dr => dr.IdDoctor.Equals(doctor.IdDoctor));
            if (idExists) throw new ArgumentException();

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<Doctor> UpdateDoctorAsync(int doctorId, Doctor doctor)
        {
            doctor.IdDoctor = doctorId;

            var idExists = await _context.Doctors.AnyAsync(dr => dr.IdDoctor.Equals(doctorId));
            if (!idExists) throw new ArgumentException();

            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }

        public async Task<Doctor> DeleteDoctorAsync(int doctorId)
        {
            var idExists = await _context.Doctors.AnyAsync(dr => dr.IdDoctor.Equals(doctorId));
            if (!idExists) throw new ArgumentException();

            var doctor = await _context.Doctors.Where(dr => dr.IdDoctor.Equals(doctorId)).FirstAsync();

            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();

            return doctor;
        }
    }
}