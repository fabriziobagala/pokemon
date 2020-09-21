using System;
using System.Threading.Tasks;

namespace Pokemon.Services
{
    public class PokeApiService : IPokeApiService
    {
        public PokeApiService()
        {
        }

        public async Task<string> GetPokemonDescriptionByNameAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
