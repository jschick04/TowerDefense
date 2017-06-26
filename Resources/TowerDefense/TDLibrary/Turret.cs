using System.Collections.Generic;
using TDLibrary.Manager;
using UnityEngine;

namespace TDLibrary {

  public class Turret : MonoBehaviour {
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
      GameObject bulletObject = Instantiate(_bulletPrefab, _firePosition.position, _firePosition.rotation);
      var bullet = bulletObject.GetComponent<Bullet>();

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
      _attackCooldown = _attackCooldown <= 0f ? 0f : _attackCooldown -= Time.deltaTime;

      if (_currentTarget == null) {
        return;
      }

      Vector3 direction = _currentTarget.position - transform.position;
      Quaternion lookRotation = Quaternion.LookRotation(direction);
      Vector3 rotation = Quaternion.Lerp(_turretBase.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
      _turretBase.rotation = Quaternion.Euler(0f, rotation.y, 0f);

      if (_attackCooldown <= 0f) {
        Attack();
        _attackCooldown = 1f / attackSpeed;
      }
    }

    private void UpdateTarget() {
      List<Enemy> enemies = EnemyManager.instance.Enemies;
      float shortestDistance = Mathf.Infinity;
      Enemy closestEnemy = null;

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