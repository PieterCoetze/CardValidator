using CardValidator.Dto;
using CardValidator.Interfaces;
using CardValidator.Models;
using Microsoft.EntityFrameworkCore;

namespace CardValidator.Services
{
    public class CardService : ICardService
    {
        private readonly DataBaseContext _context;

        public CardService(DataBaseContext context)
        {
            _context = context;
        }

        public bool CheckIfCardExists(string cardNumber)
        {
            return _context.TCards.Any(c => c.CardNumber == cardNumber);
        }

        public async Task<ICollection<Card>> GetCards()
        {
            var cards = await _context.TCards.Include(c => c.CardProvider).ToListAsync();

            return cards;
        }

        public async Task<bool> SaveCard(CardDTO cardDTO)
        {
            Card card = new Card()
            {
                CardProviderId = cardDTO.CardProviderId,
                CardNumber = cardDTO.CardNumber,
                CreatedDate = DateTime.UtcNow
            };
            
            _context.TCards.Add(card);

            return _context.SaveChanges() > 0;
        }
    }
}
