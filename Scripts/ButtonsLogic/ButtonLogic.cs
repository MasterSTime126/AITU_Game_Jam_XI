using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonLogic : MonoBehaviour
{
    public enum ButtonType
    {
        None,
        Left,
        Right,
        Choose,
        Invert,
        MoveLeft,
        MoveRight
    }

    [SerializeField] private ButtonType buttonType;
    [SerializeField] private Sprite sprite;

    private Button button;

    private void Start()
    {
        GetComponent<Image>().sprite = sprite;
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);

        if(buttonType == ButtonType.Choose)
        {
            GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                // Check if the object is inactive and has the desired tag
                if (!obj.activeInHierarchy && obj.CompareTag("Indicator"))
                {
                    obj.SetActive(true);
                    break;
                }
            }

        }
    }

    private void OnClick()
    {
        switch (buttonType)
        {
            case ButtonType.Left:
                GameManager.Instance.MoveIndicatorLeft();
                break;
            case ButtonType.Right:
                GameManager.Instance.MoveIndicatorRight();
                break;
            case ButtonType.Choose:
                GameManager.Instance.Choose();
                break;
            case ButtonType.Invert:
                GameManager.Instance.Invert();
                break;
            case ButtonType.MoveLeft:
                GameManager.Instance.MoveLeft();
                break;
            case ButtonType.MoveRight:
                GameManager.Instance.MoveRight();
                break;
        }
    }
}
