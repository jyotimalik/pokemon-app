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
    public class ShakespeareServiceTest
    {

        public ShakespeareTranslationService Setup(HttpResponseMessage result)
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
                BaseAddress = new Uri("https://api.funtranslations.com/translate/shakespeare.json")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("ShakespeareApi")).Returns(httpClient);
            var logger = Mock.Of<ILogger<ShakespeareTranslationService>>();
            return new ShakespeareTranslationService(mockHttpClientFactory.Object, logger);
        }

        [Fact]
        public async Task Method_Should_ReturnValidTranslatedDescription_When_PassedValidDesc()
        {
            TranslationResponse info = new TranslationResponse
            {
                Contents = new TranslationContent { Translated = "mewtwotranslated" },
                Success = new TranslationSuccess { Total = 1 }
            };
            HttpResponseMessage result = new HttpResponseMessage
            {
                Content = JsonContent.Create(info)
            };
            ShakespeareTranslationService service = Setup(result);
            //Act
            var response = await service.GetPokemonWithTranslation("mewtwo");
            //Assert
            Assert.Equal("mewtwotranslated", response);

        }

        [Fact]
        public async Task Method_Should_ReturnBasicDescription_When_NotFoundResponseReturn()
        {
            HttpResponseMessage result = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };

            ShakespeareTranslationService service = Setup(result);
            //Act
            var response = await service.GetPokemonWithTranslation("Test");

            //Assert
            Assert.Equal("Test", response);

        }


    }
}
