using CardValidator.Interfaces;

namespace CardValidator
{
    public class CardValidate : Validator
    {
        private List<Validator> _validators;

        public CardValidate(List<Validator> validators)
        {
            _validators = validators;
        }

        public override bool Validate()
        {
            bool isValid = _validators.All(v => v.Validate());
            return isValid;
        }
    }
}
