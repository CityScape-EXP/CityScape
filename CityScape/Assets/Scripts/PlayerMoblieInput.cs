using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMoblieInput : MonoBehaviour
{
   public Vector2 Movement {get; private set; }

   public event Action OnAttack; 
   public event Action OnJumpPressed; //점프 클릭 시
   public event Action OnJumpReleased; //점프 해제 시
   public event Action<Vector2> OnMovement;
   public event Action OnWeaponChange;

   [SerializeField]
   private Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        if (joystick == null)
        {
            Debug.LogError("Joystick 넣어라");
        }
        else
        {
            joystick.OnMove += Move;
            Debug.Log("Joystick 움직임");
        }
    }

    private void Move(Vector2 input)
    {
        Movement = input;
        OnMovement?.Invoke(Movement);
        Debug.Log("Movement: " + Movement);  // Debug the movement vector
    }
}
