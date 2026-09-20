using System.Linq.Expressions;
using Finetech.Application.User.Auth.Register;
using FluentValidation;
using Medexia.Application.Appointments.MyAppointments;
using Medexia.Application.Patients.Query.FolowTheQueue;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medexia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IMediator meditr;

        public PatientController(IMediator meditr)
        {
            this.meditr = meditr;
        }


        [HttpPost("FollowMyQueue")]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult> FollowMyQueue(FollowTheQueueQuery query)
        {
            try
            {
                var result = await meditr.Send(query);
                if (!result.IsSuccess) 
                {
                return BadRequest(new { IsSuccess = result.IsSuccess, Message = result.ErrorMessage }); 
                
                }
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                var errors =  ex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new {errorMessage =  errors });
            }
            catch (IdentityValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors });
            }
        }
        [HttpPost("ShowMyAppointments")]
        [Authorize(Roles = "Patient")]
        public async Task<ActionResult> ShowMyAppointments(ShowMyAppointmentsQuery query)
        {
            try
            {
                var result = await meditr.Send(query);
                if (!result.IsSuccess)
                {
                    return BadRequest(new { IsSuccess = result.IsSuccess, Message = result.ErrorMessage });

                }
                return Ok(result);
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors.Select(e => e.ErrorMessage).ToList();
                return BadRequest(new { errorMessage = errors });
            }
            catch (IdentityValidationException ex)
            {
                return BadRequest(new { errors = ex.Errors });
            }
        }

    }
}
