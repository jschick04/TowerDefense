using TDLibrary.Manager;
using UnityEngine;

namespace TDLibrary {

  public class Shop : MonoBehaviour {
    private BuildManager _buildmanager;

    #region TurretTypes

    [SerializeField]
    private TurretBlueprint _basicTurret;
    [SerializeField]
    private TurretBlueprint _rocketTurret;

    #endregion

    public void SelectBasicTurret() {
      Debug.Log("Purchased basic turret");
      _buildmanager.SelectTurretToBuild(_basicTurret);
    }

    public void SelectRocketTurret() {
      Debug.Log("Purchased rocket turret");
      _buildmanager.SelectTurretToBuild(_rocketTurret);
    }

    private void Start() {
      _buildmanager = BuildManager.instance;
    }
  }

}