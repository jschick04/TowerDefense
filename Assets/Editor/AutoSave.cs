using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public class AutoSave {
  static AutoSave() {
    EditorApplication.playmodeStateChanged = AutoSaveOsStateChanged;
  }

  private static void AutoSaveOsStateChanged() {
    if (EditorApplication.isPlaying) {
      return;
    }

    EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
    AssetDatabase.SaveAssets();
  }
}