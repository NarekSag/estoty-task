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
}
