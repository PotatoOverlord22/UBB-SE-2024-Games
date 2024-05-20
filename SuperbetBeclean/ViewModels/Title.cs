namespace SuperbetBeclean.Model
{
    public class Title
    {
        private int titleID;
        private string titleName;
        private int titlePrice;
        private string titleContent;

        private const int DEFAULT_TITLE_ID = 0;
        private const string DEFAULT_TITLE_NAME = "";
        private const int DEFAULT_TITLE_PRICE = 0;
        private const string DEFAULT_TITLE_CONTENT = "";

        public Title(int titleID = DEFAULT_TITLE_ID, string titleName = DEFAULT_TITLE_NAME,
            int titlePrice = DEFAULT_TITLE_PRICE, string titleContent = DEFAULT_TITLE_CONTENT)
        {
            this.titleID = titleID;
            this.titleName = titleName;
            this.titlePrice = titlePrice;
            this.titleContent = titleContent;
        }

        public int TitleID
        {
            get { return titleID; } set { titleID = value; }
        }
        public string TitleName
        {
            get { return titleName; } set { titleName = value; }
        }
        public int TitlePrice
        {
            get { return titlePrice; } set { titlePrice = value; }
        }
        public string TitleContent
        {
            get { return titleContent; } set { titleContent = value; }
        }
    }
}
