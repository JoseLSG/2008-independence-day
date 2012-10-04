using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

namespace XnaID
{
    /*
     * The player Class denotes a human game player
     * 
     * A player will always have a ship, keybindings, score, etc...
     * 
     * This class encapsulates all this information
     * 
     */ 


    class Player
    {
        private PlayerIndex index;
        private String name;
        private int score;              //the total score for the player

        public List<Life> lives = new List<Life>();

        public Ship ship;               //The player's ship
        public List<Bullet> bullets;    //this players bullets

        public InputHandler input;      //input state for controller


        public Timer deadTime = new Timer(3000);        //after being killed, make invulnerable for 3 seconds
        public Timer2 showDead = new Timer2(80, 80);  //when dead, draw ship for 100ms, then don't draw for 100ms etc..

        
        public bool connected = false;


        //Construction
        public Player(PlayerIndex index)
        {
            connected = true;
            this.index = index;
            score = 0;

            input = new InputHandler(index);

            bullets = new List<Bullet>();

            InitLives();
        }

        public void InitLives()
        {
            //Initial # of lives\
            lives.Clear();
            int initLives = 15;
            for (int i = 0; i < initLives; i++)
                lives.Add(new Life());
        }


        //Set or gets the controller number
        public PlayerIndex ControllerIndex
        {
            get { return index; }
            set { index = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Lives
        {
            get { return lives.Count; }
        }

        public void GiveLife()
        {
            Life l = new Life();
            l.LoadContent();
            lives.Add(l);
        }

        public void TakeLife()
        {
            if (lives.Count > 0)
                lives.RemoveAt(0);
            
            deadTime.Start();   //begin the 2 second invulnerability timer
        }


        public void AddScore(int points)
        {
            score += points;
        }



        //Show the player details including, name, ship name, ship weapons etc...
        public String ToString()
        {
            String s = "Player: " + name;

            s += "\n\nScore: " + score;

            if (ship != null)
            {
                s += "\n\nShip: " + ship.ToString();
            }

            return s;
        }

        public void Update(GameTime gameTime)
        {
            if (deadTime.Update(gameTime))  //if deadTimer has ended
                deadTime.Stop();    //stop the invulnerability timer

            showDead.Update(gameTime);  //toggle the showdead timer repeatedly

        }

        public void Draw(SpriteBatch sb)
        {
            if (deadTime.Running)
            {
                if (showDead.Current == Timer2.TimerNum.First)
                {
                    ship.tint = Color.White;
                    ship.Draw(sb);
                }
            }
            else
            {
                ship.tint = Color.White;
                ship.Draw(sb); //draw the player's ship
            }

            //draw all the player's bullets
            foreach (Bullet b in bullets)
                b.Draw(sb);
        }


        public void LoadContent()
        {
            foreach (Life l in lives)
                l.LoadContent();
        }


        public void UseItem(Item item)
        {
            switch (item.Type)
            {
                case ItemType.Life:
                    GiveLife();
                    break;

                case ItemType.Weapon:
                    ship.AddWeapon(Creator.CreateWeapon(((Weapon)item.content).type));
                    break;

                default:
                    break;
            }
        }



    }
}
