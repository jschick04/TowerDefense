using TDLibrary.Manager;
using UnityEngine;

namespace TDLibrary {

  public class Enemy : MonoBehaviour {
    public float moveSpeed = 10f;

    [SerializeField]
    private GameObject _deathEffect;
    [SerializeField]
    private int _health = 100;
    [SerializeField]
    private int _rewardAmount = 25;
    private int _waypointIndex;
    private Transform _waypointTarget;

    public int Health { get { return _health; } }

    public void ManagedUpdate() {
      Vector3 direction = _waypointTarget.position - transform.position;
      transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

      if (Vector3.Distance(transform.position, _waypointTarget.position) <= 0.4f) {
        GetNextWaypoint();
      }
    }

    public void TakeDamage(int damage) {
      _health -= damage;

      if (Health <= 0) {
        Die();
      }
    }

    private void Die() {
      GameObject effect = Instantiate(_deathEffect, transform.position, Quaternion.identity);
      Destroy(effect, 5f);

      PlayerManager.money += _rewardAmount;

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
      EnemyManager.instance.Unregister(this);
    }

    private void OnEnable() {
      EnemyManager.instance.Register(this);
      _waypointTarget = Waypoints.points[0];
    }

    private void ReachFinalWaypoint() {
      PlayerManager.lives--;
      Destroy(gameObject);
    }
  }

}