using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaID
{
    class Credits : Module
    {
        //Content Managers
        Fonts FONTS = Fonts.GetInstance();
        Textures TEXTURES = Textures.GetInstance();
        Sounds SOUNDS = Sounds.GetInstance();
        Properties PROPERTIES = Properties.GetInstance();

        private GameObject background;

        public Credits()
        {
            //Setup the background sprite (gameobject)
            background = new GameObject(Textures.TextureName.CreditsBackground);
            background.MoveTo(PROPERTIES.screenCenter);
        }


        public override void Draw(SpriteBatch sb)
        {
            background.Draw(sb);
        }

        public override ModuleName Update(GameTime gameTime)
        {
            if (PROP.allInput.Back())
                return ModuleName.MainMenu;

            return base.Update(gameTime);
        }

        public override void LoadContent()
        {
            background.LoadContent();
        }



    }
}
