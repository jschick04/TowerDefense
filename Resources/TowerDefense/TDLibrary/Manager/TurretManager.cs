using System.Collections.Generic;

namespace TDLibrary.Manager {

  public class TurretManager : SingletonManager<TurretManager> {
    protected TurretManager() {
      Turrets = new List<Turret>();
    }

    public List<Turret> Turrets { get; protected set; }

    public void Register(Turret turret) {
      if (!Turrets.Contains(turret)) {
        Turrets.Add(turret);
      }
    }

    public void Unregister(Turret turret) {
      if (Turrets.Contains(turret)) {
        Turrets.Remove(turret);
      }
    }

    private void Update() {
      for (int i = 0; i < Turrets.Count; i++) {
        Turrets[i].ManagedUpdate();
      }
    }
  }
}