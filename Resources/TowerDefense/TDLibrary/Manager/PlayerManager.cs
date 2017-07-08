using UnityEngine;

namespace TDLibrary.Manager {

  public class PlayerManager : SingletonManager<PlayerManager> {
    public static int lives;
    public static int money;

    [SerializeField]
    private int _startingLives = 20;
    [SerializeField]
    private int _startingMoney = 400;

    protected PlayerManager() { }

    private void Start() {
      money = _startingMoney;
      lives = _startingLives;
    }
  }
}