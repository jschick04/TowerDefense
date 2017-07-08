using System.Collections.Generic;
using TDLibrary.Manager;
using TDLibrary.Model.TurretType;
using UnityEngine;

namespace TDLibrary.Model {

  public abstract class TurretModel : MonoBehaviour {
    [SerializeField]
    internal BaseTurret turretType;

    protected float attackCooldown;
    protected Enemy currentTarget;
    [SerializeField]
    protected Transform firePosition;
    [SerializeField]
    protected Transform turretBase;

    [SerializeField]
    private string _name;
    private TurretManager _turretManager;

    public string Name => _name;

    public abstract void ManagedUpdate();

    protected virtual void Attack() {
      if (currentTarget != null) {
        turretType.Attack(firePosition, currentTarget);
      }
    }

    protected void FindClosestTarget() {
      List<Enemy> enemies = EnemyManager.Instance.Enemies;
      float shortestDistance = Mathf.Infinity;
      Enemy closestEnemy = null;

      foreach (var enemy in enemies) {
        float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

        if (distanceToEnemy < shortestDistance) {
          shortestDistance = distanceToEnemy;
          closestEnemy = enemy;
        }

        currentTarget = closestEnemy != null && shortestDistance <= turretType.attackRange ? closestEnemy : null;
      }
    }

    protected void OnDisable() {
      _turretManager?.Unregister((Turret)this);
    }

    protected void OnEnable() {
      _turretManager = TurretManager.Instance;
      _turretManager.Register((Turret)this);
    }
  }

}