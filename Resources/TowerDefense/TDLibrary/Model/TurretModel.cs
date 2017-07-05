using TDLibrary.Manager;
using UnityEngine;

namespace TDLibrary.Model {

  public abstract class TurretModel : MonoBehaviour {
    public int cost;
    public GameObject prefab;
    public float turnSpeed = 10f;

    protected Transform currentTarget;
    [SerializeField]
    protected Transform firePosition;
    [SerializeField]
    protected Transform turretBase;

    private TurretManager _turretManager;

    protected abstract void Attack();

    public abstract void ManagedUpdate();

    protected void OnDisable() {
      _turretManager.Unregister((Turret)this);
    }

    protected void OnEnable() {
      _turretManager = TurretManager.instance;
      _turretManager.Register((Turret)this);
    }
  }
}