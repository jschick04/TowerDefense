using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TDLibrary.Manager {

  public class GameManager : SingletonManager<GameManager> {
    public Transform enemyPrefab;
    public Text moneyBalance;
    public Text remainingLives;
    public Transform spawnPoint;
    public Text waveCountdownText;
    public float waveDuration = 20f;

    internal bool gameOver;
    [SerializeField]
    internal GameObject gameOverMenu;

    private float _waveCountdown;
    private int _waveNumber;

    protected GameManager() { }

    public int GetWaveNumber() {
      return _waveNumber;
    }

    internal void EndGame() {
      gameOver = true;
      gameOverMenu.SetActive(true);
      Debug.Log("Game Over!");
    }

    private void SpawnEnemy() {
      Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }

    private IEnumerator SpawnWave() {
      _waveNumber++;

      for (int i = 0; i < _waveNumber; i++) {
        SpawnEnemy();
        yield return new WaitForSeconds(0.5f);
      }
    }

    private void Update() {
      if (gameOver) {
        return;
      }

      if (PlayerManager.Instance.Lives <= 0) {
        EndGame();
      }

      moneyBalance.text = $"$ {PlayerManager.Instance.Money}";
      remainingLives.text = $"{PlayerManager.Instance.Lives} Lives";

      if (_waveCountdown <= 0) {
        StartCoroutine(SpawnWave());
        _waveCountdown = waveDuration;
      }

      _waveCountdown -= Time.deltaTime;
      _waveCountdown = Mathf.Clamp(_waveCountdown, 0f, Mathf.Infinity);
      waveCountdownText.text = $"{_waveCountdown:00.0}";
    }
  }

}