using CardValidator.Interfaces;

namespace CardValidator
{
    public class ExistsValidate : Validator
    {
        private string _cardNumber;
        private readonly ICardService _cardService;
        private readonly HttpContext _httpContext;

        public ExistsValidate(ICardService cardService, string cardNumber, HttpContext httpContext)
        {
            _cardService = cardService;
            _cardNumber = cardNumber;
            _httpContext = httpContext;
        }

        public override bool Validate()
        {
            if (_cardService.CheckIfCardExists(_cardNumber))
            {
                _httpContext.Session.SetString("Message", "Card already exists, error");
                return false;
            }

            return true;
        }
    }
}
