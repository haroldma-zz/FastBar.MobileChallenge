namespace Portable.FluentRest.Deserializers
{
    public interface ISerializer
    {
        string Serialize(object obj);
    }
}