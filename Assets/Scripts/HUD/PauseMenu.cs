using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.PostProcessing;

public class PauseMenu : MonoBehaviour
{

    public static bool gameIsPause = false;
    public GameObject pauseMenuUI;
    private InputMenuController inputController;
    [SerializeField]
    private PostProcessVolume postProcessVolume;

    private void Awake()
    {
        inputController = new InputMenuController();
    }

    private void OnEnable()
    {
        inputController.Enable();
    }

    private void OnDisable()
    {
        inputController.Disable();
    }

    private void Start()
    {
        inputController.Menu.Pause.performed += _ => ToogleState(); 
    }

    public void ToogleState()
    {
        if (gameIsPause)
            Resume();
        else
            Pause();
    }

    private void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
    }

    public void SetOclussion(bool state)
    {
        print(state);
        AmbientOcclusion oclussion;
        postProcessVolume.profile.TryGetSettings(out oclussion);
        oclussion.active = state;
    }


    public void SetBloom(bool state)
    {
        Bloom bloom;
        postProcessVolume.profile.TryGetSettings(out bloom);
        bloom.active = state;
    }

}
