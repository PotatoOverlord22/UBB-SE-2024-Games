namespace GameWorld.Models
{
    public class Title
    {
        private const string DEFAULT_TITLE_NAME = "";
        private const int DEFAULT_TITLE_PRICE = 0;
        private const string DEFAULT_TITLE_CONTENT = "";

        public Title(Guid titleID = new Guid(), string titleName = DEFAULT_TITLE_NAME,
            int titlePrice = DEFAULT_TITLE_PRICE, string titleContent = DEFAULT_TITLE_CONTENT)
        {
            this.TitleID = titleID;
            this.TitleName = titleName;
            this.TitlePrice = titlePrice;
            this.TitleContent = titleContent;
        }

        public Guid TitleID
        {
            get; set;
        }
        public string TitleName
        {
            get; set;
        }
        public int TitlePrice
        {
            get; set;
        }
        public string TitleContent
        {
            get; set;
        }
    }
}
