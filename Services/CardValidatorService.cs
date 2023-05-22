using CardValidator.Interfaces;
using CardValidator.Models;
using CreditCardValidator;
using Microsoft.EntityFrameworkCore;

namespace CardValidator.Services
{
    public class CardValidatorService : ICardValiditorService
    {
        private readonly DataBaseContext _context;

        public CardValidatorService(DataBaseContext context)
        {
            _context = context;
        }

        public string GetCardProvider(string cardNumber)
        {
            return cardNumber.CreditCardBrand().ToString();
        }

        public bool ValidateCard(string cardNumber)
        {
            if (!double.TryParse(cardNumber, out _))
                return false;
            else if (cardNumber.CreditCardBrand().ToString().ToLower() == "unknown")
                return false;

            return true;
        }

        public bool CheckIfCardProviderValid(string cardNumber)
        {
            return _context.TCardProviders.Any(c => c.CardProviderName == GetCardProvider(cardNumber) && (c.Configured ?? false));
        }
    }
}
