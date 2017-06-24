using UnityEngine;

namespace TDLibrary {

  public class Turret : MonoBehaviour {
    public string enemyTag = "Enemy";
    public float turnSpeed = 10f;

    [SerializeField]
    private GameObject _bulletPrefab;
    private Transform _currentTarget;
    [SerializeField]
    private Transform _firePosition;
    [SerializeField]
    private Transform _turretBase;

    #region CombatProperties

    [Header("Combat Properties")]
    public float attackRange = 15f;
    public float attackSpeed = 1f;
    private float _attackCooldown;

    #endregion

    private void Attack() {
      var bulletGO = (GameObject)Instantiate(_bulletPrefab, _firePosition.position, _firePosition.rotation);
      var bullet = bulletGO.GetComponent<Bullet>();

      if (bullet != null) {
        bullet.Track(_currentTarget);
      }
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Start() {
      InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    private void Update() {
      if (_currentTarget == null) {
        return;
      }

      var direction = _currentTarget.position - transform.position;
      var lookRotation = Quaternion.LookRotation(direction);
      var rotation = Quaternion.Lerp(_turretBase.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
      _turretBase.rotation = Quaternion.Euler(0f, rotation.y, 0f);

      if (_attackCooldown <= 0f) {
        Attack();
        _attackCooldown = 1f / attackSpeed;
      }

      _attackCooldown -= Time.deltaTime;
    }

    private void UpdateTarget() {
      GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
      float shortestDistance = Mathf.Infinity;
      GameObject closestEnemy = null;

      foreach (var enemy in enemies) {
        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

        if (distanceToEnemy < shortestDistance) {
          shortestDistance = distanceToEnemy;
          closestEnemy = enemy;
        }

        if (closestEnemy != null && shortestDistance <= attackRange) {
          _currentTarget = closestEnemy.transform;
        }
      }
    }
  }

}