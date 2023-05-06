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

    public delegate void OntriggerEvents();
    public static event OntriggerEvents coincollect_update;
    public static event OntriggerEvents level_complete;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!UiManager.gameStart)
            return;

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coincollect_update?.Invoke();
            other.gameObject.SetActive(false);
        }

        if(other.gameObject.CompareTag("Finish"))
        {
            level_complete?.Invoke();
        }
    }
}


[System.Serializable]
public class CameraMovement
{
    public Camera camera;

    public float horizontal;
    public float vertical;
}
