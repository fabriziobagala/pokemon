using System;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;
using Pokemon.Controllers;

namespace Pokemon.Tests
{
    public class PokemonControllerTests
    {
        private PokemonController pokemonController;

        [SetUp]
        public void Setup()
        {
            pokemonController = new PokemonController();
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
            Assert.AreEqual(apiResponse, "All is OK");
        }

        [Test]
        public void Get_WithName_ReturnsOkObjectResult()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Get_WithNonExistingName_ReturnsNotFoundObjectResult()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void Get_WithFunTranslationsApiNotAvailable_ReturnsServiceUnavailable()
        {
            throw new NotImplementedException();
        }
    }
}
