using UnityEngine;

namespace TDLibrary.Manager {
  public class PlayerManager : MonoBehaviour {
    public static int money;
    [SerializeField]
    private int _startingMoney = 400;

    private void Start() {
      money = _startingMoney;
    }
  }
}
