using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace XnaID
{
    /*
     * Each level has an Item Manager
     * 
     * Motivation:
     * It is difficult to control the number of items in a level.
     * Since the player may not be able to get all the items that appear
     * we cannot really know at a given point in time which items 
     * a player might have.
     * 
     * Purpose:
     * For each level, set a fixed number of items of each type.
     * Allow items to be dropped by ships as long as there are still items 
     * remaining in the local lists, but only reduce the number of items 
     * in these lists when a played actually consumes one of the items, 
     * rather then when one is dropped.
     */ 


    class ItemManager
    {
        //Properties (for the Randomizer)
        private Properties PROP = Properties.GetInstance();


        private int numWeapons;     //number of weapons remaining to hand out
        private int numLives;       //number of lives remaining to hand out
        
        List<WeaponType> weapons;   //list of available weapons to distribute
        

        //CHANCE for each type of item to be dropped
        //A random number 'R' is generated from 0 to 100
        //If   chance.X <= R <= chance.Y then the item is generated
        //but only if there are still items of that type left in the count
        private Vector2 chanceLife = new Vector2(0f, 5f);     
        private Vector2 chanceWeapon = new Vector2(20f, 30f);

        //Reference to the level's slider so we can set the
        //Item slider refference on creation
        private LevelSlider slider;


        public ItemManager(ref LevelSlider refSlider)
        {
            slider = refSlider;

            //Init data lists
            weapons = new List<WeaponType>();
        }


        //Set the number of lives available for this level
        public void SetLives(int numLives)
        {
            this.numLives = numLives;
        }

        //Set the number of weapons, and the type of weapons
        //available for this level
        public void SetWeapons(int numWeapons, params WeaponType[] availableTypes)
        {
            this.numWeapons = numWeapons;

            foreach (WeaponType wt in availableTypes)
                weapons.Add(wt);
        }


        //Ask the manager for an item
        //Randomly select a type of item to generate
        //return null if there is no availability for that type
        public Item GetItem()
        {
            //get a random number between 0 and 100
            int chance = PROP.random.Next(0, 101);

            ItemType typeToMake = ItemType.Life;    //default item
            GameObject obj = null;                  //object to put inside the item

            //Determine which item to return
            //if no item return null
            //if no items of the random type chosen remain, return null also
            if (chance >= chanceLife.X && chance <= chanceLife.Y)
            {
                if (HasRemaining(ItemType.Life))
                {
                    obj = new Life();
                    typeToMake = ItemType.Life;
                }
            }
            else if (chance >= chanceWeapon.X && chance <= chanceWeapon.Y)
            {
                if (HasRemaining(ItemType.Weapon))
                {
                    //randomly select a weapon type form available list
                    int rand = PROP.random.Next(0, weapons.Count);

                    obj = Creator.CreateWeapon(weapons[rand]);
                    typeToMake = ItemType.Weapon;
                }
            }
            else
            {
                return null;
            }


            if (obj == null)
                return null;
            else
                return Creator.CreateItem(typeToMake, obj, ref slider);
        }


        //Call when player consumes an item
        //removes one of the items corresponding itemtype-list
        public void ConsumeItem(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Life:
                    numLives--;
                    break;

                case ItemType.Weapon:
                    numWeapons--;
                    break;

                default:
                    break;
            }  
        }


        //Returns true if the manager contains at least
        //1 item of the given item type.
        public bool HasRemaining(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Life:
                    return (numLives > 0);

                case ItemType.Weapon:
                    return (numWeapons > 0);

                default:
                    return false;
            }
        }

        //Returns the number of items remaining of the given type
        public int NumRemaining(ItemType itemType)
        {
            switch (itemType)
            {
                case ItemType.Life:
                    return numLives;

                case ItemType.Weapon:
                    return numWeapons;

                default:
                    return 0;
            }
        }










    }
}
