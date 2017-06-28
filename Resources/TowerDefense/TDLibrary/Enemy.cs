using TDLibrary.Manager;
using UnityEngine;

namespace TDLibrary {

  public class Enemy : MonoBehaviour {
    public float moveSpeed = 10f;

    private int _waypointIndex;
    private Transform _waypointTarget;

    public void ManagedUpdate() {
      Vector3 direction = _waypointTarget.position - transform.position;
      transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

      if (Vector3.Distance(transform.position, _waypointTarget.position) <= 0.4f) {
        GetNextWaypoint();
      }
    }

    private void GetNextWaypoint() {
      if (_waypointIndex >= Waypoints.points.Length - 1) {
        Destroy(gameObject);
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
  }

}