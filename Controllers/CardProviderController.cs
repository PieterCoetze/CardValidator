using CardValidator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardValidator.Controllers
{
    public class CardProviderController : Controller
    {
        private readonly ICardProviderService _cardProviderService;
        private readonly IHttpContextAccessor _httpContext;

        public CardProviderController(ICardProviderService cardProviderService, IHttpContextAccessor httpContext)
        {
            _cardProviderService = cardProviderService;
            _httpContext = httpContext;
            _cardProviderService.HttpContext = httpContext.HttpContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _cardProviderService.GetCardProviders());
        }

        public async Task<IActionResult> EnableProvider(int id ,bool enable)
        {
            await _cardProviderService.SetCardProviderConfiguration(id, enable);

            return View("Index", await _cardProviderService.GetCardProviders());
        }
    }
}
