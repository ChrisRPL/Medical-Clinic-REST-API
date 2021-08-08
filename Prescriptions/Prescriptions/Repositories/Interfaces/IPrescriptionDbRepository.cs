using System.Threading.Tasks;
using Prescriptions.DTO.Responses;
using Prescriptions.Models;

namespace Prescriptions.Repositories.Interfaces
{
    public interface IPrescriptionDbRepository
    {
        public Task<PrescriptionResponseDto> GetPrescription(int id);
    }
}