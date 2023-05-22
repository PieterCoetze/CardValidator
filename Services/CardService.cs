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
        private readonly IHttpContextAccessor _httpContext;

        public CardService(DataBaseContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<Card> GetCard(int id)
        {
            return await _context.TCards.Where(x => x.CardId == id).FirstOrDefaultAsync();
        }

        public bool CheckIfCardExists(string cardNumber)
        {
            return _context.TCards.Any(c => c.CardNumber == cardNumber);
        }
       
        public async Task<ICollection<Card>> GetCards()
        {
            return await _context.TCards.Include(c => c.CardProvider).ToListAsync();
        }

        public async Task SaveCard(CardDTO cardDTO)
        {
            Card card = new Card()
            {
                CardProviderId = cardDTO.CardProviderId,
                CardNumber = cardDTO.CardNumber,
                CreatedDate = DateTime.UtcNow
            };
            
            _context.TCards.Add(card);

            if (await _context.SaveChangesAsync() > 0)
                _httpContext.HttpContext?.Session.SetString("Message", "Card added, success");
            else
                _httpContext.HttpContext?.Session.SetString("Message", "Unable to add card, error, error");
        }

        public async Task DeleteCard(int id)
        {
            var card = await GetCard(id);

            _context.Entry(card).State = EntityState.Deleted;

            if(_context.SaveChanges() > 0)
                _httpContext.HttpContext?.Session.SetString("Message", "Card deleted, success");
            else
                _httpContext.HttpContext?.Session.SetString("Message", "Card could not be deleted, error");
        }
    }
}
