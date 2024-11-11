using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
// Game1.Instance.GetSoundLib().PlaySound("fireball"); use things like this to play the sound in your own method
namespace MarioGame
{
    public class SoundLib
    {
        private Dictionary<string, SoundEffect> soundEffects; 

        public SoundLib()
        {
            soundEffects = new Dictionary<string, SoundEffect>();
        }

  
        public void LoadContent(ContentManager content)
        {
            soundEffects["jump"] = content.Load<SoundEffect>("jump");
            soundEffects["fireball"] = content.Load<SoundEffect>("fireball");
            soundEffects["coin"] = content.Load<SoundEffect>("coin");
            soundEffects["killEnemy"] = content.Load<SoundEffect>("bosspain");
        }

  
        public void PlaySound(string soundName)
        {
            if (soundEffects.ContainsKey(soundName))
            {
                soundEffects[soundName].Play();
            }
        }
    }
}
