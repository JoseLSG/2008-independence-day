using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace XnaID
{
    /*
     * The Input handler class is just to save some typing
     * and also to allow changing buttons in the options
     * screen. (custom button bindings)
    */

    public class InputHandler
    {
        //Gamepad devices and associated states
        //Function calls will -OR- the result of each device
        //IE: MenuNext() checks all the controllers, and if ANY are true, then return true
        //This helps, for example this way any player can controll the menus
        public PlayerIndex[] devices;
        public GamePadState[] oldStates;
        public GamePadState[] newStates;


        //FOR NOW WHILE USING PC
        private KeyboardState keyState;  //the current state of the gamepad
        private KeyboardState oldKeyState;  //The previous state of the gamepad
        private Keys firePrimary;
        private Keys fireSecondary;


        //Ship movement
        private Keys keyLeft = Keys.NumPad4;
        private Keys keyRight = Keys.NumPad6;
        private Keys keyUp = Keys.NumPad8;
        private Keys keyDown = Keys.NumPad5;
        

        //Constructor
        //Setup our default button / control binding here
        public InputHandler(params PlayerIndex[] controllers)
        {
            devices = new PlayerIndex[controllers.Length];
            oldStates = new GamePadState[controllers.Length];
            newStates = new GamePadState[controllers.Length];


            //Default bindings
            firePrimary = Keys.Tab;
            fireSecondary = Keys.Space;
        }


        //During gloable Update() the module manager (Game1 class)
        //will update this inpute handler if it is the active
        //Module.
        public void Update()
        {
            //FOR NOW WHILE USING PC
            oldKeyState = this.keyState;
            this.keyState = Keyboard.GetState();

            //Update the relevant Gamepad controller(s) state
            for (int i = 0; i < devices.Length; i++)
            {
                oldStates[i] = newStates[i];    //save state as previous state
                newStates[i] = GamePad.GetState(devices[i]);
            }
        }


        //Captures a key being release
        //IE: the old state showed the key as being down
        //    The new state shows the key as being up
        //PRIVATE !!!! (please)
        private bool Release(Keys key)
        {
            return (oldKeyState.IsKeyDown(key) &&
                    keyState.IsKeyUp(key));
        }

        //Captures a key being pressed for the first time
        //IE: The old state has key being Up
        //    The current (new) state has key being down
        //PRIVATE !!!! (please)
        private bool Press(Keys key)
        {
            return (oldKeyState.IsKeyUp(key) &&
                    keyState.IsKeyDown(key));
        }




        //---------- Properties for BUTTON/CONTROL BINDING ----------

        public Keys FirePrimaryBinding
        {
            get { return firePrimary; }
            set { firePrimary = value; }
        }

        public Keys FireSecondaryBinding
        {
            get { return fireSecondary; }
            set { fireSecondary = value; }
        }



        //------------ MAIN FUNCTIONS ------------------


        //General Purpose back button for modules
        public bool Back()
        {
            return Release(Keys.Escape);
        }

        //Pause...something
        public bool Pause()
        {
            return Press(Keys.P);
        }

        //Select the next item (DOWN) in a menu
        public bool MenuNext()
        {
            return Press(Keys.Down);
        }

        //The active menu has been selected
        public bool SelectMenuItem()
        {
            return Press(Keys.Enter);
        }

        //Select the previous item (UP) in a menu
        public bool MenuPrevious()
        {
            return Press(Keys.Up);
        }

        //Select the next sub-item in a menu (to the right of the selected [sub] menu item)
        public bool MenuRight()
        {
            return false;
        }

        //Select the previous sub-item in a menu (to the left of the selected [sub] menu item)
        public bool MenuLeft()
        {
            return false;
        }

        //Player trying to fire his primary weapon sytem
        public bool ShipFirePrimary()
        {
            return keyState.IsKeyDown(Keys.Space);
        }

        //When game is paused this is the key to show the options menu
        public bool ShowOptions()
        {
            return Press(Keys.O);
        }

        //When game is paused this is the key to show the options menu
        public bool ShowInstructions()
        {
            return Press(Keys.I);
        }


        public bool SpeedUp()
        {
            return Press(Keys.Add);
        }

        public bool SlowDown()
        {
            return Press(Keys.Subtract);
        }

        //Get a normalized vector for player movement
        public Vector2 ShipMoveDirNormal()
        {
            Vector2 v = Vector2.Zero;
            bool move = false;

            if (keyState.IsKeyDown(keyLeft))
            {
                v.X = -1;
                move = true;
            }
            else if (keyState.IsKeyDown(keyRight))
            {
                v.X = 1;
                move = true;
            }


            if (keyState.IsKeyDown(keyUp))
            {
                v.Y = -1;
                move = true;
            }
            else if (keyState.IsKeyDown(keyDown))
            {
                v.Y = 1;
                move = true;
            }

            if (move)
                v.Normalize();

            return v;
        }


        public bool CheatClearEnemyBullets() { return Press(Keys.F5); }
        public bool CheatClearEnemyShips() { return Press(Keys.F6); }
        public bool CheatSkipLevel() { return Press(Keys.F7); }




    }
}
