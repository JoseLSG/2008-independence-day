using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace XnaID
{

    /*
     * The Level slider is a sprite of the entire level background image
     * 
     * During Update, it will move the source rectangle appropriately along the image
     * Slider has a "VisualObject" which is used as the source rectangle. Since
     * VisualObject has a direction, speed etc... all this moving during the update 
     * becomes trivial.
     * 
     * We maintain a HEAD and a TAIL position relative to orriginal Image
     * If we have a vertical level, 0 denots the bottom of the immage, and image_Height
     * denotes the top of the image.
     * 
     * Thus at any point in time, Head will be Tail + window height.
     * 
     * We stop scrolling when Head >= image_height
     * 
     * Similar situation for side scrolling
     * 
     * With this HEAD and TAIL system in mind, we have 2 lists of objects.
     * One is a list of Ship Spawns, The other a list of Level Obstacles
     * 
     * These both have "triggers" and Trigger TYPES
     * The trigger is simply a Y coordinate (vertical) or X coordinate (horizontal scrolling)
     * and the type simply determines which check will activate them.
     * Check 1
     *    Check all spawns with trigger type head
     *    If slider HEAD <= trigger ---> create spawn
     * 
     * Check 2
     *    Check all spawns with tail triggers
     *    If slider TAIL <= trigger --> create spawn
     * 
     * When we say CREATE SPAWN
     * What we mean is use the data in the span list(s) to create a list of
     * Spawn Ships, and a List of spawn Obstacles.
     * After the Slider Update, the level will grab these spawns, and dump them into
     * his own list.
     * 
    */


    class LevelSlider : GameObject
    {

        //Content Managers
        Fonts FONTS = Fonts.GetInstance();
        Textures TEXTURES = Textures.GetInstance();
        Sounds SOUNDS = Sounds.GetInstance();
        Properties PROPERTIES = Properties.GetInstance();
        //Creator CREATOR = Creator.GetInstance();

        //Various Coordinates
        private Rectangle viewport;
        private Vector2 center;
        private Vector2 midBottom;
        private Vector2 midTop;

        //Spawn Lists
        private List<Spawn> spawns;

        //Actual object lists
        private List<Ship> ships;


        //this is actually the source rectangle overlayed on the background (level)image
        private GameObject window;
        //Window positions
        private int head;
        private int tail;

        //if paused, during update the window will not move
        private bool paused;

        public bool moving = true; //false when the slider has reached the top of the image


        private int step = 0;   //the chance in Y during the last update


        //Provide the texture name for this level's Background
        //Provide the rectangle in screen coordinates where this background
        //  will be drawn, and also used for window size to match destination
        public LevelSlider(Textures.TextureName background, Rectangle destination)
            : base(background)
        {
            viewport = destination;
            center = new Vector2((float)viewport.X + (float)viewport.Width / 2.0f,
                                 (float)viewport.Y + (float)viewport.Height / 2.0f);
            midTop = new Vector2(center.X, (float)viewport.Top);
            midBottom = new Vector2(center.X, (float)viewport.Bottom);

            //the window does not need a texture since we are only using
            //it as a rectangle that moves during update
            window = new GameObject(Textures.TextureName.NullImage);
            window.SetRotation(new Vector2(0, -1));
            window.speed = 0.03f;
        }


        //Add spawns to the level slider
        public void AddSpawns(List<Spawn> newSpawns)
        {
            spawns.AddRange(newSpawns);
        }


        //
        public void Load()
        {
            //Init the data structures lists
            spawns = new List<Spawn>();
            ships = new List<Ship>();

            //WINDOW INIT
            //fix the source and destination bounds according to immage size
            base.LoadContent();
            //change the source and destination to match the game window
            base.Stretch(new Vector2(viewport.Width, viewport.Height));
            base.MoveTo(center);
            //change the size to match the slider destination
            window.Stretch(new Vector2(viewport.Width, viewport.Height));
            //Move slider to beginning of map
            Begin();


            FixHeadTail();

        }


        //sets the head and tail positions as per where the 
        //source window is inside the level background
        private void FixHeadTail()
        {
            head = window.bounds.Top;
            tail = window.bounds.Bottom;
        }


        public override void Update(GameTime gameTime)
        {
            if (moving)
            {
                int i = window.bounds.Top;

                window.Update(gameTime);    //Move window (slide background)

                step = i - window.bounds.Top;


                //Make sure we don't pass the top of the image
                if (window.bounds.Y < 0)
                {
                    moving = false;
                    window.bounds.Y = 0;
                }

                //Set source for slider background as the (moved) window
                base.source = window.bounds;

                //Update the head/tail position
                FixHeadTail();
            }

            //Add any ships if a spawn is triggered
            CheckSpawns();

        }


        //Takes a spawn record and converts the position
        //from an offset to a screen position.
        private void ChangeToScreenPosition(ref Spawn spawn)
        {
            //Set spawn orrigin as per the spawn triggerType
            Vector2 origin = (spawn.triggerType == SpawnTrigger.Head) ? midTop : midBottom;

            spawn.position.X += origin.X;
            spawn.position.Y += origin.Y;
        }


        //Compared the current Head/Tail position against the spawn trigger positions
        //Populates the ships and obstacles list with any objects triggered
        //The level will pick these up when it wants with GetShips() or GetObstacles()
        private void CheckSpawns()
        {
            //List of spawns to trigger this frame
            List<Spawn> tempSpawns = new List<Spawn>();

            //Check each spawn to see if they should be triggered
            foreach (Spawn s in spawns)
            {
                //Check is spawn falls in proper difficulty setting
                if ((int)s.difficulty > (int)Properties.difficulty)
                    continue;

                switch (s.triggerType)
                {
                    case SpawnTrigger.Head:
                        if (head <= s.trigger)
                            tempSpawns.Add(s);
                        break;

                    case SpawnTrigger.Tail:
                        if (tail <= s.trigger)
                            tempSpawns.Add(s);
                        break;
                }
            }

            //Create the ships from the list of spawns to trigger
            for (int i = 0; i < tempSpawns.Count; i++)
            {
                Spawn refSpawn = tempSpawns[i];
                ChangeToScreenPosition(ref refSpawn);

                ships.Add(Creator.CreateShip(tempSpawns[i].shipType, tempSpawns[i].position, tempSpawns[i].direction));
            }

            //removed the consumed spawns from the list
            foreach (Spawn s in tempSpawns)
                spawns.Remove(s);
        }


        public List<Ship> GetShips()
        {
            //Copy the list of ships to a new list
            List<Ship> lt = new List<Ship>();
            lt.AddRange(ships);

            //Empty the old list
            ships.Clear();

            //return the new list
            return lt;
        }


        //Move the window to the bottom of the texture
        public void Begin()
        {
            //move the window down to the bottom of the source image
            //centered on the source image, and with the bottom of the window
            //at the bottom of the image
            //rememeber moving, moves as per the center of the GameObject
            window.MoveTo(new Vector2((float)base.texture.Width / 2.0f,
                                      (float)base.texture.Height - (float)viewport.Height / 2.0f));
        }

        //Stop the window from moving during Update calls
        public void Pause()
        {
            paused = true;
        }

        //Un-Pause
        public void Resume()
        {
            paused = false;
        }


        public int GetHead()
        {
            return head;
        }

        public int GetTail()
        {
            return tail;
        }

        //get the change in Y of the window position on the last update
        public int Step
        {
            get { return step; }
        }

        //FOR CHEATS / DEBUGGING / TESTING
        public void SetSpeed(float speed)
        {
            window.speed = speed;
        }


    }
}
