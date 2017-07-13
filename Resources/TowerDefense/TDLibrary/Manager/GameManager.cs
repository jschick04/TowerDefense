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
    private GameObject _gameOverMenu;
    private PlayerManager _playerManager;
    private float _waveCountdown;
    private int _waveNumber;

    protected GameManager() { }

    internal void EndGame() {
      gameOver = true;
      _gameOverMenu.SetActive(true);
      Debug.Log("Game Over!");
    }

    private void Awake() {
      _playerManager = PlayerManager.Instance;
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

      if (_playerManager.Lives <= 0) {
        EndGame();
      }

      moneyBalance.text = $"$ {_playerManager.Lives}";
      remainingLives.text = $"{_playerManager.Lives} Lives";

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