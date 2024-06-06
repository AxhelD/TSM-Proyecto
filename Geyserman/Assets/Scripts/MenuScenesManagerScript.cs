using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScenesManagerScript : MonoBehaviour
{
    public AudioClip highlightedClip;
    public AudioClip pressedClip;
    public AudioClip victoryClip;
    public AudioClip deadClip;

    public GameObject loadingImage;
    public GameObject HUD;
    public GameObject pauseMenu;
    public GameObject mapMenu;
    public GameObject helpMenu;
    public GameObject deadScene;
    public GameObject victoryScene;

    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject player;

    private float playerHealth;
    private bool pausedGame = false;
    private bool spawn1Destroyed = false;
    private bool spawn2Destroyed = false;
    private bool spawn3Destroyed = false;

    void Start()
    {
        playerHealth = player.GetComponent<PlayersHealthScript>().playersHealthScript;
        spawn1Destroyed = spawn1.GetComponent<SpawnScript>().spawnDestroyed;
        spawn2Destroyed = spawn2.GetComponent<SpawnScript>().spawnDestroyed;
        spawn3Destroyed = spawn3.GetComponent<SpawnScript>().spawnDestroyed;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(pausedGame)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        if (playerHealth <= 0)
        {
            DeadScene();
        }

        if (spawn1Destroyed && spawn2Destroyed && spawn3Destroyed)
        {
            VictoryScene();
        }
    }

    public void OnButtonHighlighted()
    {
        GetComponent<AudioSource>().PlayOneShot(highlightedClip);
    }

    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pausedGame = true;
        Time.timeScale = 0f;
        HUD.SetActive(true);
        pauseMenu.SetActive(true);
        mapMenu.SetActive(false);
        helpMenu.SetActive(false);
        deadScene.SetActive(false);
        victoryScene.SetActive(false);
    }

    public void Resume()
    {
        GetComponent<AudioSource>().PlayOneShot(pressedClip);
        pausedGame = false;
        Time.timeScale = 1f;
        HUD.SetActive(true);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        helpMenu.SetActive(false);
        deadScene.SetActive(false);
        victoryScene.SetActive(false);
    }

    public void Map()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<AudioSource>().PlayOneShot(pressedClip);
        pausedGame = true;
        Time.timeScale = 0f;
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(true);
        helpMenu.SetActive(false);
        deadScene.SetActive(false);
        victoryScene.SetActive(false);
    }

    public void Help()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<AudioSource>().PlayOneShot(pressedClip);
        pausedGame = true;
        Time.timeScale = 0f;
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        helpMenu.SetActive(true);
        deadScene.SetActive(false);
        victoryScene.SetActive(false);
    }

    public void Exit(string sceneName)
    {
        GetComponent<AudioSource>().PlayOneShot(pressedClip);
        StartCoroutine(DelayExitToMainMenu(sceneName));
    }

    private IEnumerator DelayExitToMainMenu(string sceneName)
    {
        yield return new WaitForSeconds(1);
        loadingImage.SetActive(true);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void Restart(string sceneName)
    {
        GetComponent<AudioSource>().PlayOneShot(pressedClip);
        StartCoroutine(DelayRestartGame(sceneName));
    }

    private IEnumerator DelayRestartGame(string sceneName)
    {
        yield return new WaitForSeconds(1);
        loadingImage.SetActive(true);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void DeadScene()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<AudioSource>().PlayOneShot(deadClip);
        pausedGame = true;
        Time.timeScale = 0f;
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        helpMenu.SetActive(false);
        deadScene.SetActive(true);
        victoryScene.SetActive(false);
    }

    public void VictoryScene()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        GetComponent<AudioSource>().PlayOneShot(victoryClip);
        pausedGame = true;
        Time.timeScale = 0f;
        HUD.SetActive(false);
        pauseMenu.SetActive(false);
        mapMenu.SetActive(false);
        helpMenu.SetActive(false);
        deadScene.SetActive(false);
        victoryScene.SetActive(true);
    }

}
