using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;


namespace XnaID
{
    /*
     * The properties class is a singleton class.
     * 
     * The purpose behind it is to easily track and maintain certain
     * program properties throughought the entire project.
     * It can be thought of as the class of "Global Variables"
     * but made in a clean and efficient manner.
     * 
    */

    class Properties
    {
        //Rectangle representing the screen size
        //(X,Y) = 0,0 = top left 
        //(+width,+height) = monitor resolution = bottom right
        //Set at beginning of program in Game1 class
        public Rectangle screen = Rectangle.Empty;  


        //Exact center of the screen
        //IE: center of the screen rectangle (above)
        //Set at beginning of program in Game1 class
        public Vector2 screenCenter = Vector2.Zero;

        //General prupose random objects
        //Should be used for all randomality (that a word ?)
        //Perhaps we should seed this for testing......
        public Random random = new Random();


        //default to medium difficulty
        public static Difficulty difficulty = Difficulty.Medium;
        

        public List<Player> players = new List<Player>();   //players connected to the Xbox
        public InputHandler allInput;                       //input handler that checks all controllers


        #region Singleton Pattern
        private static Properties instance; //The ONLY instance of PROPERTIES

        private Properties()
        {
            players.Add(new Player(PlayerIndex.One));
            //players.Add(new Player(PlayerIndex.Two));

            //For Xbox
            if (GamePad.GetState(PlayerIndex.One).IsConnected)
                players.Add(new Player(PlayerIndex.One));
            if (GamePad.GetState(PlayerIndex.Two).IsConnected)
                players.Add(new Player(PlayerIndex.Two));
            if (GamePad.GetState(PlayerIndex.Three).IsConnected)
                players.Add(new Player(PlayerIndex.Three));
            if (GamePad.GetState(PlayerIndex.Four).IsConnected)
                players.Add(new Player(PlayerIndex.Four));

            PlayerIndex[] indexes = new PlayerIndex[players.Count];
            for (int i = 0; i < players.Count; i++)
                indexes[i] = players[i].ControllerIndex;

            allInput = new InputHandler(indexes);
        }

        public static Properties GetInstance()
        {
            if (instance == null)
                instance = new Properties();

            return instance;
        } 
        #endregion


        //Creates a ship for each player
        public void InitPlayers()
        {

            
            for (int i = 0; i < players.Count; i++)
            {
                //lives
                players[i].InitLives();
                players[i].LoadContent();
                players[i].deadTime.Stop();

                //Ships
                players[i].ship = Creator.CreateShip(ShipType.player, Vector2.Zero, Vector2.Zero);
                players[i].ship.moveMethod = new MMManual(players[i]);
                players[i].ship.fireMethod = new FMSManual(players[i]);

                switch (i)
                {
                    case 0:
                        players[i].Name = "Brian";
                        players[i].ship.tint = Color.TransparentBlack;
                        break;
                    case 1:
                        players[i].Name = "Tailor";
                        players[i].ship.tint = Color.LimeGreen;
                        break;
                    case 2:
                        players[i].Name = "Jose";
                        players[i].ship.tint = Color.Red;
                        break;
                    case 3:
                        players[i].Name = "Mazen";
                        players[i].ship.tint = Color.Red;
                        break;
                }

            }



        }





    }
}
