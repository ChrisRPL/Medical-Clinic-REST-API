using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prescriptions.Repositories.Interfaces;

namespace Prescriptions.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]
    [Authorize]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionDbRepository _prescriptionDbRepository;

        public PrescriptionsController(IPrescriptionDbRepository prescriptionDbRepository)
        {
            _prescriptionDbRepository = prescriptionDbRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrescription([FromRoute] int id)
        {
            try
            {
                var result = await _prescriptionDbRepository.GetPrescription(id);

                if (result == null) return BadRequest();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return Conflict("Prescription with that id does not exist!");
            }
        }
    }
}