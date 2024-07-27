using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    private CharacterController controller;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float xDir = Input.GetAxisRaw("Horizontal");
        float zDir = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(xDir, 0, zDir);
        movement = Camera.main.transform.TransformDirection(movement);
        movement.y = 0f;
        controller.Move(movement * speed * Time.deltaTime);
    }
}
