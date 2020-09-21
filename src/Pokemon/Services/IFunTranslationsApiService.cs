using System.Threading.Tasks;

namespace Pokemon.Services
{
    public interface IFunTranslationsApiService
    {
        Task<string> GetShakespeareTranslationAsync(string description);
    }
}
