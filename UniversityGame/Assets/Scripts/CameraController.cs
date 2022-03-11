using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float wsadSpeed = 1f;
    public float scrollSpeed = 1f;
    public float mouseRotateSpeed = 1f;
    public enum State
    {
        Transition,
        Normal,
        BuildMode
    }
    public State currentState = State.Normal;

    private void Start()
    {
        //camPos = transform.GetChild(0);
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Normal:
                normalMovement();
                break;
            case State.BuildMode:
                buildModeMovement();
                break;
            default:
                //don't accept input if in a transitionary state
                break;
        }
    }
    private void normalMovement()
    {
        Vector3 moveDir = Vector3.zero;
        Vector3 newPos = this.gameObject.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            Vector3 w = transform.forward;
            w.y = 0;
            moveDir += w;
        }
        if (Input.GetKey(KeyCode.S))
        {
            Vector3 s = -transform.forward;
            s.y = 0;
            moveDir += s;
        }
        if (Input.GetKey(KeyCode.A))
        {
            Vector3 a = -transform.right;
            a.y = 0;
            moveDir += a;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir += transform.right;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveDir += Vector3.up;
        }
        if (Input.GetKey(KeyCode.LeftControl))
        {
            moveDir += Vector3.down;
        }
        moveDir = moveDir.normalized;

        newPos += moveDir * wsadSpeed * Time.deltaTime;
        newPos += transform.forward * Input.mouseScrollDelta.y * scrollSpeed * Time.deltaTime;

        transform.position = newPos;

        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Vector3 rotation = new Vector3(-Input.GetAxis("Mouse Y") * mouseRotateSpeed * Time.deltaTime, Input.GetAxis("Mouse X") * mouseRotateSpeed * Time.deltaTime, 0);
            transform.Rotate(rotation);

            float x = transform.rotation.eulerAngles.x;
            float y = transform.rotation.eulerAngles.y;

            transform.rotation = Quaternion.Euler(x, y, 0);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void buildModeMovement()
    {
        Vector3 moveDir = Vector3.zero;
        Vector3 newPos = this.gameObject.transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            moveDir += transform.up;
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDir += transform.up * -1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDir += transform.right * -1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDir += transform.right;
        }
        moveDir = moveDir.normalized;
        newPos += moveDir * wsadSpeed * Time.deltaTime;
        Camera.main.orthographicSize += Input.mouseScrollDelta.y * scrollSpeed * Time.deltaTime;
        if (newPos.y < 2)
        {
            newPos.y = 2;
        }
        transform.position = newPos;
    }
}
