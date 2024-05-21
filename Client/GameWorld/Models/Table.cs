namespace GameWorld.Models
{
    public class Table
    {
        public Table(Guid tableID, string tableName, int tableBuyIn, int tablePlayerLimit)
        {
            this.TableID = tableID;
            this.TableName = tableName;
            this.TableBuyIn = tableBuyIn;
            this.TablePlayerLimit = tablePlayerLimit;
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
