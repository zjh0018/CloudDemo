using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInput : MonoBehaviour
{
    PlayerInputActions PIA;
    [SerializeField] private float jumpInputBufferTime = 0.3f;
    WaitForSeconds waitJumpInputBufferTime;
    private void Awake()
    {
        PIA = new PlayerInputActions();
        waitJumpInputBufferTime = new WaitForSeconds(jumpInputBufferTime);
    }
    private void OnEnable()
    {
        PIA.PlayerActions.Jump.canceled += delegate
        {
            JumpInputBuffer = false;
        };
    }
    private void OnGUI()
    {
        Rect r = new Rect(0, 0, 100, 100);
        string message = "JumpBuffer:"+JumpInputBuffer;
        GUIStyle style = new GUIStyle();
        style.fontSize = 20;
        style.fontStyle = FontStyle.Bold;
        GUI.Label(r, message, style);
    }

    public void EnablePlayerActions()
    {
        PIA.PlayerActions.Enable();
        //Cursor.lockState = CursorLockMode.Locked;
    }
    public void SetJumpInputBufferTimer()
    {
        StopCoroutine(nameof(JumpInputBufferCoroutine));
        StartCoroutine(nameof(JumpInputBufferCoroutine));
    }
    IEnumerator JumpInputBufferCoroutine()
    {
        JumpInputBuffer = true;
        yield return waitJumpInputBufferTime;
        JumpInputBuffer = false;
    }
    private void Update()
    {

            D = Down;

        
    }
    public bool JumpInputBuffer { get; set; }
    public bool Down => PIA.PlayerActions.Down.IsPressed();
    public bool D;
    public bool Jump => !Down && PIA.PlayerActions.Jump.WasPressedThisFrame();
    public bool StopJump => !Down && PIA.PlayerActions.Jump.WasReleasedThisFrame();
    public bool Dash => PIA.PlayerActions.Dash.WasPressedThisFrame();
    Vector2 MoveX => PIA.PlayerActions.MoveX.ReadValue<Vector2>();
    public float AxisX => MoveX.x;
    public bool Move => AxisX != 0f;
}
