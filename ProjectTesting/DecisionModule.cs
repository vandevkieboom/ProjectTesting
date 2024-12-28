using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTesting
{
    public class DecisionModule
    {
        private readonly IGameService _gameService;
        private readonly IActionHandler _actionHandler;
        private readonly IDiscountService _discountService;
        private readonly IUserService _userService;

        public DecisionModule(IGameService gameService, IActionHandler actionHandler, IDiscountService discountService, IUserService userService)
        {
            _gameService = gameService;
            _actionHandler = actionHandler;
            _discountService = discountService;
            _userService = userService;
        }

        public string MakeDecision(int gameId, int userId)
        {
            try
            {
                var game = _gameService.GetGame(gameId);
                if (game is null)
                {
                    throw new ArgumentException("Invalid game ID");
                }

                var user = _userService.GetUser(userId);
                if (user is null)
                {
                    throw new ArgumentException("Invalid user ID");
                }

                string decision;
                double discount = 0; //default to 0 if user is not eligible for discount
                if (user.UserAge < game.AgeRating)
                {
                    decision = "Rejected";
                }
                else
                {
                    decision = "Approved";
                    if (user.UserAge >= game.IsEligibleForDiscount)
                    {
                        discount = _discountService.isEligibleForDiscount(user.UserAge);
                    }
                }

                return _actionHandler.HandleDecision(decision, game, discount);
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new Exception($"An error occurred while making a decision");
            }
        }
    }
}
