using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
//first created SoundLib private SoundLib soundLib; then choose your sound  soundLib.PlaySound("fireball");
namespace MarioGame
{
    public class SoundLib
    {
        // Dictionary to store sound effects by name
        private Dictionary<string, SoundEffect> soundEffects;

        /// <summary>
        /// Initializes the SoundLib instance with an empty dictionary for sound effects.
        /// </summary>
        public SoundLib()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
        }

        /// <summary>
        /// Loads sound effects from the ContentManager. Call this once during initialization.
        /// </summary>
        /// <param name="content">ContentManager used to load sound assets.</param>
        public void LoadContent(ContentManager content)
        {
            // Load each sound effect and add it to the dictionary
            soundEffects["jump"] = content.Load<SoundEffect>("jump");
            soundEffects["fireball"] = content.Load<SoundEffect>("fireball");
            soundEffects["coin"] = content.Load<SoundEffect>("coin");
            soundEffects["killEnemy"] = content.Load<SoundEffect>("killEnemy");
            soundEffects["dokey"] = content.Load<SoundEffect>("dokey");
            soundEffects["marioDie"] = content.Load<SoundEffect>("marioDie");
             soundEffects["gameOver"] = content.Load<SoundEffect>("gameOver");
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
    }
}
