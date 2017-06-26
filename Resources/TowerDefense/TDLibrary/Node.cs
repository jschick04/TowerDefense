using TDLibrary.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TDLibrary {

  public class Node : MonoBehaviour {
    private BuildManager _buildManager;
    [SerializeField]
    private Color _hoverColor;
    [SerializeField]
    private Vector3 _offset;
    private Renderer _rend;
    private Color _startColor;
    private GameObject _turret;

    private void OnMouseDown() {
      if (EventSystem.current.IsPointerOverGameObject()) {
        return;
      }

      if (_buildManager.GetTurretToBuild() == null) {
        return;
      }

      if (_turret != null) {
        Debug.Log("Cannot build here");
        return;
      }

      GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
      _turret = Instantiate(turretToBuild, transform.position + _offset, transform.rotation);
    }

    private void OnMouseEnter() {
      if (EventSystem.current.IsPointerOverGameObject()) {
        return;
      }

      if (_buildManager.GetTurretToBuild() == null) {
        return;
      }

      _rend.material.color = _hoverColor;
    }

    private void OnMouseExit() {
      _rend.material.color = _startColor;
    }

    private void Start() {
      _buildManager = BuildManager.instance;
      _rend = GetComponent<Renderer>();
      _startColor = _rend.material.color;
    }
  }

}