using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace XnaID
{

    public class Game1 : Game
    {
        //Our objects
        //----------------------------------------------
        //Single Initialization and ContentLoading
        private Properties PROP = Properties.GetInstance();
        private Textures TEXTURES = Textures.GetInstance();
        private Fonts FONTS = Fonts.GetInstance();
        private Sounds SOUNDS = Sounds.GetInstance();
        
        //private Creator CREATOR = Creator.GetInstance();

        private Module[] modules;       //Our Game modules
        private Module activeModule;    //The currently active module

        int elapsed = 0;
        int frameCount = 0;
        String fps = "";

        //----------------------------------------------

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            //WINDOW SETUP
            this.Window.Title = "Independance Day - Alpha";
            this.IsFixedTimeStep = false;

            PROP.screen = new Rectangle(0, 0, //top left of course
                                   graphics.GraphicsDevice.DisplayMode.Width, 
                                   graphics.GraphicsDevice.DisplayMode.Height);

            PROP.screenCenter = new Vector2((float)PROP.screen.Width / 2.0f,
                                                  (float)PROP.screen.Height / 2.0f);

            if (false) //NORMAL FULLSCREEN
            {
                graphics.IsFullScreen = true;
                graphics.PreferredBackBufferHeight = (int)PROP.screen.Height;
                graphics.PreferredBackBufferWidth = (int)PROP.screen.Width;
                graphics.ApplyChanges();
            }
            else //DEBUG WINDOW
            {
                graphics.PreferredBackBufferHeight = (int)PROP.screen.Height - 90;
                graphics.PreferredBackBufferWidth = (int)PROP.screen.Width - 30;
                graphics.ApplyChanges();
            }

            //Create the Modules
            modules = new Module[6];
            modules[(int)ModuleName.Intro] = new Intro();
            modules[(int)ModuleName.MainMenu] = new MainMenu();
            modules[(int)ModuleName.Engine] = new Engine();
            modules[(int)ModuleName.Options] = new Options();
            modules[(int)ModuleName.Instructions] = new Instructions();
            modules[(int)ModuleName.Credits] = new Credits();


            for (int i = 0; i < 6; i++)
            {
                modules[i].refIntro = (Intro)modules[(int)ModuleName.Intro];
                modules[i].refMainMenu = (MainMenu)modules[(int)ModuleName.MainMenu];
                modules[i].refEngine = (Engine)modules[(int)ModuleName.Engine];
                modules[i].refInstructions = (Instructions)modules[(int)ModuleName.Instructions];
                modules[i].refOptions = (Options)modules[(int)ModuleName.Options];
                modules[i].refCredits = (Credits)modules[(int)ModuleName.Credits];
            }
            


            //Set the startup module
            activeModule = modules[(int)ModuleName.MainMenu];

            base.Initialize();
        }

        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }


        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Load all the textures
            TEXTURES.LoadContent(this.Content);

            //Load all our fonts
            FONTS.LoadContent(this.Content);


            //Load the content for our game
            foreach (Module m in modules)
            {
                if (m != null)
                    m.LoadContent();
            }
            
        }


        protected override void Update(GameTime gameTime)
        {
            frameCount++;
            elapsed += gameTime.ElapsedGameTime.Milliseconds;

            if (elapsed >= 1000)
            {
                fps = frameCount.ToString();

                frameCount = 0;
                elapsed = 0;
            }

            //Update the in[put state for the active module
            foreach (Player p in PROP.players)
                p.input.Update();
            PROP.allInput.Update();

            //Store current module
            ModuleName pre = activeModule.moduleName;

            //Get the next Module name
            ModuleName post = activeModule.Update(gameTime);

            //If change, then tell old one and new one of the change
            if (pre != post)
            {
                //Inform the 'old' module that it is no longer active
                activeModule.Deactivated();

                //Check to exit the game
                if (post == ModuleName.Exit)
                    Exit();
                else
                {
                    //Update module and possibly change active module
                    activeModule = modules[(int)post];

                    //Inform the 'new' module that it has been activated
                    activeModule.Activated();
                }

            }

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            spriteBatch.DrawString(FONTS.GetFont(Fonts.FontName.Arial10B), fps, Vector2.Zero, Color.White);

                activeModule.Draw(this.spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
