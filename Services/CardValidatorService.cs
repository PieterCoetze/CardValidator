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
            string cardProvider = cardNumber.CreditCardBrand().ToString();

            return cardProvider;
        }

        public bool ValidateCard(string cardNumber)
        {
            if (!double.TryParse(cardNumber, out _))
                return false;

            string cardProvider = cardNumber.CreditCardBrand().ToString();

            if (cardProvider.ToLower() == "unknown")
                return false;

            return true;
        }

        public bool CheckIfCardProviderValid(string cardNumber)
        {
            bool isValid =  _context.TCardProviders.Any(c => c.CardProviderName == GetCardProvider(cardNumber) && (c.Configured ?? false));

            return isValid;
        }
    }
}
