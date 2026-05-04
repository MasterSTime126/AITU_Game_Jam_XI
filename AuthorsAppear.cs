using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthorsAppear : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image[] images;
    [SerializeField] private CanvasGroup[] canvasGroups;
    void Start()
    {
        foreach (var img in images)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        }
        List<CanvasGroup> cgList = new List<CanvasGroup>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var cg = transform.GetChild(i).GetComponent<CanvasGroup>();
            if (cg != null)
            {
                cgList.Add(cg);
            }
        }
        canvasGroups = cgList.ToArray();
        foreach (var cg in canvasGroups)
        {
            cg.alpha = 0f;
        }
        StartCoroutine(Appear());
    }

    IEnumerator Appear()
    {
        yield return new WaitForSeconds(7f);
        for(int i = 0; i < 100; i++){
            foreach (var img in images)
            {
                img.color = Color.Lerp(img.color, new Color(img.color.r, img.color.g, img.color.b, 1f), Time.deltaTime * 5f);
            }
            foreach (var cg in canvasGroups)
            {
                cg.alpha = Mathf.Lerp(cg.alpha, 1f, Time.deltaTime * 5f);
            }
                yield return null;
            }
    }

    public void Disappear()
    {
        foreach (var img in images)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, 0f);
        }
        foreach (var cg in canvasGroups)
        {
            cg.alpha = 0f;
        }
    }

}
