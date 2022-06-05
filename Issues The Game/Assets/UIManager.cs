using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static bool GameIsPaused = false;
    private PlayerControls playerControls;
    private GameObject canvas;
    private PauseMenu pauseMenuUI;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        
        playerControls = new PlayerControls();

    }

    private static UIManager instance;
    public static UIManager Instance
    {
        get
        {
            if(instance == null)
            {
                Debug.LogError("");
            }
            return instance;
        }
    }

    private void Start()
    {
        if(canvas==null)
        canvas = GameObject.FindGameObjectWithTag("UICanvas");
        if (pauseMenuUI == null)
        {
            pauseMenuUI = canvas.GetComponentInChildren<PauseMenu>();
            pauseMenuUI.gameObject.SetActive(false);
        }
    }

    
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerControls.Main.Pause.triggered)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.gameObject.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Resume();
        
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Level 1 (Tutorial)");
        Resume();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
