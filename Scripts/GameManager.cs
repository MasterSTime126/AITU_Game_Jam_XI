using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject animationCirclePrefab;
    [SerializeField] private GameObject animationCircle;
    [SerializeField] private GameObject lockPrefab;

    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private GameObject sortableItemPrefab;
    [SerializeField] private int numberOfItems = 5;
    [SerializeField] private int numberOfLockedItems = 1;
    [SerializeField] private float itemSpacing = 1.5f;
    [SerializeField] private Transform selectionIndicator; // A visual indicator for the current index
    [SerializeField] private int[] initialItemOrder;
    [SerializeField] private float hueCycleSpeed = 0.025f;
    [SerializeField] private bool isFinish = false;
    [SerializeField] private bool isExtraLevel = false;

    [SerializeField] private GameObject[] buttonsToHide;

    [SerializeField] private AuthorsAppear authorsAppear;

    private List<SortableItem> sortableItems = new List<SortableItem>();
    private int currentIndex = 0;
    private SortableItem currentlyChosenItem = null;

    private bool WinAnimationRunning = false;
    private float currentHue = 0f;

    private void Awake()
    {
        if(isExtraLevel)
        {
            if(PlayerPrefs.GetInt("FinishedGame", 0) == 0)
            {
                SceneManager.LoadScene(0);
                return;
            }
            numberOfItems = PlayerPrefs.GetInt("BlocksCount", 5);
            numberOfLockedItems = PlayerPrefs.GetInt("LocksCount", 0);
            


            buttonsToHide[0].SetActive(PlayerPrefs.GetInt("Left", 1) == 1);
            buttonsToHide[1].SetActive(PlayerPrefs.GetInt("Right", 1) == 1);
            buttonsToHide[2].SetActive(PlayerPrefs.GetInt("Inverse", 1) == 1);
            buttonsToHide[3].SetActive(PlayerPrefs.GetInt("IndicatorLeft", 1) == 1);
            buttonsToHide[4].SetActive(PlayerPrefs.GetInt("IndicatorRight", 1) == 1);
            buttonsToHide[5].SetActive(PlayerPrefs.GetInt("Indicator", 1) == 1);
            Debug.Log("Done 1");
        }
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        StartCoroutine(StartAnimation());

        if(isFinish)
        {
            selectionIndicator.gameObject.SetActive(false);
            return;
        }

        currentHue = Random.value; 
        InitializeItems();
    }

    private void Update()
    {
        if(isFinish)
        {
            if (Input.GetMouseButtonDown(0) && !WinAnimationRunning)
            {
                WinAnimationRunning = true;
                StartCoroutine(WinAnimation());
            }
            return;
        }

        UpdateSelectionIndicator();
        CheckWinCondition();

        if(!WinAnimationRunning){
            currentHue = (currentHue + hueCycleSpeed * Time.deltaTime) % 1f;
            Camera.main.backgroundColor = Color.HSVToRGB(currentHue, 0.5f, 0.75f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("LevelConstructor");
        }
    }

    private void InitializeItems()
    {
        List<int> values;

        // Check if initialItemOrder is valid and not all zeros
        if (initialItemOrder != null && initialItemOrder.Length == numberOfItems && initialItemOrder.Any(v => v != 0))
        {
            values = initialItemOrder.ToList();
        }
        else
        {
            values = Enumerable.Range(0, numberOfItems).ToList();
            // Shuffle values to make it a puzzle
            for (int i = 0; i < values.Count; i++)
            {
                int temp = values[i];
                int randomIndex = Random.Range(i, values.Count);
                values[i] = values[randomIndex];
                values[randomIndex] = temp;
            }
        }

        for (int i = 0; i < numberOfItems; i++)
        {
            GameObject itemGO = Instantiate(sortableItemPrefab, GetPositionForIndex(i), Quaternion.identity);
            SortableItem item = itemGO.GetComponent<SortableItem>();
            item.value = values[i];
            item.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(item.value / (float)numberOfItems, 0.8f, 0.9f);
            sortableItems.Add(item);
        }

        // Lock a certain number of items
        List<SortableItem> itemsToLock = sortableItems.OrderBy(x => Random.value).Take(numberOfLockedItems).ToList();
        foreach (var item in itemsToLock)
        {
            item.SetLocked(true, lockPrefab);
        }

        // Reshuffle if items are already in sorted order
        while (IsSorted(values))
        {
            for (int i = 0; i < values.Count; i++)
            {
                int temp = values[i];
                int randomIndex = Random.Range(i, values.Count);
                values[i] = values[randomIndex];
                values[randomIndex] = temp;
            }
        }
        Debug.Log("Done 2");
        UpdateTargetPositions();
    }

    private void UpdateTargetPositions()
    {
        for (int i = 0; i < sortableItems.Count; i++)
        {
            sortableItems[i].targetPosition = GetPositionForIndex(i);
        }
    }

    private Vector3 GetPositionForIndex(int index)
    {
        return new Vector3((index - (numberOfItems - 1) / 2f) * itemSpacing, 0, 0);
    }

    private void UpdateSelectionIndicator()
    {
        if (selectionIndicator != null)
        {
            selectionIndicator.position = GetPositionForIndex(currentIndex);
        }
    }

    public void MoveLeft()
    {
        if (sortableItems.Count == 0) return;
            // Rotate all items
            SortableItem first = sortableItems[0];
            sortableItems.RemoveAt(0);
            sortableItems.Add(first);
            UpdateTargetPositions();

        Debug.Log("Move Left");
    }

    public void MoveRight()
    {
        if (sortableItems.Count == 0) return;

        SortableItem last = sortableItems[sortableItems.Count - 1];
        sortableItems.RemoveAt(sortableItems.Count - 1);
        sortableItems.Insert(0, last);
        UpdateTargetPositions();
        Debug.Log("Move Right");
    }

    public void Choose()
    {
        if (sortableItems.Count == 0) return;

        if (currentlyChosenItem == null)
        {
            if (sortableItems[currentIndex].isLocked)
            {
                Debug.Log("This item is locked and cannot be chosen.");
                return;
            }
            selectionIndicator.gameObject.GetComponent<SpriteRenderer>().color = Color.clear; // Indicate choosing mode
            // Choose the item at currentIndex
            currentlyChosenItem = sortableItems[currentIndex];
            currentlyChosenItem.isChosen = true;
            currentlyChosenItem.transform.position += Vector3.up * 1.0f; // Move chosen item up
        }
        else
        {
            selectionIndicator.gameObject.GetComponent<SpriteRenderer>().color = Color.white; // Reset indicator color
            // Un-choose the item and place it back at currentIndex
            currentlyChosenItem.isChosen = false;
            
            // Find the chosen item in the list, remove it, and insert it at the current index
            sortableItems.Remove(currentlyChosenItem);
            sortableItems.Insert(currentIndex, currentlyChosenItem);

            currentlyChosenItem = null;
            UpdateTargetPositions(); // Return to line
        }
        Debug.Log("Choose");
    }

    public void Invert()
    {
        if (sortableItems.Count == 0 || currentlyChosenItem != null) return;
        
        sortableItems.Reverse();
        UpdateTargetPositions();
        Debug.Log("Invert");
    }

    public void MoveIndicatorLeft(){
        if (sortableItems.Count == 0) return;
        if(currentlyChosenItem != null) return;
        currentIndex = (currentIndex - 1 + numberOfItems) % numberOfItems;
        UpdateSelectionIndicator();
    }

    public void MoveIndicatorRight(){
        if (sortableItems.Count == 0) return;
        if (currentlyChosenItem != null) return;
        currentIndex = (currentIndex + 1) % numberOfItems;
        UpdateSelectionIndicator();
    }

    public void CheckWinCondition()
    {
        if(WinAnimationRunning) return;
        if (sortableItems.Count == 0) return;

        if (!IsSorted(sortableItems.Select(item => item.value).ToList()))
            return;
        //Debug.Log("You win!");
        //Add win animation here + next level loading here
        WinAnimationRunning = true;
        StartCoroutine(WinAnimation());
    }

    private bool IsSorted(List<int> values)
    {
        for (int i = 0; i < values.Count - 1; i++)
        {
            if (values[i] > values[i + 1])
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator WinAnimation()
    {

        transform.GetChild(0).gameObject.SetActive(false); // Hide buttons
        selectionIndicator.gameObject.SetActive(false); // Hide selection indicator
        yield return new WaitForSeconds(0.5f); // Small delay before starting the animation

        Color tempColor = Camera.main.backgroundColor;
        for (int i = 0; i < sortableItems.Count; i++)
        {
            // Create a circle animation at the center of the screen
            GameObject circle = Instantiate(animationCirclePrefab, sortableItems[i].transform.position, Quaternion.identity);
            SpriteRenderer sr = circle.GetComponent<SpriteRenderer>();
            float duration = 0.5f;
            float elapsed = 0f;
            tempColor = Color.HSVToRGB(sortableItems[i].value / (float)numberOfItems, 0.8f, 0.9f);
            sr.color = tempColor;
            Destroy(sortableItems[i].gameObject);
            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float scale = Mathf.Lerp(1f, 35f, elapsed / duration);
                circle.transform.localScale = new Vector3(scale, scale, 1);
                yield return null;
            }
            Camera.main.backgroundColor = tempColor;
            Destroy(circle);
            
        }
        
        
        if (isFinish)
        {
            authorsAppear.Disappear();
            yield return new WaitForSeconds(0.5f); // Small delay before resetting the game
            SceneManager.LoadScene(0);
            yield break;
        }

        
        for(int i = 0; i < 100; i++){
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, Color.black, Time.deltaTime * 5f);
            yield return null;
        }
        yield return new WaitForSeconds(0.5f); // Small delay before resetting the game
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }

    private IEnumerator StartAnimation()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        float duration = 1.0f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            animationCircle.transform.localScale = Vector3.Lerp(Vector3.one*35f, Vector3.zero, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        animationCircle.SetActive(false);

        yield return null;
        if(isFinish)
            yield break;
        transform.GetChild(0).gameObject.SetActive(true);
    }

}