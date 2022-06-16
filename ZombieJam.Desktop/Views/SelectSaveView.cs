using Lun.Controls;
using Lun.SFML.Audio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZombieJam.Desktop.Views
{
    internal class SelectSaveView  : SceneBase
    {
        Texture logo;
        SoundBuffer sound_hover;

        int CurrentSlot = 0;
        int hoverSlot   = -1;

        const int MENU_CREATE = 0;
        const int MENU_DELETE = 1;
        const int MENU_BACK   = 2;
        string[]  Menu        = { "New Game", "Delete", "Back" };
        int       hoverMenu   = -1;

        int lastSoundHover  = -1;
        int lastSoundHover2 = -1;

        public override void LoadContent()
        {
            logo = new Texture("res/ui/logo.png") { Smooth = true };
            sound_hover = new SoundBuffer("res/snd/button-hover.wav");
        }

        public override void UnloadContent()
        {
            logo.Destroy();
            sound_hover.Dispose();
            base.UnloadContent();
        }

        public override void Draw()
        {
            DrawTexture(Resources.UI["background"], new Rectangle(Vector2.Zero, Size), Color.Red);
            DrawTexture(logo, new Vector2((Size.x - logo.size.x) / 2, Size.y / 2 - logo.size.y - 80));

            DrawSlots();

            
            base.Draw();
        }

        void DrawSlots()
        {
            var ratio = Size * .3f;
            var normalSize = Vector2.Min(new Vector2(300,200), Vector2.Max(new Vector2(150,100), ratio));
            
            var startPos = new Vector2((Size.x - (normalSize.x + 20) * 3 + 20) / 2, Size.y / 2 - 20);
            for(int i = 0; i < 3; i++)
            {
                var outlineColor = i == CurrentSlot ? new Color(154, 32, 32) :
                     (hoverSlot == i ? new Color(100,100,100) : new Color(60,60,60));

                DrawRoundedRectangle(startPos + new Vector2((normalSize.x + 20) * i, 0),
                    normalSize, new Color(10, 10, 10), 8,0, 12, 4, outlineColor);
                DrawRectangle(startPos + new Vector2((normalSize.x + 20) * i, normalSize.y + 4),
                    new Vector2(normalSize.x, 80), new Color(10, 10, 10), 4, outlineColor);

                DrawText("Slot Vazio", 14,
                    startPos + new Vector2((normalSize.x + 20) * i + (normalSize.x - GetTextWidth("Slot Vazio",14)) / 2, normalSize.y + 4 + 40 - 8),
                    Color.White);
            }

            var xOff = 0f;
            foreach (var i in Menu)
            {
                var fontSize = hoverMenu == Array.IndexOf(Menu, i) ? 26 : 22;
                DrawText(i, fontSize,
                    new Vector2((Size.x - 150 * 3) / 2 + (150 - GetTextWidth(i, (uint)fontSize)) / 2 + xOff,startPos.y +  normalSize.y + 100), Color.White);

                xOff += 150;
            }

        }

        public override bool MouseMoved(Vector2 e)
        {
            var result = base.MouseMoved(e);

            hoverMenu = -1;
            hoverSlot = -1;
            if (!result)
            {
                var ratio = Size * .3f;
                var normalSize = Vector2.Min(new Vector2(300,200), Vector2.Max(new Vector2(150,100), ratio));

                var startPos = new Vector2((Size.x - (normalSize.x + 20) * 3 + 20) / 2, Size.y / 2 - 20);
                var xOff = 0f;
                foreach (var i in Menu)
                {
                    var pos = new Vector2((Size.x - 150 * 3) / 2 + xOff, startPos.y + normalSize.y + 100);                   
                    if (new Rectangle(pos, new Vector2(150,26)).Contains(e))
                    {
                        hoverMenu = Array.IndexOf(Menu, i);
                        lastSoundHover2 = -1;
                        if (lastSoundHover != hoverMenu)
                        {
                            lastSoundHover = hoverMenu;
                            Sound.PlaySound(sound_hover);
                        }
                        return true;
                    }
                    xOff += 150;
                }

                for(int i = 0; i < 3; i++)
                {
                    var pos = startPos + new Vector2((normalSize.x + 20) * i, 0);
                    if (new Rectangle(pos, normalSize + new Vector2(0, 84)).Contains(e))
                    {
                        hoverSlot      = i;
                        lastSoundHover = -1;
                        if (lastSoundHover2 != hoverSlot)
                        {
                            lastSoundHover2 = hoverSlot;
                            Sound.PlaySound(sound_hover);
                        }
                        return true;
                    }

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
                        case MENU_CREATE:
                            break;

                        case MENU_DELETE:
                            break;

                        case MENU_BACK:
                            Game.SetScene<MenuView>();
                            break;

                    }
                    return true;
                }

                if (hoverSlot > -1)
                {
                    CurrentSlot = hoverSlot;
                    return true;
                }
            }

            return result;
        }
    }
}
