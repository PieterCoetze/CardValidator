using CreditCardValidator;

namespace CardValidator.Interfaces
{
    public interface ICardValiditorService
    {
        bool ValidateCard(string cardNumber);
        string GetCardProvider(string cardNumber);
        bool CheckIfCardProviderValid(string cardProvider);
    }
}
