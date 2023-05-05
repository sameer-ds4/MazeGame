using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CameraMovement cameraMain;
    public float sensitivity;
    public float speed;

    public CharacterController character;

    //private Rigidbody rb;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MouseControl();
        Movement();
    }

    private void MouseControl()
    {
        cameraMain.horizontal += Input.GetAxis("Mouse X") * sensitivity;
        cameraMain.vertical -= Input.GetAxis("Mouse Y") * sensitivity;

        //xrot -= cameraMain.vertical;
        //xrot = Mathf.Clamp(xrot, -90, 90);

        //transform.localRotation = Quaternion.Euler(xrot, 0, 0);
        //transform.Rotate(Vector3.up * cameraMain.horizontal);

        UpdateCamera(cameraMain.horizontal, cameraMain.vertical);
    }

    private void UpdateCamera(float horizontal, float vertical)
    {
        var rotation = Quaternion.Euler(vertical, horizontal, 0);
        cameraMain.camera.transform.rotation = Quaternion.Slerp(cameraMain.camera.transform.rotation, rotation, 0.5f);
        rotation.x = Mathf.Clamp(rotation.x, -90, 90);


        
        cameraMain.camera.transform.position = character.transform.position;
        character.transform.rotation = Quaternion.Euler(0, horizontal, 0);
    }


    private void Movement()
    {
        float z = Input.GetAxis("Vertical");
        float x = Input.GetAxis("Horizontal");

        Vector3 movement = transform.right * x + transform.forward * z;

        character.Move(movement * speed * Time.deltaTime);
    }
}


[System.Serializable]
public class CameraMovement
{
    public Camera camera;

    public float horizontal;
    public float vertical;
}




//private void calculatePositionAndTarget(float horizontal, float vertical, Vector3 pivot, Vector3 offset, out Vector3 position, out Vector3 target)
//{
//    var rotation = Quaternion.Euler(vertical, horizontal, 0);
//    var transformPivot = _motorRotation * pivot + _motorPosition;

//    position = transformPivot + rotation * offset;
//    target = _motorPosition + pivot + rotation * Vector3.forward * 10;
//}