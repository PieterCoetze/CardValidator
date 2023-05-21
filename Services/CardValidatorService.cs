using CardValidator.Interfaces;
using CreditCardValidator;

namespace CardValidator.Services
{
    public class CardValidatorService : ICardValiditorService
    {
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
    }
}
