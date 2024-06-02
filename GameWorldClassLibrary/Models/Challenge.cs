namespace GameWorldClassLibrary.Models
{
    public class Challenge
    {
        private Guid challengeId;
        private string challengeDescription;
        private string challengeRule;
        private int challengeAmount;
        private int challengeReward;

        public Challenge(Guid challengeId,
            string challengeDescription,
            string challengeRule,
            int challengeAmount,
            int challengeReward)
        {
            this.challengeId = challengeId;
            this.challengeDescription = challengeDescription;
            this.challengeRule = challengeRule;
            this.challengeAmount = challengeAmount;
            this.challengeReward = challengeReward;
        }

        public Guid ChallengeId
        {
            get { return challengeId; }
            set { challengeId = value; }
        }
        public string ChallengeDescription
        {
            get { return challengeDescription; }
            set { challengeDescription = value; }
        }
        public string ChallengeRule
        {
            get { return challengeRule; }
            set { challengeRule = value; }
        }
        public int ChallengeAmount
        {
            get { return challengeAmount; }
            set { challengeAmount = value; }
        }
        public int ChallengeReward
        {
            get { return challengeReward; }
            set { challengeReward = value; }
        }
    }
}
