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
    [SerializeField]
    private GameObject camera3rdPerson;

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
        gameObject.SetActive(true);
        camera3rdPerson.SetActive(false);
        virtualCamera.Priority += priorityBoostAmount;
        aimdPersonCanvas.enabled = true;
        thridPersonCanvas.enabled = false;
    }


    private void CancelAim()
    {
        gameObject.SetActive(false);
        camera3rdPerson.SetActive(true);
        virtualCamera.Priority -= priorityBoostAmount;
        aimdPersonCanvas.enabled = false;
        thridPersonCanvas.enabled = true;
    }
}
