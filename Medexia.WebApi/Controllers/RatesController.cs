using Finetech.Application.User.Auth.Register;
using FluentValidation;
using Medexia.Application.Rates.Command.PatientRate;
using Medexia.Application.Rates.DTOs;
using Medexia.Application.Rates.Query.GetRates_of_any_doctor;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Medexia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IMediator meditr;

        public RatesController(IMediator meditr)
        {
            this.meditr = meditr;
        }

        [HttpPost("RatingAfterAppointment")]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult<string>> RatingAfterAppointment(PatientRateCommand command) 
        {
            try
            {
                var result = await meditr.Send(command);
             if (!result.IsSuccess) 
              {
                return BadRequest( new { issuccess = result.IsSuccess , message = result.Message });
              }
                return Ok(result);

            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors });
            }
            catch (IdentityValidationException ex)
            {
                return BadRequest(new {errors = ex.Errors});
            }
       
        }
        [HttpPost("GetDoctorsReview")]
        
        public async Task<ActionResult<DoctorReviewsDto>> GetDoctorsReview(GetRateQuery query) 
        {
            try
            {
                var result = await meditr.Send(query);
           
                return Ok(result);

            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errors });
            }
            catch (IdentityValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors });
            }




        }
    }
}
