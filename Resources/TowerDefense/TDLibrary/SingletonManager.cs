using UnityEngine;

namespace TDLibrary {

  public class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour {
    public bool isPersistant;

    public static T Instance { get; private set; }

    protected virtual void Awake() {
      if (isPersistant) {
        if (Instance == null) {
          Instance = this as T;
        } else {
          DestroyObject(gameObject);
        }

        DontDestroyOnLoad(gameObject);
      } else {
        Instance = this as T;
      }
    }
  }

}