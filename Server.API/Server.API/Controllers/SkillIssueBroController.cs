using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Server.API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillIssueBroController : ControllerBase
    {
        private readonly IGameStateService gameState;

        public SkillIssueBroController(IGameStateService gameState)
        {
            this.gameState = gameState;
        }

        // GET: api/SkillIssueBro/GetPawns
        [HttpGet]
        [Route("GetPawns")]
        public ActionResult<IEnumerable<Pawn>> GetPawns()
        {
            /*
             * Pawns are in order Blue x 4, Yellow x 4, Green x 4, Red x 4
             */
            // return as json objects
            return Ok(gameState.GamePawns);
        }

        // GET: api/SkillIssueBro/RollDice
        [HttpGet]
        [Route("RollDice")]
        public ActionResult<int> RollDice()
        {
            return Ok(gameState.GameBoard.GetDice().RollDice());
        }

        // GET: api/SkillIssueBro/MovePawnBasedOnClick
        [HttpGet]
        [Route("MovePawnBasedOnClick")]
        public ActionResult MovePawnBasedOnClick(int column, int row, int leftDiceValue, int rightDiceValue)
        {
            try
            {
                Pawn pawn = gameState.DeterminePawnBasedOnColumnAndRow(column, row);

                gameState.MovePawn(pawn, leftDiceValue, rightDiceValue, gameState.Players[gameState.currentPlayerIndex].Id);

                return Ok("Pawn moved successfully");
            }
            catch (Exception ex)
            {
                // send the exception message to the client\
                return BadRequest(ex.Message);
            }
        }

        // GET: api/SkillIssueBro/ChangeCurrentPlayer
        [HttpGet]
        [Route("ChangeCurrentPlayer")]
        public ActionResult ChangeCurrentPlayer()
        {
            gameState.currentPlayerIndex = gameState.DetermineNextPlayerIndex();
            return Ok();
        }

        // GET: api/SkillIssueBro/GetCurrentPlayerColor
        [HttpGet]
        [Route("GetCurrentPlayerColor")]
        public IActionResult GetCurrentPlayerColor()
        {
            try
            {
                string color = gameState.GetCurrentPlayerColor();
                return Ok(color); // Return result with 200 OK status
            }
            catch (Exception ex)
            {
                // Log the exception (using your preferred logging mechanism)
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
    }
}
