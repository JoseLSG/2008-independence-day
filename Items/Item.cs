using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace XnaID
{
    /*
     * Items are things that enemy ships and obstacles
     * can leave behind after they are destroyed.
     * The player can then pick them up by flying through them
     * 
     * Item is itself a gameObject whose prite is the default
     * background for all items
     * 
     * There are a variety of Item types
     * Weapons, Lives, Secondary Weapons, Helped Ships
     * 
     */ 

    class Item : GameObject
    {

        //Content Managers
        Textures TEXTURES = Textures.GetInstance();


        public ItemType type;      //type determines the item contents
        public GameObject content; //the life, weapon etc... that this item contains

        public LevelSlider slider;


        //Constructor
        public Item(ItemType type, GameObject obj)
            : base(Textures.TextureName.ItemBg)
        {
            this.type = type;
            content = obj;
        }

        public void Draw(SpriteBatch sb)
        {
            //Draw the default Item background
            base.Draw(sb);

            //draw the item it contains
            switch (type)
            {
                case ItemType.Life:
                    ((Life)content).Draw(sb);
                    break;

                case ItemType.Weapon:
                    ((Weapon)content).Draw(sb);
                    break;
                
                default:
                    break;
            }
            
        }

        public void LoadContent()
        {
            //load content for item background
            base.LoadContent();

            //load the content for the item contents
            content.LoadContent();
        }


        public override void Update(GameTime gameTime)
        {
            //move the default background
            //base.Update(gameTime);

            if (slider != null)
                Offset(new Vector2(0, slider.Step));

            //center the contained item on the background
            content.MoveTo(this.position);
        }

        public ItemType Type
        {
            get { return type; }
        }

    }
}
