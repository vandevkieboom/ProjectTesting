using Xunit.Gherkin.Quick;

namespace ProjectTesting.AcceptanceTests.StepDefinitions
{
    [FeatureFile("./Features/MakeDecisionFailure.feature")]
    public sealed class AcceptanceTestsFailureSteps : Feature
    {
        private const string mockoonUsers = "http://localhost:3001/Users";
        private const string mockoonGames = "http://localhost:3001/Games";

        private readonly IGameService _gameService;
        private readonly IActionHandler _actionHandler;
        private readonly IDiscountService _discountService;
        private readonly IUserService _userService;
        private readonly DecisionModule _decisionModule;

        private int _gameId;
        private int _userId;
        private Exception _caughtException;

        public AcceptanceTestsFailureSteps()
        {
            _gameService = new GameServiceApi();
            _actionHandler = new ActionHandler();
            _discountService = new DiscountService();
            _userService = new UserServiceApi();
            _decisionModule = new DecisionModule(_gameService, _actionHandler, _discountService, _userService);
        }

        [Given(@"a game with ID (\d+)")]
        public void GivenAGameWithID(int gameId)
        {
            _gameId = gameId;
            _gameService.GameUrl = $"{mockoonGames}/{gameId}";
        }

        [And(@"a user with ID (\d+)")]
        public void AndAUserWithID(int userId)
        {
            _userId = userId;
            _userService.UserUrl = $"{mockoonUsers}/{userId}";
        }

        [When(@"the decision is made")]
        public void WhenTheDecisionIsMade()
        {
            try
            {
                _decisionModule.MakeDecision(_gameId, _userId);
            }
            catch (Exception ex)
            {
                _caughtException = ex;
            }
        }

        [When(@"the decision is made with invalid URLs")]
        public void WhenTheDecisionIsMadeWithInvalidURLs()
        {
            _gameService.GameUrl = $"invalidurl/{_gameId}";
            _userService.UserUrl = $"invalidurl/{_userId}";

            try
            {
                _decisionModule.MakeDecision(_gameId, _userId);
            }
            catch (Exception ex)
            {
                _caughtException = ex;
            }
        }

        [Then(@"an ArgumentException should be thrown with message ""(.*)""")]
        public void ThenAnArgumentExceptionShouldBeThrownWithMessage(string expectedMessage)
        {
            Assert.NotNull(_caughtException);
            var argumentException = Assert.IsType<ArgumentException>(_caughtException);
            Assert.Equal(expectedMessage, argumentException.Message);
        }

        [Then(@"an Exception should be thrown with message ""(.*)""")]
        public void ThenAnExceptionShouldBeThrownWithMessage(string expectedMessage)
        {
            Assert.NotNull(_caughtException);
            var exception = Assert.IsType<Exception>(_caughtException);
            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}