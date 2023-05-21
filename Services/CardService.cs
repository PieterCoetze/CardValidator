using CardValidator.Dto;
using CardValidator.Interfaces;
using CardValidator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace CardValidator.Services
{
    public class CardService : ICardService
    {
        private readonly DataBaseContext _context;

        public HttpContext HttpContext { get; set; }

        public CardService(DataBaseContext context, HttpContext httpContext = null)
        {
            _context = context;
            HttpContext = httpContext;
        }

        public async Task<Card> GetCard(int id)
        {
            var card = await _context.TCards.Where(x => x.CardId == id).FirstOrDefaultAsync();

            return card;
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

            if (_context.SaveChanges() > 0)
            {
                HttpContext.Session.SetString("Message", "Card added, success");
                return true;
            }
            else
            {
                HttpContext.Session.SetString("Message", "Unable to add card, error, error");
                return false;
            }
        }

        public async Task<bool> DeleteCard(int id)
        {
            var card = await GetCard(id);

            _context.Entry(card).State = EntityState.Deleted;

            if(_context.SaveChanges() > 0)
            {
                HttpContext.Session.SetString("Message", "Card deleted, success");
                return true;
            }
            else
            {
                HttpContext.Session.SetString("Message", "Card could not be deleted, error");
                return false;
            }
        }
    }
}
