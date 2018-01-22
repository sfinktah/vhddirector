using System;
using System.Media;

namespace CSharp.cc.Media
{
    public class Audio
    {
        static public Boolean PlaySoundBg(String SoundLocation)
        {
            SoundPlayer sp = new SoundPlayer();
            sp.SoundLocation = SoundLocation;

            try
            {
                sp.Play();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("SoundPlay: " + e.Message);
                return false;
            }

            return true;
        }
    }
}
