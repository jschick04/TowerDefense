using System.Collections.Generic;
using UnityEngine;

namespace TDLibrary.Manager {

  public class EnemyManager : MonoBehaviour {
    public static EnemyManager instance;

    public List<Enemy> Enemies { get; private set; }

    public void Register(Enemy enemy) {
      if (!Enemies.Contains(enemy)) {
        Enemies.Add(enemy);
      }
    }

    public void Unregister(Enemy enemy) {
      if (Enemies.Contains(enemy)) {
        Enemies.Remove(enemy);
      }
    }

    private void Awake() {
      if (instance != null) {
        Destroy(this);
      }

      instance = this;

      Enemies = new List<Enemy>();
    }

    private void Update() {
      for (int i = 0; i < Enemies.Count; i++) {
        Enemies[i].ManagedUpdate();
      }
    }
  }

}