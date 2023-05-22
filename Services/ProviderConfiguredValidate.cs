using CardValidator.Interfaces;

namespace CardValidator.Services
{
    public class ProviderConfiguredValidate : Validator
    {
        private string _cardNumber;
        private readonly ICardValiditorService _cardValiditorService;
        HttpContext _httpContext;

        public ProviderConfiguredValidate(ICardValiditorService cardValiditorService, string cardNumber, HttpContext httpContext)
        {
            _cardValiditorService = cardValiditorService;
            _cardNumber = cardNumber;
            _httpContext = httpContext;
        }

        public override bool Validate()
        {
            if (!_cardValiditorService.CheckIfCardProviderValid(_cardNumber))
            {
                _httpContext.Session.SetString("Message", "Card is not part of enabled provider list, error");
                return false;
            }

            return true;
        }
    }
}
