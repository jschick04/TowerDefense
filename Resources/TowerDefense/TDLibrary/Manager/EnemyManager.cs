using System.Collections.Generic;

namespace TDLibrary.Manager {

  public class EnemyManager : SingletonManager<EnemyManager> {
    protected EnemyManager() {
      Enemies = new List<Enemy>();
    }

    public List<Enemy> Enemies { get; protected set; }

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

    private void Update() {
      for (int i = 0; i < Enemies.Count; i++) {
        Enemies[i].ManagedUpdate();
      }
    }
  }

}