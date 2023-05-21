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
        
        public CardController(ICardService cardService, 
                              ICardValiditorService cardValiditorService, 
                              ICardProviderService cardProviderService)
        {
            _cardService = cardService;
            _cardValiditorService = cardValiditorService;
            _cardProviderService = cardProviderService;
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

            bool saved = await _cardService.SaveCard(new Dto.CardDTO
            {
                CardProviderId = cardProvider.CardProviderId,
                CardNumber = cardNumber,
            });

            if (saved) 
                ViewBag.Message = "Card added, success";
            else
                ViewBag.Message = "Unable to add card, error";

            return View("Index", await _cardService.GetCards());
        }

        public bool Validate(string cardNumber)
        {
            if (!_cardValiditorService.ValidateCard(cardNumber))
            {
                ViewBag.Message = "Invalid card number, error";
                return false;
            }

            if (_cardService.CheckIfCardExists(cardNumber))
            {
                ViewBag.Message = "Card already exists, error";
                return false;
            }

            return true;
        }
    }
}
