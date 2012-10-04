using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace XnaID
{
    class Engine : Module
    {
        private Textures TEXTURES = Textures.GetInstance();
        private Fonts FONTS = Fonts.GetInstance();
        private Sounds SOUNDS = Sounds.GetInstance();
        private Properties PROPERTIES = Properties.GetInstance();
        //private Creator CREATOR = Creator.GetInstance();


        private Level[] levels;         //The array of all the levels
        private int activeLevel = 0;    //The active level (pointer to level in the array)
        private GameState state = GameState.Unloaded;

        private Rectangle bounds;

        private GameObject pauseImg;    //image overlay when game is paused
        private GameObject deadImg;     //image overlay when no lives remaining for all players

        public Engine()
        {
            //**** Size of the game "view port"
            int width = 600;
            int height = 800;
            bounds = new Rectangle(0, 0, width, height);
            bounds.Offset((int)((PROPERTIES.screen.Width - width) / 2.0f),
                          (int)((PROPERTIES.screen.Height - height) / 2.0f));

            //Create the (unloaded) level objects
            levels = new Level[4];
            levels[0] = new Level01(bounds);
            levels[1] = new Level02(bounds);
            levels[2] = new Level03(bounds);
            levels[3] = new Level04(bounds);
        }


        public void LoadLevel(LevelNumber number)
        {
            //unload old level
            levels[activeLevel].Unload();

            //change active level
            activeLevel = (int)number;

            levels[activeLevel].Load(Textures.TextureName.Level01Background);

            state = GameState.Intro;
        }

        public void LoadNextLevel()
        {
            switch (activeLevel)
            {
                case (int)LevelNumber.One:
                    LoadLevel(LevelNumber.Two);
                    break;
                case (int)LevelNumber.Two:
                    LoadLevel(LevelNumber.Three);
                    break;
                case (int)LevelNumber.Three:
                    LoadLevel(LevelNumber.Four);
                    break;
                case (int)LevelNumber.Four:         //????? change to finishing the game
                    LoadLevel(LevelNumber.One);
                    break;
            }
        }


        public override void LoadContent()
        {
            pauseImg = new GameObject(Textures.TextureName.Pause);
            pauseImg.LoadContent();
            pauseImg.CenterIn(bounds);

            deadImg = new GameObject(Textures.TextureName.DeadRetry);
            deadImg.LoadContent();
            deadImg.CenterIn(bounds);
        }

        public override void Draw(SpriteBatch sb)
        {
            switch (state)
            {
                case GameState.Unloaded:
                    //don't draw
                    break;

                case GameState.Options:
                    levels[activeLevel].Draw(sb);
                    refOptions.Draw(sb);
                    break;

                case GameState.Instructions:
                    levels[activeLevel].Draw(sb);
                    refInstructions.Draw(sb);
                    break;

                case GameState.Dead:
                    levels[activeLevel].Draw(sb);
                    deadImg.Draw(sb);
                    break;

                case GameState.Pause:
                    levels[activeLevel].Draw(sb);
                    pauseImg.Draw(sb);
                    break;

                default:
                    levels[activeLevel].Draw(sb);
                    break;
            }
        }

        public override ModuleName Update(GameTime gameTime)
        {
            //CHEATS
            if (PROP.allInput.CheatClearEnemyBullets())
                levels[activeLevel].CHEAT(Level.Cheats.ClearEnemyBullets);
            else if (PROP.allInput.CheatClearEnemyShips())
                levels[activeLevel].CHEAT(Level.Cheats.ClearEnemyShips);
            if (PROP.allInput.CheatSkipLevel())
            {
                LoadNextLevel();
                return this.moduleName;
            }


            
            //Update the level state to match engine
            levels[activeLevel].state = this.state;
            //Update the level
            switch (state)
            {
                case GameState.Intro:
                    if (PROP.allInput.Back())       //user has skipped the intro movie
                        state = GameState.Play;     //switch to gameplay
                    else
                        state = levels[activeLevel].Update(gameTime);
                    break;

                case GameState.Instructions:
                    if (PROP.allInput.Back())       //user has left the instructions page
                        state = GameState.Pause;    //switch back to paused
                    else
                        refInstructions.Update(gameTime);
                    break;

                case GameState.Options:
                    if (PROP.allInput.Back())       //user has skipped the intro movie
                        state = GameState.Pause;    //switch back to paused
                    else
                        refOptions.Update(gameTime);
                    break;

                case GameState.Pause:
                    if (PROP.allInput.Back())
                    {
                        levels[activeLevel].Unload();
                        activeLevel = 0;
                        state = GameState.Intro;
                        return ModuleName.MainMenu;
                    }
                    else if (PROP.allInput.Pause())
                        state = GameState.Play;
                    else if (PROP.allInput.ShowInstructions())
                        state = GameState.Instructions;
                    else if (PROP.allInput.ShowOptions())
                        state = GameState.Options;
                    break;

                case GameState.Dead:
                    if (PROP.allInput.Back())
                    {
                        levels[activeLevel].Unload();
                        activeLevel = 0;
                        state = GameState.Intro;
                        return ModuleName.MainMenu;
                    }
                    break;

                case GameState.Play:
                    if (PROP.allInput.Pause())
                        state = GameState.Pause;
                    else
                        state = levels[activeLevel].Update(gameTime);
                    break;

                case GameState.Final:
                    if (PROP.allInput.Back())       //user has skipped the closing movie
                        LoadNextLevel();            //switch to next level
                    else
                        state = levels[activeLevel].Update(gameTime);
                    break;

            }
            
            return base.Update(gameTime);
        }



    }
}
