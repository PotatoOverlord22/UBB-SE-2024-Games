using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;

namespace GameWorldClassLibrary.Services
{
    public class SkillIssueBroService : ISkillIssueBroService
    {
        ISkillIssueBroRepository skillIssueBroRepository;
        public SkillIssueBroService(ISkillIssueBroRepository skillIssueBroRepository)
        {
            this.skillIssueBroRepository = skillIssueBroRepository;
        }
        public async Task<string> GetCurrentPlayerColor()
        {
            try
            {
                return await skillIssueBroRepository.GetCurrentPlayerColor();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Pawn>> GetPawns()
        {
            try
            {
                return await skillIssueBroRepository.GetPawns();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public async Task MovePawnBasedOnClick(int column, int row, int leftDiceValue, int rightDiceValue)
        {
            try
            {
                await skillIssueBroRepository.MovePawnBasedOnClick(column, row, leftDiceValue, rightDiceValue);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public async Task NextPlayer()
        {
            try
            {
                await skillIssueBroRepository.NextPlayer();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }

        public async Task<int> RollDice()
        {
            try
            {
                return await skillIssueBroRepository.RollDice();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw new Exception(e.Message);
            }
        }
    }
}
