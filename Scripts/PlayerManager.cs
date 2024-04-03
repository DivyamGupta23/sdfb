using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
public class PlayerManager : NetworkBehaviour
{
    public InputManager inputManager;
    public CameraManager cameraManager;
    public PlayerLocomotion playerLocomotion;

    public RawImage rawImage;
    public GameObject imagePanelUI;

    private void Awake()
    {
        cameraManager = FindObjectOfType<CameraManager>();
        
        playerLocomotion = GetComponent<PlayerLocomotion>();
    }

    public override void OnNetworkSpawn()
    {
         base.OnNetworkSpawn();
        if(IsClient && IsOwner)
        {
            inputManager = GetComponent<InputManager>();
            inputManager.enabled = true;
            playerLocomotion.cameraObject = cameraManager.cameraTransform;
            cameraManager.inputManager = inputManager;
            cameraManager.targetTransform = transform;
        }
    }

    private void Update()
    {
       
        if (IsOwner)
        {
            inputManager.HandleAllInputs();
        }
    }
    private void FixedUpdate()
    {
        if (IsOwner)
        {
            playerLocomotion.HandleAllMovement();
        }
    }

    private void LateUpdate()
    {
        if (IsOwner)
        {
            cameraManager.HandleAllCameraMovement();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsOwner && other.CompareTag("Panel"))
        {
            rawImage.texture = other.GetComponent<ImageHandler>().pickedImage;
            imagePanelUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsOwner && other.CompareTag("Panel"))
        {
            imagePanelUI.SetActive(false);
        }
    }




}
