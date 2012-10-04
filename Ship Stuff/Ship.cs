using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;

namespace XnaID
{
    class Ship : GameObject
    {
        //Global Managers
        Textures TEXTURES = Textures.GetInstance();
        Sounds SOUNDS = Sounds.GetInstance();
        Properties PROP = Properties.GetInstance();

        //Properties
        public ShipType type;
        public String name;
        public int health; //1 for players
        public int points; //0 for players

        private Color hitColor = Color.Red; //flash this color when ship is hit
        private int hitColorClock = 15;     //flash for this long
        private int hitColorCtr = 0;        //when not 0, ship is tinted as hitColor


        //Ship BEHAVIOR
        public IMoveMethod moveMethod;
        public IFireMethodShip fireMethod;

        private CollisionMethod collisionMethod = CollisionMethod.BoundingBox;

        //Weapons
        private int weaponPortCount = 0;
        private int weaponCount = 0;
        private Vector2[] weaponPort;       //location on shipImage for each port
        private Vector2[] weaponPortSave;   //Origin port positions (when ship not rotated)
        private Weapon[] weapons;           //the collection of weapons this ship has


        //Sounds
        public Sounds.CueName soundExplode;

        
        //Construction
        public Ship(Textures.TextureName shipTexture)
            : base(shipTexture)

        {
            //Prevent the level cleaner from removing this ship
            //until flagged true
            canClean = false;

            //Ship Default settings
            health = 1;
            points = 1;

            //Init data lists
            weaponPort = new Vector2[0];
            weaponPortSave = new Vector2[0];
            weapons = new Weapon[0];
        }


        //Make sure the weapon is positioned and rotated correctly
        //according to ship position, and AimMethod
        public void AdjustWeaponPos()
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                //possibly lost a weapon at some point
                //Or ship starts with more weapon ports then weapons
                if (weapons[i] == null)
                    continue;

                //Rotate the weapon position to match ship rotation
                Vector2 v = weaponPortSave[i];                //get orrignal offset
                VecUtil.Rotate((double)this.rotation, ref v);   //rotate it to match ship rotation
                this.weaponPort[i] = v;                       //update real offset
                
