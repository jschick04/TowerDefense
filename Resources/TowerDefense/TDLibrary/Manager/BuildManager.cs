using UnityEngine;

namespace TDLibrary.Manager {

  public class BuildManager : MonoBehaviour {
    public static BuildManager instance;

    [SerializeField]
    private GameObject _basicTurretPrefab;
    private GameObject _turretToBuild;

    public GameObject GetTurretToBuild() {
      return _turretToBuild;
    }

    private void Awake() {
      if (instance != null) {
        Destroy(this);
      }
      instance = this;
    }

    private void Start() {
      _turretToBuild = _basicTurretPrefab;
    }
  }
}