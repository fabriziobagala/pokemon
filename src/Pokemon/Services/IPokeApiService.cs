using System.Threading.Tasks;

namespace Pokemon.Services
{
    public interface IPokeApiService
    {
        Task<string> GetPokemonDescriptionByNameAsync(string name);
    }
}
