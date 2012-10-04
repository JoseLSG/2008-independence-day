using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    /*
     * Because of various ship behaviors, there are many different places 
     * (classes and functions) that handle the updating of a ship. Since they
     * all should get the same information, we define this class to store the 
     * information. This way every time we add something, we only add it in
     * this one place
     */ 

    class ShipUpdateInfo
    {
        public GameTime gameTime;   //GameTtime elapsed since last Update Call

        public List<Ship> npcs;     //All the enemy ships on screen

        public Rectangle viewport;  //The rectangular bounds of the level

        public LevelSlider slider;  //the sliding window for "this" level
    }
}
