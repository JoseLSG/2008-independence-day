using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace XnaID
{
    class Module
    {
        //Content Managers
        protected Fonts FONTS = Fonts.GetInstance();
        protected Textures TEXTURES = Textures.GetInstance();
        protected Sounds SOUNDS = Sounds.GetInstance();
        protected Properties PROP = Properties.GetInstance();

        public ModuleName moduleName;   //the name (enum) of this module

        //Refference to all the other modules
        public Intro refIntro;
        public MainMenu refMainMenu;
        public Engine refEngine;
        public Options refOptions;
        public Instructions refInstructions;
        public Credits refCredits;


        //Called automatically when this module becomes the active module
        public virtual void Activated()
        {

        }


        //Called automatically when this module stops being the active module
        public virtual void Deactivated()
        {

        }


        //Called every frame while this module is the active module
        public virtual ModuleName Update(GameTime gameTime)
        {
            //by default, return itself
            //IE: do not change active modules
            return moduleName;
        }


        //Only called once at the beginning of the program
        public virtual void LoadContent()
        {

        }


        //Called every frame when this module is active
        public virtual void Draw(SpriteBatch sb)
        {

        }

    }
}
