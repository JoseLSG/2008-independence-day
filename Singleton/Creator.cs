using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    /*
     * Builder Pattern
     * 
     * Creates Ships, Weapons etc... based on variouse compositions or behaviors
     * and various property settings
     * 
     */ 

    static class Creator
    {
        public static Ship CreateShip(ShipType ship, Vector2 position, Vector2 newDirection)
        {
            Ship s;    //to return

            switch (ship)
            {
                //----------------------------------------------------------------
                case ShipType.player:
                    //Create Ship, load the texture, and size properties
                    s = new Ship(Textures.TextureName.ShipRound);
                    s.LoadContent();
                    s.name = "Interceptor";
                    //Positioning
                    s.SetRotation(newDirection);        
                    s.MoveTo(position);                 
                    //Properties and Behaviors
                    s.speed = 0.3f;                    
                    //Weapons
                    s.AddWeaponPort(new Vector2(  0, 18));
                    s.AddWeaponPort(new Vector2(-35, 12));
                    s.AddWeaponPort(new Vector2( 35, 12));
                    s.AddWeaponPort(new Vector2( 10,-18));
                    s.AddWeaponPort(new Vector2(-10,-18));
                    s.AddWeaponPort(new Vector2( 10, -28));
                    s.AddWeaponPort(new Vector2(-10, -28));
                    s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    s.AddWeapon(CreateWeapon(WeaponType.UpShooter));
                    s.AddWeapon(CreateWeapon(WeaponType.UpShooter));
                    //*********** DEFER ALL BEHAVIORS ... elsewhere....
                    break;

                //----------------------------------------------------------------
                case ShipType.demoEnemy:
                    //Create Ship, load the texture, and size properties
                    s = new Ship(Textures.TextureName.ShipTriangle);
                    s.LoadContent();
                    s.name = "Ugly";
                    //Positioning
                    s.SetRotation(newDirection);
                    s.MoveTo(position);
                    //Properties and behaviors
                    s.moveMethod = new MMDownWithin100ofCenterX();
                    s.fireMethod = new FMSInterval(15, 400);
                    s.speed = 0.1f;
                    s.points = 22;
                    s.health = 2;
                    //Weapons
                    s.AddWeaponPort(new Vector2( 28, -28));
                    s.AddWeaponPort(new Vector2(-28, -28));
                    //s.AddWeaponPort(new Vector2( 13,   5));
                    //s.AddWeaponPort(new Vector2(-13,   5));
                    s.AddWeapon(CreateWeapon(WeaponType.BasicCanon));
                    s.AddWeapon(CreateWeapon(WeaponType.SideShooter));
                    //s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    //s.AddWeapon(CreateWeapon(WeaponType.UpShooter));
                    //s.AddWeapon(CreateWeapon(WeaponType.SideShooter));
                    //s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    break;

                //----------------------------------------------------------------
                case ShipType.demoEnemy2:    //########################
                    //Create Ship, load the texture, and size properties
                    s = new Ship(Textures.TextureName.ShipSmall1);
                    s.LoadContent();
                    s.name = "SmallBlack";
                    //Positioning    
                    s.SetRotation(newDirection);
                    s.MoveTo(position);
                    //Ship properties
                    s.points = 3;
                    s.health = 1;
                    s.speed = 0.1f;
                    s.moveMethod = new MMSnakeFast(((double)MathHelper.ToRadians(60f) / 1000), 1000);
                    s.fireMethod = new FMSasap();
                    //Weapons
                    s.AddWeaponPort(new Vector2(0, 10));
                    s.AddWeapon(CreateWeapon(WeaponType.BasicCanon));
                    return s;


                //----------------------------------------------------------------
                case ShipType.demoEnemy3:
                    //Create Ship, load the texture, and size properties
                    s = new Ship(Textures.TextureName.ShipTriangle);
                    s.LoadContent();
                    s.name = "Ugly";
                    //Positioning
                    s.SetRotation(newDirection);
                    s.MoveTo(position);
                    //Properties and behaviors
                    s.moveMethod = new MMLinear();
                    s.fireMethod = new FMSInterval(50, 2000);
                    s.speed = 0.15f;
                    s.points = 22;
                    s.health = 10;
                    //Weapons
                    //s.AddWeaponPort(new Vector2(28, -28));
                    //s.AddWeaponPort(new Vector2(-28, -28));
                    s.AddWeaponPort(new Vector2(13, 5));
                    s.AddWeaponPort(new Vector2(-13, 5));
                    s.AddWeapon(CreateWeapon(WeaponType.BasicCanon));
                    s.AddWeapon(CreateWeapon(WeaponType.BasicCanon));
                    //s.AddWeapon(CreateWeapon(WeaponType.IntCanon));
                    //s.AddWeapon(CreateWeapon(WeaponType.IntCanon));
                    break;


                //----------------------------------------------------------------
                case ShipType.demoEnemy4:
                    //Create Ship, load the texture, and size properties
                    s = new Ship(Textures.TextureName.ShipSkull);
                    s.LoadContent();
                    s.name = "Weeeee";
                    //Positioning
                    s.SetRotation(newDirection);
                    s.MoveTo(position);
                    //Properties and behaviors
                    s.moveMethod = new MMLinear();
                    s.fireMethod = new FMSInterval(100,1000);
                    s.speed = 0.1f;
                    s.points = 22;
                    s.health = 6;
                    //Weapons
                    s.AddWeaponPort(new Vector2(13, 5));
                    s.AddWeaponPort(new Vector2(-13, 5));
                    s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    break;



                //---------------Jose ADDED ---------------------------

                case ShipType.shipEnemy1:
                    //Create Ship, load the texture, and size properties
                    s = new Ship(Textures.TextureName.ShipEnemy1);
                    s.LoadContent();
                    s.name = "shipEnemy1";
                    //Positioning
                    s.SetRotation(newDirection);
                    s.MoveTo(position);
                    //Properties and behaviors

                    s.moveMethod = new MMCardinalSpeedup(MMCardinalSpeedup.speedUpDir.SE, 0.4, 400);
                    s.fireMethod = new FMSInterval(80, 1000);
                    s.speed = 0.1f;
                    s.points = 22;
                    s.health = 6;
                    //Weapons
                    s.AddWeaponPort(new Vector2(13, 5));
                    s.AddWeaponPort(new Vector2(-13, 5));
                    s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    break;


                case ShipType.shipEnemy2:
                    //Create Ship, load the texture, and size properties
                    s = new Ship(Textures.TextureName.ShipEnemy2);
                    s.LoadContent();
                    s.name = "shipEnemy2";
                    //Positioning
                    s.SetRotation(newDirection);
                    s.MoveTo(position);
                    //Properties and behaviors

                    s.moveMethod = new MMManeuver1();
                    s.fireMethod = new FMSInterval(80, 1000);
                    s.speed = 0.1f;
                    s.points = 22;
                    s.health = 6;
                    //Weapons
                    s.AddWeaponPort(new Vector2(13, 5));
                    s.AddWeaponPort(new Vector2(-13, 5));
                    s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    break;

                case ShipType.shipEnemy3:
                    //Create Ship, load the texture, and size properties
                    s = new Ship(Textures.TextureName.ShipEnemy3);
                    s.LoadContent();
                    s.name = "shipEnemy3";
                    //Positioning
                    s.SetRotation(newDirection);
                    s.MoveTo(position);
                    //Properties and behaviors

                    s.moveMethod = new MMSnakeFastB(((double)MathHelper.ToRadians(60f) / 1000), 400);
                    s.fireMethod = new FMSInterval(80, 1000);
                    s.speed = 0.1f;
                    s.points = 22;
                    s.health = 6;
                    //Weapons
                    s.AddWeaponPort(new Vector2(13, 5));
                    s.AddWeaponPort(new Vector2(-13, 5));
                    s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    s.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    break;



                //----------------Jose ADDED END ------------------------------------------------
               
                default:
                    s = CreateShip(ShipType.demoEnemy, position, newDirection);
                    break;
            }


            switch (Properties.difficulty)
            {
                case Difficulty.Easy:
                    s.points /= 2;
                    s.health /= 2;
                    break;

                case Difficulty.Medium: //standard
                    break;

                case Difficulty.Hard:
                    s.points = (int)(s.points * 1.5f);
                    s.health = (int)(s.health * 1.5f);
                    break;
                    
                case Difficulty.Insanity:
                    s.points *= 3;
                    s.health *= 3;
                    s.speed *= 1.5;
                    break;
            }

            return s;
        }

        //############################################################################################
        //############################################################################################

        public static Weapon CreateWeapon(WeaponType type)
        {
            Weapon w;

            switch (type)
            {

                //----------------------------------------------------------------
                case WeaponType.BasicCanon:     
                    //Create weapon, load content and size info
                    w = new Weapon(Textures.TextureName.weaponCanon1);
                    w.LoadContent();
                    //Set weapon properties
                    w.name = "Simple Canon";
                    w.bulletType = BulletType.BRoundRedSlow;
                    w.bulletOffset = new Vector2(0, 16f);
                    //Behaviors
                    w.SetAimMethod(new AMLinear());
                    //w.SetAimMethod(new AMNearestPlayer());
                    w.SetFirePattern(new FPStraightSingle());
                    w.SetFireMethod(new FMWConstant(100));
                    break;


                //----------------------------------------------------------------
                case WeaponType.TripleCanon:
                    //Create weapon, load content and size info
                    w = new Weapon(Textures.TextureName.weaponCanon1);
                    w.LoadContent();
                    //Set weapon properties
                    w.name = "Triple Canon";
                    w.bulletType = BulletType.Round;
                    w.bulletOffset = new Vector2(0, 16f);
                    //Behaviors
                    w.SetAimMethod(new AMLinear());
                    w.SetFirePattern(new FPTripleWide());
                    w.SetFireMethod(new FMWConstant(100));
                    break;


                //----------------------------------------------------------------
                case WeaponType.SideShooter:
                    //Create weapon, load content and size info
                    w = new Weapon(Textures.TextureName.weaponCanon1);
                    w.LoadContent();
                    //Set weapon properties
                    w.name = "Right Shooter";
                    w.bulletType = BulletType.PeaShot;
                    w.bulletOffset = new Vector2(0, 10f);
                    //Behaviors
                    w.SetAimMethod(new AMCardinal(Cardinal.Right));
                    w.SetFirePattern(new FPStraightSingle());
                    w.SetFireMethod(new FMWConstant(300));
                    break;


                //----------------------------------------------------------------
                case WeaponType.UpShooter:
                    //Create weapon, load content and size info
                    w = new Weapon(Textures.TextureName.NullImage);
                    w.LoadContent();
                    //Set weapon properties
                    w.name = "Up Shooter";
                    w.bulletType = BulletType.Fatty;
                    w.bulletOffset = new Vector2(0, 10f);
                    //Behaviors
                    w.SetAimMethod(new AMCardinal(Cardinal.Up));
                    w.SetFirePattern(new FPStraightSingle());
                    w.SetFireMethod(new FMWConstant(100));
                    break;


                //----------------------------------------------------------------
                case WeaponType.FunGun:
                    //Create weapon, load content and size info
                    w = new Weapon(Textures.TextureName.weaponCanon1);
                    w.LoadContent();
                    //Set weapon properties
                    w.name = "BFG";
                    w.bulletType = BulletType.Fatty;
                    w.bulletOffset = new Vector2(0, 30f);
                    //Behaviors
                    w.SetAimMethod(new AMLinear());
                    w.SetFirePattern(new FPTripleNarrow());
                    w.SetFireMethod(new FMWConstant(80));
                    break;


                //----------------------------------------------------------------
                default:
                    w = CreateWeapon(WeaponType.BasicCanon);
                    break;
            }

            w.type = type;
            return w;
        }

        //############################################################################################
        //############################################################################################

        public static Bullet CreateBullet(BulletType type, Vector2 position, Vector2 direction)
        {
            Bullet b;

            switch (type)
            {
                case BulletType.PeaShot:

                    b = new Bullet(Textures.TextureName.bulletBasic);
                    b.LoadContent();

                    b.MoveTo(position);
                    b.speed = 0.3;
                    b.SetRotation(VecUtil.GetAngle(ref direction));

                    b.Damage = 1;

                    return b;


                case BulletType.Round:

                    b = new Bullet(Textures.TextureName.bulletBasic2);
                    b.LoadContent();

                    b.MoveTo(position);
                    b.speed = 0.3f;
                    b.SetRotation(VecUtil.GetAngle(ref direction));
                    b.moveMethod = new MMSnakeFast(MathHelper.ToRadians(180f)/1000f, 400);


                    b.Damage = 1;

                    return b;


                case BulletType.BRoundRedFast:

                    b = new Bullet(Textures.TextureName.bulletRedRound);
                    b.LoadContent();

                    b.MoveTo(position);
                    b.speed = 0.3f;
                    b.SetRotation(VecUtil.GetAngle(ref direction));

                    b.Damage = 1;

                    return b;


                case BulletType.BRoundRedSlow:

                    b = new Bullet(Textures.TextureName.bulletRedRound);
                    b.LoadContent();

                    b.MoveTo(position);
                    b.speed = 0.3f;
                    b.SetRotation(VecUtil.GetAngle(ref direction));

                    b.Damage = 1;

                    return b;


                case BulletType.Fatty:

                    b = new Bullet(Textures.TextureName.bulletFatty);
                    b.LoadContent();

                    b.MoveTo(position);
                    b.speed = 0.3;
                    b.SetRotation(VecUtil.GetAngle(ref direction));

                    b.Damage = 1;

                    return b;


            }

            return null;
        }

        //############################################################################################
        //############################################################################################

        public static Item CreateItem(ItemType type, GameObject item, ref LevelSlider refSlider)
        {
            Item i = new Item(type, item);
            i.LoadContent();
            i.slider = refSlider;

            return i;
        }

        //############################################################################################
        //############################################################################################

        public static Boss CreateBoss(LevelNumber levelNum)
        {
            //The boss to return
            Boss b = new Boss(Textures.TextureName.ShipBoss1);

            switch (levelNum)
            {
                case LevelNumber.One:
                    b = new Boss(Textures.TextureName.ShipBoss1);
                    b.LoadContent();
                    b.name = "Boss #1";
                    //Positioning
                    //b.SetRotation(newDirection);
                    //b.MoveTo(position);
                    //Properties and behaviors
                    //b.moveMethod = new MM();
                    b.fireMethod = new FMSInterval(100, 1000);
                    b.speed = 0.1f;
                    b.points = 22;
                    b.health = 6;
                    //Weapons
                    b.AddWeaponPort(new Vector2(13, 5));
                    b.AddWeaponPort(new Vector2(-13, 5));
                    b.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    b.AddWeapon(CreateWeapon(WeaponType.TripleCanon));
                    break;

                    

            }

            return b;
        }

    }
}
