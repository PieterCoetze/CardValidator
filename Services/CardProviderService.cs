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
            return await _context.TCardProviders.Where(c => c.CardProviderId == id).FirstOrDefaultAsync();
        }

        public async Task<CardProvider?> GetCardProvider(string providerName)
        {
            return await _context.TCardProviders.Where(c => c.CardProviderName == providerName).FirstOrDefaultAsync();
        }

        public async Task<ICollection<CardProvider>> GetCardProviders()
        {
            return await _context.TCardProviders.ToListAsync();
        }

        public async Task SetCardProviderConfiguration(int id, bool config)
        {
            var cardProvider = await GetCardProvider(id);
            string configuration = config ? "enabled" : "disabled";

            if (cardProvider == null)
            {
                _httpContext.HttpContext?.Session.SetString("Message", $"{cardProvider?.CardProviderName} {configuration}, error");
                return;
            }

            cardProvider.Configured = config;

            if(!(_context.SaveChanges() > 0))
                _httpContext.HttpContext?.Session.SetString("Message", $"{cardProvider.CardProviderName} {configuration}, error");

            _httpContext.HttpContext?.Session.SetString("Message", $"{cardProvider.CardProviderName} {configuration}, success");
        }
    }
}
