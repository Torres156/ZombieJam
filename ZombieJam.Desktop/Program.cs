global using Lun;
global using static Lun.LunEngine;
global using static ZombieJam.Desktop.Services.GlobalService;
global using Sound = Lun.Sound;

using System;
using System.IO;
using ZombieJam.Desktop.Services;
using ZombieJam.Desktop.Views;

namespace ZombieJam.Desktop
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            CheckDirectories();
            OptionsService.Load();

            Game.WindowSizeMin   = new Vector2(800, 600);
            Game.WindowTitle     = "Zombie Jam";
            Game.WindowCanResize = true;
            Game.WindowSize      = new Vector2(OptionsService.Options.WindowWidth, OptionsService.Options.WindowHeight);
            Game.BackgroundColor = Color.Black;
            Game.WindowMaximize  = OptionsService.Options.WindowMaximized;

            LoadFont("res/Friz Quadrata Bold.otf");

            Game.SetScene<LoadingView>();
            Game.OnResize += Game_OnResize;

            Game.Run();
        }

        private static void Game_OnResize()
        {
            if (!Game.WindowMaximize)
            {
                OptionsService.Options.WindowWidth = (int)Game.WindowSize.x;
                OptionsService.Options.WindowHeight = (int)Game.WindowSize.y;
            }
            OptionsService.Options.WindowMaximized = Game.WindowMaximize;
            OptionsService.Save();
        }

        static void CheckDirectories()
        {
            Directory.CreateDirectory(PATH_DATA);
            Directory.CreateDirectory(PATH_RESOURCE);
            Directory.CreateDirectory(PATH_UI);
        }
    }
}