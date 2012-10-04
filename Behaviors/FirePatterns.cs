using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    //Determines how many bullets and which direction 
    //they are going when they come out
    interface IFirePattern
    {
        //Each fire pattern is given 
        List<Bullet> GetBullets(Weapon weapon);
    }


    //Single Shot in a straight line
    class FPStraightSingle : IFirePattern
    {
        public List<Bullet> GetBullets(Weapon weapon)
        {
            List<Bullet> toReturn = new List<Bullet>();

            toReturn.Add(
                Creator.CreateBullet(
                    weapon.bulletType, 
                    weapon.GetBulletSpawnPosition(), 
                    VecUtil.GetNormAng(weapon.rotation)));

            return toReturn;
        }
    }


    //Triple Shot Wide
    class FPTripleWide : IFirePattern
    {
        public List<Bullet> GetBullets(Weapon weapon)
        {
            List<Bullet> toReturn = new List<Bullet>();

            Vector2 v1, v2, v3;
            v1 = new Vector2(-0.3f, 1);   //x degrees left
            v2 = new Vector2( 0, 1);      //Striaght up
            v3 = new Vector2( 0.3f, 1);   //x degrees right
            v1.Normalize();
            v3.Normalize();

            //Rotate the 3 bullets to match (this) weapon rotation
            VecUtil.Rotate((double)weapon.rotation, ref v1);
            VecUtil.Rotate((double)weapon.rotation, ref v2);
            VecUtil.Rotate((double)weapon.rotation, ref v3);

            toReturn.Add(Creator.CreateBullet(weapon.bulletType, weapon.GetBulletSpawnPosition(), v1));
            toReturn.Add(Creator.CreateBullet(weapon.bulletType, weapon.GetBulletSpawnPosition(), v2));
            toReturn.Add(Creator.CreateBullet(weapon.bulletType, weapon.GetBulletSpawnPosition(), v3));

            return toReturn;
        }
    }



    //Triple Shot Wide
    class FPTripleNarrow : IFirePattern
    {
        public List<Bullet> GetBullets(Weapon weapon)
        {
            List<Bullet> toReturn = new List<Bullet>();

            Vector2 v1, v2, v3;
            v1 = new Vector2(-0.1f, 1);   //x degrees left
            v2 = new Vector2(0, 1);      //Striaght up
            v3 = new Vector2(0.1f, 1);   //x degrees right
            v1.Normalize();
            v3.Normalize();

            //Rotate the 3 bullets to match (this) weapon rotation
            VecUtil.Rotate((double)weapon.rotation, ref v1);
            VecUtil.Rotate((double)weapon.rotation, ref v2);
            VecUtil.Rotate((double)weapon.rotation, ref v3);

            toReturn.Add(Creator.CreateBullet(weapon.bulletType, weapon.GetBulletSpawnPosition(), v1));
            toReturn.Add(Creator.CreateBullet(weapon.bulletType, weapon.GetBulletSpawnPosition(), v2));
            toReturn.Add(Creator.CreateBullet(weapon.bulletType, weapon.GetBulletSpawnPosition(), v3));

            return toReturn;
        }
    }


}
