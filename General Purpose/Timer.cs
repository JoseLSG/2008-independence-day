using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    class Timer
    {
        private int interval;   //then timespan between ticks
        private int counter;    //the timespan accumulator
        private bool running;   //tells update whether to count time or not

        //Construction
        //pass the interval of time between ticks
        public Timer(int timeSpan)
        {
            interval = timeSpan;
            counter = 0;

            running = false;
        }

        //Starts the timer
        public void Start()
        {
            running = true;
        }

        //Stops the timer and resets the accumulated time
        public void Stop()
        {
            running = false;
            counter = 0;
        }

        public void Pause()
        {
            running = false;
        }

        public void Resume()
        {
            running = true;
        }


        //Returns true if timer was activated
        public bool Update(GameTime gameTime)
        {
            //If clock is turned off, do nothing
            if (!running)
                return false;

            //Accumulted time since last update
            counter += gameTime.ElapsedGameTime.Milliseconds;

            //Check if accumulator has reached (or exceeded) the desired interval
            if (counter >= interval)
            {
                counter = counter % interval;
                return true;
            }

            return false;
        }

        //Get whether this timer is started or not
        public bool Running
        {
            get { return running; }
        }

    }
}
