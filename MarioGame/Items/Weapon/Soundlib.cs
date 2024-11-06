using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

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
