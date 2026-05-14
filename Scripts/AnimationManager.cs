using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationManager : MonoBehaviour
{
    [SerializeField] private GameObject worldCirclePrefab; // For animations on game objects
    [SerializeField] private GameObject canvasCirclePrefab; // For UI animations on canvas
    [SerializeField] private Transform canvasTransform; // Assign your canvas transform here

    public static AnimationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        Debug.Log($"AnimationManager Awake: worldCirclePrefab is {(worldCirclePrefab == null ? "NULL" : "Assigned")}");
        Debug.Log($"AnimationManager Awake: canvasCirclePrefab is {(canvasCirclePrefab == null ? "NULL" : "Assigned")}");
    }

    public IEnumerator PlayStartAnimation()
    {
        if (canvasCirclePrefab == null)
        {
            Debug.LogError("CanvasCirclePrefab is not assigned in AnimationManager.");
            yield break;
        }
        GameObject circle = Instantiate(canvasCirclePrefab, canvasTransform);
        circle.transform.localScale = Vector3.one * 35f;

        float duration = 1.0f;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            circle.transform.localScale = Vector3.Lerp(Vector3.one * 35f, Vector3.zero, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        Destroy(circle);
    }

    public IEnumerator PlayWinAnimation(List<SortableItem> items, float totalItems)
    {
        Debug.Log($"PlayWinAnimation started. worldCirclePrefab is {(worldCirclePrefab == null ? "NULL" : "Assigned")}. Items count: {items?.Count}");

        if (worldCirclePrefab == null)
        {
            Debug.LogError("WorldCirclePrefab is not assigned in AnimationManager.");
            yield break;
        }
        if (items == null)
        {
            Debug.LogError("Items list is null in PlayWinAnimation.");
            yield break;
        }

        Color tempColor = Camera.main.backgroundColor;
        foreach (var item in items)
        {
            if (item == null)
            {
                Debug.LogError("Found a null item in the list during win animation.");
                continue; // Skip this iteration
            }
            if (AudioSound.instance != null)
            {
                AudioSound.instance.PlayOncePitch(item.value);
            }
            GameObject circle = Instantiate(worldCirclePrefab, item.transform.position, Quaternion.identity);
            SpriteRenderer sr = circle.GetComponent<SpriteRenderer>();
            
            float duration = 0.5f;
            float elapsed = 0f;
            
            tempColor = Color.HSVToRGB(item.value / totalItems, 0.8f, 0.9f);
            sr.color = tempColor;
            Destroy(item.gameObject);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float scale = Mathf.Lerp(1f, 30f, elapsed / duration); // Scale appropriately for world space
                circle.transform.localScale = new Vector3(scale, scale, 1);
                yield return null;
            }
            Camera.main.backgroundColor = tempColor;
            PlayerPrefs.SetString("BgColor", tempColor.GetHashCode().ToString());
            Debug.Log(PlayerPrefs.GetString("BgColor"));
            Destroy(circle);
        }

        if (canvasCirclePrefab == null)
        {
            Debug.LogError("CanvasCirclePrefab is not assigned in AnimationManager.");
            yield break;
        }
        // Now play the final screen-covering animation on the canvas
        GameObject canvasCircle = Instantiate(canvasCirclePrefab, canvasTransform);
        canvasCircle.transform.localScale = Vector3.zero;
        float finalDuration = 0.5f;
        float finalElapsed = 0f;
        while (finalElapsed < finalDuration)
        {
            finalElapsed += Time.deltaTime;
            float scale = Mathf.Lerp(0f, 35f, finalElapsed / finalDuration);
            canvasCircle.transform.localScale = new Vector3(scale, scale, 1);
            yield return null;
        }
        // Keep the circle to hide the scene change
    }
}
