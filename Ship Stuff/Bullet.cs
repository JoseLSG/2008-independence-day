using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    class Bullet : GameObject
    {
        private BulletType type;        //determines the damage, texture
        private BulletLevel level;      //affects the damage scale

        public IMoveMethod moveMethod;

        private int damage = 1;             //the base damage (pre-Scaled)
        

        public Bullet(Textures.TextureName bulletTexture)
            : base(bulletTexture)
        {
            moveMethod = new MMLinear();    //default move in straight line
        }

        public void Update(GameTime gameTime, ShipUpdateInfo info)
        {
            moveMethod.Move(this, info);

            base.Update(gameTime);
        }


        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

    }
}
