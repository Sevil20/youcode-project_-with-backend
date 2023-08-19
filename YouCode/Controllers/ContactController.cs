using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using YouCode.Models.Context;
using YouCode.Models.Entity;
using System;
using System.Net;
using System.Net.Mail;

namespace YouCode.Controllers
{
    public class ContactController : Controller
    {
        private readonly YouCodeContext _context;

        public ContactController(YouCodeContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SendMessage(Contact contact)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Index", contact);
                }

                // E-posta gönderme işlemi
                string recipient = contact.Email;
                string subject = contact.Subject;
                string message = contact.Message;

                if (string.IsNullOrEmpty(recipient) || !IsValidEmail(recipient))
                {
                    TempData["isError"] = "Invalid recipient email address";
                    return RedirectToAction("Index");
                }
                else
                {
                    SmtpClient client = new SmtpClient();
                    client.Host = "smtp.gmail.com";
                    client.Port = 587;
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential("sevilmirzyeva010@gmail.com", "vlgjaqimltazltvb");

                    MailMessage emailMessage = new MailMessage();
                    emailMessage.From = new MailAddress("sevilmirzyeva010@gmail.com");
                    emailMessage.To.Add(recipient);
                    emailMessage.Subject = subject;
                    emailMessage.Body = message;
                    try
                    {
                        client.Send(emailMessage);
                    }
                    catch (Exception ex)
                    {
                        TempData["isError"] = $"An error occurred while sending the email: {ex.Message}";
                        return RedirectToAction("Index");
                    }
                }

                // Veritabanına kaydetme işlemi
                _context.Contacts.Add(contact);
                _context.SaveChanges();

                TempData["isSuccess"] = "Message sent successfully!";
                return RedirectToAction("Index");
            }
            catch 
            {
                TempData["isError"] = "An error occurred while processing your request.";
                return RedirectToAction("Index");
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
