using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;


public class ExtraLevelBuild : MonoBehaviour
{
    [Tooltip("buttons heree")]
    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;
    [SerializeField] private Button inverseButton;
    [SerializeField] private Button indicatorLeftButton;
    [SerializeField] private Button indicatorRightButton;
    [SerializeField] private Button indicatorButton;

    [Tooltip("blocks and locks")]
    [SerializeField] private Button blocksLessButton;
    [SerializeField] private Button blocksMoreButton;
    [SerializeField] private Button locksLessButton;
    [SerializeField] private Button locksMoreButton;

    [SerializeField] private TextMeshProUGUI blocksText;
    [SerializeField] private TextMeshProUGUI locksText;

    [Tooltip("start and back")]
    [SerializeField] private Button backButton;
    [SerializeField] private Button playButton;

    private bool left = true;
    private bool right = true;
    private bool inverse = true;
    private bool indicatorLeft = true;
    private bool indicatorRight = true;
    private bool indicator = true;

    private int blocksCount = 5;
    private int locksCount = 0;

    private int maxBlocks = 12;
    private int minBlocks = 3;
    private int maxLocks = 3;
    private int minLocks = 0;

    private void Start()
    {

        blocksCount = PlayerPrefs.GetInt("BlocksCount", 5);
        locksCount = PlayerPrefs.GetInt("LocksCount", 0);
        left = PlayerPrefs.GetInt("Left", 1) == 1;
        right = PlayerPrefs.GetInt("Right", 1) == 1;
        inverse = PlayerPrefs.GetInt("Inverse", 1) == 1;
        indicatorLeft = PlayerPrefs.GetInt("IndicatorLeft", 1) == 1;
        indicatorRight = PlayerPrefs.GetInt("IndicatorRight", 1) == 1;
        indicator = PlayerPrefs.GetInt("Indicator", 1) == 1;
        
        leftButton.onClick.AddListener(OnLeftButton);
        rightButton.onClick.AddListener(OnRightButton);
        inverseButton.onClick.AddListener(OnInverseButton);
        indicatorLeftButton.onClick.AddListener(OnIndicatorLeftButton);
        indicatorRightButton.onClick.AddListener(OnIndicatorRightButton);
        indicatorButton.onClick.AddListener(OnIndicatorButton);
        blocksLessButton.onClick.AddListener(OnBlocksLessButton);
        blocksMoreButton.onClick.AddListener(OnBlocksMoreButton);
        locksLessButton.onClick.AddListener(OnLocksLessButton);
        locksMoreButton.onClick.AddListener(OnLocksMoreButton);
        backButton.onClick.AddListener(OnBackButton);
        playButton.onClick.AddListener(OnPlayButton);
    
        UpdateUI();
    }

    private void UpdateUI()
    {
        blocksText.text = $"{blocksCount}";
        locksText.text = $"{locksCount}";

        blocksLessButton.interactable = blocksCount > minBlocks;
        blocksMoreButton.interactable = blocksCount < maxBlocks;
        locksLessButton.interactable = locksCount > minLocks;
        locksMoreButton.interactable = locksCount < maxLocks;
    }

    public void OnLeftButton()
    {
        if(left)
        {
            left = false;
            leftButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            left = true;
            leftButton.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnRightButton()
    {
        if(right)
        {
            right = false;
            rightButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            right = true;
            rightButton.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnInverseButton()
    {
        if(inverse)
        {
            inverse = false;
            inverseButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            inverse = true;
            inverseButton.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnIndicatorLeftButton()
    {
        if(indicatorLeft)
        {
            indicatorLeft = false;
            indicatorLeftButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            indicatorLeft = true;
            indicatorLeftButton.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnIndicatorRightButton()
    {
        if(indicatorRight)
        {
            indicatorRight = false;
            indicatorRightButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            indicatorRight = true;
            indicatorRightButton.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnIndicatorButton()
    {
        if(indicator)
        {
            indicator = false;
            indicatorButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            indicator = true;
            indicatorButton.GetComponent<Image>().color = Color.white;
        }
    }
    public void OnBlocksLessButton()
    {
        if(blocksCount > minBlocks)
        {
            blocksCount--;
            UpdateUI();
        }
    }
    public void OnBlocksMoreButton()
    {
        if(blocksCount < maxBlocks)
        {
            blocksCount++;
            UpdateUI();
        }
    }
    public void OnLocksLessButton()
    {
        if(locksCount > minLocks)
        {
            locksCount--;
            UpdateUI();
        }
    }
    public void OnLocksMoreButton()
    {
        if(locksCount < maxLocks)
        {
            locksCount++;
            UpdateUI();
        }
    }
    public void OnBackButton()
    {
        StartCoroutine(LoadScene("_MainMenu"));
    }
    public void OnPlayButton()
    {
        PlayerPrefs.SetInt("BlocksCount", blocksCount);
        PlayerPrefs.SetInt("LocksCount", locksCount);
        PlayerPrefs.SetInt("Left", left ? 1 : 0);
        PlayerPrefs.SetInt("Right", right ? 1 : 0);
        PlayerPrefs.SetInt("Inverse", inverse ? 1 : 0);
        PlayerPrefs.SetInt("IndicatorLeft", indicatorLeft ? 1 : 0);
        PlayerPrefs.SetInt("IndicatorRight", indicatorRight ? 1 : 0);
        PlayerPrefs.SetInt("Indicator", indicator ? 1 : 0);
        StartCoroutine(LoadScene("LevelConstructed"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        // Simulate loading time
        yield return new WaitForSeconds(.5f);
        for(int i = 0; i < 100; i++){
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, Time.deltaTime * 5f);
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
    }

}
