using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MLS.Framework.Common.Response;
using Moq;
using RockPaperScissors.Domain.Enums;
using RockPaperScissors.Domain.Exceptions;
using RockPaperScissors.Service;
using System.Net;
using Xunit;

namespace RockPaperScissors.Test
{
    public class RockPaperScissorsFunctionTest
    {

        private readonly Mock<IRockPaperScissorsService> rockPaperScissorsServiceMock = new();
        private readonly Mock<ILogger<RockPaperScissorsFunctions>> loggerMock = new();
        private readonly DefaultHttpContext context = new();

        [Fact]
        public void GetWinner_ValidInputOutput_Returns200OK()
        {
            MlsResponse <WinnerResponse> expectedResponse = new() { Data = new WinnerResponse { Winner = WinnerEnum.PlayerOne.ToString()}, ResponseCode = (ResponseCode)HttpStatusCode.OK};
            var request = context.Request;
            request.QueryString = request.QueryString.Add("playerOneChoice", WeaponsEnum.Paper.ToString());
            rockPaperScissorsServiceMock.Setup(x => x.GetWinner(WeaponsEnum.Paper.ToString())).Returns(expectedResponse);
            var function = CreateRockPaperScissorsFunctionMocks();

            var response = (OkObjectResult)function.GetWinner(request);

            Assert.NotNull(response);
            Assert.Equal(expectedResponse, response.Value);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void GetWinner_BadRequest_Returns400BadRequest()
        {
            string expectedResponse = "Invalid Weapon Choice";
            var request = context.Request;
            var function = CreateRockPaperScissorsFunctionMocks();

            var response = (BadRequestObjectResult)function.GetWinner(request);

            Assert.NotNull(response);
            Assert.Equal(expectedResponse, response.Value);
            Assert.Equal((int)HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public void GetWinner_ThrowsWeaponServiceFailedException_Returns500InternalServerError()
        {
            MlsResponse<WinnerResponse> expectedResponse = new() { ResponseCode = (ResponseCode)HttpStatusCode.InternalServerError };
            var request = context.Request;
            request.QueryString = request.QueryString.Add("playerOneChoice", WeaponsEnum.Paper.ToString());
            rockPaperScissorsServiceMock.Setup(x => x.GetWinner(WeaponsEnum.Paper.ToString())).Throws(new WeaponServiceFailedException());
            var function = CreateRockPaperScissorsFunctionMocks();

            var response = (ObjectResult)function.GetWinner(request);

            Assert.NotNull(response);
            Assert.Equal("Error occured whilst generating a winner: Exception of type 'RockPaperScissors.Domain.Exceptions.WeaponServiceFailedException' was thrown.", response.Value);
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public void GetAIWinner_ValidInputOutput_Returns200OK()
        {
            MlsResponse<WinnerResponse> expectedResponse = new() { Data = new WinnerResponse
            { PlayerOneWeapon = WeaponsEnum.Rock.ToString(), PlayerTwoWeapon = WeaponsEnum.Paper.ToString(),Winner = WinnerEnum.PlayerTwo.ToString() }
            , ResponseCode = (ResponseCode)HttpStatusCode.OK };
            var request = context.Request;
            rockPaperScissorsServiceMock.Setup(x => x.GetAIWinner()).Returns(expectedResponse);
            var function = CreateRockPaperScissorsFunctionMocks();

            var response = (OkObjectResult)function.GetAIWinner(request);

            Assert.NotNull(response);
            Assert.Equal(expectedResponse, response.Value);
            Assert.Equal((int)HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void GetAIWinner_ThrowsWeaponServiceFailedException_Returns500InternalServerError()
        {
            MlsResponse<WinnerResponse> expectedResponse = new() { ResponseCode = (ResponseCode)HttpStatusCode.InternalServerError };
            var request = context.Request;
            rockPaperScissorsServiceMock.Setup(x => x.GetAIWinner()).Throws(new WeaponServiceFailedException());
            var function = CreateRockPaperScissorsFunctionMocks();

            var response = (ObjectResult)function.GetAIWinner(request);

            Assert.NotNull(response);
            Assert.Equal("Error occured whilst generating a winner: Exception of type 'RockPaperScissors.Domain.Exceptions.WeaponServiceFailedException' was thrown.", response.Value); 
            Assert.Equal((int)HttpStatusCode.InternalServerError, response.StatusCode);
        }

        private RockPaperScissorsFunctions CreateRockPaperScissorsFunctionMocks() => new(loggerMock.Object, rockPaperScissorsServiceMock.Object);
    }
}