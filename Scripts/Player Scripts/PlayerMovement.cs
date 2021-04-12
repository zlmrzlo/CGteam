﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private CharacterController character_Controller;

    private Vector3 move_Direction;

    private int count = 0;
    public float speed = 5f;
    private float gravity = 20f;

    public float jump_Force = 10f;
    private float vertical_Velocity;

    private bool gravityChange;

    void Awake() {
        character_Controller = GetComponent<CharacterController>();
    }
	
	void Update () {
        MoveThePlayer();
	}

    void MoveThePlayer() {

        move_Direction = new Vector3(Input.GetAxis(Axis.HORIZONTAL), 0f,
                                     Input.GetAxis(Axis.VERTICAL));

        move_Direction = transform.TransformDirection(move_Direction);
        move_Direction *= speed * Time.deltaTime;

        ApplyGravity();

        character_Controller.Move(move_Direction);


    } // move player

    void ApplyGravity() {
        if (gravityChange)
        {
            if(count == 0)
            {
                vertical_Velocity = 0;
                count++;
            }
            vertical_Velocity += gravity * Time.deltaTime;
        }
        else
        {
            if (count == 0)
            {
                vertical_Velocity = 0;
                count++;
            }
            vertical_Velocity -= gravity * Time.deltaTime;
        }

        // jump
        PlayerJump();

        move_Direction.y = vertical_Velocity * Time.deltaTime;

    } // apply gravity

    void PlayerJump() {

        if(character_Controller.isGrounded && Input.GetKeyDown(KeyCode.Space)) {
            vertical_Velocity = jump_Force;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Change")
        {
            print("Change");
            gravityChange = !gravityChange;
            count = 0;
        }
    }

} // class


































