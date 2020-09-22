﻿using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokemon.Constants;
using Pokemon.Models.PokemonApi;
using Pokemon.Services;

namespace Pokemon.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        private readonly IPokeApiService _pokeApiService;
        private readonly IFunTranslationsApiService _funTranslationsApiService;

        public PokemonController(IPokeApiService pokeApiService, IFunTranslationsApiService funTranslationsApiService)
        {
            _pokeApiService = pokeApiService;
            _funTranslationsApiService = funTranslationsApiService;
        }

        // GET pokemon
        [HttpGet]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult Get()
        {
            return Ok(ControllerMessage.ALL_OK);
        }

        // GET pokemon/{name}
        [HttpGet("{name}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status503ServiceUnavailable)]
        public async Task<ActionResult> Get(string name)
        {
            // Gets the description of the Pokemon from its name.
            var description = await _pokeApiService.GetPokemonDescriptionByNameAsync(name);

            if (string.IsNullOrWhiteSpace(description))
            {
                return NotFound(new ApiErrorResponse(name, string.Empty, ControllerMessage.POKEMON_NOT_FOUND));
            }

            // Replaces the control characters with whitespace.
            description = new string(description.Select(c => char.IsControl(c) ? ' ' : c).ToArray());

            // Gets the Shakespearean description from the Pokemon description.
            var translatedDescription = await _funTranslationsApiService.GetShakespeareTranslationAsync(description);

            if (string.IsNullOrWhiteSpace(translatedDescription))
            {
                return StatusCode(
                    StatusCodes.Status503ServiceUnavailable,
                    new ApiErrorResponse(name, description, ControllerMessage.FUN_TRANSLATIONS_UNAVAILABLE));
            }

            return Ok(new ApiResponse(name, translatedDescription));
        }
    }
}
