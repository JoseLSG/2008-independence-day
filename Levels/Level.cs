using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;


namespace XnaID
{
    class Level
    {
        //Content Managers
        protected Fonts FONTS = Fonts.GetInstance();
        protected Textures TEXTURES = Textures.GetInstance();
        protected Sounds SOUNDS = Sounds.GetInstance();
        protected Properties PROP = Properties.GetInstance();

        //The bounds of the level
        //IE the VIEWPORT
        private Rectangle bounds;

        //The sprite border of the viewport
        //this is used to hide all the game objects that are outside
        //or overlapping the viewport.
        private GameObject border;

        //The level only cares about 4 states, all others are handled by the engine
        //Intro - update / draw the intro movie
        //Play - playing the game, update and draew everything in game
        //Pause - game is playing, but paused, draw but no update
        //Final - update and draw the closing movie
        public GameState state = GameState.Intro;

        public LevelNumber levelNumber;     //from enum

        //The level background (top scrolling sliding window)
        public LevelSlider slider;

        //The level Data structures
        protected List<Ship> enemyShips;        //NPS ships
        protected List<Bullet> enemyBullets;    //NPC bullets
        protected List<Item> items;


        //The boss ship
        protected Boss boss;
        private bool bossActive = false;


        //Items
        protected ItemManager itemMan;
        

        //Update information passed to each ship during update
        protected ShipUpdateInfo SUI;   

        //Mem management
        private bool loaded = false;


        //Contruction
        //should not contain very much stuff
        //since mostly the stuff is loaded during Load() function
        public Level(Rectangle bounds, LevelNumber fromEnum)
        {
            this.bounds = bounds;
            this.levelNumber = fromEnum;
        }


        //The main loading of the level
        //Child classes (the actuall level objects)
        //should call this base method first to Init the basic stuff
        public virtual void Load(Textures.TextureName levelBackground)
        {
            loaded = true;

            border = new GameObject(Textures.TextureName.LevelBorder);
            border.LoadContent();
            border.CenterIn(bounds);

            slider = new LevelSlider(levelBackground, bounds);
            slider.Load();

            //Items
            itemMan = new ItemManager(ref slider);


            //Init data structures
            enemyShips = new List<Ship>();
            enemyBullets = new List<Bullet>();
            items = new List<Item>();


            //Link the UpdateInfo to appropriate data
            SUI = new ShipUpdateInfo();
            SUI.viewport = bounds;
            SUI.npcs = enemyShips;
            SUI.slider = this.slider;


            //Position the player ships
            foreach (Player p in PROP.players)
            {
                if (p.ControllerIndex == PlayerIndex.Two)
                {
                    p.ship.MoveTo(GetPlayerInitPos());
                    p.ship.position.X += 60;
                }
                else
                    p.ship.MoveTo(GetPlayerInitPos());
            }

        }

        //The position in screen coordinates where the player ship 
        //starts, or appears after dying
        protected Vector2 GetPlayerInitPos()
        {
            return new Vector2(
                (float)bounds.X + (float)bounds.Width / 2.0f,   //X - centered in level viewport
                (float)bounds.Y + (float)bounds.Height - 90f);  //Y - 90 up from bottom of viewport
        }


        //Unload the level
        //clear all data for memory management reasons
        public virtual void Unload()
        {
            state = GameState.Unloaded;

            slider = null;
            SUI = null;

            if (loaded)
            {
                enemyShips.Clear();
                enemyBullets.Clear();
            }

            foreach (Player p in PROP.players)
                p.bullets.Clear();
        }


        public virtual void Draw(SpriteBatch sb)
        {

            switch (state)
            {
                case GameState.Intro:   //-----------------------------------------------
                    break;

                case GameState.Final:   //-----------------------------------------------
                    break;

                case GameState.Dead:
                case GameState.Pause:   //-----------------------------------------------
                case GameState.Play:    //-----------------------------------------------

                    //the scrolling background
                    slider.Draw(sb, true);

                    //Enemy ships and bullets
                    foreach (Ship s in enemyShips)
                        s.Draw(sb);
                    foreach (Bullet b in enemyBullets)
                        b.Draw(sb);

                    //Players
                    //Draw their ships and their bullets
                    foreach (Player p in PROP.players)
                        p.Draw(sb);

                    
                    //Draw the items
                    foreach (Item i in items)
                    {
                        i.Draw(sb);
                    }


                    //BOSS
                    if (bossActive)
                        boss.Draw(sb);

                
                    //level "viewport" border
                    //** needs to be draw after everything but the HUD
                    //border.Draw(sb, true);

                        
                    //HUD Display
                    Vector2 v = new Vector2(bounds.X - 120, bounds.Y);
                    foreach (Player p in PROP.players)
                    {
                        sb.DrawString(FONTS.GetFont(Fonts.FontName.Arial10B), p.ToString(), v, Color.White);
                        v.Y += 200;
                    }
                    //Show level @ top center
                    sb.DrawString(FONTS.GetFont(Fonts.FontName.Arial10B), "Level: " + ((int)levelNumber+1), new Vector2(bounds.X - 50 + (float)bounds.Width / 2.0f, bounds.Top - 20), Color.White);
                    
                    //Show player lives
                    DisplayLives(sb);


                    break;
            }
        }

