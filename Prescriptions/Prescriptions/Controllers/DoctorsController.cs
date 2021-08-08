using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Prescriptions.Models;
using Prescriptions.Repositories.Interfaces;

namespace Prescriptions.Controllers
{
    [ApiController]
    [Route("api/doctors")]
    [Authorize]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorDbRepository _doctorDbRepository;

        public DoctorsController(IDoctorDbRepository doctorDbRepository)
        {
            _doctorDbRepository = doctorDbRepository;
        }


        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {
            var result = await _doctorDbRepository.GetDoctorsFromDbAsync();

            if (result == null) return BadRequest();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDoctor([FromBody] Doctor doctor)
        {
            try
            {
                var result = await _doctorDbRepository.AddDoctorAsync(doctor);

                if (result == null) return BadRequest();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return Conflict("Doctor with that id already exists!");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDoctor([FromRoute] int id, [FromBody] Doctor doctor)
        {
            try
            {
                var result = await _doctorDbRepository.UpdateDoctorAsync(id, doctor);

                if (result == null) return BadRequest();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound("Doctor with that id does not exist!");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDoctor([FromRoute] int id)
        {
            try
            {
                var result = await _doctorDbRepository.DeleteDoctorAsync(id);

                if (result == null) return BadRequest();

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound("Doctor with that id does not exist!");
            }
        }
    }
}