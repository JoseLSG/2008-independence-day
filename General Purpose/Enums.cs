using System;
using System.Collections.Generic;
using System.Text;


namespace XnaID
{

    public enum Difficulty
    {
        Easy = 0,
        Medium = 1,
        Hard = 2,
        Insanity = 3
    }

    public enum ModuleName
    {
        Intro = 0,
        MainMenu,
        Engine,
        Options,
        Instructions,
        Credits,
        Exit
    };

    public enum LevelNumber
    {
        One = 0,
        Two,
        Three,
        Four
    }

    public enum SpawnType
    {
        Ship,
        Obstacle
    }

    public enum ShipType           //Different kinds of ships
    {
        player,
        demoEnemy,
        demoEnemy2,
        demoEnemy3,
        demoEnemy4,
        /************** Jose ADDED ************/
        shipEnemy1,
        shipEnemy2,
        shipEnemy3
        /************** Jose ADDED END ************/
    };


    //The method used to determine the number of bullets, and the direction of bullets
    //when a weapon is fired. These are relative to the direction that the weapon is facing
    //Set in Weapon.Fire() function
    public enum FiringMethod
    {
        Single,
        Triple
    }


    public enum WeaponType         //Various weapon types for different ships
    {
        BasicCanon,
        TripleCanon,
        SideShooter,
        UpShooter,
        FunGun,
    };

    public enum WeaponLevel         //Change speed/size, power scale factors...
    {
        one = 1,
        two,
        three
        //...
    }


    public enum CollisionMethod
    {
        BoundingBox,    //checks if 2 rectangles intersect
        Radius          //checks if distance between 2 objects is less then the sum of their collision radii
    };




    public enum BulletType         //Different types of projectiles for weapons
    {
        PeaShot,
        Round,
        Fatty,
        BRoundRedFast,
        BRoundRedSlow
    };


    public enum BulletLevel         //Different types of projectiles for weapons
    {
        one = 1,
        two,
        three
    };

    public enum SpawnTrigger        //For the slider spawn positions
    {
        Head,
        Tail
    };


    public enum ItemType
    {
        Weapon, //some kind of weapon upgrade
        Bullet, //bullet upgrade
        Nuke,   //blow all the ships off the maps ???? could be fun :)
        Bot,    //helper ship attaches to your ship
        Life,   //gives you an extra life
    };

    public enum GameState
    {
        Unloaded,       //Level object created, but nothing is loaded
        Intro,          //the pre-level video
        Play,           //game in progress
        Pause,          //game is paused
        Options,        //game in progress but options screen open (game paused in background)
        Instructions,   //game in progress but Instructions open (game paused in background)
        Results,        //The results of completing the level, display the points, time etc...
        Dead,
        Final,          //the post-level video
        Finished        //All complete, the level Manager should load next level
    };


    public enum ObstacleType    //determines the graphic (sprite texture)
    {
        Wall01,
        Wall02,
        etc
    };


    public enum SliderState
    {
        Start,  //the window is at the start of the map
        Mid,    //the window is moving accross the map somewhere
        End,    //The window has reached the end and stopped
        Empty   //The slider has reached the end and no longer contains any spawns
    };

    public enum Cardinal
    {
        Up,
        Down,
        Right,
        Left
    };


}
