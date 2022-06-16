using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZombieJam.Desktop
{
    internal static class Resources
    {
        public const int MAX_LOADS = 1;

        public static string CurrentText = "Loading UIs";
        public static int CurrentLoad = 0;

        public static Dictionary<string, Texture> UI;

        public static void Load()
        {
            UI = new Dictionary<string, Texture>();
            UI.Add("background", new Texture("res/ui/background.jpg", true) { Smooth = true });
            CurrentLoad++;
        }
    }
}
