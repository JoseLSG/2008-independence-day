using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaID
{
    class Weapon : GameObject
    {
        //Weapon Properties
        public String name;
        public WeaponType type;
        private WeaponLevel level = WeaponLevel.one;

        public int fireSpeed = 1000;   //Default 1000 milisecond per shot (5x per second)
        public int recharge = 0;       //Time before the next shot
        public bool canFire = true;    //flag for the recharge

        //Bullet Info
        public BulletType bulletType;               //the type of bullet this weapon fires
        public Vector2 bulletOffset = Vector2.Zero; //where on the weapon image does the bullet start

        //Behaviors
        public IAimMethod           aimMethod;      //Determines which direction the weapon is aiming
        private IFirePattern        firePattern;    //Determines the number of bullets and relative bullet directions when weapon is fired
        private IFireMethodWeapon   fireMethod;     //Determines the recharge pattern for the weapon (when it can fire)


        //Construction
        public Weapon(Textures.TextureName weaponTexture)
            : base(weaponTexture)
        {
            level = WeaponLevel.one;
        }

        //Screen coordinates where the bullets should appear (orriginate from)
        public Vector2 GetBulletSpawnPosition()
        {
            Vector2 v = this.bulletOffset;
            VecUtil.Rotate((double)this.rotation, ref v);

            return new Vector2(this.position.X + v.X, this.position.Y + v.Y);
        }

        //Assign the firing pattern behavior for this weapon
        public void SetFirePattern(IFirePattern pattern)
        {
            this.firePattern = pattern;
        }

        //Assign the weapon aiming behaavior for this weapon
        public void SetAimMethod(IAimMethod method)
        {
            this.aimMethod = method;
        }

        //Assign the weapon fireMethod behavior for this weapon
        public void SetFireMethod(IFireMethodWeapon method)
        {
            fireMethod = method;
        }


        //Fire the weapon (or at least ATTEMPT to fire)
        public List<Bullet> Fire(ShipUpdateInfo info)
        {
            //The list of bullets we return to the ship
            List<Bullet> toReturn = new List<Bullet>();

            ////If we can't shoot yet, return empty list
            //if (!canFire)
            //    return toReturn;    

            ////So... I guess we can shoot afterall
            //canFire = false;    //flag false untill done recharging
            //recharge = (int)((float)fireSpeed / (float)level);

            if (fireMethod.Fire(info))
                toReturn.AddRange(firePattern.GetBullets(this));

            return toReturn;
        }

        //Update the weapon, recharge weapon
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            ////If still recharging.... charge it up a bit !
            //if (!canFire)
            //    recharge -= gameTime.ElapsedGameTime.Milliseconds;

            ////If we reached full charge
            //if (recharge <= 0)
            //{
            //    canFire = true;     //flag as fire-able
            //    recharge = 0;       //book keeping
            //}
            //else
            //    canFire = false;    //still charging.... booooo
        }

    }
}
