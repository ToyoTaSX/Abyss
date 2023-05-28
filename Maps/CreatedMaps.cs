using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abyss;
using Abyss.Maps;
using Abyss.ContentClasses;
using Abyss.Enemies;
using Abyss.Entities;
using Abyss.Objects;
using Abyss.Weapons;
using Abyss.Architecture;
using System.Drawing;
using System.Reflection.Metadata;

namespace Abyss.Maps
{
    public static class CreatedMaps
    {
        public static Map Map1 { get => MapGenerator.CreateMapFromImage(MapImages.Map1); }
        public static Map Map2 { get => MapGenerator.CreateMapFromImage(MapImages.Map1); }
        public static Map Map3 { get => MapGenerator.CreateMapFromImage(MapImages.Map1); }
        public static Map Map4 { get => MapGenerator.CreateMapFromImage(MapImages.Map1); }
        public static Map Map5 { get => MapGenerator.CreateMapFromImage(MapImages.Map1); }

        public static Map EmptyMap { get => MapGenerator.CreateEmptyMap(75, 75); }
        
    }
}
