namespace GameWorldClassLibrary.Models
{
    public class Font
    {
        private Guid fontID;
        private string fontName;
        private int fontPrice;
        private string fontType;

        public Font(Guid fontID, string fontName, int fontPrice, string fontType)
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
