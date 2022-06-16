using Lun.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ZombieJam.Desktop.Views
{
    internal class LoadingView  : SceneBase
    {
        Texture logo;
        Thread threadResource;

        public override void LoadContent()
        {
            logo = new Texture("res/ui/logo.png") { Smooth = true };

            threadResource = new Thread(Resources.Load);
            threadResource.Start();
        }

        public override void UnloadContent()
        {
            logo.Destroy();
            base.UnloadContent();
        }

        public override void Draw()
        {
            DrawTexture(logo, new Vector2((Size.x - logo.size.x) / 2, Size.y / 2 - logo.size.y - 80));

            var barSize = new Vector2(600,20);
            var barPosition = (Size - barSize) / 2;
            DrawRectangle(barPosition, barSize, new Color(40, 40, 40));

            DrawRectangle(barPosition, new Vector2(barSize.x * (Resources.CurrentLoad / (float)Resources.MAX_LOADS),barSize.y), new Color(178, 38, 38));

            DrawText(Resources.CurrentText, 14, barPosition - new Vector2(0, 24), Color.White);

            if (Resources.CurrentLoad == Resources.MAX_LOADS)
                Game.SetScene<MenuView>();

            base.Draw();
        }
    }
}
