using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XnaID
{
    public sealed class Fonts
    {

        public String[] assetNames; //the content asset name 1-1 with enum below
        public SpriteFont[] fonts;  //the fonts

        public enum FontName
        {
            Arial10B = 0,
            zMAX
        };

        //---------------
        #region Singleton Pattern
        private static Fonts instance;

        private Fonts()
        {
            assetNames = new String[(int)(FontName.zMAX)];
            fonts = new SpriteFont[(int)(FontName.zMAX)];


            assetNames[(int)FontName.Arial10B] = "Arial10B";
            
        }

        public static Fonts GetInstance()
        {
            if (instance == null)
                instance = new Fonts();

            return instance;
        } 
        #endregion
        //---------------


        public void LoadContent(ContentManager cm)
        {
            fonts[(int)FontName.Arial10B] = cm.Load<SpriteFont>(assetNames[(int)FontName.Arial10B]);
        }

        public SpriteFont GetFont(FontName name)
        {
            switch (name)
            {
                case FontName.Arial10B:
                    return fonts[(int)name];
            }

            return null;
        }



    }
}
