namespace Structure
{
    public interface ITable{
        public Task<string> IsValidName(string tableName);
        public Task<string> IsValidNameDb(string tableName);
    }
}