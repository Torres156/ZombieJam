using Lun.Controls;
using Lun.SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieJam.Desktop.Views
{
    internal class MenuView : SceneBase
    {
        const int MENU_START     = 0;
        const int MENU_CREDITS   = 1;
        const int MENU_OPTIONS   = 2;
        const int MENU_EXIT      = 3;
        string[]  Menu           = { "Start", "Credits", "Options", "Exit" };
        int       hoverMenu      = -1;
        int       lastSoundHover = -1;

        Texture logo;
        SoundBuffer sound_hover;

        public override void LoadContent()
        {
            logo = new Texture("res/ui/logo.png") { Smooth = true };

            sound_hover = new SoundBuffer("res/snd/button-hover.wav");           
        }

        public override void UnloadContent()
        {
            sound_hover.Dispose();
            logo.Destroy();
            base.UnloadContent();
        }

        public override void Draw()
        {
            DrawTexture(Resources.UI["background"], new Rectangle(Vector2.Zero, Size),Color.Red);
            DrawTexture(logo, new Vector2((Size.x - logo.size.x) / 2, Size.y / 2 - logo.size.y - 80));

            float yOff = 0;
            foreach(var i in Menu)
            {
                int fontSize =   Array.IndexOf(Menu, i) == hoverMenu ?  26 : 22;
                var pos = new Vector2((Size.x - GetTextWidth(i, (uint)fontSize)) / 2, Size.y / 2  + yOff);
                DrawText(i, fontSize, pos, Color.White, true);

                yOff += 32;
            }

            base.Draw();
        }

        public override bool MouseMoved(Vector2 e)
        {
            var result = base.MouseMoved(e);

            hoverMenu = -1;

            if (!result)
            {
                float yOff = 0;
                foreach (var i in Menu)
                {
                    var pos = new Vector2((Size.x - GetTextWidth(i, 26)) / 2, Size.y / 2  + yOff);
                    if (new Rectangle(pos, new Vector2(GetTextWidth(i, 26), 32)).Contains(e))
                    {   
                        hoverMenu = Array.IndexOf(Menu, i);
                        if (lastSoundHover != Array.IndexOf(Menu, i))
                        {
                            lastSoundHover = hoverMenu;
                            Sound.PlaySound(sound_hover);
                        }
                        return true;
                    }

                    yOff += 32;
                }
            }

            return result;
        }

        public override bool MouseReleased(MouseButtonEventArgs e)
        {
            var result = base.MouseReleased(e);

            if (!result)
            {
                if (hoverMenu > -1)
                {
                    switch(hoverMenu)
                    {
                        case MENU_START:
                            Game.SetScene<SelectSaveView>();
                            break;

                        case MENU_CREDITS:

                            break;

                        case MENU_OPTIONS:
                            
                            break;

                        case MENU_EXIT:
                            Game.Running = false;
                            break;
                    }
                }
            }

            return result;
        }

    }
}
