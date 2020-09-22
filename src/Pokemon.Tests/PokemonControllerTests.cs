using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Pokemon.Constants;
using Pokemon.Controllers;
using Pokemon.Models.PokemonApi;
using Pokemon.Services;

namespace Pokemon.Tests
{
    public class PokemonControllerTests
    {
        private const string POKEMON_NAME = "charizard";
        private const string POKEMON_DESCRIPTION = @"Lorem ipsum dolor sit amet,\nconsectetur adipiscing\felit";
        private const string POKEMON_TRANSLATED_DESCRIPTION = "Lorem ipsum dolor sit amet, consectetur adipiscing elit";
        private const string NON_EXISTENT_POKEMON_NAME = "foo";

        private PokemonController pokemonController;
        private Mock<IPokeApiService> pokeApiServiceMock;
        private Mock<IFunTranslationsApiService> funTranslationsApiServiceMock;

        [SetUp]
        public void Setup()
        {
            pokeApiServiceMock = new Mock<IPokeApiService>();
            funTranslationsApiServiceMock = new Mock<IFunTranslationsApiService>();

            pokemonController = new PokemonController(pokeApiServiceMock.Object, funTranslationsApiServiceMock.Object);
        }

        [Test]
        public void Get_ReturnsOkObjectResult()
        {
            // Act
            var actionResult = pokemonController.Get();
            var okObjectResult = (OkObjectResult)actionResult;
            var apiResponse = (string)okObjectResult.Value;

            // Assert
            Assert.AreEqual(okObjectResult.StatusCode, 200);
            Assert.AreEqual(apiResponse, ControllerMessage.ALL_OK);
        }

        [Test]
        public void Get_WithName_ReturnsOkObjectResult()
        {
            // Arrange
            pokeApiServiceMock
                .Setup(p => p.GetPokemonDescriptionByNameAsync(It.IsAny<string>()))
                .Returns(Task.Run(() => POKEMON_DESCRIPTION));

            funTranslationsApiServiceMock
                .Setup(p => p.GetShakespeareTranslationAsync(It.IsAny<string>()))
                .Returns(Task.Run(() => POKEMON_TRANSLATED_DESCRIPTION));

            // Act
            var actionResult = pokemonController.Get(POKEMON_NAME);
            var okObjectResult = (OkObjectResult)actionResult.Result;
            var apiResponse = (ApiResponse)okObjectResult.Value;

            // Assert
            Assert.AreEqual(okObjectResult.StatusCode, 200);
            Assert.AreEqual(apiResponse.Description, POKEMON_TRANSLATED_DESCRIPTION);
            Assert.AreEqual(apiResponse.Name, POKEMON_NAME);
        }

        [Test]
        public void Get_WithNonExistingName_ReturnsNotFoundObjectResult()
        {
            // Arrange
            pokeApiServiceMock
                .Setup(p => p.GetPokemonDescriptionByNameAsync(It.IsAny<string>()))
                .Returns(Task.Run(() => string.Empty));

            // Act
            var actionResult = pokemonController.Get(NON_EXISTENT_POKEMON_NAME);
            var okObjectResult = (NotFoundObjectResult)actionResult.Result;
            var apiResponse = (ApiErrorResponse)okObjectResult.Value;

            // Assert
            Assert.AreEqual(okObjectResult.StatusCode, 404);
            Assert.AreEqual(apiResponse.Name, NON_EXISTENT_POKEMON_NAME);
            Assert.IsEmpty(apiResponse.Description);
            Assert.AreEqual(apiResponse.Message, ControllerMessage.POKEMON_NOT_FOUND);
        }

        [Test]
        public void Get_WithFunTranslationsApiNotAvailable_ReturnsServiceUnavailable()
        {
            // Arrange
            pokeApiServiceMock
                .Setup(p => p.GetPokemonDescriptionByNameAsync(It.IsAny<string>()))
                .Returns(Task.Run(() => POKEMON_DESCRIPTION));

            funTranslationsApiServiceMock
                .Setup(p => p.GetShakespeareTranslationAsync(It.IsAny<string>()))
                .Returns(Task.Run(() => string.Empty));

            // Act
            var actionResult = pokemonController.Get(POKEMON_NAME);
            var okObjectResult = (ObjectResult)actionResult.Result;
            var apiResponse = (ApiErrorResponse)okObjectResult.Value;

            // Assert
            Assert.AreEqual(okObjectResult.StatusCode, 503);
            Assert.AreEqual(apiResponse.Name, POKEMON_NAME);
            Assert.AreEqual(apiResponse.Description, POKEMON_DESCRIPTION);
            Assert.AreEqual(apiResponse.Message, ControllerMessage.FUN_TRANSLATIONS_UNAVAILABLE);
        }
    }
}
