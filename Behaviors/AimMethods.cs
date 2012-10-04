using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    //Control the rotation of a weapon
    interface IAimMethod
    {
        void Aim(Ship ship, int port, ShipUpdateInfo info);
    }

    
    //LINEAR
    //Always aim the same direction that the ship is moving
    class AMLinear : IAimMethod
    {
        public void Aim(Ship ship, int port, ShipUpdateInfo info)
        {
            ship.SetWeaponRotation(port, ship.rotation);
        }
    }


    //CARDINAL AIMING
    //Your choice of always aiming left, right, up or down
    class AMCardinal : IAimMethod
    {
        private Vector2 dir;

        public AMCardinal(Cardinal direction)
        {
            switch (direction)
            {
                case Cardinal.Down:
                    dir = VecUtil.GetNormDown();
                    break;
                case Cardinal.Up:
                    dir = VecUtil.GetNormUP();
                    break;
                case Cardinal.Left:
                    dir = VecUtil.GetNormLeft();
                    break;
                case Cardinal.Right:
                    dir = VecUtil.GetNormRight();
                    break;
            }
        }

        public void Aim(Ship ship, int port, ShipUpdateInfo info)
        {
            ship.SetWeaponRotation(port, dir);
        }
    }


    //Nearest Player
    class AMNearestPlayer : IAimMethod
    {
        //Needed for the players
        private Properties PROP = Properties.GetInstance();

        public void Aim(Ship ship, int port, ShipUpdateInfo info)
        {
            //get player ship position
            Vector2 dif = PROP.players[0].ship.position;
            
            //get weapon position
            Vector2 wepPos = ship.GetWeaponPosition(port);

            //subtract to get Change vector
            dif.X -= wepPos.X;
            dif.Y -= wepPos.Y;

            dif.Normalize();    //normalize for direciton

            ship.SetWeaponRotation(port, dif);
        }
    }


}
