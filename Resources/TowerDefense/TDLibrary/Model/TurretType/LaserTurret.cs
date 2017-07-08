using UnityEngine;

namespace TDLibrary.Model.TurretType {

  [CreateAssetMenu(fileName = "Laser", menuName = "Turret Type/Laser")]
  public class LaserTurret : BaseTurret {
    [SerializeField]
    internal ParticleSystem laserEffectPrefab;
    [SerializeField]
    internal LineRenderer lineRendererPrefab;

    private readonly int _damageOverTime = 30;
    private Light _impactLight;
    private LineRenderer _laserBeam;
    private ParticleSystem _laserEffect;
    [SerializeField]
    private float _slowPercent = .5f;

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

    private Light ImpactLight => _impactLight ?? (_impactLight = LaserEffect.GetComponentInChildren<Light>());

    public override void Attack(Transform firePosition, Enemy currentTarget) {
      if (!LaserBeam.enabled) {
        LaserBeam.enabled = true;
        LaserEffect.Play();
        if (_impactLight != null) {
          ImpactLight.enabled = true;
        }
      }

      LaserBeam.SetPosition(0, firePosition.position);
      LaserBeam.SetPosition(1, currentTarget.transform.position);

      Vector3 direction = firePosition.position - currentTarget.transform.position;

      LaserEffect.transform.rotation = Quaternion.LookRotation(direction);
      LaserEffect.transform.position = currentTarget.transform.position + direction.normalized;

      currentTarget.TakeDamage(_damageOverTime * Time.deltaTime);
      currentTarget.Slow(_slowPercent);
    }

    /// <summary>Disables all laser visual effects</summary>
    public void DisableLaser() {
      if (LaserBeam.enabled) {
        LaserBeam.enabled = false;
        LaserEffect.Stop();
        if (_impactLight != null) {
          ImpactLight.enabled = false;
        }
      }
    }
  }

}