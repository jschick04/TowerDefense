using System.Collections.Generic;
using TDLibrary.Model;
using UnityEngine;

namespace TDLibrary.Manager {

  public class TurretManager : MonoBehaviour {
    public static TurretManager instance;

    public List<Turret> Turrets { get; private set; }

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

    private void Awake() {
      if (instance != null) {
        Destroy(this);
      }

      instance = this;

      Turrets = new List<Turret>();
    }

    private void Update() {
      foreach (Turret turret in Turrets) {
        turret.ManagedUpdate();
      }
    }
  }
}