using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models.ViewModels;
using SalesWebMvc.Services;
using SalesWebMvc.Services.Exceptions;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SalesWebMvc.Controllers
{
    public class AdviceController : Controller
    {
        private readonly AdviceService _adviceService;

        public AdviceController(AdviceService adviceService)
        {
            _adviceService = adviceService;
        }
        
        
        public async Task<IActionResult> Index()
        {
            try
            {
                var slip = await _adviceService.GetAdvice();                
                var advice = new AdviceFormViewModel(slip.Advice);
                var viewModel = advice.Advices;
                return View(viewModel);
            }
            catch(HttpClientException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }
        }

        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }
    }
}