﻿using UnityEngine;

namespace TDLibrary {

  public class SingletonManager<T> : MonoBehaviour where T : MonoBehaviour {
    private static readonly object _lock = new object();
    private static T _instance;

    public static T Instance {
      get {
        lock (_lock) {
          if (_instance == null) {
            _instance = (T)FindObjectOfType(typeof(T));

            if (_instance == null) {
              var singleton = new GameObject();
              _instance = singleton.AddComponent<T>();
              singleton.name = typeof(T).ToString();

              DontDestroyOnLoad(singleton);
            }
          }

          return _instance;
        }
      }
    }
  }

}