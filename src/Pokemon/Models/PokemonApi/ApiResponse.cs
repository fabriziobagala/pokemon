namespace Pokemon.Models.PokemonApi
{
    public class ApiResponse
    {
        public string Name { get; }
        public string Description { get; }

        public ApiResponse(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
