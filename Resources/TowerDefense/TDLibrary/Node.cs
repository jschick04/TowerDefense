using TDLibrary.Manager;
using UnityEngine;
using UnityEngine.EventSystems;

namespace TDLibrary {

  public class Node : MonoBehaviour {
    private BuildManager _buildManager;
    [SerializeField]
    private Color _hoverColor;
    [SerializeField]
    private Color _insufficientFundsColor;
    [SerializeField]
    private Vector3 _offset;
    private Renderer _rend;
    private Color _startColor;
    public GameObject Turret { get; set; }

    public Vector3 GetBuildPosition() {
      return transform.position + _offset;
    }

    private void OnMouseDown() {
      if (EventSystem.current.IsPointerOverGameObject()) {
        return;
      }

      if (!_buildManager.CanBuild) {
        return;
      }

      if (Turret != null) {
        Debug.Log("Cannot build here");
        return;
      }

      _buildManager.BuildTurretOn(this);
    }

    private void OnMouseEnter() {
      if (EventSystem.current.IsPointerOverGameObject()) {
        return;
      }

      if (!_buildManager.CanBuild) {
        return;
      }

      if (_buildManager.IsAffordable) {
        _rend.material.color = _hoverColor;
      } else {
        _rend.material.color = _insufficientFundsColor;
      }
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