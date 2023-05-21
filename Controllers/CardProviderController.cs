using CardValidator.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CardValidator.Controllers
{
    public class CardProviderController : Controller
    {
        private readonly ICardProviderService _cardProviderService;

        public CardProviderController(ICardProviderService cardProviderService)
        {
            _cardProviderService = cardProviderService;
        }

        public async Task<IActionResult> Index()
        {
            var cardProviders = await _cardProviderService.GetCardProviders();

            List<SelectListItem> cardProviderDropDown = cardProviders.Select(c => new SelectListItem
            {
                Value = c.CardProviderId.ToString(),
                Text = c.CardProviderName
            }).ToList();

            ViewBag.CardProviders = cardProviderDropDown;

            return View();
        }
    }
}
