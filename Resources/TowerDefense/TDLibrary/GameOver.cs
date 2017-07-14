using TDLibrary.Manager;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TDLibrary {

  public class GameOver : MonoBehaviour {
    public Text waveText;

    public void LoadMainMenu() {
      // TODO: Create Menu Loading Function
      Debug.Log("Loading Menu");
    }

    public void Retry() {
      // TODO: Maybe move all DoNotDestroy to base sceene and addin level to reset added in scene
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      GameManager.Instance.gameOver = false;
      GameManager.Instance.gameOverMenu.SetActive(false);
    }

    private void OnEnable() {
      waveText.text = GameManager.Instance.GetWaveNumber().ToString();
    }
  }

}