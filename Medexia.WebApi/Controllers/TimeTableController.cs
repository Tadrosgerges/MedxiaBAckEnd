using Finetech.Application.User.Auth.Register;
using FluentValidation;
using Medexia.Application.Appointment.Delete_An_Appointment;
using Medexia.Application.Patient.Book_An_Appointment;
using Medexia.Application.Timetable.Command.Delete_a_Timetable;
using Medexia.Application.Timetable.Command.SetTimeTable;
using Medexia.Application.Timetable.Query.GetAllDoctorsTimetable;
using Medexia.Application.user.Register.AsPatient;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Medexia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeTableController : ControllerBase
    {
        private readonly IMediator meditr;

        public TimeTableController(IMediator meditr)
        {
            this.meditr = meditr;
        }
        [HttpPost("SetTime")]
        [Authorize(Roles = "Doctor")]

        public async Task<IActionResult> SetTime(SetTimeTableQuery query)
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
        [HttpPost("GetAllTimeTables")]
        [Authorize(Roles = "Patient")]

        public async Task<IActionResult> GetTimeTable()
        {
            try
            {
                var result = await meditr.Send(new GetAllTimeTableQuery ());

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
      
        [HttpDelete("Delete")]
        [Authorize(Roles ="Doctor")]
        public async Task<IActionResult> deleteAnAppointment(DeleteTimeTableQuerey Queury)
        {
            try
            {
                var result = await meditr.Send(Queury);
               if (!result.IsSuccess) 
                {
                return BadRequest( new { Message = result.Message });
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
