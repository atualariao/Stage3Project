using System.Text;
using Newtonsoft.Json;

namespace IntegrationTest
{
    public static class ContentHelper
    {
        public static StringContent GetStringContent(object obj) 
            => new(JsonConvert.SerializeObject(obj), Encoding.Default, "application/json");
    }
}
