using CardValidator.Interfaces;

namespace CardValidator
{
    public class CardNumberValidate : Validator
    {
        private readonly ICardValiditorService _cardValiditorService;
        private string _cardNumber;
        private readonly HttpContext _httpContext;

        public CardNumberValidate(ICardValiditorService cardValiditorService, string cardNumber, HttpContext httpContext)
        {
            _cardNumber = cardNumber;
            _httpContext = httpContext;
            _cardValiditorService = cardValiditorService;
        }

        public override bool Validate()
        {
            if (!_cardValiditorService.ValidateCard(_cardNumber))
            {
                _httpContext.Session.SetString("Message", "Invalid card number, error");
                return false;
            }

            return true;
        }
    }
}
