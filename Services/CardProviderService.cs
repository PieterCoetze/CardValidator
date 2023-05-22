using CardValidator.Interfaces;
using CardValidator.Models;
using CreditCardValidator;
using Microsoft.EntityFrameworkCore;

namespace CardValidator.Services
{
    public class CardProviderService : ICardProviderService
    {
        private readonly DataBaseContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CardProviderService(DataBaseContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<CardProvider?> GetCardProvider(int id)
        {
            var cardProvider = await _context.TCardProviders.Where(c => c.CardProviderId == id).FirstOrDefaultAsync();

            return cardProvider;
        }

        public async Task<CardProvider?> GetCardProvider(string providerName)
        {
            var cardProvider = await _context.TCardProviders.Where(c => c.CardProviderName == providerName).FirstOrDefaultAsync();

            return cardProvider;
        }

        public async Task<ICollection<CardProvider>> GetCardProviders()
        {
            var cardProviders = await _context.TCardProviders.ToListAsync();

            return cardProviders;
        }

        public async Task SetCardProviderConfiguration(int id, bool config)
        {
            var cardProvider = await GetCardProvider(id);
            string configuration = config ? "enabled" : "disabled";

            if (cardProvider == null)
            {
                _httpContext.HttpContext.Session.SetString("Message", $"{cardProvider.CardProviderName} {configuration}, error");
                return;
            }

            cardProvider.Configured = config;

            if(!(_context.SaveChanges() > 0))
                _httpContext.HttpContext.Session.SetString("Message", $"{cardProvider.CardProviderName} {configuration}, error");

            _httpContext.HttpContext.Session.SetString("Message", $"{cardProvider.CardProviderName} {configuration}, success");
        }
    }
}
