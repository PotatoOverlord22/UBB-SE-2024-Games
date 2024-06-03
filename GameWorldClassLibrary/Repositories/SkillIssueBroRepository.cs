using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Utils;
using Newtonsoft.Json;

namespace GameWorldClassLibrary.Repositories
{
    public class SkillIssueBroRepository : ISkillIssueBroRepository
    {
        private readonly IRequestClient requestClient;
        public SkillIssueBroRepository(IRequestClient requestClinet) 
        {
            this.requestClient = requestClinet;
        }
        public async Task<string> GetCurrentPlayerColor()
        {
            var response = await requestClient.GetAsync($"{Apis.SKILL_ISSUE_BRO_BASE_URL}/GetCurrentPlayerColor");
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task<List<Pawn>> GetPawns()
        {
            var response = await requestClient.GetAsync($"{Apis.SKILL_ISSUE_BRO_BASE_URL}/GetPawns");
            string responseContentAsString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<Pawn>? pawns = JsonConvert.DeserializeObject<List<Pawn>>(responseContentAsString);
                return pawns;
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No pawns found");
                return new List<Pawn>();
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }

        public async Task MovePawnBasedOnClick(int column, int row, int leftDiceValue, int rightDiceValue)
        {
            var response = await requestClient.GetAsync($"{Apis.SKILL_ISSUE_BRO_BASE_URL}/MovePawnBasedOnClick?column={column}&row={row}&leftDiceValue={leftDiceValue}&rightDiceValue={rightDiceValue}");
            if (response.IsSuccessStatusCode)
            {
                // Maybe do something more interesting here
                Console.WriteLine("Pawn moved successfully.");
            }
            else
            {
                // Log the response status code and reason phrase
                string errorMessage = $"Error: {response.StatusCode}, {response.ReasonPhrase}";
                Console.WriteLine(errorMessage);

                // Read and log the response content for more details
                string responseContent = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {responseContent}");

                // Throw an exception with the detailed error message
                throw new Exception(errorMessage);
            }
        }

        public async Task NextPlayer()
        {
            var response = await requestClient.GetAsync($"{Apis.SKILL_ISSUE_BRO_BASE_URL}/ChangeCurrentPlayer");
            if (!response.Content.ReadAsStringAsync().Result.Equals("Moved pawn successfully"))
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
        }

        public async Task<int> RollDice()
        {
            var response = await requestClient.GetAsync($"{Apis.SKILL_ISSUE_BRO_BASE_URL}/RollDice");
            string responseContentAsString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                int diceRoll = JsonConvert.DeserializeObject<int>(responseContentAsString);
                return diceRoll;
            }
            else
            {
                throw new Exception($"Error: {response.StatusCode}, {response.ReasonPhrase}");
            }
        }
    }
}
