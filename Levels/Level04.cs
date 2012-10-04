using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    class Level04 : Level
    {


        public Level04(Rectangle bounds)
            : base(bounds, LevelNumber.Four)
        {
        }


        public override void Load(Textures.TextureName levelBackground)
        {
            base.Load(Textures.TextureName.Level04Background);

            //-------------------------------------
            //--------------- ITEMS ---------------
            //-------------------------------------
            itemMan.SetLives(1);
            itemMan.SetWeapons(2, WeaponType.TripleCanon);


            //-------------------------------------
            //--------------- SHIPS ---------------
            //-------------------------------------

            List<Spawn> shipSpawns = new List<Spawn>();

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1000, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1040, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1080, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1120, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1000, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1040, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1080, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 1120, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));


            for (int i = 0; i < 50; i++)
                shipSpawns.Add(new Spawn(SpawnTrigger.Head, PROP.random.Next(400, 2201), ShipType.demoEnemy2, new Vector2(PROP.random.Next(-200, 201), -100), new Vector2(0, 1), Difficulty.Medium));
            for (int i = 0; i < 50; i++)
                shipSpawns.Add(new Spawn(SpawnTrigger.Head, PROP.random.Next(400, 2201), ShipType.demoEnemy3, new Vector2(PROP.random.Next(-200, 201), -100), new Vector2(0, 1), Difficulty.Medium));

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2000, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2040, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2080, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2120, ShipType.demoEnemy, new Vector2(-350, 0), new Vector2(0.5f, 0.6f), Difficulty.Medium));

            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2000, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2040, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2080, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));
            shipSpawns.Add(new Spawn(SpawnTrigger.Head, 2120, ShipType.demoEnemy, new Vector2(350, 0), new Vector2(-0.5f, 0.6f), Difficulty.Medium));


            slider.AddSpawns(shipSpawns);
        }


    }
}
