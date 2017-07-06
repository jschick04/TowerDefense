using UnityEngine;

namespace TDLibrary.Model.TurretType {

  [CreateAssetMenu(fileName = "Turret", menuName = "Turrets/LaserTurret")]
  public class LaserTurret : BaseTurret {
    public ParticleSystem laserEffect;
    public LineRenderer lineRenderer;

    public override TurretType TurretType { get; protected set; } = TurretType.Laser;

    public override void Attack(Transform firePosition, Transform currentTarget) {
      if (!lineRenderer.enabled) {
        lineRenderer.enabled = true;
        laserEffect.Play();
      }

      lineRenderer.SetPosition(0, firePosition.position);
      lineRenderer.SetPosition(1, currentTarget.position);

      Vector3 direction = firePosition.position - currentTarget.position;

      laserEffect.transform.rotation = Quaternion.LookRotation(direction);
      laserEffect.transform.position = currentTarget.position + direction.normalized;
    }

    public void DisableLaser() {
      if (lineRenderer.enabled) {
        lineRenderer.enabled = false;
        laserEffect.Stop();
      }
    }
  }

}