using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaID
{
    class Boss : Ship
    {
        //Sleep boss at the begining so he stays hidden form the level
        //only wake him up once the slider has stopped moving
        private bool sleep = true;

        //While the boss is moving from off the screen
        //onto the screen, he controls the movement
        //of his sub ships, one he stops moving,
        //(IE he is fully in the viewport) then the sub ships
        //begin moving according to their moveMethods
        private bool moving = true;

        //The sub-ships that the boss own
        //Note, these need not look like ships, move like ships
        //or anything else, this is just a sprite that can contain
        //weapons and behaviors bassically
        private List<Ship> subShips = new List<Ship>();


        protected HealthBar hp;
        

        public Boss(Textures.TextureName textureEnum)
            : base(textureEnum)
        {
            base.LoadContent();
        }

        //Start the boos ship moving into the vieport
        public void Begin()
        {
            sleep = false;
        }

        //Stop the boss ship from moving dow into the viewport
        public void StopMoving()
        {
            moving = false;
        }


        //Returns true if at least 1 ships exists
        private bool SubShipsRemaining()
        {
            return (subShips.Count > 0);
        }


        public override void Update(ShipUpdateInfo info)
        {
            if (!sleep) //do nothing while sleeping
            {
                if (moving) //while moving, fix subShip positions manually
                {
                    

                    //Override the boss move method
                    base.SetRotation(VecUtil.GetNormDown());
                    CenterInHoriz(info.viewport);

                    //move boss down at a fixed rate
                    float speed = 0.05f;
                    speed *= (float)info.gameTime.ElapsedGameTime.Milliseconds;
                    Vector2 v = new Vector2(0, speed);
                    base.Offset(v);

                    AdjustWeaponPos();

                    //base.Update(info, false);

                    //Override subship movement
                    FixSubShipPositions();
                }
                else  //once stopped moving allow sub ships to move themselves
                {
                    hp.MoveTopLeftTo(new Vector2(info.viewport.Left + 150f, info.viewport.Top + 30));
                    hp.SetHealth(this.health);

                    //Allow all the subShips to move
                    foreach (Ship s in subShips)
                        s.Update(info);

                    //Allow main boss ship to move according ot its move method
                    base.Update(info);
                }
            }
            
        }


        protected virtual void FixSubShipPositions()
        {
            //default, can be overridden (should be probably)
            foreach (Ship s in subShips)
            {
                s.MoveTo(this.position);
                s.AdjustWeaponPos();
            }
        }



        public override void Draw(SpriteBatch sb)
        {
            //draw sub ships
            foreach (Ship s in subShips)
                s.Draw(sb);

            //draw main boss
            base.Draw(sb);

            if (!moving)
                hp.Draw(sb);
        }


        public override List<Bullet> Shoot(ShipUpdateInfo info)
        {
            List<Bullet> toReturn = new List<Bullet>();

            //Sub ship bullets
            foreach (Ship s in subShips)
                toReturn.AddRange(s.Shoot(info));

            //main boss bullets
            toReturn.AddRange(base.Shoot(info));

            return toReturn;
        }


        public override int CheckCollision(ref List<Bullet> bullets)
        {
            //If there are sub ships, then we can't hit the boss yet
            //we must firt kill the sub ships
            if (SubShipsRemaining())
            {
                //Add sub ship points to the bosses points
                foreach (Ship s in subShips)
                    this.points += s.CheckCollision(ref bullets);

                //only return the total of all the points when the 
                //boss ship is finally destroyed
                return 0;
            }
            else //otherwise we allow bullets to hit the boss
                return base.CheckCollision(ref bullets);
        }


        public bool CanCollide
        {
            get { return !moving; }
        }


    }
}
