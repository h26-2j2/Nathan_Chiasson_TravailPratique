using UnityEngine;
using UnityEngine.InputSystem;

public class BasketMovement : MonoBehaviour
{
    public float speed = 8f;

    void Update()
    {
        float move = 0f;

        if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            move = -1f;
        }
        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            move = 1f;
        }

        transform.position += Vector3.right * move * speed * Time.deltaTime;
    }
}