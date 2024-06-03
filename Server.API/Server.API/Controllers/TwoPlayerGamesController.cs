using System.Reflection;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using GameWorldClassLibrary.Services;
using Microsoft.AspNetCore.Mvc;

namespace Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwoPlayerGamesController : ControllerBase
    {
        private IPlayService playService;
        private readonly IStatsRepository statsRepository;

        public TwoPlayerGamesController(IStatsRepository statsRepository, IPlayService playService)
        {
            this.playService = playService;
            this.statsRepository = statsRepository;
        }

        [HttpGet]
        [Route("GetWinner")]
        public IActionResult GetWinner()
        {
            if (playService.GetWinner() == null)
            {
                return StatusCode(StatusCodes.Status204NoContent);
            }
            return Ok(playService.GetWinner());
        }

        [HttpGet]
        [Route("GetTurn")]
        public IActionResult GetTurn()
        {
            return Ok(playService.GetTurn());
        }

        [HttpGet]
        [Route("HasData")]
        public IActionResult HasData()
        {
            return Ok(((OnlineGameService)playService).HasData());
        }

        [HttpGet]
        [Route("PlayOther")]
        public IActionResult PlayOther()
        {
            playService.PlayOther();
            return Ok();
        }

        [HttpGet]
        [Route("IsGameOver")]
        public IActionResult IsGameOver()
        {
            playService.IsGameOver();
            return Ok();
        }

        [HttpGet]
        [Route("SetFirstTurn")]
        public IActionResult SetFirstTurn()
        {
            ((OnlineGameService)playService).SetFirstTurn();
            return Ok();
        }

        [HttpGet]
        [Route("GetStats")]
        public PlayerStats GetStats(Player player)
        {
            return statsRepository.GetProfileStatsForPlayer(player);
        }

        [HttpGet]
        [Route("Play")]
        public IActionResult Play(int numberOfParameters, object[] parameters)
        {
            playService.Play(numberOfParameters, parameters);
            return Ok();
        }

        [HttpGet]
        [Route("GetBoard")]
        public IActionResult GetBoard()
        {
            return Ok(playService.GetBoard());
        }

        [HttpGet]
        [Route("StartPlayer")]
        public IActionResult StartPlayer()
        {
            return Ok(playService.StartPlayer());
        }

        // ...
        [HttpGet]
        [Route("CreatePlayService")]
        public IActionResult CreatePlayService(string playServiceType, object[] parameters)
        {
            Type type = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(t => t.Name == playServiceType);
            if (type == null || !typeof(IPlayService).IsAssignableFrom(type))
            {
                return BadRequest("Invalid play service type");
            }

            try
            {
                playService = (IPlayService)Activator.CreateInstance(type, parameters);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetPlayerStats")]
        public IActionResult GetPlayerStats(Player player)
        {
            return Ok(statsRepository.GetProfileStatsForPlayer(player));
        }

        [HttpGet]
        [Route("GetGameStats")]
        public IActionResult GetGameStats(Player player, string gameType)
        {
            try
            {
                var stats = statsRepository.GetGameStatsForPlayer(player, new Games(gameType));
                return Ok(stats);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
        }

        [HttpGet]
        [Route("GetGames")]
        public IActionResult GetGames()
        {
            return Ok(GameStore.Games.Values);
        }
    }
}
