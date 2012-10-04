using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaID
{

    /*
     * Displays a health background
     * with the actual health overlayed on the background
     */ 


    class HealthBar
    {
        //TEXTURE manager
        private Textures TEXMAN = Textures.GetInstance();

        //The sprite textures for the healthbar
        private Texture2D background;
        private Texture2D foreground;

        //Positioning
        private Rectangle recBack;
        private Rectangle recFront;
        private Vector2 topLeft = Vector2.Zero; //top left corner of health bar
        
        private int health;     //current health
        private int maxHealth;  //maximum ammount of health
        private int viewHealth;
        
        public HealthBar(int maxHealth)
        {
            this.maxHealth = maxHealth;
            health = maxHealth;

            background = TEXMAN.GetTexture(Textures.TextureName.HealthBarBG);
            foreground = TEXMAN.GetTexture(Textures.TextureName.HealthBar);

            recBack = new Rectangle(0, 0, background.Width, background.Height);
            recFront = new Rectangle(0, 0, foreground.Width, foreground.Height);
        }


        public void Draw(SpriteBatch sb)
        {
            sb.Draw(background, recBack, Color.White);

            Rectangle r = new Rectangle(0, 0, recFront.Width, recFront.Height);

            sb.Draw(foreground, recFront, r, Color.White);
        }

        //adjusts the health size as per ammount of health
        //compared to the total health
        private void FixSizePos()
        {
            recBack.X = (int)topLeft.X;
            recBack.Y = (int)topLeft.Y;

            recFront.X = (int)topLeft.X;
            recFront.Y = (int)topLeft.Y;

            viewHealth = (int)(((float)health / (float)maxHealth) * (float)recBack.Width);
            recFront.Width = viewHealth;
        }



        public void Increase(int ammount)
        {
            health += ammount;
            FixHealth();
        }

        public void Decrease(int ammount)
        {
            health -= ammount;
            FixHealth();
        }


        private void FixHealth()
        {
            if (health > maxHealth)
                health = maxHealth;

            if (health < 0)
                health = 0;

            FixSizePos();
        }


        public void SetHealth(int ammount)
        {
            health = ammount;
            FixHealth();
        }


        public void MoveTopLeftTo(Vector2 topLeft)
        {
            this.topLeft = topLeft;
            FixSizePos();
        }



    }
}
