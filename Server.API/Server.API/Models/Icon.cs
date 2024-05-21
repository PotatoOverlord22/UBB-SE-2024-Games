namespace Server.API.Models
{
    public class Icon
    {
        private Guid iconID;
        private string iconName;
        private int iconPrice;
        private string iconPath;

        private const string DEFAULT_ICON_NAME = "";
        private const int DEFAULT_ICON_PRICE = 0;
        private const string DEFAULT_ICON_PATH = "";

        public Icon(Guid iconID = new Guid(), string iconName = DEFAULT_ICON_NAME, int iconPrice = DEFAULT_ICON_PRICE, string iconPath = DEFAULT_ICON_PATH)
        {
            this.iconID = iconID;
            this.iconName = iconName;
            this.iconPrice = iconPrice;
            this.iconPath = iconPath;
        }

        public Guid IconID
        {
            get { return iconID; }
            set { iconID = value; }
        }
        public string IconName
        {
            get { return iconName; }
            set { iconName = value; }
        }
        public int IconPrice
        {
            get { return iconPrice; }
            set { iconPrice = value; }
        }
        public string IconPath
        {
            get { return iconPath; }
            set { iconPath = value; }
        }
    }
}
