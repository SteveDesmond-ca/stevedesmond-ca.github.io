using System;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Web.Models;

namespace Web.Controllers
{
    public sealed class ContactController : Controller
    {
        private readonly ICache _cache;

        public ContactController(ICache cache)
        {
            _cache = cache;
        }

        [HttpGet("/work-with-steve")]
        public IActionResult Index()
        {
            ViewBag.Subtitle = "Work With Steve";
            return View();
        } 

        [HttpPost("/work-with-steve")]
        public IActionResult Submit(ContactForm form)
        {
            if (!ModelState.IsValid)
                return View("Index", form);

            var emailMessage = new MimeMessage();

            var address = new MailboxAddress(Settings.Title, Settings.EmailFromAndTo);
            emailMessage.From.Add(address);
            emailMessage.To.Add(address);
            emailMessage.Subject = Settings.EmailSubject;
            var message = "Name: " + form.Name + Environment.NewLine
                + "Company: " + form.Company + Environment.NewLine
                + form.PreferredMethod + ": " + form.Email + form.Phone + Environment.NewLine
                + "Message:" + Environment.NewLine
                + form.Message;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                client.Connect(Settings.EmailServer, 465);
                client.Authenticate(Settings.EmailUser, _cache.Config["EmailPassword"]);
                client.Send(emailMessage);
                client.Disconnect(true);
            }
            return View("Thanks");
        }
    }
}