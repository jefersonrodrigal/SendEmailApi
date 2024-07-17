using Microsoft.AspNetCore.Mvc;
using SendEmailApi.Services;
using SendEmailApi.ViewModels;
using System.Text.RegularExpressions;

namespace SendEmailApi.Controllers
{
    [ApiController]
    public class SendMailController : ControllerBase
    {
        [HttpPost("/enviar")]
        public async Task<IActionResult> SendEmail([FromServices] SendEmailService sendEmail, [FromBody] EmailViewModel request)
        {
            List<string> emails = new List<string>();

            foreach (var email in request.EmailsTo)
            {
                if (Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    emails.Add(email);
                }

            }
            try
            {
                await sendEmail.SendEmail(request.From, emails, request.Subject, request.Message);
                return Ok();
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
             
        }
    }
}
