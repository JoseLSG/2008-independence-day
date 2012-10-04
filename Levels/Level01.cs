using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    class Level01 : Level
    {


        public Level01(Rectangle bounds)
            : base(bounds, LevelNumber.One)
        {
        }


        public override void Load(Textures.TextureName levelBackground)
        {
            base.Load(Textures.TextureName.Level01Background);

            //-------------------------------------
            //--------------- BOSS ----------------
            //-------------------------------------
            boss = new Boss1();


            //-------------------------------------
            //--------------- ITEMS ---------------
            //-------------------------------------
            itemMan.SetLives(1);
            itemMan.SetWeapons(30, WeaponType.TripleCanon);


            //-------------------------------------
            //--------------- SHIPS ---------------
            //-------------------------------------

            List<Spawn> shipSpawns = new List<Spawn>();

            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1000, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1040, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1080, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1120, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));

            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1000, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1040, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1080, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1120, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));

            
            //for (int i = 0; i < 20; i++)
            //    shipSpawns.Add(new Spawn(SpawnTrigger.Head, PROP.random.Next(400, 2201), ShipType.demoEnemy4, new Vector2(PROP.random.Next(-200, 201), -100), new Vector2(0, 1), Difficulty.Medium));
            //for (int i = 0; i < 20; i++)
            //    shipSpawns.Add(new Spawn(SpawnTrigger.Head, PROP.random.Next(400, 2201), ShipType.demoEnemy3, new Vector2(PROP.random.Next(-200, 201), -100), new Vector2(0, 1), Difficulty.Medium));

            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2000, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Insanity));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2040, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Hard));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2080, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2120, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Easy));

            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2000, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Insanity));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2040, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Hard));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2080, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            //shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2120, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Easy));

            /************** Jose ADDED ************/
            //for (int i = 0; i < 20; i++)
            //    shipSpawns.Add(new Spawn(SpawnTrigger.Head, PROP.random.Next(400, 2201), ShipType.shipEnemy1, new Vector2(PROP.random.Next(-200, 201), -100), new Vector2(0, 1), Difficulty.Easy));

            for (int i = 0; i < 20; i++)
                shipSpawns.Add(new Spawn(SpawnTrigger.Head, PROP.random.Next(400, 2201), ShipType.shipEnemy2, new Vector2(PROP.random.Next(-200, 201), -100), new Vector2(0, 1), Difficulty.Easy));

            int Leadpos_Y;
            int Leadpos_X;

            //Squadron 1a
            Leadpos_Y = 2060;   // Big numbers -> Lower map pos / Small numbs -> Higher map pos
            Leadpos_X = 0 - 100;  //center of the viewport (0,0)

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y, ShipType.shipEnemy1, new Vector2(Leadpos_X, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy1, new Vector2(Leadpos_X - 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy1, new Vector2(Leadpos_X + 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 20, ShipType.shipEnemy1, new Vector2(Leadpos_X - 70, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 20, ShipType.shipEnemy1, new Vector2(Leadpos_X + 70, 0), new Vector2(0f, 1f), Difficulty.Easy));

            //Squadron 1b
            Leadpos_Y = 2100;   // Big numbers -> Lower map pos / Small numbs -> Higher map pos
            Leadpos_X = 0 + 100;  //center of the viewport (0,0)

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y, ShipType.shipEnemy1, new Vector2(Leadpos_X, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy1, new Vector2(Leadpos_X - 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy1, new Vector2(Leadpos_X + 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 20, ShipType.shipEnemy1, new Vector2(Leadpos_X - 70, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 20, ShipType.shipEnemy1, new Vector2(Leadpos_X + 70, 0), new Vector2(0f, 1f), Difficulty.Easy));

            //Squadron 2
            Leadpos_Y = 1940;
            Leadpos_X = 0;

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y, ShipType.shipEnemy2, new Vector2(Leadpos_X, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy2, new Vector2(Leadpos_X - 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy2, new Vector2(Leadpos_X + 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 20, ShipType.shipEnemy2, new Vector2(Leadpos_X - 70, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 20, ShipType.shipEnemy2, new Vector2(Leadpos_X + 70, 0), new Vector2(0f, 1f), Difficulty.Easy));

            //Squadron 3
            Leadpos_Y = 1874;
            Leadpos_X = 0;

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y, ShipType.shipEnemy2, new Vector2(Leadpos_X, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy2, new Vector2(Leadpos_X - 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy2, new Vector2(Leadpos_X + 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 20, ShipType.shipEnemy2, new Vector2(Leadpos_X - 70, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 20, ShipType.shipEnemy2, new Vector2(Leadpos_X + 70, 0), new Vector2(0f, 1f), Difficulty.Easy));


            //Squadron 4a
            Leadpos_Y =1780;   // Big numbers -> Lower map pos / Small numbs -> Higher map pos
            Leadpos_X = 0 - 120;  //center of the viewport (0,0)

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y, ShipType.shipEnemy3, new Vector2(Leadpos_X, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy3, new Vector2(Leadpos_X - 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy3, new Vector2(Leadpos_X + 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
 
            //Squadron 4b
            Leadpos_Y = 1780;   // Big numbers -> Lower map pos / Small numbs -> Higher map pos
            Leadpos_X = 0 + 120;  //center of the viewport (0,0)

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y, ShipType.shipEnemy3, new Vector2(Leadpos_X, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy3, new Vector2(Leadpos_X - 40, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y - 10, ShipType.shipEnemy3, new Vector2(Leadpos_X + 40, 0), new Vector2(0f, 1f), Difficulty.Easy));

            //Squadron 5
            Leadpos_Y = 1720;
            Leadpos_X = 0;

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y, ShipType.shipEnemy2, new Vector2(Leadpos_X, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y + 5, ShipType.shipEnemy2, new Vector2(Leadpos_X - 60, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y + 5, ShipType.shipEnemy2, new Vector2(Leadpos_X + 60, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y + 15, ShipType.shipEnemy2, new Vector2(Leadpos_X - 80, 0), new Vector2(0f, 1f), Difficulty.Easy));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, Leadpos_Y + 15, ShipType.shipEnemy2, new Vector2(Leadpos_X + 80, 0), new Vector2(0f, 1f), Difficulty.Easy));

            /************** Jose ADDED ************/

            slider.AddSpawns(shipSpawns);
        }


    }
}
