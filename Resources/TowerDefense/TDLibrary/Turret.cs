using TDLibrary.Model;
using TDLibrary.Model.TurretType;
using UnityEngine;

namespace TDLibrary {

  public class Turret : TurretModel {
    public override void ManagedUpdate() {
      attackCooldown = attackCooldown <= 0f ? 0f : attackCooldown -= Time.deltaTime;

      if (currentTarget == null) {
        if (turretType.TurretType == TurretType.Laser) {
          ((LaserTurret)turretType).DisableLaser();
        }

        return;
      }

      LockOnTarget();

      switch (turretType.TurretType) {
        case TurretType.Laser :
          Attack();
          break;

        case TurretType.Bullet :
          if (attackCooldown <= 0f) {
            Attack();
            attackCooldown = 1f / ((BulletTurret)turretType).attackSpeed;
          }
          break;

        default :
          Debug.LogWarning("Invalid TurretType");
          break;
      }
    }

    private void Awake() {
      // Clone's the turret type and replaces the attached prefab to prevent laser's from stop
      // working after a new laser is spawned
      if (turretType.TurretType == TurretType.Laser) {
        var laserTurret = (LaserTurret)Instantiate(turretType, gameObject.transform);
        laserTurret.LaserEffect = Instantiate(laserTurret.laserEffectPrefab, laserTurret.transform);
        laserTurret.LaserBeam = Instantiate(laserTurret.lineRendererPrefab, laserTurret.transform);
        laserTurret.ImpactLight = GetComponentInChildren<Light>();
        turretType = laserTurret;
      }
    }

    /// <summary>Keeps the turret's rotation facing the current target</summary>
    private void LockOnTarget() {
      Vector3 direction = currentTarget.transform.position - transform.position;
      Quaternion lookRotation = Quaternion.LookRotation(direction);
      Vector3 rotation = Quaternion.Lerp(
        turretBase.rotation,
        lookRotation,
        Time.deltaTime * turretType.turnSpeed
      ).eulerAngles;
      turretBase.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void OnDrawGizmosSelected() {
      Gizmos.color = Color.red;
      Gizmos.DrawWireSphere(transform.position, turretType.attackRange);
    }

    private void Start() {
      InvokeRepeating(nameof(FindClosestTarget), 0f, 0.5f);
    }
  }

}