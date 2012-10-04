using System;
using System.Collections.Generic;
using System.Text;

namespace XnaID
{
    class Menu
    {

        private LinkedList<MenuItem> items;   //the menu items

        public Menu()
        {
            items = new LinkedList<MenuItem>();
        }

        public void AddItem(
            String name,
            String label,
            Textures.TextureName defaultTexture,
            Textures.TextureName selectedTexture,
            Textures.TextureName disabledTexture,
            MenuItem nullableParent)
        {
            items.AddLast(new MenuItem(name, label, defaultTexture, selectedTexture, disabledTexture, nullableParent));
        }





        //selectNext
        //selectFirst
        //selectPrevious
        //
        //addItem
        //removeItem
        //
        //





    }
}
