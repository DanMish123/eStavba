using Microsoft.AspNetCore.Mvc;
using eStavba.Data;
using eStavba.Models;
using Microsoft.AspNetCore.Authorization;

namespace eStavba.Controllers
{
    public class ReportProblemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReportProblemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult SubmitProblem(ReportProblemModel model)
        {
            if (ModelState.IsValid)
            {
                _context.ReportedProblems.Add(model);
                _context.SaveChanges();

                ViewBag.Message = "We will get back to you shortly...";
                return View("Confirmation");
            }

            return View("Index", model);
        }
    }
}
