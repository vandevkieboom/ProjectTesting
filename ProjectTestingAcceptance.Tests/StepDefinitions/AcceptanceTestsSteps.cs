using Xunit.Gherkin.Quick;

namespace ProjectTesting.AcceptanceTests.StepDefinitions
{
    [FeatureFile("./Features/MakeDecision.feature")]
    public sealed class AcceptanceTestsSteps : Feature
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
        private string _decision;
        private double _discount;
        private Game _game;
        private User _user;

        public AcceptanceTestsSteps()
        {
            _gameService = new GameServiceApi();
            _actionHandler = new ActionHandler();
            _discountService = new DiscountService();
            _userService = new UserServiceApi();
            _decisionModule = new DecisionModule(_gameService, _actionHandler, _discountService, _userService);
        }

        [Given(@"a game with ID (\d+) and age rating (\d+) and discount eligibility age (\d+)")]
        public void GivenAGameWithIDAndAgeRating(int gameId, int expectedAgeRating, int expectedDiscountEligibility)
        {
            _gameId = gameId;
            _gameService.GameUrl = $"{mockoonGames}/{gameId}";

            _game = _gameService.GetGame(gameId);

            Assert.NotNull(_game);
            Assert.Equal(gameId, _game.Id);
            Assert.Equal(expectedAgeRating, _game.AgeRating);
            Assert.Equal(expectedDiscountEligibility, _game.IsEligibleForDiscount);
        }

        [And(@"a user with ID (\d+) and age (\d+)")]
        public void AndAUserWithIDAndAge(int userId, int expectedUserAge)
        {
            _userId = userId;
            _userService.UserUrl = $"{mockoonUsers}/{userId}";

            _user = _userService.GetUser(userId);

            Assert.NotNull(_user);
            Assert.Equal(userId, _user.Id);
            Assert.Equal(expectedUserAge, _user.UserAge);
        }

        [When(@"the decision is made and user is not eligible for discount")]
        public void WhenTheDecisionIsMadeAndIsNotEligibleForDiscount()
        {
            _decision = _decisionModule.MakeDecision(_gameId, _userId);
        }

        [When(@"the decision is made and user is eligible for discount")]
        public void WhenTheDecisionIsMadeAndIsEligibleForDiscount()
        {
            _decision = _decisionModule.MakeDecision(_gameId, _userId);
            _discount = _discountService.isEligibleForDiscount(_user.UserAge);
        }

        [Then(@"the decision should be ""(.*)""")]
        public void ThenTheDecisionShouldBe(string expectedDecision)
        {
            Assert.Equal(expectedDecision, _decision);
        }

        [And(@"the discount should be (\d+)%")]
        public void ThenTheDiscountShouldBe(double expectedDiscount)
        {
            Assert.Equal(expectedDiscount, _discount);
        }
    }
}