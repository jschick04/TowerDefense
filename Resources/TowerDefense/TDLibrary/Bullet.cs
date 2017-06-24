using UnityEngine;

namespace TDLibrary {

  public class Bullet : MonoBehaviour {
    public float speed = 70f;

    [SerializeField]
    private GameObject _impactEffect;
    private Transform _target;

    public void Track(Transform target) {
      _target = target;
    }

    private void HitTarget() {
      var effect = Instantiate(_impactEffect, transform.position, transform.rotation);
      Destroy(effect, 2f);
      Destroy(gameObject);
      Destroy(_target.gameObject);
    }

    private void Update() {
      if (_target == null) {
        Destroy(gameObject);
        return;
      }

      var direction = _target.position - transform.position;
      var distanceThisFrame = speed * Time.deltaTime;

      if (direction.magnitude <= distanceThisFrame) {
        HitTarget();
        return;
      }

      transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }
  }

}