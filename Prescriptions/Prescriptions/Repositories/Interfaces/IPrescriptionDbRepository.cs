using System.Threading.Tasks;
using Prescriptions.DTO.Responses;

namespace Prescriptions.Repositories.Interfaces
{
    public interface IPrescriptionDbRepository
    {
        public Task<PrescriptionResponseDto> GetPrescription(int id);
    }
}