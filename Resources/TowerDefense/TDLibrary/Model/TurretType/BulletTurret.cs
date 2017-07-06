using UnityEngine;

namespace TDLibrary.Model.TurretType {

  [CreateAssetMenu(fileName = "Turret", menuName = "Turrets/BulletTurret")]
  public class BulletTurret : BaseTurret {
    public float attackSpeed = 1f;
    public GameObject bulletPrefab;

    public override TurretType TurretType { get; protected set; } = TurretType.Bullet;

    public override void Attack(Transform firePosition, Transform currentTarget) {
      GameObject bulletObject = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
      var bullet = bulletObject.GetComponent<Bullet>();

      if (bullet != null) {
        bullet.Track(currentTarget);
      }
    }
  }

}