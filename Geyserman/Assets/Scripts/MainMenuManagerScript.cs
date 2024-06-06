using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenuManagerScript : MonoBehaviour
{
    public AudioClip highlightedClip;
    public AudioClip pressedClip;
    public GameObject loadingImage;

    public void OnButtonHighlighted()
    {
        GetComponent<AudioSource>().PlayOneShot(highlightedClip);
    }

    public void OnButtonPressed(string sceneName)
    {
        GetComponent<AudioSource>().PlayOneShot(pressedClip);
        loadingImage.SetActive(true);
        StartCoroutine(DelaySceneChange(sceneName));
    }

    public void OnQuitPressed()
    {
        GetComponent<AudioSource>().PlayOneShot(pressedClip);
        StartCoroutine(DelayQuit());
    }

    private IEnumerator DelaySceneChange(string sceneName)
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator DelayQuit()
    {
        yield return new WaitForSeconds(1);
        Application.Quit();
    }
}
