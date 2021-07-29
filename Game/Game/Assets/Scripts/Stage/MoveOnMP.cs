using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnMP : MonoBehaviour
{
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;

    private float applySpeed;
    private bool isRun = false;
    private bool isCrouch = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void TryRun()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Running();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            RunningCancel();
        }
    }

    private void Running()
    {
        if (isCrouch)
            //Crouch();

        isRun = true;
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        applySpeed = walkSpeed;
    }
    private void Move()
    {
        // GetAxisRaw를 통해서 방향을 얻어올 수 있다.
        float moveDirX = Input.GetAxisRaw("Horizontal");
        float moveDirZ = Input.GetAxisRaw("Vertical");

        // 방향을 향해서 움직일 수 있게 만든다.
        Vector3 moveHorizontal = transform.right * moveDirX;
        Vector3 moveVertical = transform.forward * moveDirZ;

        // 한 번에 얼마나 움직일 것인지 결정한다.
        Vector3 velocity = (moveHorizontal + moveVertical).normalized;
        velocity *= applySpeed;

        // 델타 타임을 통해서 뚝뚝 끊기는 화면을 부드럽게 만들어준다.
        //myRigid.MovePosition(transform.position + velocity * Time.deltaTime);
    }
}
