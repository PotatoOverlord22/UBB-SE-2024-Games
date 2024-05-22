namespace Server.API.Models
{
    public class Title
    {
        public Title(Guid titleId, string titleName,
            int titlePrice, string titleContent)
        {
            this.TitleId = titleId;
            this.TitleName = titleName;
            this.TitlePrice = titlePrice;
            this.TitleContent = titleContent;
        }

        public Guid TitleId
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
