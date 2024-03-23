namespace Structure
{
    public interface ITableName
    {
        public  Task<List<string>> IsValidTableName(string tableID);

    }


}