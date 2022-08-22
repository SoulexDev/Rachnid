using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;
    [HideInInspector] public Vector3 velocity;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float camMoveSpeed = 5;
    //[SerializeField] private float jumpHeight = 5;
    [SerializeField] private float maxSpeed = 10;
    [SerializeField] private float airMoveAmount = 40;
    [SerializeField] private Camera cam;
    [SerializeField] public Transform camT;
    private float x, z;
    private float xBound, zBound;
    public static float camX, camY;

    Vector3 lastDir;
    Vector3 moveDir;
    Vector3 lerpDir;
    Vector3 yVel;
    Vector3 groundNormal;
    //int layerMask = 1 << 2;
    public bool grounded => Grounded();
    //bool atStep = false;

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SaveManager.OnSave += SaveManager_OnSave;
        SaveManager.OnLoad += SaveManager_OnLoad;
    }

    void Update()
    {
        if (!Player.Instance.canMove)
            return;
        PlayerInput();
        Movement();
        //DownwardStairCheck();
    }
    private void LateUpdate()
    {
        if (!Player.Instance.canMove)
            return;
        CamMovement();
    }
    void PlayerInput()
    {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        camX += Input.GetAxis("Mouse X") * camMoveSpeed;
        camY += Input.GetAxis("Mouse Y") * camMoveSpeed;
        camY = Mathf.Clamp(camY, -90, 90);
    }
    //void ChangeSensitivity()
    //{
    //    camMoveSpeed = PlayerPrefs.GetFloat("Sensitivity");
    //}
    void Movement()
    {
        //if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 1))
        //{
        //    groundNormal = hit.normal;
        //}
        //else
        //    groundNormal = Vector3.up;

        moveDir = x * transform.right + z * transform.forward;
        moveDir = Vector3.ProjectOnPlane(moveDir, groundNormal);
        moveDir.Normalize();
        moveDir *= moveSpeed;

        //atStep = Physics.Raycast(transform.position, Vector3.down, controller.height / 2 + controller.stepOffset + 0.15f);

        if (lastDir != Vector3.zero)
            lerpDir = new Vector3(lastDir.x, 0, lastDir.z);
        if (grounded)
        {
            yVel.y = 0;
            lastDir = moveDir;
            if (moveDir.magnitude == 0)
            {
                lerpDir = Vector3.Lerp(lerpDir, Vector3.zero, Time.deltaTime * 10);
                controller.Move(lerpDir * Time.deltaTime);
            }
            //if (Input.GetButtonDown("Jump"))
            //{
            //    airborne = true;
            //    yVel.y = jumpHeight;
            //    lastDir += controller.velocity;
            //    Invoke("ResetJump", 0.4f);
            //}
        }
        else
        {
            if (moveDir.magnitude > 0)
            {
                lastDir += moveDir * Time.deltaTime * airMoveAmount;

                if (lastDir.magnitude > maxSpeed)
                    lastDir *= maxSpeed / lastDir.magnitude;
            }

            xBound = controller.velocity.x <= 0 ? controller.velocity.x : -controller.velocity.x;
            zBound = controller.velocity.z <= 0 ? controller.velocity.z : -controller.velocity.z;

            lastDir = new Vector3(Mathf.Clamp(lastDir.x, xBound, -xBound), 0, Mathf.Clamp(lastDir.z, zBound, -zBound));
            if (controller.velocity.y == 0)
                yVel.y = 0;

            //if(atStep && yVel.y == 0)
            //    controller.Move(Vector3.down * 50);
            yVel.y -= 9.81f * Time.deltaTime;

            moveDir = lastDir + moveDir * Time.deltaTime;
        }
        moveDir += yVel;
        controller.Move(moveDir * Time.deltaTime);
        //controller.Move(yVel * Time.deltaTime);
    }
    //public void AddForce(Vector3 force)
    //{
    //    airborne = true;
    //    yVel.y = force.y;
    //    Invoke("ResetJump", 0.4f);
    //}
    void CamMovement()
    {
        transform.rotation = Quaternion.Euler(new Vector3(0, camX, 0));
        camT.transform.rotation = Quaternion.Euler(new Vector3(-camY, camX, 0));
        cam.transform.SetPositionAndRotation(camT.position, camT.rotation);
    }
    //void ResetJump()
    //{
    //    airborne = false;
    //}
    public bool Grounded()
    {
        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.35f, Vector3.down, out hit, controller.height / 2 + 0.1f))
        {
            groundNormal = hit.normal;
            return true;
        }
        else
            groundNormal = Vector3.up;
        return false;
    }

    private void SaveManager_OnSave()
    {
        SaveData.current.playerData.velocity = velocity;
        SaveData.current.playerData.position = transform.position;
        SaveData.current.playerData.camX = camX;
        SaveData.current.playerData.camY = camY;
    }
    private void SaveManager_OnLoad()
    {
        controller.enabled = false;

        velocity = SaveData.current.playerData.velocity;

        transform.position = SaveData.current.playerData.position;
        camX = SaveData.current.playerData.camX;
        camY = SaveData.current.playerData.camY;

        controller.enabled = true;
    }
}