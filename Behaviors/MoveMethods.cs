using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    interface IMoveMethod
    {
        //Defines the GameObject rotations each frame
        void Move(GameObject obj, ShipUpdateInfo info);
    }


    //MANUAL -- PLAYER CONTROLLER
    class MMManual : IMoveMethod
    {
        private Player player;  //the player that controlls this ship

        public MMManual(Player controller)
        {
            player = controller;
        }

        public void Move(GameObject obj, ShipUpdateInfo info)
        {
            //Get the direction from the player (controller)
            Vector2 direction = player.input.ShipMoveDirNormal();

            //Check is the player is trying to move
            //Since update (GameObject) will try and move the ship
            //if player is not trying to move we flag it false
            if (direction.X == 0 && direction.Y == 0)
                obj.moving = false;
            else
	        {
                obj.moving = true;             //re-flag true in case set to false

                direction.Normalize();          //just in case....
                obj.SetRotation(direction);

                //Since SetRotation() above, also turns the "face" direction
                //we need to correct this for the human players whose ships
                //always face directly upwards on the screen
                obj.SetFaceDir(VecUtil.GetNormUP());
	        }
        }
    }


    //LINEAR MOVE
    //Keeps the ship moving in a straight line
    class MMLinear : IMoveMethod
    {
        public void Move(GameObject obj, ShipUpdateInfo info)
        {
            //don't need to change anything


        }
    }

    //MOVE CARDINAL
    //Makes the ship move exactly left, right, up or down as provided
    class MMCardinal : IMoveMethod
    {
        //For performance we flag done once ship is set to the direction
        //this removes the semi-expensive call to SetRotation()
        bool done = false;

        private Vector2 dir;    //left, right, up, or down, in Vector2 form

        public MMCardinal(Cardinal direction)
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

        public void Move(GameObject obj, ShipUpdateInfo info)
        {
            if (done)   //performance reasons
                return;

            done = true;
            obj.SetRotation(dir);
        }
    }



    //MOVE...down... sort of
    //As soon as ship is within 100 units of the center of the viewport
    //then set its direction as straight down
    class MMDownWithin100ofCenterX : IMoveMethod
    {
        //For performance we flag done once ship is set to move down
        //this removes the semi-expensive call to SetRotation()
        bool done = false;

        public void Move(GameObject obj, ShipUpdateInfo info)
        {
            if (done)
                return;

            if (obj.position.X > (info.viewport.Left + 100) &&
                obj.position.X < (info.viewport.Right - 100))
            {
                done = true;
                obj.SetRotation(VecUtil.GetNormDown());
            }

        }
    }


    //STOPPED
    //IE: don't move at all
    class MMStop : IMoveMethod
    {
        public void Move(GameObject obj, ShipUpdateInfo info)
        {
            obj.moving = false;
        }
    }



    //SNAKE FAST
    //turns turns left then right
    //switches every 2 second, and rotates 15 degees per second
    //direction relative to orriginal travelling direction IE: can snake sideways if u want
    class MMSnakeFast : IMoveMethod
    {
        private double rotateSpeed; //30 degrees per second
        private int clock = 1000;                                               //2 second intervals
        private int elapsed = 500;                                                //trigger for direction change
        private float sign = 1;                                                 //positive rotation initially
        private double angle = 0f;                                              //angle to rotate each frame


        public MMSnakeFast(double radAnglePerSec, int tickTime)
        {
            rotateSpeed = radAnglePerSec;
            clock = tickTime;
            elapsed = (int)((float)clock / 2.0f);
        }


        public void Move(GameObject obj, ShipUpdateInfo info)
        {
            int time = info.gameTime.ElapsedGameTime.Milliseconds;
            elapsed -= time;

            angle = (double)time * rotateSpeed * sign;

            if (elapsed <= 0)   //time to change direction
            {
                sign *= -1;         //flip sign
                elapsed = clock;    //reset clock
            }

            obj.Rotate((float)angle);
            
        }
    }


    //Move left then right for certain ammount of time
    //always face the bottom of the screen
    class MMLeftRightStrafe : IMoveMethod
    {
        private Timer2 timer;

        public MMLeftRightStrafe(int time1, int time2)
        {
            timer = new Timer2(time1, time2, true);
        }

        public void Move(GameObject obj, ShipUpdateInfo info)
        {
            timer.Update(info.gameTime);

            switch (timer.Current)
            {
                case Timer2.TimerNum.First:
                    obj.SetRotation(VecUtil.GetNormLeft());
                    break;

                case Timer2.TimerNum.Second:
                    obj.SetRotation(VecUtil.GetNormRight());
                    break;
            }

            obj.SetFaceDir(VecUtil.GetNormDown());
        }


    }

    /********* Jose ADDED *******************/

    /*
     * Turns ship towards cardinal point selected and speeds up
     * 
     * ONLY SE AND SW WORKING FOR NOW
     */
    class MMCardinalSpeedup : IMoveMethod
    {

        public enum speedUpDir
        {
            SE,
            SW,
            NE,
            NW,           
        }

        private Vector2 dir;
        private float sign;
        private double speed;
        private float Y_trigger;

        public MMCardinalSpeedup(speedUpDir direction, double speedUp, float Ytrigger)
        {
            speed = speedUp;
            Y_trigger = Ytrigger;
            switch (direction)
            {
                case speedUpDir.SE:
                    dir = new Vector2(0, 1);
                    sign = 1;
                    break;
                case speedUpDir.SW:
                    dir = new Vector2(0, 1);
                    sign = -1;
                    break;
                //case speedUpDir.NE:
                //    dir = new Vector2(0, -1);
                //    sign = 1;
                //    break;
                //case speedUpDir.NW:
                //    dir = new Vector2(0, -1);
                //    sign = -1;
                //    break;
            }
        }

        public void Move(GameObject obj, ShipUpdateInfo info)
        {


            if (obj.position.Y > (info.viewport.Top + Y_trigger))
            {

                dir.X += (float)0.002 * info.gameTime.ElapsedGameTime.Milliseconds * sign;
                
                obj.SetRotation(dir);
                obj.speed = speed;
                
                if (dir.X > sign)
                {
                    dir.X = 1;
                    return;

                }
            }
        }

        
        


    }

    class MMManeuver1 : IMoveMethod
    {
        private enum status
        {
            upTurn,
            downTurn,
            linearUp,
            linearDown,
            End

        }

        private status state = status.linearDown;
        private float turnRad;

        public void Move(GameObject obj, ShipUpdateInfo info)
        {
            switch (state)
            {
                case status.linearDown:
                    if (obj.position.Y > (info.viewport.Center.Y + 100))
                        state = status.upTurn;

                    break;

                case status.linearUp:
                    break;

                case status.upTurn:

                    if (turnRad > 180)
                    {
                        obj.SetRotation(new Vector2(-1,-1) );
                        state = status.linearUp;
                    }
                    

                    turnRad += (float)0.2*info.gameTime.ElapsedGameTime.Milliseconds;
                    obj.SetRotation(MathHelper.ToRadians(turnRad) );
                    obj.speed = 0.3;


                    

                    break;

                case status.downTurn:

                    break;

                case status.End:

                    break;

            }
        }


    }

    class MMSnakeFastB : IMoveMethod
    {
        private double rotateSpeed; //30 degrees per second
        private int clock = 1000;                                               //2 second intervals
        private int elapsed = 500;                                                //trigger for direction change
        private float sign = 1;                                                 //positive rotation initially
        private double angle = 0f;                                              //angle to rotate each frame

        Vector2 dir = new Vector2(0, 1);

        public MMSnakeFastB(double radAnglePerSec, int tickTime)
        {
            rotateSpeed = radAnglePerSec;
            clock = tickTime;
            elapsed = (int)((float)clock / 2.0f);
        }


        public void Move(GameObject obj, ShipUpdateInfo info)
        {
            int time = info.gameTime.ElapsedGameTime.Milliseconds;
            elapsed -= time;

            angle = (double)time * rotateSpeed * sign;

            if (elapsed <= 0)   //time to change direction
            {
                sign *= -1;         //flip sign
                elapsed = clock;    //reset clock
            }
            if (obj.position.Y > (info.viewport.Top + 500))
            {

                dir.X += (float)0.002 * info.gameTime.ElapsedGameTime.Milliseconds * sign;

                obj.SetRotation(dir);
                obj.speed = 0.35;

                if (dir.X > sign)
                {
                    dir.X = 1;
                    return;
                }
                return;
            }

            obj.Rotate((float)angle);

        }
    }

    /***************** jose ADDED END ***********/

} //Namespace
