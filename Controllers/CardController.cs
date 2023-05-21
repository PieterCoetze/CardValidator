using CardValidator.Dto;
using CardValidator.Interfaces;
using CardValidator.Models;
using CardValidator.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CardValidator.Controllers
{
    public class CardController : Controller
    {
        private readonly ICardService _cardService;
        private readonly ICardValiditorService _cardValiditorService;
        private readonly ICardProviderService _cardProviderService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly List<Validator> _validators;

        public CardController(ICardService cardService,
                              ICardValiditorService cardValiditorService,
                              ICardProviderService cardProviderService,
                              IHttpContextAccessor httpContext)
        {
            _cardService = cardService;
            _cardValiditorService = cardValiditorService;
            _cardProviderService = cardProviderService;
            _httpContext = httpContext;
            _validators = new List<Validator>();
            _cardService.HttpContext = httpContext.HttpContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _cardService.GetCards());
        }

        public async Task<IActionResult> SaveCard(string cardNumber)
        {
            if (!Validate(cardNumber))
                return View("Index", await _cardService.GetCards()); 

            var cardProvider = await _cardProviderService.GetCardProvider(_cardValiditorService.GetCardProvider(cardNumber));

            await _cardService.SaveCard(new Dto.CardDTO
            {
                CardProviderId = cardProvider.CardProviderId,
                CardNumber = cardNumber,
            });

            return View("Index", await _cardService.GetCards());
        }

        public async Task<IActionResult> DeleteCard(int id)
        {
            await _cardService.DeleteCard(id);

            return View("Index", await _cardService.GetCards());
        }

        public bool Validate(string cardNumber)
        {
            _validators.Add(new CardNumberValidate(_cardValiditorService, cardNumber, _httpContext.HttpContext));
            _validators.Add(new ExistsValidate(_cardService, cardNumber, _httpContext.HttpContext));
            _validators.Add(new ProviderConfiguredValidate(_cardValiditorService, cardNumber, _httpContext.HttpContext));

            CardValidate cardValidator = new CardValidate(_validators);

            return cardValidator.Validate();
        }
    }
}
