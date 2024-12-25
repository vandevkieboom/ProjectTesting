using Moq;
using Xunit;
using System;

namespace ProjectTesting.Tests
{
    public class DecisionModuleTests
    {
        private const string Approved = "Approved";
        private const string Rejected = "Rejected";

        private readonly Mock<IGameService> _mockService;
        private readonly Mock<IActionHandler> _mockActionHandler;
        private readonly Mock<IDiscountService> _mockDiscountService;
        private readonly Mock<IUserService> _mockUserService;
        private readonly DecisionModule _decisionModule;

        public DecisionModuleTests()
        {
            _mockService = new Mock<IGameService>();
            _mockActionHandler = new Mock<IActionHandler>();
            _mockDiscountService = new Mock<IDiscountService>();
            _mockUserService = new Mock<IUserService>();
            _decisionModule = new DecisionModule(_mockService.Object, _mockActionHandler.Object, _mockDiscountService.Object, _mockUserService.Object);
        }

        [Fact]
        public void MakeDecision_ShouldReturnApproved_WhenUserAgeIsGreaterThanAgeRating()
        {
            //arrange
            var game = new Game
            {
                Id = 1,
                GameTitle = "Path of Exile 2",
                AgeRating = 17,
                IsEligibleForDiscount = 55
            };
            var user = new User
            {
                Id = 1,
                UserName = "vandevkieboom",
                UserAge = 28
            };
            _mockService.Setup(service => service.GetGame(1)).Returns(game);
            _mockUserService.Setup(service => service.GetUser(1)).Returns(user);
            _mockActionHandler.Setup(handler => handler.HandleDecision(Approved, game, null)).Returns(Approved);
            _mockActionHandler.Setup(handler => handler.HandleDecision(Rejected, game, null)).Returns(Rejected);

            //act
            var result = _decisionModule.MakeDecision(1, 1);

            //assert
            Assert.Equal(Approved, result);
            _mockActionHandler.Verify(handler => handler.HandleDecision(Approved, game, null), Times.Once);
            _mockActionHandler.Verify(handler => handler.HandleDecision(Rejected, game, null), Times.Never);
            _mockDiscountService.Verify(service => service.isEligibleForDiscount(user.UserAge), Times.Never);
        }

        [Fact]
        public void MakeDecision_ShouldReturnRejected_WhenUserAgeIsLessThanAgeRating()
        {
            //arrange
            var game = new Game
            {
                Id = 2,
                GameTitle = "World of Warcraft",
                AgeRating = 12,
                IsEligibleForDiscount = 70
            };
            var user = new User
            {
                Id = 2,
                UserName = "FortniteGamer",
                UserAge = 10
            };
            _mockService.Setup(service => service.GetGame(2)).Returns(game);
            _mockUserService.Setup(service => service.GetUser(2)).Returns(user);
            _mockActionHandler.Setup(handler => handler.HandleDecision(Approved, game, null)).Returns(Approved);
            _mockActionHandler.Setup(handler => handler.HandleDecision(Rejected, game, null)).Returns(Rejected);

            //act
            var result = _decisionModule.MakeDecision(2, 2);

            //assert
            Assert.Equal(Rejected, result);
            _mockActionHandler.Verify(handler => handler.HandleDecision(Rejected, game, null), Times.Once);
            _mockActionHandler.Verify(handler => handler.HandleDecision(Approved, game, null), Times.Never);
            _mockDiscountService.Verify(service => service.isEligibleForDiscount(user.UserAge), Times.Never);
        }

        [Fact]
        public void MakeDecision_ShouldApplyDiscount_WhenUserAgeIsGreaterThanOrEqualToGameDiscountAge()
        {
            //arrange
            var discount = 20;
            var game = new Game
            {
                Id = 3,
                GameTitle = "Senior Adventures",
                AgeRating = 21,
                IsEligibleForDiscount = 65
            };
            var user = new User
            {
                Id = 3,
                UserName = "Senior Gamer",
                UserAge = 71
            };
            _mockService.Setup(service => service.GetGame(3)).Returns(game);
            _mockUserService.Setup(service => service.GetUser(3)).Returns(user);
            _mockDiscountService.Setup(service => service.isEligibleForDiscount(user.UserAge)).Returns(discount);
            _mockActionHandler.Setup(handler => handler.HandleDecision(Approved, game, discount)).Returns(Approved);
            _mockActionHandler.Setup(handler => handler.HandleDecision(Rejected, game, discount)).Returns(Rejected);

            //act
            var result = _decisionModule.MakeDecision(3, 3);

            //assert
            Assert.Equal(Approved, result);
            _mockActionHandler.Verify(handler => handler.HandleDecision(Approved, game, discount), Times.Once);
            _mockActionHandler.Verify(handler => handler.HandleDecision(Rejected, game, discount), Times.Never);
            _mockDiscountService.Verify(service => service.isEligibleForDiscount(user.UserAge), Times.Once);
        }

        [Fact]
        public void MakeDecision_ShouldThrowArgumentException_WhenGameIsNull()
        {
            //arrange
            _mockService.Setup(service => service.GetGame(200))
                .Returns((Game)null);

            //act
            var exception = Assert.Throws<ArgumentException>(() => _decisionModule.MakeDecision(200, 1));

            //assert
            Assert.Equal("Invalid game ID", exception.Message);
        }

        [Fact]
        public void MakeDecision_ShouldThrowArgumentException_WhenUserIsNull()
        {
            //arrange
            var game = new Game
            {
                Id = 1,
                GameTitle = "Path of Exile 2",
                AgeRating = 18,
                IsEligibleForDiscount = 60
            };
            _mockService.Setup(service => service.GetGame(1)).Returns(game);
            _mockUserService.Setup(service => service.GetUser(200))
                .Returns((User)null);

            //act
            var exception = Assert.Throws<ArgumentException>(() => _decisionModule.MakeDecision(1, 200));

            //assert
            Assert.Equal("Invalid user ID", exception.Message);
        }

        [Fact]
        public void MakeDecision_ShouldThrowGenericException_WhenUnexpectedErrorOccurs()
        {
            //arrange
            _mockService.Setup(service => service.GetGame(1))
                .Throws(new Exception());

            //act
            var exception = Assert.Throws<Exception>(() => _decisionModule.MakeDecision(1, 1));

            //assert
            Assert.Equal("An error occurred while making a decision", exception.Message);
        }
    }
}