using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MarioGame
{
    public class SoundLib
    {
        private Dictionary<string, SoundEffect> soundEffects; 
        private SoundEffectInstance themeInstance; 

        public SoundLib()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
        }

        /// <summary>
        /// Loads sound effects from the ContentManager. This method should be called once during initialization.
        /// </summary>
        /// <param name="content">ContentManager used to load sound assets.</param>
        public void LoadContent(ContentManager content)
        {
           
            soundEffects["jump"] = content.Load<SoundEffect>("jump");
            soundEffects["fireball"] = content.Load<SoundEffect>("fireball");
            soundEffects["coin"] = content.Load<SoundEffect>("coin");
            soundEffects["killEnemy"] = content.Load<SoundEffect>("killEnemy");

            
            var theme = content.Load<SoundEffect>("theme"); 
            themeInstance = theme.CreateInstance();
            themeInstance.IsLooped = true; // keep playing
        }

        /// <summary>
        /// Plays a sound effect based on the provided sound name.
        /// </summary>
        /// <param name="soundName">The name of the sound effect to play.</param>
        public void PlaySound(string soundName)
        {
            if (soundEffects.ContainsKey(soundName))
            {
                soundEffects[soundName].Play();
            }
        }

        /// <summary>
        /// Starts playing the theme song in a loop if it's not already playing.
        /// </summary>
        public void PlayTheme()
        {
            if (themeInstance.State != SoundState.Playing)
            {
                themeInstance.Play();
            }
        }

        /// <summary>
        /// Pauses the theme song if it's currently playing.
        /// </summary>
        public void PauseTheme()
        {
            if (themeInstance.State == SoundState.Playing)
            {
                themeInstance.Pause();
            }
        }

        /// <summary>
        /// Resumes the theme song if it's currently paused.
        /// </summary>
        public void ResumeTheme()
        {
            if (themeInstance.State == SoundState.Paused)
            {
                themeInstance.Resume();
            }
        }

        /// <summary>
        /// Stops the theme song from playing.
        /// </summary>
        public void StopTheme()
        {
            if (themeInstance.State != SoundState.Stopped)
            {
                themeInstance.Stop();
            }
        }
    }
}
