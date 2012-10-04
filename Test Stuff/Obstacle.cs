//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ID
//{
//    /*
//     * Obstacle is just a Sprite, but its a name for a specific collection
//     * of sprites in each level.
//     * 
//     * An Obstacle is simply and object in the level that has collision.
//     * IE: The Players ship can crash into it, and Bullets should not pass 
//     * through of over them.
//     * 
//    */

//    class Obstacle : GameObject
//    {
//        //Content Managers
//        Textures TEXTURES = Textures.GetInstance();
//        Sounds SOUNDS = Sounds.GetInstance();


//        //Members
//        private ObstacleType type;          //the type to determine the texture

//        private int points;                 //the points your get for breaking this thing ?

//        private float health;               //for breakable objects ?

//        //example sound stuff
//        private Sounds.CueName soundHit;    //the sound it makes when hit by...something
//        private Sounds.CueName soundBreak;  //the sound it makes when it dies/explodes


//        //Default constructor
//        public Obstacle(Textures.TextureName obstacleTexture)
//            : base(obstacleTexture)
//        {
//            type = ObstacleType.Wall01; //set some default type
//        }


//        //Notice no Content manager
//        public void LoadContent()
//        {
//            switch (type)
//            { //example
//                case ObstacleType.Wall01:

//                    break;
//                default:
//                    break;
//            }
//        }

//        //TO DO:
//        //void Draw(SpriteBatch sb)....

//        //return points if obstacle destroyed, else 0
//        public int Hit(Bullet bulletThatHitMe)
//        {
//            //TO DO:
//            //example code
//            //
//            //SOUNDS.PlayCue(soundHit)
//            //
//            //health -= bullet.damage
//            //
//            //if health <= 0 
//            //    SOUNDS.PlayCue(soundBreak)
//            //    return points
//            //else
//            //    return 0.
//            //
//            //return 0 tells the collision engine that the obstacle is still alive

//            return 0;   //to compile
//        }





//    }
//}
