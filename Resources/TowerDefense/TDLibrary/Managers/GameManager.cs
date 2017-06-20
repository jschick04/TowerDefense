using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace TDLibrary.Managers {

  public class GameManager : MonoBehaviour {
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public Text waveCountdownText;
    public float waveDuration = 5f;

    private float _waveCountdown = 2f;
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
      if (_waveCountdown <= 0) {
        StartCoroutine(SpawnWave());
        _waveCountdown = waveDuration;
      }

      _waveCountdown -= Time.deltaTime;

      waveCountdownText.text = Mathf.Round(_waveCountdown).ToString();
    }
  }

}