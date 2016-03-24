using Newtonsoft.Json;

namespace Portable.FluentRest.Deserializers
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(object obj)
        {
            try
            {
                return JsonConvert.SerializeObject(obj);
            }
            catch
            {
                return null;
            }
        }
    }
}