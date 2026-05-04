using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private Button gameButton;
    [SerializeField] private Button extraLevelButton;

    private void Start()
    {
        PlayerPrefs.SetInt("FinishedGame", 1);
        if(PlayerPrefs.GetInt("FinishedGame", 0) == 1)
        {
            extraLevelButton.gameObject.SetActive(true);
            extraLevelButton.onClick.RemoveAllListeners();
            extraLevelButton.onClick.AddListener(StartExtraLevel);
        }
        else
        {
            extraLevelButton.gameObject.SetActive(false);
        }
        gameButton.onClick.AddListener(StartGame);
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
        gameButton.gameObject.SetActive(false);
        extraLevelButton.gameObject.SetActive(false);
        // Simulate loading time
        yield return new WaitForSeconds(.5f);
        for(int i = 0; i < 100; i++){
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, Time.deltaTime * 5f);
            yield return null;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("LevelConstructor");
    }

    IEnumerator LoadLevel()
    {
        gameButton.gameObject.SetActive(false);
        extraLevelButton.gameObject.SetActive(false);
        // Simulate loading time
        yield return new WaitForSeconds(.5f);
        for(int i = 0; i < 100; i++){
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, Time.deltaTime * 5f);
            yield return null;
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene("SortLevel 1");
    }
}
