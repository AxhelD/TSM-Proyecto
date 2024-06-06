using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LoadMainMenuManagerScript : MonoBehaviour
{
    public string sceneName;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(DelayLoadMainMenu(sceneName));
        }
    }
    
    private IEnumerator DelayLoadMainMenu(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
