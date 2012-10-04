using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;

namespace XnaID
{
    class Sounds
    {
        //The sound objects
        private AudioEngine audioEngine;
        private SoundBank audioSoundBank;
        private WaveBank audioWaveBank;
        private AudioCategory audioCategory;  //needed for volume


        private String[] cueNames;  //list of cue names

        //This is so we can modify volume
        public enum AudioCat
        {   //example...
            Menu,
            Shooting,
            Explosions,
            Music
        };


        //Use enum so when playing sounds from other parts of the 
        //program we don't have to remember the stupid string names
        public enum CueName    //for sounds
        {
            Sound01,
            Sound02,
            zMAX //important
        };

        //---------------
        #region Singleton Pattern
        private static Sounds instance = new Sounds();

        private Sounds()
        {
            cueNames = new String[(int)(CueName.zMAX) - 1];
            cueNames[(int)CueName.Sound01] = "mysound1cuename";
        }
        public static Sounds GetInstance()
        {
            return instance;
        }
        #endregion
        //---------------


        //TO DO
        //void PlaySound(CueName cueNameFromEnum)
        //void SetVolume(float volume)
        //void SetVolume(float volume, AudioCat category)
        //void IncVolume()
        //void IncVolume(AudioCat category)
        //void DecVolume()
        //void DecVolume(AudioCat category)
        
    }
}
