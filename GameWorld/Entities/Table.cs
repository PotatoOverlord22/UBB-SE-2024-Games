namespace GameWorld.Entities
{
    public class Table
    {
        private int tableID;
        private string tableName;
        private int tableBuyIn;
        private int tablePlayerLimit;

        public Table()
        {
        }

        public Table(int tableID, string tableName, int tableBuyIn, int tablePlayerLimit)
        {
            this.tableID = tableID;
            this.tableName = tableName;
            this.tableBuyIn = tableBuyIn;
            this.tablePlayerLimit = tablePlayerLimit;
        }

        public int TableID
        {
            get { return tableID; } set { tableID = value; }
        }
        public string TableName
        {
            get { return tableName; } set { tableName = value; }
        }
        public int TableBuyIn
        {
            get { return tableBuyIn; } set { tableBuyIn = value; }
        }
        public int TablePlayerLimit
        {
            get { return tablePlayerLimit; } set { tablePlayerLimit = value; }
        }
    }
}