        public virtual GameState Update(GameTime time)
        {
            switch (state)
            {
                case GameState.Intro:
                    //if movie sitll playing
                    return GameState.Intro;
                    //else if Move is finished
                    //return Gamestate.play...

                case GameState.Pause:
                    return GameState.Pause;

                case GameState.Dead:
                    //make sure player ships do not go outside the viewport
                    foreach (Player p in PROP.players)
                        p.ship.AdjustShipPosition(ref this.bounds);
                    return GameState.Dead;

                case GameState.Final:
                    //if movie sitll playing....
                    return GameState.Final;
                    //else if movie is finished
                    //return GameState.Finished


                case GameState.Play:

                    //Update the Ship updater Object with new gametime
                    SUI.gameTime = time;

                    //Check for Cheats on all inputs
                    if (PROP.allInput.SpeedUp())
                        slider.SetSpeed(0.3f);
                    else if (PROP.allInput.SlowDown())
                        slider.SetSpeed(0.03f);

                    //Update the Slider - will move background
                    //and trigger any new ships / obstacles
                    slider.Update(time);

                    //Get any ships / obstacles from the slider
                    enemyShips.AddRange(slider.GetShips());


                    //Update all the player ships and bullets
                    foreach (Player p in PROP.players)
                    {
                        p.Update(time);

                        p.ship.Update(SUI);

                        foreach (Bullet b in p.bullets)
                        {
                            b.Update(time, SUI);
                        }

                        //make sure player ships do not go outside the viewport
                        p.ship.AdjustShipPosition(ref this.bounds);
                    }


                    //Update all the enemy ships and bullets
                    foreach (Ship s in enemyShips)
                    {
                        if (s == null)
                            continue;

                        s.Update(SUI);
                    }
                    foreach (Bullet b in enemyBullets)
                    {
                        if (b != null)
                            b.Update(time, SUI);
                    }


                    //See if players are trying to fire
                    foreach (Player p in PROP.players)
                        p.bullets.AddRange(p.ship.Shoot(SUI));

                    //Allow eney ships to fire
                    foreach (Ship s in enemyShips)
                    {
                        if (s == null)
                            continue;

                        enemyBullets.AddRange(s.Shoot(SUI));
                    }

                    //ITEMS
                    foreach (Item i in items)
                        i.Update(time);

                    //Check Collisions
                    CheckEnemyCollisions();

                    //Check player collissions
                    if (!CheckPlayerCollisions())
                        return GameState.Dead;

                    //Remove objects outsideof the viewport
                    CleanLevel();

                    //set HUD positions
                    PositionLives();


                    //See if the slider is still moving along the level
                    if (!slider.moving)
                        if (boss.health <= 0)
                            return GameState.Final;
                        else
                        {
                            //If boss is not yet active, move him into
                            //position and begin his descent into the viewport
                            if (!bossActive)
                            {
                                //move boss to proper position on screen
                                boss.MoveTo(new Vector2((float)bounds.Left + (float)bounds.Width / 2f, (float)bounds.Top - 100f));

                                //start the boss moving
                                boss.Begin();

                                bossActive = true;
                            }
                            else
                            {
                                //Stop the boss decending when he reaches certain point
                                if (boss.position.Y > (bounds.Top + 100))
                                    boss.StopMoving();

                                boss.Update(SUI);

                                //check is player has hit the boss
                                //Cannt hit the boss until he has sotpped moving onto the screen
                                //Boss cannot fire until stopped moving also
                                if (boss.CanCollide)
                                {
                                    CheckBossCollisions();
                                    enemyBullets.AddRange(boss.Shoot(SUI));
                                }
                            }

                            return GameState.Play;
                        }
                    else //slider still moving, state still play
                        return GameState.Play;



                default:
                    return GameState.Play;
            }
        }


        private void CheckBossCollisions()
        {
            foreach (Player p in PROP.players)
                p.AddScore(boss.CheckCollision(ref p.bullets));
        }


