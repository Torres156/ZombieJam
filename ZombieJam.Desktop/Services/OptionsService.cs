using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZombieJam.Desktop.Models;

namespace ZombieJam.Desktop.Services
{
    internal static class OptionsService
    {
        public static OptionsModel Options { get; private set; }

        public static void Load()
        {
            var filePath = PATH_DATA + "windowoptions.json";
            if (File.Exists(filePath))
                Options = JsonConvert.DeserializeObject<OptionsModel>(File.ReadAllText(filePath));
            else
            {
                Options = new OptionsModel();
                Save();
            }

            Options.WindowWidth = Options.WindowWidth < 800 ? 800 : Options.WindowWidth;
            Options.WindowHeight = Options.WindowHeight < 600 ? 600 : Options.WindowHeight;
        }

        public static void Save()
        {
            var filePath = PATH_DATA + "windowoptions.json";
            File.WriteAllText(filePath, JsonConvert.SerializeObject(Options));
        }
    }
}
