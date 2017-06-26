using UnityEngine;

namespace TDLibrary.Manager {

  public class BuildManager : MonoBehaviour {
    public static BuildManager instance;

    public GameObject basicTurretPrefab;

    private GameObject _turretToBuild;

    public GameObject GetTurretToBuild() {
      return _turretToBuild;
    }

    public void SetTurretToBuild(GameObject turret) {
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