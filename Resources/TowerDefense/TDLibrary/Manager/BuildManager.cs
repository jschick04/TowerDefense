using UnityEngine;

namespace TDLibrary.Manager {

  public class BuildManager : MonoBehaviour {
    public static BuildManager instance;

    [SerializeField]
    private GameObject _buildEffectPrefab;
    private Turret _turretToBuild;

    public bool CanBuild => _turretToBuild != null;

    public bool IsAffordable => PlayerManager.money >= _turretToBuild.turretType.cost;

    public void BuildTurretOn(Node node) {
      if (PlayerManager.money < _turretToBuild.turretType.cost) {
        Debug.Log("Not enough money to build");
        return;
      }

      PlayerManager.money -= _turretToBuild.turretType.cost;
      node.Turret = Instantiate(_turretToBuild.turretType.prefab, node.GetBuildPosition(), Quaternion.identity);

      GameObject effect = Instantiate(_buildEffectPrefab, node.GetBuildPosition(), Quaternion.identity);
      Destroy(effect, 5f);

      Debug.Log($"Money left: {PlayerManager.money}");
    }

    public void SelectTurretToBuild(Turret turret) {
      _turretToBuild = turret;
    }

    private void Awake() {
      if (instance != null) {
        Destroy(this);
      }

      instance = this;
    }
  }

}