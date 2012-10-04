using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    class Boss1 : Boss
    {

        public Boss1()
            : base(Textures.TextureName.ShipBoss1)
        {
            base.name = "Big Boss BLAAAAA";

            //Properties and behaviors
            base.moveMethod = new MMLeftRightStrafe(3000,3000);
            base.fireMethod = new FMSInterval(1, 600);
            base.speed = 0.1f;
            base.points = 2000;
            base.health = 1500;

            //Weapons
            base.AddWeaponPort(new Vector2( 100, 0));
            base.AddWeaponPort(new Vector2(-100, 0));
            base.AddWeapon(Creator.CreateWeapon(WeaponType.TripleCanon));
            base.AddWeapon(Creator.CreateWeapon(WeaponType.TripleCanon));

            hp = new HealthBar(this.health);
        }





    }
}
