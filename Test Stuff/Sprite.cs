using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace XnaID
{

    /// <summary>
    /// A standard Sprite Class
    /// </summary>

    class Sprite
    {
        // General Sprite Info

        public Rectangle destination = Rectangle.Empty;
        public Rectangle source = Rectangle.Empty;
        public Vector2 position = Vector2.Zero;

        public Color color = Color.White;
        public float depth = 0.0f;
        public float rotation = 0.0f;
        
        public SpriteEffects effects = SpriteEffects.None;
        public Texture2D texture = null;
        
        //Contruction
        public Sprite()
        {
        }

        //Notice there is no ContentManager Passed
        public void LoadContent(ContentManager cm, String assetName)
        {
            texture = cm.Load<Texture2D>(assetName);
        }

        //Notice the Base attributes being taken
        public void Draw(SpriteBatch sb)
        {
            sb.Draw(texture, destination, source, color, rotation, position, effects, depth);
        }

    }
}
