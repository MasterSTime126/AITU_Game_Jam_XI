using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


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
        UpdateUI();

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
        SceneManager.LoadScene("_MainMenu");
    }
    public void OnPlayButton()
    {
        SceneManager.LoadScene("LevelConstructed");
    }

}
