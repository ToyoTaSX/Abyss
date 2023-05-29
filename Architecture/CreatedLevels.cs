using Abyss.Entities;
using Abyss.Maps;

namespace Abyss.Architecture
{
    public static class CreatedLevels
    {
        public static Level GetLevel1(Player player, int? enemiesCount = null, int? targetsCount = null, int? targetsCollected = null) =>
            new Level(
                    player,
                    CreatedMaps.Map1,
                    targetsCount == null ? 6 : (int)targetsCount,
                    enemiesCount == null ? 8 : (int)enemiesCount,
                    targetsCollected == null ? 0 : (int)targetsCollected);

        public static Level GetLevel2(Player player, int? enemiesCount = null, int? targetsCount = null, int? targetsCollected = null) =>
            new Level(
                    player,
                    CreatedMaps.Map2,
                    targetsCount == null ? 9 : (int)targetsCount,
                    enemiesCount == null ? 8 : (int)enemiesCount,
                    targetsCollected == null ? 0 : (int)targetsCollected);

        public static Level GetLevel3(Player player, int? enemiesCount = null, int? targetsCount = null, int? targetsCollected = null) =>
            new Level(
                    player,
                    CreatedMaps.Map3,
                    targetsCount == null ? 6 : (int)targetsCount,
                    enemiesCount == null ? 10 : (int)enemiesCount,
                    targetsCollected == null ? 0 : (int)targetsCollected);

        public static Level GetLevel4(Player player, int? enemiesCount = null, int? targetsCount = null, int? targetsCollected = null) =>
            new Level(
                    player,
                    CreatedMaps.Map4,
                    targetsCount == null ? 6 : (int)targetsCount,
                    enemiesCount == null ? 10 : (int)enemiesCount,
                    targetsCollected == null ? 0 : (int)targetsCollected);

        public static Level GetLevel5(Player player, int? enemiesCount = null, int? targetsCount = null, int? targetsCollected = null) =>
            new Level(
                    player,
                    CreatedMaps.Map5,
                    targetsCount == null ? 8 : (int)targetsCount,
                    enemiesCount == null ? 12 : (int)enemiesCount,
                    targetsCollected == null ? 0 : (int)targetsCollected);

    }
}
