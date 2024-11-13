using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace MarioGame
{
    public class SoundLib
    {
        private Dictionary<string, SoundEffect> soundEffects;
        private SoundEffectInstance themeInstance; // Theme song instance to control looping
        private bool isThemePlaying; // Track if the theme is currently playing to prevent overlaps

        public SoundLib()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
            isThemePlaying = false; // Initialize theme as not playing
        }

        /// <summary>
        /// Loads sound effects from the ContentManager. This method should be called once during initialization.
        /// </summary>
        /// <param name="content">ContentManager used to load sound assets.</param>
        public void LoadContent(ContentManager content)
        {
            // Load sound effects
            soundEffects["jump"] = content.Load<SoundEffect>("jump");
            soundEffects["fireball"] = content.Load<SoundEffect>("fireball");
            soundEffects["coin"] = content.Load<SoundEffect>("coin");
            soundEffects["killEnemy"] = content.Load<SoundEffect>("killEnemy");
            soundEffects["dokey"] = content.Load<SoundEffect>("dokey");
            soundEffects["gameOver"] = content.Load<SoundEffect>("gameOver");

            // Load and configure theme music instance
            var theme = content.Load<SoundEffect>("theme");
            themeInstance = theme.CreateInstance();
            themeInstance.IsLooped = true; // Loop the theme music
        }

        /// <summary>
        /// Plays a specific sound effect by name.
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
        /// Starts playing the theme song if it's not already playing.
        /// </summary>
        public void PlayTheme()
        {
            if (!isThemePlaying && themeInstance.State != SoundState.Playing)
            {
                themeInstance.Play();
                isThemePlaying = true;
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
                isThemePlaying = false;
            }
        }

        /// <summary>
        /// Resumes the theme song if it was previously paused.
        /// </summary>
        public void ResumeTheme()
        {
            if (themeInstance.State == SoundState.Paused)
            {
                themeInstance.Resume();
                isThemePlaying = true;
            }
        }

        /// <summary>
        /// Stops the theme song and resets the playing state.
        /// </summary>
        public void StopTheme()
        {
            if (themeInstance.State != SoundState.Stopped)
            {
                themeInstance.Stop();
                isThemePlaying = false;
            }
        }
    }
}
