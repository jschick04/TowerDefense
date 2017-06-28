using UnityEngine;

namespace TDLibrary.Manager {

  public class BuildManager : MonoBehaviour {
    public static BuildManager instance;

    public GameObject basicTurretPrefab;
    public GameObject rocketTurretPrefab;

    private TurretBlueprint _turretToBuild;

    public bool CanBuild { get { return _turretToBuild != null; } }

    public void BuildTurretOn(Node node) {
      if (PlayerManager.money < _turretToBuild.cost) {
        Debug.Log("Not enough money to build");
        return;
      }

      PlayerManager.money -= _turretToBuild.cost;
      node.Turret = Instantiate(_turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity);

      Debug.Log($"Money left: {PlayerManager.money}");
    }

    public void SelectTurretToBuild(TurretBlueprint turret) {
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