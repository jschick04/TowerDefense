using TDLibrary.Manager;
using UnityEngine;

namespace TDLibrary {

  public class Node : MonoBehaviour {
    [SerializeField]
    private Color _hoverColor;
    [SerializeField]
    private Vector3 _offset;
    private Renderer _rend;
    private Color _startColor;
    private GameObject _turret;

    private void OnMouseDown() {
      if (_turret != null) {
        Debug.Log("Cannot build here");
        return;
      }

      GameObject turretToBuild = BuildManager.instance.GetTurretToBuild();
      _turret = Instantiate(turretToBuild, transform.position + _offset, transform.rotation);
    }

    private void OnMouseEnter() {
      _rend.material.color = _hoverColor;
    }

    private void OnMouseExit() {
      _rend.material.color = _startColor;
    }

    private void Start() {
      _rend = GetComponent<Renderer>();
      _startColor = _rend.material.color;
    }
  }

}