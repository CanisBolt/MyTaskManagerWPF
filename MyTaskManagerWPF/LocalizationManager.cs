using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MyTaskManagerWPF
{
    public static class LocalizationManager
    {
        private static readonly ResourceManager _resourceManager =
            new ResourceManager("MyTaskManagerWPF.Resource", Assembly.GetExecutingAssembly());

        public static string GetString(string name)
        {
            return _resourceManager.GetString(name, CultureInfo.CurrentUICulture);
        }
    }
}
