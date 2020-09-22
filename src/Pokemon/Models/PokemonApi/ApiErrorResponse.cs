namespace Pokemon.Models.PokemonApi
{
    public class ApiErrorResponse : ApiResponse
    {
        public string Message { get; }

        public ApiErrorResponse(string name, string description, string message) : base(name, description)
        {
            Message = message;
        }
    }
}
