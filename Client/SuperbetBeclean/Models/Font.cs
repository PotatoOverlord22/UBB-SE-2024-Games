namespace SuperbetBeclean.Models
{
    public class Font
    {
        private int fontID;
        private string fontName;
        private int fontPrice;
        private string fontType;

        private const int DEFAULT_FONT_ID = 0;
        private const string DEFAULT_FONT_NAME = "";
        private const int DEFAULT_FONT_PRICE = 0;
        private const string DEFAULT_FONT_TYPE = "";

        public Font(int fontID = DEFAULT_FONT_ID, string fontName = DEFAULT_FONT_NAME, int fontPrice = DEFAULT_FONT_PRICE, string fontType = DEFAULT_FONT_TYPE)
        {
            this.fontID = fontID;
            this.fontName = fontName;
            this.fontPrice = fontPrice;
            this.fontType = fontType;
        }

        public int FontID
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
