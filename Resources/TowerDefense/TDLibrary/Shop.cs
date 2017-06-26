using TDLibrary.Manager;
using UnityEngine;

namespace TDLibrary {

  public class Shop : MonoBehaviour {
    private BuildManager _buildmanager;

    public void PurchaseBasicTurret() {
      Debug.Log("Purchased basic turret");
      _buildmanager.SetTurretToBuild(_buildmanager.basicTurretPrefab);
    }

    private void Start() {
      _buildmanager = BuildManager.instance;
    }
  }

}