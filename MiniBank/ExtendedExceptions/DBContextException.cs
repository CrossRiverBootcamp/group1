
namespace ExtendedExceptions
{
    public class DBContextException : Exception
    {
        public DBContextException(string exMessage):base(exMessage)
        {
        }
    }
}
