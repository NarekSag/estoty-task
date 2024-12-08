using UnityEditor;
using UnityEditor.SceneManagement;

public class SceneSwitcher
{
    [MenuItem("Tools/Scenes/Bootstrap &1", priority = 201)]
    public static void OpenBootstrapScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/0.Bootstrap.unity");
    }

    [MenuItem("Tools/Scenes/GameScene &2", priority = 202)]
    public static void OpenCoreScene()
    {
        EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
        EditorSceneManager.OpenScene("Assets/_Project/Scenes/GameScene.unity");
    }
}
