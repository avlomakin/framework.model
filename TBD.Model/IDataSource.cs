namespace TBD.Model
{
    public interface IDataSource
    {
        T TryGetAs<T>() where T : class;
    }
}