                //move weapon to new offset
                weapons[i].MoveTo(new Vector2(position.X + v.X, position.Y + v.Y));
            }
        }

        //Makes sure player ships cannot go outside the viewport rectangle
        //Pass the viewport rectangle to the ship
        public void AdjustShipPosition(ref Rectangle bounds)
        {
            float dy = 0f;
            float dx = 0f;

            //top
            if (base.bounds.Top < bounds.Top)
                dy = bounds.Top - base.bounds.Top;
            //bottom
            else if (base.bounds.Bottom > bounds.Bottom)
                dy = bounds.Bottom - base.bounds.Bottom;

            //left
            if (base.bounds.Left < bounds.Left)
                dx = bounds.Left - base.bounds.Left;
            //right
            else if (base.bounds.Right > bounds.Right)
                dx = bounds.Right - base.bounds.Right;

            base.Offset(new Vector2(dx, dy));

            AdjustWeaponPos();
        }


        public Vector2 GetPortOffset(int port)
        {
            if (port < 0 || port > (weaponPort.Length - 1) || weaponPort[port] == null)
                return Vector2.Zero; //bad port nbumber

            return weaponPort[port];
        }
        public Vector2 GetWeaponPosition(int port)
        {
            if (port < 0 || port > (weaponPort.Length - 1) || weaponPort[port] == null)
                return Vector2.Zero; //bad port nbumber

            Vector2 toReturn = this.position;
            toReturn.X += weaponPort[port].X;
            toReturn.Y += weaponPort[port].Y;

            return toReturn;
        }
        public void SetWeaponRotation(int port, float rotation)
        {
            if (port < 0 || port > (weapons.Length - 1))
                return;     //bad port number

            if (weapons[port] != null)
                weapons[port].SetRotation(rotation);
        }
        public void SetWeaponRotation(int port, Vector2 rotationVector)
        {
            if (port < 0 || port > (weapons.Length - 1))
                return;     //bad port number

            if (weapons[port] != null)
                weapons[port].SetRotation(rotationVector);
        }

        public void AddWeaponPort(Vector2 imageOffset)
        {
            //increase count of available weapon ports on the ship
            weaponPortCount++;

            //---------
            //Create the new weapon port

            //add a new port to the list
            Vector2[] newPorts = new Vector2[weaponPortCount];

            //add new port to the end of the list
            newPorts[weaponPortCount-1] = imageOffset;

            //Copy old ports into new list
            for (int i = 0; i < weaponPort.Length; i++)
                newPorts[i] = weaponPort[i];

            //replace old list with new list (new null element on the end)
            weaponPort = newPorts;


            //---------
            //Save orriginal position of port

            //make new list of port saves
            Vector2[] newPortSaves = new Vector2[weaponPortCount];

            //copy port saves into the new list
            for (int i = 0; i < weaponPortCount - 1; i++)
                newPortSaves[i] = weaponPortSave[i];

            //add the new save to the end
            newPortSaves[weaponPortCount - 1] = imageOffset;

            //replace old list with new
            weaponPortSave = newPortSaves;


            //---------
            //Now fix the size of the weapons array (add a null weapon to the end of the array)

            //make new weapon list same size as port
            Weapon[] newWeaponList = new Weapon[weaponPortCount];

            //copy the weapons into the new list
            for (int i = 0; i < (weaponPort.Length - 1); i++)
                newWeaponList[i] = weapons[i];

            //replace old weapons array with the new one that hass a nul;l on the end
            weapons = newWeaponList;
        }

        //Set a weapon port to a new weapon
        public void AddWeapon(Weapon newWeapon, int zeroBasePortNumber)
        {
            //Increase the count of the number of weapons attached to the ship
            weaponCount++;

            //Check if vali port number was given
            if (zeroBasePortNumber < 0 || zeroBasePortNumber > (weaponCount - 1))
                return;       //Invalid weapon port #

            //assign weapon to the port
            weapons[zeroBasePortNumber] = newWeapon;
        }

        //Find the first free port on the ship and put the weapon there
        //if no port is found, do nothing
        public void AddWeapon(Weapon newWeapon)
        {
            int emptyPort = -1; //port with no weapon assigned

            for (int i = 0; i < weaponPortCount; i++)
            {
                if (weapons[i] == null) //found empty port
                {
                    emptyPort = i;
                    break;
                }
            }

            //Check if no port found
            if (emptyPort == -1)    
                return;

            //Increase the count of the number of weapons attached to the ship
            weaponCount++;

            //assign weapon to the port
            weapons[emptyPort] = newWeapon;
        }



        public virtual List<Bullet> Shoot(ShipUpdateInfo info)
        {
            //list to accumulate bullets from each weapon on the ship
            List<Bullet> toReturn = new List<Bullet>();

            //Check the fire method to see if we should fire
            if (!fireMethod.Fire(info))
                return toReturn;

            foreach (Weapon w in weapons)
            {
                //make sure not an empty port
                if (w == null)
                    continue;

                //copy fixed bullets to return list and clear temp bullet
                toReturn.AddRange(w.Fire(info));
            }
            
            return toReturn;
        }


        public virtual void Update(ShipUpdateInfo info, bool UseMoveMethod)
        {
            if (UseMoveMethod)
            {
                //rotate the ship according to moveMethod
                moveMethod.Move(this, info);
            }

            //move the ship on screen
            base.Update(info.gameTime);

            //fix weapon position overlay on ship
            AdjustWeaponPos();

            //aim weapons according to aimMethod
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] == null)
                    continue;

                weapons[i].aimMethod.Aim(this, i, info);
            }

            //Update weapons to recharge them
            foreach (Weapon w in weapons)
                if (w != null)
                    w.Update(info.gameTime);


            //constantly
            if (hitColorCtr <= 0) //no longer flashing as hit
            {
                base.tint = Color.White;
                hitColorCtr = 0;
            }
            else //Still flashing as hit
            {
                hitColorCtr -= info.gameTime.ElapsedGameTime.Milliseconds;
                base.tint = hitColor;
            }
        }


        public virtual void Update(ShipUpdateInfo info)
        {
            Update(info, true);
        }


        //Save anchor positions, so that new weapon offsets can always
        //be calculated based on the originals
        public void SaveAnchorPositions()
        {
            weaponPortSave = new Vector2[weaponPort.Length];

            for (int i = 0; i < weaponPort.Length; i++)
            {
                weaponPortSave[i] = weaponPort[i];
            }
        }


        public override void Draw(SpriteBatch sb)
        {
            //draw the ship
            base.Draw(sb);
                
            //draw the weapons
            foreach (Weapon w in weapons)
                if (w != null)
                    w.Draw(sb);
        }


        //Returns the points of the ship if ship is destroyed
        //Otherwise return 0;
        //Note, making points = 0 on a ship now could be pretty dangerous
        //perhaps this is a poor design decision. And by perhaps I mean...
        //it certainly is, but hey, we're not all perfect. By the way, did you
        //hear the one about the chicken and the....
        public virtual int CheckCollision(ref List<Bullet> bullets)
        {
            int returnPoints = 0;
            List<Bullet> deadBullets = new List<Bullet>();

            foreach (Bullet b in bullets)
            {
                switch (collisionMethod)
                {
                    case CollisionMethod.BoundingBox:
                        if (b.bounds.Intersects(this.bounds))   
                        {                                       
                            returnPoints += Hit(b.Damage);      
                            deadBullets.Add(b);                 
                        }                                       
                        break;

                    case CollisionMethod.Radius:
                        //get the distance between the 2 gameObject in screen coords
                        float distanceBetween = Vector2.Subtract(this.position, b.position).Length();

                        //check if this distance is greatetr then the sum of the 2 radii
                        if (distanceBetween <= (this.collisionRadius + b.collisionRadius))
                        {
                            returnPoints += Hit(b.Damage);
                            deadBullets.Add(b);
                        }

                        break;
                }


                           
            }

            foreach (Bullet b in deadBullets)
                bullets.Remove(b);

            return returnPoints;
        }


        public void SetCollisionStyle(CollisionMethod style)
        {
            collisionMethod = style;
        }



        //Returns true if this ship collided with one of the ships passed to funciton
        public virtual bool CheckCollision(ref List<Ship> ships)
        {
            List<Ship> deadShips = new List<Ship>();

            foreach (Ship s in ships)
            {
                switch (collisionMethod)
                {
                    case CollisionMethod.BoundingBox:
                        if (s.bounds.Intersects(this.bounds))
                            deadShips.Add(s);
                        break;

                    case CollisionMethod.Radius:
                        //get the distance between the 2 gameObject in screen coords
                        float distanceBetween = Vector2.Subtract(this.position, s.position).Length();

                        //check if this distance is greatetr then the sum of the 2 radii
                        if (distanceBetween <= (this.collisionRadius + s.collisionRadius))
                            deadShips.Add(s);
                        break;
                }
            }

            foreach (Ship s in deadShips)
                ships.Remove(s);

            return (deadShips.Count > 0);   
            //if at least 1 ship died, count > 0 
            //so return true    ie: collision happened
        }



        //Returns lst of items that this ship has hit
        public virtual List<Item> CheckCollision(ref List<Item> items)
        {
            List<Item> deadItems = new List<Item>();

            foreach (Item i in items)
            {
                switch (collisionMethod)
                {
                    case CollisionMethod.BoundingBox:
                        if (i.bounds.Intersects(this.bounds))
                            deadItems.Add(i);
                        break;

                    case CollisionMethod.Radius:
                        //get the distance between the 2 gameObject in screen coords
                        float distanceBetween = Vector2.Subtract(this.position, i.position).Length();

                        //check if this distance is greatetr then the sum of the 2 radii
                        if (distanceBetween <= (this.collisionRadius + i.collisionRadius))
                            deadItems.Add(i);
                        break;
                }
            }

            return deadItems;
        }





        //Ship has been hit, or did it, something
        private int Hit(int damage)
        {
            health -= damage;

            if (health <= 0)
            {
                //play ship destroyed sound
                //

                return points;
            }
            else
            {
                //play shit hit sound ?
                //

                //change tint color
                hitColorCtr = hitColorClock;

                return 0;
            }
        }


        public virtual String ToString()
        {
            //Name of the ship
            String s = name;

            //weapons
            s += "\n\nWeapons:\n";
            int count = 0;
            foreach (Weapon w in weapons)
            {
                if (w != null)
                {
                    count++;

                    s += "  " + count + " - " + w.name + "\n";
                }
            }


            return s;
        }





    }
}
