using Moq;
using ProjectTesting;

namespace ProjectTestingIntegration.Tests
{
    public class IntegrationTests
    {
        private const string Approved = "Approved";
        private const string Rejected = "Rejected";
        private const string mockoonUsers = "http://localhost:3001/Users";
        private const string mockoonGames = "http://localhost:3001/Games";

        private readonly IGameService _gameService;
        private readonly IActionHandler _actionHandler;
        private readonly IDiscountService _discountService;
        private readonly IUserService _userService;
        private readonly DecisionModule _decisionModule;
        

        public IntegrationTests()
        {
            _gameService = new GameServiceApi();
            _actionHandler = new ActionHandler();
            _discountService = new DiscountService();
            _userService = new UserServiceApi();
            _decisionModule = new DecisionModule(_gameService, _actionHandler, _discountService, _userService);
        }

        [Fact]
        public void MakeDecision_ShouldReturnApproved_WhenUserAgeIsGreaterThanAgeRating()
        {
            //arrange
            var gameId = 1; //agerating = 17
            var userId = 1; //userage = 28
            _gameService.GameUrl = $"{mockoonGames}/{gameId}";
            _userService.UserUrl = $"{mockoonUsers}/{userId}";

            //act
            var result = _decisionModule.MakeDecision(gameId, userId);
            var game = _gameService.GetGame(gameId);
            var user = _userService.GetUser(userId);

            //assert
            Assert.Equal(Approved, result);
            Assert.NotNull(game);
            Assert.NotNull(user);
            Assert.Equal(gameId, game.Id);
            Assert.Equal(userId, user.Id);
            Assert.Equal(17, game.AgeRating);
            Assert.Equal(28, user.UserAge);
        }

        [Fact]
        public void MakeDecision_ShouldReturnRejected_WhenUserAgeIsLessThanAgeRating()
        {
            //arrange
            var gameId = 1; //agerating = 17
            var userId = 2; //userage = 10
            _gameService.GameUrl = $"{mockoonGames}/{gameId}";
            _userService.UserUrl = $"{mockoonUsers}/{userId}";

            //act
            var result = _decisionModule.MakeDecision(gameId, userId);
            var game = _gameService.GetGame(gameId);
            var user = _userService.GetUser(userId);

            //assert
            Assert.Equal(Rejected, result);
            Assert.NotNull(game);
            Assert.NotNull(user);
            Assert.Equal(gameId, game.Id);
            Assert.Equal(userId, user.Id);
            Assert.Equal(17, game.AgeRating);
            Assert.Equal(10, user.UserAge);
        }

        [Fact]
        public void MakeDecision_ShouldApplyDiscount_WhenUserAgeIsGreaterThanOrEqualToGameDiscountAge()
        {
            //arrange
            var gameId = 3; //agerating = 21 & eligible for discount = 65
            var userId = 3; //userage = 71
            _gameService.GameUrl = $"{mockoonGames}/{gameId}";
            _userService.UserUrl = $"{mockoonUsers}/{userId}";

            //act
            var result = _decisionModule.MakeDecision(gameId, userId);
            var game = _gameService.GetGame(gameId);
            var user = _userService.GetUser(userId);
            var discount = _discountService.isEligibleForDiscount(user.UserAge);

            //assert
            Assert.Equal(Approved, result);
            Assert.NotNull(game);
            Assert.NotNull(user);
            Assert.Equal(gameId, game.Id);
            Assert.Equal(userId, user.Id);
            Assert.Equal(21, game.AgeRating);
            Assert.Equal(71, user.UserAge);
            Assert.True(discount > 0);
            Assert.Equal(7.1, discount);
        }

        [Fact]
        public void MakeDecision_ShouldThrowArgumentException_WhenGameIdIsInvalid()
        {
            //arrange
            var invalidGameId = 100; //invalid game ID
            var userId = 1; //valid user ID
            _gameService.GameUrl = $"{mockoonGames}/{invalidGameId}";
            _userService.UserUrl = $"{mockoonUsers}/{userId}";

            //act & assert
            Assert.Throws<ArgumentException>(() => _decisionModule.MakeDecision(invalidGameId, userId));
        }

        [Fact]
        public void MakeDecision_ShouldThrowArgumentException_WhenUserIdIsInvalid()
        {
            //arrange
            var gameId = 1; //valid game ID
            var invalidUserId = 100; //invalid user ID
            _gameService.GameUrl = $"{mockoonGames}/{gameId}";
            _userService.UserUrl = $"{mockoonUsers}/{invalidUserId}";

            //act & assert
            Assert.Throws<ArgumentException>(() => _decisionModule.MakeDecision(gameId, invalidUserId));
        }

        [Fact]
        public void MakeDecision_ShouldThrowException_WhenAnErrorOccurs()
        {
            // arrange
            var gameId = 1; //valid game ID
            var userId = 1; //valid user ID
            _gameService.GameUrl = $"invalidurl/{gameId}";
            _userService.UserUrl = $"invalidurl/{userId}";

            // act & assert
            Assert.Throws<Exception>(() => _decisionModule.MakeDecision(gameId, userId));
        }
    }
}