using UnityEngine;

namespace TDLibrary.Model.TurretType {

  public enum TurretType {
    Bullet,
    Laser
  }

  public abstract class BaseTurret : ScriptableObject {
    public float attackRange = 15f;
    public int cost;
    public GameObject prefab;
    public float turnSpeed = 10f;

    internal abstract TurretType TurretType { get; }

    public abstract void Attack(Transform firePosition, Transform currentTarget);
  }

}