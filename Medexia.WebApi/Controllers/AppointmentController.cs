using Finetech.Application.User.Auth.Register;
using FluentValidation;
using Medexia.Application.Appointment.Delete_An_Appointment;
using Medexia.Application.Patient.Book_An_Appointment;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medexia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator meditr;

        public AppointmentController(IMediator meditr)
        {
            this.meditr = meditr;
        }

        [HttpPost("Book An Appointment")]
        [Authorize(Roles = "Patient")]

        public async Task<IActionResult> Appointment(BookAppointmentCommand command)
        {
            try
            {
                var result = await meditr.Send(command);
                if (!result.IsSuccess)
                {
                    return BadRequest(new
                    {
                        IsSuccess = result.IsSuccess
                        ,
                        message = result.Message
                    });
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
                return BadRequest(new { errors = ex.Errors });
            }
        }
        [HttpDelete("delete An Appointment")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> deleteAnAppointment(DeleteAnAppiontmentCommand command)
        {
            try
            {
                var result = await meditr.Send(command);
                if (!result.IsSuccess)
                {
                    return BadRequest(new { Message = result.Message });
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
                return BadRequest(new { errors = ex.Errors });
            }



        }
    }
}
