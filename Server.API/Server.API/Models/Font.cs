namespace Server.API.Models
{
    public class Font
    {
        private Guid fontID;
        private string fontName;
        private int fontPrice;
        private string fontType;
        private const string DEFAULT_FONT_NAME = "";
        private const int DEFAULT_FONT_PRICE = 0;
        private const string DEFAULT_FONT_TYPE = "";

        public Font(Guid fontID = new Guid(), string fontName = DEFAULT_FONT_NAME, int fontPrice = DEFAULT_FONT_PRICE, string fontType = DEFAULT_FONT_TYPE)
        {
            this.fontID = fontID;
            this.fontName = fontName;
            this.fontPrice = fontPrice;
            this.fontType = fontType;
        }

        public Guid FontID
        {
            get { return fontID; }
            set { fontID = value; }
        }
        public string FontName
        {
            get { return fontName; }
            set { fontName = value; }
        }
        public int FontPrice
        {
            get { return fontPrice; }
            set { fontPrice = value; }
        }
        public string FontType
        {
            get { return fontType; }
            set { fontType = value; }
        }
    }
}
