using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XnaID
{
    class Instructions : Module
    {
        private GameObject background;

        public Instructions()
        {
            background = new GameObject(Textures.TextureName.InstructionsBackground);
            background.MoveTo(PROP.screenCenter);
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

        public override void Draw(SpriteBatch sb)
        {
            background.Draw(sb);
        }

    }
}
