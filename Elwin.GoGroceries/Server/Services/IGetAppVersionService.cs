using System.Reflection;

namespace Elwin.GoGroceries.API.Services
{
    public interface IGetAppVersionService
    {
        string Version { get; }
    }

    public class GetAppVersionService : IGetAppVersionService
    {
        public string Version =>
            Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion ?? string.Empty;
    }
}
