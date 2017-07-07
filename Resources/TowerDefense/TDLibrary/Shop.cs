using System.Collections.Generic;
using TDLibrary.Manager;
using UnityEngine;
using UnityEngine.UI;

namespace TDLibrary {

  public class Shop : MonoBehaviour {
    private BuildManager _buildmanager;
    [SerializeField]
    private CanvasRenderer _itemTemplate;
    [SerializeField]
    private List<Turret> _turretPrefabs;

    private void CreateMenuItem(Turret turret) {
      CanvasRenderer button = Instantiate(_itemTemplate, transform);
      Text[] menuItemText = button.GetComponentsInChildren<Text>();

      foreach (var itemText in menuItemText) {
        switch (itemText.name) {
          case "Item":
            itemText.text = turret.Name;
            break;

          case "Cost":
            itemText.text = $"$ {turret.turretType.cost}";
            break;

          default:
            break;
        }
      }

      var clickEvent = button.GetComponent<Button>();
      clickEvent.onClick.AddListener(() => _buildmanager.SelectTurretToBuild(turret));
    }

    private void Start() {
      _buildmanager = BuildManager.Instance;

      foreach (var prefab in _turretPrefabs) {
        CreateMenuItem(prefab);
      }
    }
  }

}