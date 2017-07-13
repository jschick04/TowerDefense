using UnityEngine;

namespace TDLibrary.Manager {
  public class DebugManager : SingletonManager<DebugManager> {
    protected DebugManager() { }

    private void Update() {
      if (!Application.isPlaying) { return; }

      if (Input.GetKeyDown(KeyCode.P)) {
        GameManager.Instance.EndGame();
      }
    }
  }
}
