using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public int JmpForce = 1000;

    public bool bIsJumping = true;

    public float original_walkSpeed = 10;

    public float original_lookSensitivity = 2;

    private float walkSpeed;

    private float lookSensitivity;

    [SerializeField]
    private float cameraRotationLimit_Up = 90;
    private float cameraRotationLimit_Down = 90;
    private float currentCameraRotationX;

    [SerializeField]
    private Camera theCamera;
    private Rigidbody myRigid;
    private GameObject level;

    void Start()
    {
        level = GameObject.Find("Level");
        Cursor.lockState = CursorLockMode.Locked;
        myRigid = GetComponent<Rigidbody>();
        theCamera = Camera.main;
    }

    void Update()
    {
        walkSpeed = original_walkSpeed * Time.deltaTime;
        lookSensitivity = original_lookSensitivity * (Time.deltaTime * 60);

        Move();
        CameraRotation();
        CharacterRotation();
        Jump();
    }

    private bool isGrounded()
    {
        RaycastHit hit; 
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && Physics.Raycast(gameObject.transform.position, Vector3.down, out hit, 0.5f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                return true;
            }
        }
        return false;
    }

    private void Jump()
    {
        if (isGrounded())
        {
            myRigid.AddForce(Vector3.up * JmpForce);
        }
    }

    private void Move()
    {
        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");
        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * (Input.GetKey(KeyCode.LeftControl) ? walkSpeed * 2f : walkSpeed * 0.5f);

        myRigid.MovePosition(transform.position + _velocity);
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;

        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit_Down, cameraRotationLimit_Up);

        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(_characterRotationY));
    }
}