using CardValidator.Models;

namespace CardValidator.Interfaces
{
    public interface ICardProviderService
    {
        public HttpContext HttpContext { get; set; }
        Task SetCardProviderConfiguration(int id, bool config);
        Task<CardProvider?> GetCardProvider(int id);
        Task<CardProvider?> GetCardProvider(string providerName);
        Task<ICollection<CardProvider>> GetCardProviders();
    }
}
