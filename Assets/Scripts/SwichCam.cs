using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class SwichCam : MonoBehaviour
{

    [SerializeField]
    private PlayerInput playerInput;
    [SerializeField]
    private int priorityBoostAmount = 10;
    [SerializeField]
    private Canvas thridPersonCanvas;
    [SerializeField]
    private Canvas aimdPersonCanvas;

    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => CancelAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => CancelAim();
    }


    private void StartAim()
    {
        virtualCamera.Priority += priorityBoostAmount;
        aimdPersonCanvas.enabled = true;
        thridPersonCanvas.enabled = false;
    }


    private void CancelAim()
    {
        virtualCamera.Priority -= priorityBoostAmount;
        aimdPersonCanvas.enabled = false;
        thridPersonCanvas.enabled = true;
    }
}
