using CardValidator.Interfaces;
using CardValidator.Models;
using CreditCardValidator;
using Microsoft.EntityFrameworkCore;

namespace CardValidator.Services
{
    public class CardProviderService : ICardProviderService
    {
        private readonly DataBaseContext _context;

        public CardProviderService(DataBaseContext context)
        {
            _context = context;
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

        public async Task<bool> SetCardProviderConfiguration(int id, bool config)
        {
            var cardProvider = await GetCardProvider(id);

            if (cardProvider == null)
                return false;

            cardProvider.Configured = config;

            return _context.SaveChanges() > 0;
        }
    }
}
