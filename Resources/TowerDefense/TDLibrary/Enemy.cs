using TDLibrary.Manager;
using UnityEngine;

namespace TDLibrary {

  public class Enemy : MonoBehaviour {
    public float moveSpeed = 10f;

    [SerializeField]
    private GameObject _deathEffect;
    private EnemyManager _enemyManager;
    [SerializeField]
    private float _health = 100;
    [SerializeField]
    private int _rewardAmount = 25;
    private float _startSpeed;
    private int _waypointIndex;
    private Transform _waypointTarget;

    public float Health => _health;

    public void ManagedUpdate() {
      Vector3 direction = _waypointTarget.position - transform.position;
      transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

      if (Vector3.Distance(transform.position, _waypointTarget.position) <= 0.4f) {
        GetNextWaypoint();
      }

      moveSpeed = _startSpeed;
    }

    public void TakeDamage(float damage) {
      _health -= damage;

      if (Health <= 0) {
        Die();
      }
    }

    internal void Slow(float slowPercent) {
      moveSpeed = _startSpeed * (1f - slowPercent);
    }

    private void Die() {
      GameObject effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
      Destroy(effect, 5f);

      PlayerManager.Instance.Money += _rewardAmount;

      Destroy(gameObject);
    }

    private void GetNextWaypoint() {
      if (_waypointIndex >= Waypoints.points.Length - 1) {
        ReachFinalWaypoint();
        return;
      }

      _waypointIndex++;
      _waypointTarget = Waypoints.points[_waypointIndex];
    }

    private void OnDisable() {
      _enemyManager?.Unregister(this);
    }

    private void OnEnable() {
      _startSpeed = moveSpeed;
      _enemyManager = EnemyManager.Instance;
      _enemyManager.Register(this);
      _waypointTarget = Waypoints.points[0];
    }

    private void ReachFinalWaypoint() {
      PlayerManager.Instance.Lives--;
      Destroy(gameObject);
    }
  }

}