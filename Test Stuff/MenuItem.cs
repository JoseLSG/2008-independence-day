using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XnaID
{
    class MenuItem : GameObject
    {
        //Identifiers
        private String name;
        private String label;

        //Toggles
        private bool subItem = false;   //if the menu calls selectNext() and th next item is a subitem, it will continue and find the next 'real' item
        private bool visible = true;    //false = we don't draw the item and selections pass over it since it is hidden
        private bool disabled = false;  //selections pass over this
        private bool selected = false;  //whether this item is the actively selected item

        //Textures
        private Textures.TextureName texNormal;     //enabled, but not selected
        private Textures.TextureName texSelected;   //enabled and selected
        private Textures.TextureName texDisabled;   //disabled (can't be selected)

        //States for general usage
        private float quantity = 0.0f;   
        private bool toogle = false;

        public MenuItem(
            String name,
            String label,
            Textures.TextureName defaultTexture,
            Textures.TextureName selectedTexture,
            Textures.TextureName disabledTexture,
            MenuItem nullableParent)
            : base(Textures.TextureName.zMAX)
        {
            this.name = name;
            this.label = label;
            this.texNormal = defaultTexture;
            this.texSelected = selectedTexture;
            this.texDisabled = disabledTexture;

            if (nullableParent == null)
                this.subItem = false;
            else
                this.subItem = true;
        }


        //################ PROPERTIES #################

        //Enable or Diasble this menu item
        public bool Enabled
        {
            get { return !disabled; }
            set
            {
                if (value)  //enable
                {
                    disabled = false;
                    base.textureName = texNormal;
                }
                else        //disable
                {
                    disabled = true;
                    base.textureName = texDisabled;
                }
                base.LoadContent(); //pull the appropriat texture
            }
        }


        //Select or Un-Select this menu item
        public bool Selected
        {
            get { return selected; }
            set
            {
                if (value)  //Select
                {
                    selected = true;
                    base.textureName = texSelected;
                    base.LoadContent(); //pull the appropriat texture
                }
                else        //Unselect
                {
                    selected = false;
                    this.Enabled = !disabled;   //call Enable property to do the work
                }
            }
        }

        //Get or Set the visibility of this menu item
        public bool Visible
        {
            set { visible = value; }
            get { return visible; }
        }


        //READONLY - Get the friendly name for this menu item
        public String Name
        {
            get { return name; }
        }

        //Get or set this menu item's label
        public String Label
        {
            get { return label; }
            set { label = value; }
        }

        //READONLY - Check if this item is a subItem
        public bool SubItem
        {
            get { return subItem; }
        }

        //Get or Set the toggle value for this menu item
        public bool Toggle
        {
            get { return toogle; }
            set { toogle = value; }
        }

        //Get or Set the quantity for this menu item
        public float Quantity
        {
            get { return quantity; }
            set { quantity = value; }
        }


    }
}
