using System.Collections.Generic;
using System.Threading.Tasks;
using Prescriptions.Models;

namespace Prescriptions.Repositories.Interfaces
{
    public interface IDoctorDbRepository
    {
        public Task<ICollection<Doctor>> GetDoctorsFromDbAsync();
        public Task<Doctor> AddDoctorAsync(Doctor doctor);
        public Task<Doctor> UpdateDoctorAsync(int doctorId, Doctor doctor);
        public Task<Doctor> DeleteDoctorAsync(int doctorId);
    }
}