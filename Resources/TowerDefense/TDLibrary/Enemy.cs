using UnityEngine;

namespace TDLibrary {

  public class Enemy : MonoBehaviour {
    public float speed = 10f;

    private Transform _target;
    private int _wavepointIndex = 0;

    private void Start() {
      _target = Waypoints.waypoints[0];
    }

    private void Update() {
      Vector3 direction = _target.position - transform.position;
      transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);


    }
  }

}