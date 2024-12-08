using UnityEngine.SceneManagement;

public static class RuntimeConstants
{
    public static class Scenes
    {
        public static readonly int Bootstrap = SceneUtility.GetBuildIndexByScenePath("0.Bootstrap");
        public static readonly int GameScene = SceneUtility.GetBuildIndexByScenePath("1.Core");
    }
}
