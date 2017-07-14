using UnityEngine;

namespace TDLibrary.Manager {

  public class BuildManager : SingletonManager<BuildManager> {
    [SerializeField]
    private GameObject _buildEffectPrefab;
    private Turret _turretToBuild;

    protected BuildManager() { }

    public bool CanBuild => _turretToBuild != null;

    public bool IsAffordable => PlayerManager.Instance.Money >= _turretToBuild.turretType.cost;

    public void BuildTurretOn(Node node) {
      if (PlayerManager.Instance.Money < _turretToBuild.turretType.cost) {
        Debug.Log("Not enough money to build");
        return;
      }

      PlayerManager.Instance.Money -= _turretToBuild.turretType.cost;
      node.Turret = Instantiate(_turretToBuild.turretType.prefab, node.GetBuildPosition(), Quaternion.identity);

      GameObject effect = Instantiate(_buildEffectPrefab, node.GetBuildPosition(), Quaternion.identity);
      Destroy(effect, 5f);
    }

    public void SelectTurretToBuild(Turret turret) {
      _turretToBuild = turret;
    }
  }

}