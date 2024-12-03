using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

public class SoundLib
{
    private Dictionary<string, SoundEffect> soundEffects;
    private SoundEffectInstance themeInstance;
    private bool isThemePlaying;

    public SoundLib()
    {
        soundEffects = new Dictionary<string, SoundEffect>();
        isThemePlaying = false;
    }

    public void LoadContent(ContentManager content)
    {
        soundEffects["jump"] = content.Load<SoundEffect>("jump");
        soundEffects["fireball"] = content.Load<SoundEffect>("fireball");
        soundEffects["coin"] = content.Load<SoundEffect>("coin");
        soundEffects["killEnemy"] = content.Load<SoundEffect>("killEnemy");
        soundEffects["hitflag"] = content.Load<SoundEffect>("dokey");
        soundEffects["gameOver"] = content.Load<SoundEffect>("gameOver");
        soundEffects["theme"] = content.Load<SoundEffect>("theme");
        soundEffects["pipe"] = content.Load<SoundEffect>("pipe");
        var theme = content.Load<SoundEffect>("theme");
        themeInstance = theme.CreateInstance();
        themeInstance.IsLooped = true;
    }

    public void PlaySound(string soundName)
    {
        if (soundEffects.ContainsKey(soundName))
        {
            soundEffects[soundName].Play();
        }
    }
    public void PlayTheme()
    {
        DisposeTheme(); 
        themeInstance = soundEffects["theme"].CreateInstance();
        themeInstance.IsLooped = true;
        themeInstance.Play();
    }


   public void StopTheme()
    {
        if (themeInstance != null && themeInstance.State == SoundState.Playing)
        {
            themeInstance.Stop();
        }
    }

   public void DisposeTheme()
    {
        if (themeInstance != null)
        {
            themeInstance.Stop();    
            themeInstance.Dispose(); 
            themeInstance = null;    
        }
    }



}
