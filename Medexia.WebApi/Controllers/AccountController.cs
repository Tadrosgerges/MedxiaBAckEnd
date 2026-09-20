using Azure.Core;
using Finetech.Application.Auth.Login.query;
using Finetech.Application.User.Auth.Register;
using FluentValidation;
using Medexia.Application.Patients.Command.Edit_His_Own_Data;
using Medexia.Application.user.My_profile;
using Medexia.Application.user.Register.AsDoctor;
using Medexia.Application.user.Register.AsPatient;
using Medexia.Application.user.UploadProfilePicture;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Medexia.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator meditr;

        public AccountController(IMediator meditr)
        {
            this.meditr = meditr;
        }
        [HttpPost("DoctorRegstration")]
        public async Task< IActionResult>RegisterAsDoctor(RegisterAsDoctorCommand command) 
        {
            try
            {
                var result = await meditr.Send(command);

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
        [HttpPost("PatientRegstration")]
        public async Task<IActionResult> RegisterAsPatient(RegisterAsPatientCommand command)
        {
            try
            {
                var result = await meditr.Send(command);
                

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
        //[EnableRateLimiting("RateLimiter")]
        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginQuery request)
        {
            var result = await meditr.Send(request);
            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.Message });

            }
            return Ok(result);

        }
        [HttpPost("EditPatientInfo")]
        [Authorize (Roles = "Patient")]
        public async Task<IActionResult> EditPatientInfo(EditDataCommand command)
        {
            try
            {
                var result = await meditr.Send(command);
                if (!result.IsSuccess) 
                {
                    return BadRequest(new { message = result.Message });
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
        [HttpPost("UploadProfilePicture")]

        public async Task<IActionResult> UploadPhoto(UploadProfilePictureCommand command)
        {
            try
            {
                var result = await meditr.Send(command);
                if (!result.IsSuccess)
                {
                    return BadRequest(new { message = result.ErrorMessage });
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
        [HttpGet("MyProfileAsDoc")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> MyProfileAsDoctor([FromHeader] MyProfileAsDocQuerey Query)
        {
            try
            {
                var result = await meditr.Send(Query);
                if (!result.IsSuccess)
                {
                    return BadRequest(new { message = result.ErrorMessage });
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

        [HttpGet("MyProfileAsPatient")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> MyProfileAsPatient([FromHeader] MyProfileAsPatientQuery Query)
        {
            try
            {
                var result = await meditr.Send(Query);
                if (!result.IsSuccess)
                {
                    return BadRequest(new { message = result.ErrorMessage });
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
