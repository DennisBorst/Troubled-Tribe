using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
    [HideInInspector] public bool isBusy;
    [HideInInspector] public bool toolEquiped;
    public Items toolItem;

    [SerializeField] private float movementSpeed;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject[] toolObjects;

    private CharacterController characterController;
    private Camera mainCamera;
    private Animator anim;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = FindObjectOfType<Camera>();
        anim = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (isBusy) { anim.SetBool("isWalking", false); return; }

        Movement();
    }

    private void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        Vector3.Normalize(move);
        characterController.Move(move * movementSpeed * Time.deltaTime);

        Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            playerObject.transform.LookAt(new Vector3(pointToLook.x, playerObject.transform.position.y, pointToLook.z));
        }

        //Animations
        if(Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    public void EquipedTool(int currentTool)
    {
        toolEquiped = true;
        toolObjects[currentTool].SetActive(true);

        if(currentTool == 0) { toolItem = Items.Wood; }
        if(currentTool == 1) { toolItem = Items.Food; }
        if(currentTool == 2) { toolItem = Items.Water; }
    }

    public void DisableTool(int currentTool)
    {
        toolEquiped = false;
        toolObjects[currentTool].SetActive(false);
        toolItem = Items.Nothing;
    }

    #region Singleton
    private static PlayerMovement instance;

    public static PlayerMovement Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new PlayerMovement();
            }
            return instance;
        }
    }
    #endregion
}
