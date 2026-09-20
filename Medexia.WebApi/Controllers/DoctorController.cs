using Finetech.Application.User.Auth.Register;
using FluentValidation;
using Medexia.Application.ClinicSetup.Command.AddClinic;
using Medexia.Application.ClinicSetup.Command.DeleteClinic;
using Medexia.Application.ClinicSetup.Query.GetClinics_of_Doctor;
using Medexia.Application.Doctors.Command.Cancel_a_week_Timetable;
using Medexia.Application.Doctors.Command.Edit_Doctor_Data;
using Medexia.Application.Doctors.Command.InitializeQueue;
using Medexia.Application.Doctors.Command.Next_Patient;
using Medexia.Application.Doctors.Command.Start_a_week_TimeTable;
using Medexia.Application.Doctors.Query.Follow_the_que;
using Medexia.Application.Doctors.Query.GetAll_myTimeTablePatient;
using Medexia.Application.Doctors.Query.GetAllMyTimeTables;
using Medexia.Application.Doctors.Query.My_Rates;
using Medexia.Application.Patient.Book_An_Appointment;
using Medexia.Application.Timetable.Query.GetAllDoctorsTimetable;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Medexia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IMediator meditr;

        public DoctorController(IMediator meditr)
        {
            this.meditr = meditr;
        }

        [HttpPost("IntializeQueue")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> IntializeQueue(InitializeQueueFromAppointmentsCommand command)
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
              [HttpPost("EditDoctorData")]
            [Authorize(Roles = "Doctor")]
            public async Task<IActionResult> EditDoctorData(EditDoctorDataCommand command)
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
        [HttpPost("FollowTheQueue")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> FollowTheQueue(DrFollow_The_queueCommand command)
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
                        message = result.ErrorMessage
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
        [HttpPost("Next Patient")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> NextPatient(NextPatientCommand command)
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
        [HttpGet("GetAllMyTimeTables")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetmyTimetables([FromHeader]GetAllMyTimeTablesQuery query)
        {
            try
            {
                var result = await meditr.Send(query);
                if (!result.IsSuccess)
                {
                    return BadRequest(new
                    {
                        IsSuccess = result.IsSuccess
                        ,
                        message = result.ErrorMessage
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

        [HttpPost("Cancel a Week TimeTable")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> cancelTimetable(CancelADayCommand command)
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


        [HttpPost("Start A Week TimeTable")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> StartWeektimeTable(StartAweekTimeTableCommand command)
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
        [HttpPost("My Rate")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> MyRate(MyRatesQuery Query)
        {
            try
            {
                var result = await meditr.Send(Query);
                if (!result.IsSuccess)
                {
                    return BadRequest(new
                    {
                        IsSuccess = result.IsSuccess
                        ,
                        message = result.Value
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
        [HttpPost("MyTimeTablePatients")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> MyTimeTablePatients(GetAllMyTimeTablePatientQuery query)
        {
            try
            {
                var result = await meditr.Send(query);
                if (!result.IsSuccess)
                {
                    return BadRequest(new
                    {
                        IsSuccess = result.IsSuccess
                        ,
                        message = result.ErrorMessage
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
        [HttpPost("Add Clinic")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddClinic(AddClinicCommand command)
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
        [HttpDelete("Delete Clinic")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> DeleteClinic(DeleteClinicCommand command)
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
        [HttpGet("GetAllMyClinics")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> GetMyClinics()
        {
            try
            {
                var result = await meditr.Send(new GetDoctorClinicsQuery());
                if (!result.IsSuccess)
                {
                    return BadRequest(new
                    {
                        IsSuccess = result.IsSuccess
                        ,
                        message = result.ErrorMessage
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
    }
}
