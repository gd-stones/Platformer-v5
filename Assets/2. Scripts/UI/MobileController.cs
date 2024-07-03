using StonesGaming;
using UnityEngine;

public class MobileController : MonoBehaviour
{
    public static MobileController Instance { get; private set; }

    public bool isControlOnMobile = true;
    public Joystick leftJoystick;
    public RightTouchControl rightTouchControl; // Controls attack or jump actions
    public DashCooldown dashCooldown;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
