namespace GameWorldClassLibrary.Models
{
    public class Title
    {
        public Title(Guid titleId, string titleName,
            int titlePrice, string titleContent)
        {
            TitleId = titleId;
            TitleName = titleName;
            TitlePrice = titlePrice;
            TitleContent = titleContent;
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
