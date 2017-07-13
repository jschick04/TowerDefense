using UnityEngine;

namespace TDLibrary.Manager {

  public class PlayerManager : SingletonManager<PlayerManager> {
    [SerializeField]
    private int _lives = 20;
    [SerializeField]
    private int _money = 400;

    protected PlayerManager() { }

    public int Lives {
      get { return _lives; }
      set {
        if (value != null) {
          _lives = value;
        }
      }
    }

    public int Money {
      get { return _money; }
      set {
        if (value != null) {
          _money = value;
        }
      }
    }
  }

}