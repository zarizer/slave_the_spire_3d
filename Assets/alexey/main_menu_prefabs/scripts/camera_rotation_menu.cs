using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class camera_rotation_menu : MonoBehaviour
{
    public GameObject menu_camera;
    public float rotation_speed = 1;
    float max_x = 0.06892f;
    float min_x = 0.06882f;
    void Start()
    {

    }


    void Update()
    {
        menu_camera.transform.Rotate(new Vector3(0,Input.GetAxis("Mouse X") *Time.deltaTime*rotation_speed, 0));
        Debug.Log(menu_camera.transform.rotation);


    }
}
