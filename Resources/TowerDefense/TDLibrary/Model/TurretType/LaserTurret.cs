using UnityEngine;

namespace TDLibrary.Model.TurretType {

  [CreateAssetMenu(fileName = "Turret", menuName = "Turrets/LaserTurret")]
  public class LaserTurret : BaseTurret {
    private ParticleSystem _laserEffect;
    [SerializeField]
    private ParticleSystem _laserEffectPrefab;
    private LineRenderer _lineRenderer;
    [SerializeField]
    private LineRenderer _lineRendererPrefab;

    public override TurretType TurretType { get; protected set; } = TurretType.Laser;

    public override void Attack(Transform firePosition, Transform currentTarget) {
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

    public void DisableLaser() {
      if (_lineRenderer.enabled) {
        _lineRenderer.enabled = false;
        _laserEffect.Stop();
      }
    }

    private void OnEnable() {
      _laserEffect = Instantiate(_laserEffectPrefab);
      _lineRenderer = Instantiate(_lineRendererPrefab);
    }
  }

}