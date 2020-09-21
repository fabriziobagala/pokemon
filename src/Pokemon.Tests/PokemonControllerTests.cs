using System;
using NUnit.Framework;

namespace Pokemon.Tests
{
    public class PokemonControllerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Get_ReturnsOkObjectResult()
        {
            throw new NotImplementedException();
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
