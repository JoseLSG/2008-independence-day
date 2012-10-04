using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace XnaID
{
    public sealed class Textures
    {

        //Note: 1-1 mapping between the 2 arrays below
        private Texture2D[] textures;   //all the texture objects
        private String[] assetNames;    //all the asset names. 1-1 map

        public enum TextureName     //Friendly Texture names
        {
            NullImage = 0,  //represents the invisible texture

            //Main menu
            MainMenuNewGame,
            MainMenuOptions,
            MainMenuInstruction,
            MainMenuCredits,
            MainMenuExit,

            //credits
            CreditsBackground,

            //options
            OptionsBackground,

            //instructions
            InstructionsBackground,

            //levels
            LevelBorder,
            Level01Background,
            Level02Background,
            Level03Background,
            Level04Background,

            //ships
            ShipSkull,
            ShipTriangle,
            ShipBoss1,
            ShipSmall1,
            ShipRound,

      /************** Jose ADDED************/
            ShipEnemy1,
            ShipEnemy2,
            ShipEnemy3,
      /************** Jose ADDED END************/

            //weapons
            weaponCanon1,

            //bullets
            bulletBasic,
            bulletBasic2,
            bulletFatty,
            bulletRedRound,

            //General
            Pause,
            DeadRetry,
            HealthBar,
            HealthBarBG,

            //Items
            Life,
            ItemBg,



            zMAX  //This is important
        };

        //---------------
        #region Singleton Pattern
        //The only Textures object
        private static Textures instance;

        //Private constructor only called internally
        private Textures()
        {
            //Create the data structures
            textures = new Texture2D[(int)(TextureName.zMAX)];
            assetNames = new String[(int)(TextureName.zMAX)];

            assetNames[(int)TextureName.NullImage] = "NULL";
            
            //Main Menu (module) buttons
            assetNames[(int)TextureName.MainMenuNewGame] = "mnuMainNewGame";
            assetNames[(int)TextureName.MainMenuOptions] = "mnuMainOptions";
            assetNames[(int)TextureName.MainMenuInstruction] = "mnuMainInstructions";
            assetNames[(int)TextureName.MainMenuCredits] = "mnuMainCredits";
            assetNames[(int)TextureName.MainMenuExit] = "mnuMainExit";

            //Credits Module
            assetNames[(int)TextureName.CreditsBackground] = "CreditsBackground";


            //Options Module
            assetNames[(int)TextureName.OptionsBackground] = "OptionsBackground";

            //Instructions Module
            assetNames[(int)TextureName.InstructionsBackground] = "InstructionsBackground";

            //Level
            assetNames[(int)TextureName.LevelBorder] = "LevelBorder";
            assetNames[(int)TextureName.Level01Background] = "BackgroundLevel01";
            assetNames[(int)TextureName.Level02Background] = "BackgroundLevel02";
            assetNames[(int)TextureName.Level03Background] = "BackgroundLevel03";
            assetNames[(int)TextureName.Level04Background] = "BackgroundLevel04";

            //Ships
            assetNames[(int)TextureName.ShipSkull] = "shipSkull";
            assetNames[(int)TextureName.ShipTriangle] = "shipTriangle";
            assetNames[(int)TextureName.ShipBoss1] = "shipBoss1";
            assetNames[(int)TextureName.ShipSmall1] = "shipSmall1";
            assetNames[(int)TextureName.ShipRound] = "shipRound";

        /************** Jose ADDED************/
            assetNames[(int)TextureName.ShipEnemy1] = "shipEnemy1";
            assetNames[(int)TextureName.ShipEnemy2] = "shipEnemy2";
            assetNames[(int)TextureName.ShipEnemy3] = "shipEnemy3";
        /************** Jose ADDED END************/
            
            //weapons
            assetNames[(int)TextureName.weaponCanon1] = "weaponCanon1";

            //bullets
            assetNames[(int)TextureName.bulletBasic] = "bulletBasic1";
            assetNames[(int)TextureName.bulletBasic2] = "bulletBasic2";
            assetNames[(int)TextureName.bulletFatty] = "bulletFatty";
            assetNames[(int)TextureName.bulletRedRound] = "bulletRedRound";


            //items
            assetNames[(int)TextureName.Life] = "life";
            assetNames[(int)TextureName.ItemBg] = "ItemBG";


            //health bar
            assetNames[(int)TextureName.HealthBar] = "HealthBarHP";
            assetNames[(int)TextureName.HealthBarBG] = "HealthBarBG";


            //general
            assetNames[(int)TextureName.Pause] = "pause";
            assetNames[(int)TextureName.DeadRetry] = "deadRetry";
        }

        //The only way to get this single instance
        public static Textures GetInstance()
        {
            if (instance == null)
                instance = new Textures();

            return instance;
        }
        #endregion
        //---------------

        //Load all the textures
        //This needs to happen in the 1 and only time the Game class
        //calls load content at the start of the program
        public void LoadContent(ContentManager cm)
        {
            textures[(int)TextureName.NullImage] = cm.Load<Texture2D>(assetNames[(int)TextureName.NullImage]);

            //Main menu Module
            textures[(int)TextureName.MainMenuNewGame] = cm.Load<Texture2D>(assetNames[(int)TextureName.MainMenuNewGame]);
            textures[(int)TextureName.MainMenuOptions] = cm.Load<Texture2D>(assetNames[(int)TextureName.MainMenuOptions]);
            textures[(int)TextureName.MainMenuInstruction] = cm.Load<Texture2D>(assetNames[(int)TextureName.MainMenuInstruction]);
            textures[(int)TextureName.MainMenuCredits] = cm.Load<Texture2D>(assetNames[(int)TextureName.MainMenuCredits]);
            textures[(int)TextureName.MainMenuExit] = cm.Load<Texture2D>(assetNames[(int)TextureName.MainMenuExit]);

            //Levels
            textures[(int)TextureName.LevelBorder] = cm.Load<Texture2D>(assetNames[(int)TextureName.LevelBorder]);
            textures[(int)TextureName.Level01Background] = cm.Load<Texture2D>(assetNames[(int)TextureName.Level01Background]);
            textures[(int)TextureName.Level02Background] = cm.Load<Texture2D>(assetNames[(int)TextureName.Level02Background]);
            textures[(int)TextureName.Level03Background] = cm.Load<Texture2D>(assetNames[(int)TextureName.Level03Background]);
            textures[(int)TextureName.Level04Background] = cm.Load<Texture2D>(assetNames[(int)TextureName.Level04Background]);

            //Credits Module
            textures[(int)TextureName.CreditsBackground] = cm.Load<Texture2D>(assetNames[(int)TextureName.CreditsBackground]);

            //Options Module
            textures[(int)TextureName.OptionsBackground] = cm.Load<Texture2D>(assetNames[(int)TextureName.OptionsBackground]);

            //Instructions module
            textures[(int)TextureName.InstructionsBackground] = cm.Load<Texture2D>(assetNames[(int)TextureName.InstructionsBackground]);

            //Ships
            textures[(int)TextureName.ShipSkull] = cm.Load<Texture2D>(assetNames[(int)TextureName.ShipSkull]);
            textures[(int)TextureName.ShipTriangle] = cm.Load<Texture2D>(assetNames[(int)TextureName.ShipTriangle]);
            textures[(int)TextureName.ShipBoss1] = cm.Load<Texture2D>(assetNames[(int)TextureName.ShipBoss1]);
            textures[(int)TextureName.ShipSmall1] = cm.Load<Texture2D>(assetNames[(int)TextureName.ShipSmall1]);
            textures[(int)TextureName.ShipRound] = cm.Load<Texture2D>(assetNames[(int)TextureName.ShipRound]);
            /************** Jose ADDED ************/
            textures[(int)TextureName.ShipEnemy1] = cm.Load<Texture2D>(assetNames[(int)TextureName.ShipEnemy1]);
            textures[(int)TextureName.ShipEnemy2] = cm.Load<Texture2D>(assetNames[(int)TextureName.ShipEnemy2]);
            textures[(int)TextureName.ShipEnemy3] = cm.Load<Texture2D>(assetNames[(int)TextureName.ShipEnemy3]);
            /************** Jose ADDED END ************/

            //weapons
            textures[(int)TextureName.weaponCanon1] = cm.Load<Texture2D>(assetNames[(int)TextureName.weaponCanon1]);

            //bullets
            textures[(int)TextureName.bulletBasic] = cm.Load<Texture2D>(assetNames[(int)TextureName.bulletBasic]);
            textures[(int)TextureName.bulletBasic2] = cm.Load<Texture2D>(assetNames[(int)TextureName.bulletBasic2]);
            textures[(int)TextureName.bulletFatty] = cm.Load<Texture2D>(assetNames[(int)TextureName.bulletFatty]);
            textures[(int)TextureName.bulletRedRound] = cm.Load<Texture2D>(assetNames[(int)TextureName.bulletRedRound]);
        

            //items
            textures[(int)TextureName.ItemBg] = cm.Load<Texture2D>(assetNames[(int)TextureName.ItemBg]);
            textures[(int)TextureName.Life] = cm.Load<Texture2D>(assetNames[(int)TextureName.Life]);

            //general
            textures[(int)TextureName.Pause] = cm.Load<Texture2D>(assetNames[(int)TextureName.Pause]);
            textures[(int)TextureName.DeadRetry] = cm.Load<Texture2D>(assetNames[(int)TextureName.DeadRetry]);
            textures[(int)TextureName.HealthBar] = cm.Load<Texture2D>(assetNames[(int)TextureName.HealthBar]);
            textures[(int)TextureName.HealthBarBG] = cm.Load<Texture2D>(assetNames[(int)TextureName.HealthBarBG]);
        }

        public Texture2D GetTexture(TextureName textureName)
        {
            //make sure we're asking for a valid texture
            if (textureName < 0 || textureName >= TextureName.zMAX /*|| textureName == TextureName.NullImage*/)
                return null;    //forgot to add the texture to the enum or to the asset list ?

            return textures[(int)textureName];
        }

    }
}
