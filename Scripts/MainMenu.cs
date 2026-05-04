using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button gameButton;
    [SerializeField] private Button extraLevelButton;

    private void Start()
    {
        if(PlayerPrefs.GetInt("FinishedGame", 0) == 1)
        {
            extraLevelButton.gameObject.SetActive(true);
        }
    }

    public void StartGame()
    {
        StartCoroutine(LoadLevel());
    }

    public void StartExtraLevel()
    {
        StartCoroutine(LoadExtraLevel());
    }

    IEnumerator LoadExtraLevel()
    {
        // Simulate loading time
        yield return new WaitForSeconds(.5f);
        for(int i = 0; i < 100; i++){
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, Time.deltaTime * 5f);
            yield return null;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("SortLevel 2");
    }

    IEnumerator LoadLevel()
    {
        // Simulate loading time
        yield return new WaitForSeconds(.5f);
        for(int i = 0; i < 100; i++){
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, Time.deltaTime * 5f);
            yield return null;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("SortLevel 1");
    }
}
