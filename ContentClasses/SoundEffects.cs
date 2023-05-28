using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abyss.ContentClasses
{
    public static class SoundEffects
    {
        //private static string MapGenerator = "MapGenerator";
        public static SoundEffect ButtonOnActive;
        public static SoundEffect ButtonOnClick;
        public static SoundEffect Shoot1;
        public static SoundEffect Shoot2;
        public static SoundEffect Shoot3;
        public static SoundEffect Shoot4;
        public static SoundEffect PlayerDamage;
        public static SoundEffect EnemyDamage;
        public static void Load(ContentManager content)
        {
            ButtonOnActive = content.Load<SoundEffect>("ButtonOnActive");
            ButtonOnClick = content.Load<SoundEffect>("ButtonOnClick");
            Shoot1 = content.Load<SoundEffect>("Shoot1");
            Shoot2 = content.Load<SoundEffect>("Shoot2");
            Shoot3 = content.Load<SoundEffect>("Shoot3");
            Shoot4 = content.Load<SoundEffect>("Shoot4");
            PlayerDamage = content.Load<SoundEffect>("PlayerDamage");
            EnemyDamage = content.Load<SoundEffect>("EnemyDamage");
        }
    }
}
