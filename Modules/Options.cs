using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XnaID
{
    class Options : Module
    {
        //Content Managers
        Fonts FONTS = Fonts.GetInstance();
        Textures TEXTURES = Textures.GetInstance();
        Sounds SOUNDS = Sounds.GetInstance();
        Properties PROPERTIES = Properties.GetInstance();


        private GameObject background;

        public Options()
        {
            background = new GameObject(Textures.TextureName.OptionsBackground);
            background.MoveTo(PROPERTIES.screenCenter);
        }


        public override ModuleName Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            if (PROP.allInput.Back())
                return ModuleName.MainMenu;


            return base.Update(gameTime);
        }


        public override void LoadContent()
        {
            background.LoadContent();
        }

        public override void Draw(SpriteBatch sb)
        {
            background.Draw(sb);
        }

    }
}
