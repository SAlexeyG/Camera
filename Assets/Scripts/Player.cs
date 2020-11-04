using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Buttons
{
    WASD,
    Arrows,

}

public class Player : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float sensitivity;
    [SerializeField] private Buttons buttons;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalAxis = 0f;
        float verticalAxis = 0f;

        switch(buttons)
        {
            case Buttons.WASD:
                horizontalAxis = Input.GetKey(KeyCode.A) ? -1f : horizontalAxis;
                horizontalAxis = Input.GetKey(KeyCode.D) ? 1f : horizontalAxis;
                verticalAxis = Input.GetKey(KeyCode.W) ? 1f : verticalAxis;
                verticalAxis = Input.GetKey(KeyCode.S) ? -1f : verticalAxis;
                break;

            case Buttons.Arrows:
                horizontalAxis = Input.GetKey(KeyCode.LeftArrow) ? -1f : horizontalAxis;
                horizontalAxis = Input.GetKey(KeyCode.RightArrow) ? 1f : horizontalAxis;
                verticalAxis = Input.GetKey(KeyCode.UpArrow) ? 1f : verticalAxis;
                verticalAxis = Input.GetKey(KeyCode.DownArrow) ? -1f : verticalAxis;
                break;
        }

        transform.Translate(
            horizontalAxis * Time.deltaTime * speed,
            0,
            verticalAxis * Time.deltaTime * speed);

        transform.Rotate(
            0f,
            Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity,
            0f);
    }
}
