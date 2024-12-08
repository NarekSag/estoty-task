using UnityEngine.SceneManagement;

public static class RuntimeConstants
{
    public static class Scenes
    {
        public static readonly int Bootstrap = SceneUtility.GetBuildIndexByScenePath("0.Bootstrap");
        public static readonly int Core = SceneUtility.GetBuildIndexByScenePath("1.Core");
    }

    public static class Configs
    {
        public const string ConfigFileName = "Config";
    }

    public static class Resources
    {
        public static class Entities
        {
            public const string Player = "Entities/Player";
            public const string Enemy = "Entities/Enemy";
        }

        public static class Projectiles
        {
            public const string Player = "Projectiles/Projectile_Player";
            public const string Enemy = "Projectiles/Projectile_Enemy";
        }
    }
}
