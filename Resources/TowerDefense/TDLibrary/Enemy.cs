﻿using UnityEngine;

namespace TDLibrary {

  public class Enemy : MonoBehaviour {
    public float moveSpeed = 10f;

    private Transform _waypointTarget;
    private int _waypointIndex;

    private void GetNextWaypoint() {
      if (_waypointIndex >= Waypoints.points.Length - 1) {
        Destroy(gameObject);
        return;
      }

      _waypointIndex++;
      _waypointTarget = Waypoints.points[_waypointIndex];
    }

    private void Start() {
      _waypointTarget = Waypoints.points[0];
    }

    private void Update() {
      Vector3 direction = _waypointTarget.position - transform.position;
      transform.Translate(direction.normalized * moveSpeed * Time.deltaTime, Space.World);

      if (Vector3.Distance(transform.position, _waypointTarget.position) <= 0.4f) {
        GetNextWaypoint();
      }
    }
  }

}