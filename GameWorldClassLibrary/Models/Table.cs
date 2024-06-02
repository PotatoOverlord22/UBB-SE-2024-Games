namespace GameWorldClassLibrary.Models
{
    public class Table
    {
        public Table(Guid tableID, string tableName, int tableBuyIn, int tablePlayerLimit)
        {
            TableID = tableID;
            TableName = tableName;
            TableBuyIn = tableBuyIn;
            TablePlayerLimit = tablePlayerLimit;
        }

        public Guid TableID
        {
            get; set;
        }
        public string TableName
        {
            get; set;
        }
        public int TableBuyIn
        {
            get; set;
        }
        public int TablePlayerLimit
        {
            get; set;
        }
    }
}
