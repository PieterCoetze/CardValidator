using CardValidator.Dto;
using CardValidator.Models;

namespace CardValidator.Interfaces
{
    public interface ICardService
    {
        Task<ICollection<Card>> GetCards();
        Task<Card> GetCard(int id);
        Task<bool> SaveCard(CardDTO cardDTO);
        bool CheckIfCardExists(string cardNumber);
        Task<bool> DeleteCard(int id);
    }
}
