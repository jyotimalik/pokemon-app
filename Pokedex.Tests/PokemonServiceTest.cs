using Microsoft.Extensions.Logging;
using Moq;
using Moq.Protected;
using Pokedex.Libs.Models;
using Pokedex.Libs.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Pokedex.Tests
{
    public class PokemonServiceTest
    {

        public PokemonService Setup(HttpResponseMessage result)
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .Returns(Task.FromResult(result))
                .Verifiable()
                ;

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon-species/")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("PokemonApi")).Returns(httpClient);
            var logger = Mock.Of<ILogger<PokemonService>>();
            return new PokemonService(mockHttpClientFactory.Object, logger);
        }

        [Fact]
        public async Task Method_Should_ReturnValidPokemonInfo_When_PassedValidName()
        {
            var info = new PokemonInformation
            {
                Name = "mewtwo"
            };
            var result = new HttpResponseMessage
            {
                Content = JsonContent.Create(info)
            };

            var pokemonService = Setup(result);
            //Act
            var response = await pokemonService.GetPokemonBasicData("mewtwo");

            //Assert
            Assert.Equal("mewtwo", response.Name);

        }

        [Fact]
        public async Task Method_Should_ReturnBadRequest_When_PassedInValidName()
        {
            var result = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };

            var pokemonService = Setup(result);
            //Act
            var response = await pokemonService.GetPokemonBasicData("test");

            //Assert
            Assert.Null(response);

        }

        [Fact]
        public async Task Method_Should_ReturnNull_When_InvalidResponseReturnFromApi()
        {
            var content = "test";
            var result = new HttpResponseMessage
            {
                Content = JsonContent.Create(content)
            };

            var pokemonService = Setup(result);
            //Act
            var response = await pokemonService.GetPokemonBasicData("test");

            //Assert
            Assert.Null(response);

        }

    }
}
