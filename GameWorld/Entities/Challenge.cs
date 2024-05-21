namespace GameWorld.Entities
{
    public class Challenge
    {
        private int challengeID;
        private string challengeDescription;
        private string challengeRule;
        private int challengeAmount;
        private int challengeReward;

        private const int DEFAULT_CHALLENGE_ID = 0;
        private const string DEFAULT_CHALLENGE_DESCRIPTION = "";
        private const string DEFAULT_CHALLENGE_RULE = "";
        private const int DEFAULT_CHALLENGE_AMOUNT = 0;
        private const int DEFAULT_CHALLENGE_REWARD = 0;

        public Challenge(int challengeID = DEFAULT_CHALLENGE_ID,
            string challengeDescription = DEFAULT_CHALLENGE_DESCRIPTION,
            string challengeRule = DEFAULT_CHALLENGE_RULE,
            int challengeAmount = DEFAULT_CHALLENGE_AMOUNT,
            int challengeReward = DEFAULT_CHALLENGE_REWARD)
        {
            this.challengeID = challengeID;
            this.challengeDescription = challengeDescription;
            this.challengeRule = challengeRule;
            this.challengeAmount = challengeAmount;
            this.challengeReward = challengeReward;
        }

        public int ChallengeID
        {
            get { return challengeID; } set { challengeID = value; }
        }
        public string ChallengeDescription
        {
            get { return challengeDescription; } set { challengeDescription = value; }
        }
        public string ChallengeRule
        {
            get { return challengeRule; } set { challengeRule = value; }
        }
        public int ChallengeAmount
        {
            get { return challengeAmount; } set { challengeAmount = value; }
        }
        public int ChallengeReward
        {
            get { return challengeReward; } set { challengeReward = value; }
        }
    }
}
