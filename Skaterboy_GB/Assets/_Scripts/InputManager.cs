using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public bool IsUpButtonPressed { get; private set; }
    public bool IsDownButtonPressed { get; private set; }
    public float HorizontalAxis { get; private set; }

    public bool IsJumpButtonDown { get; private set; }
    public bool IsJumpButtonStay { get; private set; }
    public bool IsJumpButtonUp { get; private set; }

    public bool IsAttackButtonPressed { get; private set; }

    public  bool IsPauseButtonPressed { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Update()
    {
        IsUpButtonPressed = Input.GetKeyDown(KeyCode.UpArrow) ||
                            Input.GetKeyDown(KeyCode.W);
        IsDownButtonPressed = Input.GetKeyDown(KeyCode.DownArrow) ||
                              Input.GetKeyDown(KeyCode.S);
        HorizontalAxis = Input.GetAxis("Horizontal");

        IsJumpButtonDown = Input.GetButtonDown("Jump");
        IsJumpButtonStay = Input.GetButton("Jump");
        IsJumpButtonUp = Input.GetButtonUp("Jump");

        IsAttackButtonPressed = Input.GetKeyDown(KeyCode.LeftControl);

        IsPauseButtonPressed = Input.GetKeyDown(KeyCode.Escape);
    }
}
