using Abyss.ContentClasses;

namespace Abyss.Maps
{
    public static class CreatedMaps
    {
        public static Map Map1 { get => MapGenerator.CreateMapFromImage(MapImages.Map1); }
        public static Map Map2 { get => MapGenerator.CreateMapFromImage(MapImages.Map2); }
        public static Map Map3 { get => MapGenerator.CreateMapFromImage(MapImages.Map3); }
        public static Map Map4 { get => MapGenerator.CreateMapFromImage(MapImages.Map4); }
        public static Map Map5 { get => MapGenerator.CreateMapFromImage(MapImages.Map5); }

        public static Map EmptyMap { get => MapGenerator.CreateEmptyMap(75, 75); }
        
    }
}