        private void CheckEnemyCollisions()
        {
            int newPoints;
            List<Ship> deadShips = new List<Ship>();


            //--------- Enemy Ships vs Friendly Bullets
            foreach (Ship s in enemyShips)
            {
                if (s == null)
                    continue;

                foreach (Player p in PROP.players)
                {
                    newPoints = s.CheckCollision(ref p.bullets);

                    if (newPoints != 0) //enemy ship returned points, so it is dead now
                    {
                        deadShips.Add(s);           //flag ship as "to remove"
                        p.Score += newPoints; //add points to player score

                        TryItem(s.position);
                    }
                }

            }
            //Remove all ships flagged as "to remove"
            foreach (Ship s in deadShips)
                enemyShips.Remove(s);
        }


        //Ask the Item Manager for an item
        private void TryItem(Vector2 itemPosition)
        {
            Item i = itemMan.GetItem();
            if (i != null)
            {
                i.MoveTo(itemPosition);
                items.Add(i);
            }
        }



        //See if a player has been hit by an enemy bullet
        //Or if a player has hit a ship or obstacle
        //Or is a player has hit (collected) an item
        private bool CheckPlayerCollisions()
        {
            bool livesRemaining = false;

            foreach (Player p in PROP.players)
            {
                //ITEMS
                List<Item> toUse = p.ship.CheckCollision(ref items);
                foreach (Item i in toUse)
                {
                    p.UseItem(i);
                    itemMan.ConsumeItem(i.Type);
                }
                for (int i = 0; i < toUse.Count; i++)
                    items.Remove(toUse[i]);

                //Dead time, ignore collisions
                if (p.deadTime.Running)
                    return true;

                //Check vs ENEMY BULLETS
                if (p.ship.CheckCollision(ref enemyBullets) != 0)
                {
                    p.TakeLife();
                    p.ship.MoveTo(this.GetPlayerInitPos());
                }

                //Check vs ENEMY SHIPS
                if (p.ship.CheckCollision(ref enemyShips))
                {
                    p.TakeLife();
                    p.ship.MoveTo(this.GetPlayerInitPos());
                }


                if (p.lives.Count > 0)
                    livesRemaining = true;
            }

            return livesRemaining;
        }


        //Removes any objects that have moved outside the viewport
        private void CleanLevel()
        {
            //CLEANUP
            //Remove anything that has gone off the screen
            Rectangle r = bounds;

            //BULLETS
            foreach (Player p in PROP.players)
            {
                for (int i = 0; i < p.bullets.Count; i++)
                {
                    if (!r.Intersects(p.bullets[i].bounds))
                    {
                        p.bullets.RemoveAt(i);
                        --i;
                    }
                }
            }
            for (int i = 0; i < enemyBullets.Count; i++)
            {
                if (!r.Intersects(enemyBullets[i].bounds))
                {
                    enemyBullets.RemoveAt(i);
                    --i;
                }
            }

            //SHIPS
            for (int i = 0; i < enemyShips.Count; i++)
            {
                if (!r.Intersects(enemyShips[i].bounds))
                {
                    //If ship has never been in the level viewport
                    //then don't kill it yet, wait for it to unflag
                    if (!enemyShips[i].canClean)
                        continue;

                    enemyShips.RemoveAt(i);
                    --i;
                }
                else
                    enemyShips[i].canClean = true;
            }


            //ITEMS
            for (int i = 0; i < items.Count; i++)
            {
                if (!items[i].bounds.Intersects(r))
                {
                    items.RemoveAt(i);
                    --i;
                }
            }

        }


        //Move the life objects (sprites) to the top left of the viewport
        private void PositionLives()
        {
            const int horizOffset = 15;

            Vector2 pos = new Vector2(bounds.X + 15f, bounds.Y + 15f);

            foreach (Player p in PROP.players)
            {
                for (int i = 0; i < p.lives.Count; i++)
                {
                    p.lives[i].MoveTo(pos);

                    pos.X += horizOffset;
                }
            }
        }

        //draw the lives of the players
        private void DisplayLives(SpriteBatch sb)
        {
            foreach (Player p in PROP.players)
            {
                foreach (Life l in p.lives)
                    l.Draw(sb);
            }
        }




        public enum Cheats
        {
            ClearEnemyBullets,
            ClearEnemyShips
        };
        public void CHEAT(params Cheats[] cheatOps)
        {
            foreach (Cheats code in cheatOps)
            {
                switch (code)
                {
                    case Cheats.ClearEnemyBullets:
                        enemyBullets.Clear();
                        break;

                    case Cheats.ClearEnemyShips:
                        enemyShips.Clear();
                        break;
                }
            }
        }





    }
}
