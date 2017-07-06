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
        case TurretType.Laser:
          Attack();
          break;

        case TurretType.Bullet:
          if (attackCooldown <= 0f) {
            Attack();
            attackCooldown = 1f / ((BulletTurret)turretType).attackSpeed;
          }
          break;

        default:
          break;
      }
    }

    private void LockOnTarget() {
      Vector3 direction = currentTarget.position - transform.position;
      Quaternion lookRotation = Quaternion.LookRotation(direction);
      Vector3 rotation = Quaternion.Lerp(turretBase.rotation, lookRotation, Time.deltaTime * turretType.turnSpeed).eulerAngles;
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