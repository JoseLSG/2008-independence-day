using System;
using System.Collections.Generic;
using System.Text;

namespace XnaID
{
    /*
     * Simple class denoting 1 life for a player
     * 
     * Purpose:
     * - Abstract the texture assignment
     * - Use the GameObject properties to display lives during the game
     * - A life can be an item and all items must be GameObjects 
     * 
     */

    class Life : GameObject
    {
        //Manager
        private Textures TEXMAN = Textures.GetInstance();

        public Life()
            : base(Textures.TextureName.Life)
        {
        }
    }
}
