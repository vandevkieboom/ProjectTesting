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
            var gameId = 1;
            var userId = 1;
            _gameService.GameUrl = $"{mockoonGames}/{gameId}";
            _userService.UserUrl = $"{mockoonUsers}/{userId}";

            var game = _gameService.GetGame(gameId);
            var user = _userService.GetUser(userId);

            //act
            var result = _decisionModule.MakeDecision(gameId, userId);

            //assert
            Assert.Equal(Approved, result);
        }
    }
}