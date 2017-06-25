using UnityEngine;

namespace TDLibrary.Controller {

  public class CameraController : MonoBehaviour {
    public float panBoarderThickness = 10f;
    public float panSpeed = 30f;
    public float scrollSpeed = 5f;

    private readonly float _maxY = 80f;
    private readonly float _minY = 10f;

    private void Update() {
      if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - panBoarderThickness) {
        transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
      }
      if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= panBoarderThickness) {
        transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
      }
      if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= panBoarderThickness) {
        transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
      }
      if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - panBoarderThickness) {
        transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
      }

      var pos = transform.position;
      pos.y -= Input.GetAxis("Mouse ScrollWheel") * 1000 * scrollSpeed * Time.deltaTime;
      pos.y = Mathf.Clamp(pos.y, _minY, _maxY);
      transform.position = pos;
    }
  }

}