using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    /*
     * This is a record, storing all the information required to create a 
     * ship or obstacle during the game.
     * 
     * The Level slider keeps track of head and tail positions of the level.
     * During update, it will check all these spawn objects to determine
     * If the trigger area is in the viewport, it will create the Ship
     * or the Obstacle
     */


    class Spawn
    {
        public SpawnTrigger triggerType;    //Head or tail
        public int trigger;                 //Where in the levelslider to create this obstacle

        public ShipType shipType;           //The type of ship to create if SpawnType == Ship
        public ObstacleType obstacleType;   //The type of ship to create if SpawnType == Ship

        public Vector2 position;            //Position in screen coordinates    
            //If trigger type is Head, this is the offset from the TOP_CENTER of the viewport
            //If trigger type is Tail, this is the offset from the BOTTOM_CENTER of the viewport

        public Vector2 direction;           //Direction of face/move for the new GameObject


        public Difficulty difficulty = Difficulty.Medium;

        //Constructor Specific Difficulty
        public Spawn(
            SpawnTrigger triggerType, int triggerPosition,          //trigger stuff
            ShipType shipType, Vector2 position, Vector2 direction, //ship stuff
            Difficulty dif)
        {
            this.triggerType = triggerType;
            this.trigger = triggerPosition;

            this.shipType = shipType;
            this.position = position;
            this.direction = direction;

            this.difficulty = dif;
        }



    }
}
