using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XnaID
{
    class MainMenu : Module
    {
        //Content Managers
        Fonts FONTS = Fonts.GetInstance();
        Textures TEXTURES = Textures.GetInstance();
        Sounds SOUNDS = Sounds.GetInstance();
        Properties PROPERTIES = Properties.GetInstance();

        private GameObject[] menu;
        private int selected = 0;
        private Color colSel = Color.LightBlue;
        private Color colNoSel = new Color(80,80,80,255);


        public MainMenu()
        {
            //Assign the module name
            base.moduleName = ModuleName.MainMenu;

            menu = new GameObject[5];   //5 menu buttons
            menu[0] = new GameObject(Textures.TextureName.MainMenuNewGame);
            menu[1] = new GameObject(Textures.TextureName.MainMenuOptions);
            menu[2] = new GameObject(Textures.TextureName.MainMenuInstruction);
            menu[3] = new GameObject(Textures.TextureName.MainMenuCredits);
            menu[4] = new GameObject(Textures.TextureName.MainMenuExit);


            Vector2 v = PROPERTIES.screenCenter;
            v.Y -= 100;
            foreach (GameObject o in menu)
            {
                o.MoveTo(v);
                v.Y += 50;
            }

        }

        private void SelectNext()
        {
            if (selected == menu.Length - 1)
                selected = 0;
            else
                ++selected;
        }

        private void SelectPrevious()
        {
            if (selected == 0)
                selected = menu.Length - 1;
            else
                --selected;
        }


        public override void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                if (i == selected)
                    menu[i].tint = colSel;
                else
                    menu[i].tint = colNoSel;

                menu[i].Draw(sb);
            }
        }


        public override ModuleName Update(GameTime gameTime)
        {
            if (PROP.allInput.Back())
                return ModuleName.Exit;
            else if (PROP.allInput.SelectMenuItem())
            {
                switch (selected)
                {
                    case 0: //New game
                        PROP.InitPlayers();   //create a ship for each player
                        refEngine.LoadLevel(LevelNumber.One);
                        return ModuleName.Engine;
                    case 1: //Options
                        return ModuleName.Options;
                    case 2: //Instructions
                        return ModuleName.Instructions;
                    case 3: //Credits
                        return ModuleName.Credits;
                    case 4: //Exit
                        return ModuleName.Exit;
                    default: //New game
                        return ModuleName.MainMenu;
                }
            }
            else if (PROP.allInput.MenuNext())
                SelectNext();
            else if (PROP.allInput.MenuPrevious())
                SelectPrevious();
            

            return base.Update(gameTime);
        }


        public override void LoadContent()
        {
            base.LoadContent();

            foreach (GameObject o in menu)
            {
                o.LoadContent();
            }

        }

    }
}
