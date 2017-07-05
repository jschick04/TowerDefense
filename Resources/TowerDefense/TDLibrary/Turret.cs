using System.Collections.Generic;
using TDLibrary.Manager;
using TDLibrary.Model;
using UnityEngine;

namespace TDLibrary {

  public class Turret : TurretModel {
    #region BulletProperties

    [Header("Bullet Properties")]
    public float attackRange = 15f;
    public float attackSpeed = 1f;

    private float _attackCooldown;
    [SerializeField]
    private GameObject _bulletPrefab;

    #endregion

    #region LaserProperties

    [Header("Laser Properties")]
    [SerializeField]
    private ParticleSystem _laserEffect;
    [SerializeField]
    private LineRenderer _lineRenderer;
    [SerializeField]
    private bool _useLaser;

    #endregion

    protected override void Attack() {
      GameObject bulletObject = Instantiate(_bulletPrefab, firePosition.position, firePosition.rotation);
      var bullet = bulletObject.GetComponent<Bullet>();

      if (bullet != null) {
        bullet.Track(currentTarget);
      }
    }

    private void LaserAttack() {
      if (!_lineRenderer.enabled) {
        _lineRenderer.enabled = true;
        _laserEffect.Play();
      }

      _lineRenderer.SetPosition(0, firePosition.position);
      _lineRenderer.SetPosition(1, currentTarget.position);

      Vector3 direction = firePosition.position - currentTarget.position;

      _laserEffect.transform.rotation = Quaternion.LookRotation(direction);
      _laserEffect.transform.position = currentTarget.position + direction.normalized;
    }

    private void LockOnTarget() {
      Vector3 direction = currentTarget.position - transform.position;
      Quaternion lookRotation = Quaternion.LookRotation(direction);
      Vector3 rotation = Quaternion.Lerp(turretBase.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
      turretBase.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Start() {
      InvokeRepeating(nameof(UpdateTarget), 0f, 0.5f);
    }

    public override void ManagedUpdate() {
      _attackCooldown = _attackCooldown <= 0f ? 0f : _attackCooldown -= Time.deltaTime;

      if (currentTarget == null) {
        if (_useLaser) {
          if (_lineRenderer.enabled) {
            _lineRenderer.enabled = false;
            _laserEffect.Stop();
          }
        }

        return;
      }

      LockOnTarget();

      if (_useLaser) {
        LaserAttack();
      } else {
        if (_attackCooldown <= 0f) {
          Attack();
          _attackCooldown = 1f / attackSpeed;
        }
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
          currentTarget = closestEnemy.transform;
        }
      }
    }
  }

}