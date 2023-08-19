using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using YouCode.Models.Context;
using YouCode.Models.Entity;
using YouCode.Services;


namespace YouCode.Controllers
{
    public class CodeEditorController : Controller
    {
        private readonly YouCodeContext _dbContext;
        private object context;

        public CodeEditorController(YouCodeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult RunCode(string code)
        {
            string res = CompilerService.RunUserCode(code);
            return Json(res);
        }


        public IActionResult SaveProject(string projectName, string htmlCode, string cssCode)
        {
            try
            {
                Models.Entity.Project project = new Models.Entity.Project() { ProjectName = projectName };
                var res = _dbContext.Projects.Add(project);

                // Check if the project was successfully added to the database
                if (res == null)
                {
                    return Json(new { Success = false, Message = "Failed to save project." });
                }

                // Lookup the HTML language in the database
                var htmlLanguage = _dbContext.Languages.FirstOrDefault(x => x.LanguageName == "HTML");

                // Check if the HTML language was found
                if (htmlLanguage == null)
                {
                    return Json(new { Success = false, Message = "HTML language not found." });
                }

                Code codeHtml = new Code()
                {
                    CodeText = htmlCode,
                    Date = DateTime.Now,
                    ProjectId = res.Entity.Id, // Use the Id from the added project entity
                    LanguageId = htmlLanguage.Id
                };

                // Lookup the CSS language in the database
                var cssLanguage = _dbContext.Languages.FirstOrDefault(x => x.LanguageName == "CSS");

                // Check if the CSS language was found
                if (cssLanguage == null)
                {
                    return Json(new { Success = false, Message = "CSS language not found." });
                }

                Code codeCss = new Code()
                {
                    CodeText = cssCode,
                    Date = DateTime.Now,
                    ProjectId = res.Entity.Id, // Use the Id from the added project entity
                    LanguageId = cssLanguage.Id
                };

                // Save changes to the database
                _dbContext.Codes.Add(codeHtml);
                _dbContext.Codes.Add(codeCss);
                _dbContext.SaveChanges();

                return Json(new { Success = true });
            }
            catch (Exception ex)
            {
                // Handle any other exceptions that might occur during the process
                // Log the exception or take appropriate action
                return Json(new { Success = false, Message = "An error occurred while saving the project." });
            }
        }


        public IActionResult JavascriptCode()
        {
            return View("JavascriptCode");
        }
    }
}