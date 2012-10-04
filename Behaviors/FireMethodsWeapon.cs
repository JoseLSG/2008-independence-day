using System;
using System.Collections.Generic;
using System.Text;

namespace XnaID
{
    //
    // Determines when a weapon is ready to fire
    // 


    //FireMethods determine when to fire
    interface IFireMethodWeapon
    {
        bool Fire(ShipUpdateInfo info);
    }


    //Constant Time
    class FMWConstant : IFireMethodWeapon
    {
        private int chargeTime;
        private int charge;

        public FMWConstant(int miliseconds) //the miliseconds between shots
        {
            chargeTime = miliseconds;
            charge = 0;
        }

        public bool Fire(ShipUpdateInfo info)
        {
            //charge up the weapon
            charge -= info.gameTime.ElapsedGameTime.Milliseconds;

            if (charge <= 0)    //ready to fire
            {
                charge = chargeTime;    //cooldown time
                return true;
            }

            return false;
        }
    }


}
