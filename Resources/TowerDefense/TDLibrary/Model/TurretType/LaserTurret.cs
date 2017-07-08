using UnityEngine;

namespace TDLibrary.Model.TurretType {

  [CreateAssetMenu(fileName = "Laser", menuName = "Turret Type/Laser")]
  public class LaserTurret : BaseTurret {
    [SerializeField]
    internal ParticleSystem laserEffectPrefab;
    [SerializeField]
    internal LineRenderer lineRendererPrefab;

    private LineRenderer _laserBeam;
    private ParticleSystem _laserEffect;

    internal LineRenderer LaserBeam {
      get { return _laserBeam; }
      set {
        if (value != null) {
          _laserBeam = value;
        }
      }
    }

    internal ParticleSystem LaserEffect {
      get { return _laserEffect; }
      set {
        if (value != null) {
          _laserEffect = value;
        }
      }
    }

    internal override TurretType TurretType { get; } = TurretType.Laser;

    public override void Attack(Transform firePosition, Transform currentTarget) {
      if (!LaserBeam.enabled) {
        LaserBeam.enabled = true;
        LaserEffect.Play();
      }

      LaserBeam.SetPosition(0, firePosition.position);
      LaserBeam.SetPosition(1, currentTarget.position);

      Vector3 direction = firePosition.position - currentTarget.position;

      LaserEffect.transform.rotation = Quaternion.LookRotation(direction);
      LaserEffect.transform.position = currentTarget.position + direction.normalized;
    }

    public void DisableLaser() {
      if (LaserBeam.enabled) {
        LaserBeam.enabled = false;
        LaserEffect.Stop();
      }
    }
  }

}