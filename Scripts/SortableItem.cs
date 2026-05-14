using UnityEngine;
using System.Collections;

public class SortableItem : MonoBehaviour
{
    public int value;
    public Vector3 targetPosition;
    public bool isChosen = false;
    public bool isLocked = false;

    private GameObject goldenChild;

    public void SetGoldenChild(GameObject prefab)
    {
        goldenChild = Instantiate(prefab, transform);
        goldenChild.transform.localPosition = Vector3.zero;
        goldenChild.SetActive(false);
    }

    public void SetCorrectHighlight(bool correct)
    {
        if (goldenChild != null)
        {
            // Reset alpha to full when showing
            if (correct)
            {
                var sr = goldenChild.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    Color c = sr.color;
                    c.a = 1f;
                    sr.color = c;
                }
            }
            goldenChild.SetActive(correct);
        }
    }

    public IEnumerator FadeOutGoldenChild(float duration)
    {
        if (goldenChild == null || !goldenChild.activeSelf) yield break;

        SpriteRenderer sr = goldenChild.GetComponent<SpriteRenderer>();
        if (sr == null) { goldenChild.SetActive(false); yield break; }

        float elapsed = 0f;
        Color c = sr.color;
        c.a = 1f;
        sr.color = c;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, elapsed / duration);
            sr.color = c;
            yield return null;
        }

        c.a = 0f;
        sr.color = c;
        goldenChild.SetActive(false);
    }

    public void SetLocked(bool locked, GameObject lockPrefab)
    {
        isLocked = locked;
        if (isLocked)
        {
            GameObject lockIcon = Instantiate(lockPrefab, transform.position, Quaternion.identity, transform);
            lockIcon.transform.localPosition = new Vector3(0, 0, -1);
        }
    }

    private void Update()
    {
        if (!isChosen)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * 5f);
        }
    }
}
