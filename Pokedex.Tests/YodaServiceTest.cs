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
    public class YodaServiceTest
    {

        public YodaTranslationService Setup(HttpResponseMessage result)
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
                BaseAddress = new Uri("https://api.funtranslations.com/translate/yoda.json")
            };

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();

            mockHttpClientFactory.Setup(_ => _.CreateClient("YodaApi")).Returns(httpClient);
            var logger = Mock.Of<ILogger<YodaTranslationService>>();
            return new YodaTranslationService(mockHttpClientFactory.Object, logger);
        }

        [Fact]
        public async Task Method_Should_ReturnValidTranslatedDescription_When_PassedValidDesc()
        {
            var info = new TranslationResponse
            {
                Contents = new TranslationContent { Translated = "yodatranslateddesc" },
                Success = new TranslationSuccess { Total = 1 }
            };
            var result = new HttpResponseMessage
            {
                Content = JsonContent.Create(info)
            };
            var service = Setup(result);
            //Act
            var response = await service.GetPokemonWithTranslation("mewtwo");
            //Assert
            Assert.Equal("yodatranslateddesc", response);

        }

        [Fact]
        public async Task Method_Should_ReturnBasicDescription_When_NotFoundResponseReturn()
        {
            var result = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound
            };

            var service = Setup(result);
            //Act
            var response = await service.GetPokemonWithTranslation("BasicDesc");

            //Assert
            Assert.Equal("BasicDesc", response);

        }


    }
}
