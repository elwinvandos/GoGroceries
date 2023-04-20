namespace Elwin.GoGroceries.Client.Helpers
{
    public static class HttpResultHelper
    {
        public static async Task<T> Process<T>(HttpResponseMessage res, Func<Task<T>> arg)
        {
            if (res.IsSuccessStatusCode)
            {
                return await arg();
            }
            else
            {
                var error = await res.Content.ReadAsStringAsync();
                Console.WriteLine(error);
                throw new Exception(error);
            }
        }
    }
}
