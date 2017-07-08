using UnityEngine;

namespace TDLibrary {

  public class Bullet : MonoBehaviour {
    public float explosionRadius;
    public float speed = 70f;

    [SerializeField]
    private int _damage = 50;
    [SerializeField]
    private GameObject _impactEffect;
    private Transform _target;

    public void Track(Transform target) {
      _target = target;
    }

    private void Damage(Transform enemy) {
      var e = enemy.GetComponent<Enemy>();
      e?.TakeDamage(_damage);
    }

    private void Explode() {
      Collider[] targets = Physics.OverlapSphere(transform.position, explosionRadius);
      foreach (var target in targets) {
        if (target.CompareTag("Enemy")) {
          Damage(target.transform);
        }
      }
    }

    private void HitTarget() {
      GameObject effect = Instantiate(_impactEffect, transform.position, transform.rotation);
      Destroy(effect, 5f);

      if (explosionRadius > 0f) {
        Explode();
      } else {
        Damage(_target);
      }

      Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.blue;
      Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

    private void Update() {
      if (_target == null) {
        Destroy(gameObject);
        return;
      }

      Vector3 direction = _target.position - transform.position;
      float distanceThisFrame = speed * Time.deltaTime;

      if (direction.magnitude <= distanceThisFrame) {
        HitTarget();
        return;
      }

      transform.Translate(direction.normalized * distanceThisFrame, Space.World);
      transform.LookAt(_target);
    }
  }

}