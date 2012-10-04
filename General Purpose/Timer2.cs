using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    /*
     * Timer2 is a timed switch
     * 
     * Given 2 time spans: ts1 and ts2
     * For the ts1 ammount of time, Update() will return true
     * Then for ts2 ammount of time, Update() will return false
     * 
     */ 


    class Timer2
    {
        //Outside visibility
        public enum TimerNum
        {
            First,
            Second
        };

        int span1;          //the true span
        int span2;          //the false span

        TimerNum toggle;    //flips between the 2 timers

        int counter;        //the time accumulator

        public Timer2(int timeSpan1, int timeSpan2)
        {
            span1 = timeSpan1;
            span2 = timeSpan2;
            counter = 0;
            toggle = TimerNum.First;
        }

        public Timer2(int timeSpan1, int timeSpan2, bool startHalf)
            : this(timeSpan1, timeSpan2)
        {
            if (startHalf)
                counter = (int)((float)span1 / 2.0f);
        }


        //Reset the timer back to beginning
        public void Reset()
        {
            toggle = TimerNum.First;
            counter = 0;
        }

        public TimerNum Update(GameTime gameTime)
        {
            //accumulate elapsed time
            counter += gameTime.ElapsedGameTime.Milliseconds;

            switch (toggle)
            {
                case TimerNum.First:

                    if (counter >= span1)           //span1 finished
                    {
                        counter %= span1;           //remove accumulated span1 time
                        toggle = TimerNum.Second;   //flip toggle
                    }
                    break;


                case TimerNum.Second:

                    if (counter >= span2)           //span2 finished
                    {
                        counter %= span2;           //remove accumulated span2 time
                        toggle = TimerNum.First;    //flip toggle
                    }
                    break;
            }

            return toggle;
        }

        public TimerNum Current
        {
            get { return toggle; }
        }


    }
}
