using UnityEngine;

namespace TDLibrary.Model.TurretType {

  [CreateAssetMenu(fileName = "Bullet", menuName = "Turret Type/Bullet")]
  public class BulletTurret : BaseTurret {
    public float attackSpeed = 1f;
    public GameObject bulletPrefab;

    internal override TurretType TurretType { get; } = TurretType.Bullet;

    public override void Attack(Transform firePosition, Enemy currentTarget) {
      GameObject bulletObject = Instantiate(bulletPrefab, firePosition.position, firePosition.rotation);
      var bullet = bulletObject.GetComponent<Bullet>();

      bullet?.Track(currentTarget.transform);
    }
  }

}