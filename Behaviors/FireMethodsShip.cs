using System;
using System.Collections.Generic;
using System.Text;

namespace XnaID
{
    //
    // Determines when the ship should try to fire their weapons.
    // 

    //FireMethods determine when to fire
    interface IFireMethodShip
    {
        bool Fire(ShipUpdateInfo info);
    }


    //Manual - Player Controlled
    class FMSManual : IFireMethodShip
    {
        private Player player;      //The Player that controls this ship

        public FMSManual(Player controller)
        {
            player = controller;
        }

        public bool Fire(ShipUpdateInfo info)
        {
            //check if player is trying to fire
            if (player.input.ShipFirePrimary())
                return true;

            return false;
        }
    }



    //ASAP
    //Shoot as fast as you can
    class FMSasap : IFireMethodShip
    {
        public bool Fire(ShipUpdateInfo info)
        {
            return true;    //always try and fire
        }
    }



    //Attempts to fire each frame for a certain time interval
    //then stops for an interval, and repeat...
    class FMSInterval : IFireMethodShip
    {
        int[] intervals = new int[2];   //various intervals
        int fireInt = 0;                //points to array above
        int counter = 0;

        public FMSInterval(int fireTime, int chargeTime)
        {
            intervals[0] = fireTime;     //fire time
            intervals[1] = chargeTime;   //don't fire time
        }

        public bool Fire(ShipUpdateInfo info)
        {
            counter -= info.gameTime.ElapsedGameTime.Milliseconds;

            if (counter <= 0)
            {
                fireInt = (fireInt == 0) ? 1 : 0;   //flip the counter
                counter = intervals[fireInt];       //reset couter with appropriate time interval
            }

            if (fireInt == 0)
                return true;

            return false;
        }
    }




}
