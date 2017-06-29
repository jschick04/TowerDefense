using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TDLibrary.Manager {

  public class GameManager : MonoBehaviour {
    public Transform enemyPrefab;
    public Text moneyBalance;
    public Transform spawnPoint;
    public Text waveCountdownText;
    public float waveDuration = 20f;

    private float _waveCountdown;
    private int _waveNumber;

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
      moneyBalance.text = $"$ {PlayerManager.money}";

      if (_waveCountdown <= 0) {
        StartCoroutine(SpawnWave());
        _waveCountdown = waveDuration;
      }

      _waveCountdown -= Time.deltaTime;
      _waveCountdown = Mathf.Clamp(_waveCountdown, 0f, Mathf.Infinity);
      waveCountdownText.text = string.Format("{0:00.0}", _waveCountdown);
    }
  }

}