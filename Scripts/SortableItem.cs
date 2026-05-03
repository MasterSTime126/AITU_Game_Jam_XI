using UnityEngine;

public class SortableItem : MonoBehaviour
{
    public int value;
    public Vector3 targetPosition;
    public bool isChosen = false;
    public bool isLocked = false;

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
