using System.Text;
using System.Text.Json;

namespace Elwin.GoGroceries.Client.Helpers
{
    public static class JsonSerializerHelper
    {
        public static StringContent Process<T>(T dto)
        {
            return new StringContent(JsonSerializer.Serialize(dto), Encoding.UTF8, "application/json");
        }
    }
}
