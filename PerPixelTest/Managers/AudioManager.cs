namespace PerPixelTest.Managers
{
    using Microsoft.Xna.Framework.Audio;
    using Microsoft.Xna.Framework.Content;
    using System.Collections.Generic;

    public static class AudioManager
    {
        private static Dictionary<string, SoundEffect> effects = new Dictionary<string, SoundEffect>();

        public static void AddEffect(string name, SoundEffect effect)
        {
            effects.Add(name, effect);
        }

        public static void Play(string effectName)
        {
            if (effects.ContainsKey(effectName))
            {
                effects[effectName].Play();
            }
        }
    }
}
