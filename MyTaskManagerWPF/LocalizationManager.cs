using System.Globalization;
using System.Reflection;
using System.Resources;

namespace MyTaskManagerWPF
{
    public static class LocalizationManager
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("MyTaskManagerWPF.Resources.Languages.Resource", Assembly.GetExecutingAssembly());

        public static string GetString(string name)
        {
            return _resourceManager.GetString(name, CultureInfo.CurrentUICulture);
        }
    }
}
