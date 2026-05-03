using UnityEngine;
using UnityEngine.UI;
using System.Collections;

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
