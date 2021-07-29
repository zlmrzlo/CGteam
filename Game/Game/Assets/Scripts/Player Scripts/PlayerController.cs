using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 인스펙터창에서 수정이 가능하다.
    [SerializeField]
    private float walkSpeed;
    [SerializeField]
    private float runSpeed;
    [SerializeField]
    private float crouchSpeed;

    private float applySpeed;

    // 점프를 위한 순간적인 힘
    [SerializeField]
    private float jumpForce;

    // 상태 변수
    // 기본값은 false이지만 한눈에 알아볼 수 있도록 하기 위해서 초기화
    private bool isRun = false;
    private bool isCrouch = false;
    private bool isGround = false;

    // 앉았을 때 얼마나 앉을지 결정하는 변수
    [SerializeField]
    private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;

    private float deathRotX = -90;
    private float originRotX;
    private float applyDeathRotX;

    // 땅 착지 여부
    private CapsuleCollider capsuleCollider;

    // 스크립트가 들어가는 컴포넌트에 있는 리지드바디를 
    // 가지고 올 수 있도록 변수 선언
    private Rigidbody myRigid;
    private GameObject player;

    // 마우스를 얼마나 민감하게 움직일 것인지 설정한다.
    [SerializeField]
    public float lookSensitivity;

    // 마우스를 움직였을 때 화면이 계속해서 돌아가는 것을
    // 방지해주기 위해서 카메라회전에 제한을 둔다.
    [SerializeField]
    private float cameraRotationLimit;

    // 기본값은 0으로 자동으로 초기화된다.
    private float currentCameraRotationX;

    [SerializeField]
    public Camera theCamera;

    bool upGravity = false;
    bool rightGravity = false;
    bool forwardGravity = false;

    bool mouseLock = false;

    AudioSource footstep_Sound;
    private float accumulated_Distance;
    private float apply_step_Distance;
    public float origin_step_Distance = 2.0f;

    StatusController statusController;
    [SerializeField] private GameObject deathScreenObj;

    // Start is called before the first frame update
    void Start()
    {
        statusController = GetComponent<StatusController>();
        footstep_Sound = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        capsuleCollider = GetComponent<CapsuleCollider>();
        player = GameObject.FindWithTag("Player");
        myRigid = player.GetComponent<Rigidbody>();
        applySpeed = walkSpeed;
        originPosY = theCamera.transform.localPosition.y;
        originRotX = theCamera.transform.localRotation.x;
        applyCrouchPosY = originPosY;
        applyDeathRotX = -90f;
    }

    // Update is called once per frame
    // 업데이트는 대략 1초에 60번 정도 호출된다.
    void Update()
    {
        if (GameManager.canPlayerMove)
        {
            //Gravity();
            IsGround();
            IsDeath();
            TryJump();
            TryRun();
            TryCrouch();
            if (!mouseLock)
            {
                Move();
                CameraRotation();
                CharacterRotation();
            }
            // 위, 아래
            CharaterRotationTryInverse();
            // 오른쪽, 왼쪽
            CharaterRotationTrySideInverse();
            // 앞, 뒤
            CharaterRotationTryFrontInverse();
        }
    }

    private void IsDeath()
    {
        if (statusController.currentHp == 0)
        {
            mouseLock = true;
            StartCoroutine(DeathCoroutine());
            myRigid.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    IEnumerator DeathCoroutine()
    {
        float rotX = theCamera.transform.localRotation.x;
        Debug.Log(applyDeathRotX);
        int count = 0;

        while (rotX != applyDeathRotX)
        {
            count++;
            rotX = Mathf.Lerp(rotX, -90, 0.01f);
            theCamera.transform.localRotation = Quaternion.Euler(rotX, 0f, 0f);
            if (count > 300)
                break;
            // 1 프레임마다 실행시킨다.
            yield return null;
        }
        theCamera.transform.localRotation = Quaternion.Euler(-90, 0f, 0f);

        // 죽었을 시의 메뉴 등장
        GameObject[] uis = GameObject.FindGameObjectsWithTag("UIs");
        for (int i = 0; i < uis.Length; i++)
            uis[i].SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        GameManager.canPlayerMove = false;
        deathScreenObj.SetActive(true);
    }

    private void CharaterRotationTryFrontInverse()
    {
        // 키 입력 여부 확인
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CharaterRotationFrontInverse();
        }
    }

    private void CharaterRotationFrontInverse()
    {
        // 버튼을 눌렀을 때 중력이 변화하도록 설정
        forwardGravity = !forwardGravity;

        if (forwardGravity)
        {
            // 반대로 뒤집는 쿼터니언 값
            myRigid.rotation = new Quaternion(0f, 0f, 0.7f, 0.7f).normalized;
            myRigid.MoveRotation(myRigid.rotation);
        }
        else
        {
            // 원래대로 돌아오는 쿼터니언 값
            myRigid.rotation = new Quaternion(0f, 0f, -0.7f, 0.7f).normalized;
            myRigid.MoveRotation(myRigid.rotation);
        }

        //쿼터니언 회전값 확인용
        //Vector3 characterRotationX = transform.forward;
        //myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationX));
        //Debug.Log(myRigid.rotation);
    }

    private void CharaterRotationTrySideInverse()
    {
        // 키 입력 여부 확인
        if (Input.GetKeyDown(KeyCode.T))
        {
            CharaterRotationSideInverse();
        }
    }

    private void CharaterRotationSideInverse()
    {
        // 버튼을 눌렀을 때 중력이 변화하도록 설정
        rightGravity = !rightGravity;

        if (rightGravity)
        {
            // 반대로 뒤집는 쿼터니언 값
            myRigid.rotation = new Quaternion(-0.7f, 0f, 0f, 0.7f).normalized;
            myRigid.MoveRotation(myRigid.rotation);
        }
        else
        {
            // 원래대로 돌아오는 쿼터니언 값
            myRigid.rotation = new Quaternion(-0.7f, 0f, 0f, -0.7f).normalized;
            myRigid.MoveRotation(myRigid.rotation);
        }

        //쿼터니언 회전값 확인용
        //Vector3 characterRotationX = -transform.right;
        //myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationX));
        //Debug.Log(myRigid.rotation);
    }

    private void Gravity()
    {
        // 캐릭터의 아래 방향으로 중력을 작용시킴
        myRigid.velocity -= transform.up;
        //Debug.Log(myRigid.velocity);
    }

    private void CharaterRotationTryInverse()
    {
        // 키 입력 여부 확인
        if (Input.GetKeyDown(KeyCode.R))
        {
            CharaterRotationInverse();
        }
    }

    private void CharaterRotationInverse()
    {
        // 버튼을 눌렀을 때 중력이 변화하도록 설정
        upGravity = !upGravity;

        if (upGravity)
        {
            // 반대로 뒤집는 쿼터니언 값
            myRigid.rotation = new Quaternion(-1.0f, 0f, 0f, 0f).normalized;
            myRigid.MoveRotation(myRigid.rotation);
        }
        else
        {
            // 원래대로 돌아오는 쿼터니언 값
            myRigid.rotation = new Quaternion(0f, 0f, 0f, 1.0f).normalized;
            myRigid.MoveRotation(myRigid.rotation);
        }

        // 쿼터니언 회전값 확인용
        //Vector3 characterRotationX = -transform.right;
        //myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationX));
        Debug.Log(myRigid.rotation);
    }

    private void TryCrouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Crouch();
        }
    }

    private void Crouch()
    {
        isCrouch = !isCrouch;

        if (isCrouch)
        {
            applySpeed = crouchSpeed;
            applyCrouchPosY = crouchPosY;
        }
        else
        {
            applySpeed = walkSpeed;
            applyCrouchPosY = originPosY;
        }

        // 주석처리된 부분을 코루틴을 이용해서 좀더 부드럽게 표현을 한다.
        //theCamera.transform.localPosition = new Vector3(theCamera.transform.localPosition.x,
        //    applyCrouchPosY, theCamera.transform.localPosition.z);
        StartCoroutine(CrouchCoroutine());
    }

    IEnumerator CrouchCoroutine()
    {
        float posY = theCamera.transform.localPosition.y;
        int count = 0;

        while (posY != applyCrouchPosY)
        {
            count++;
            posY = Mathf.Lerp(posY, applyCrouchPosY, 0.3f);
            theCamera.transform.localPosition = new Vector3(0, posY, 0);
            if (count > 15)
                break;
            // 1 프레임마다 실행시킨다.
            yield return null;
        }
        theCamera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
    }

    private void IsGround()
    {
        // Vector3.down -> -transform.up으로 변경하면 캡슐의 아래를 의미하게 됨
        // extents는 캡슐의 반의 길이를 의미한다.
        // 백업용
        //isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        isGround = Physics.Raycast(transform.position, -transform.up, capsuleCollider.bounds.extents.y + 2f);

        // 땅 착지 여부 확인용
        //Debug.Log(capsuleCollider.bounds.extents.y);
        //Debug.Log(isGround);
    }

    private void TryJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround)
        {
            Jump();
        }
    }

    //bool upGravity = false;
    //bool rightGravity = false;
    //bool forwardGravity = false;

    private void Jump()
    {
        // 앉은 상태에서 점프 시 앉은 상태 해제
        if (isCrouch)
            Crouch();

        if (!upGravity)
        {
            myRigid.velocity = transform.up * jumpForce;
        }
        else
        {
            myRigid.velocity = transform.up * jumpForce;
        }

        if (!rightGravity)
        {
            myRigid.velocity = transform.up * jumpForce;
        }
        else
        {
            myRigid.velocity = transform.up * jumpForce;
        }

        if (!forwardGravity)
        {
            myRigid.velocity = transform.up * jumpForce;
        }
        else
        {
            myRigid.velocity = transform.up * jumpForce;
        }
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
            Crouch();

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
        myRigid.MovePosition(transform.position + velocity * Time.deltaTime);

        if (velocity.sqrMagnitude > 0 && isGround)
        {

            // accumulated distance is the value how far can we go 
            // e.g. make a step or sprint, or move while crouching
            // until we play the footstep sound
            accumulated_Distance += Time.deltaTime;
            if (isRun == true)
            {
                apply_step_Distance = origin_step_Distance / 2;
            }
            else
            {
                apply_step_Distance = origin_step_Distance;
            }

            if (accumulated_Distance > apply_step_Distance)
            {

                footstep_Sound.volume = Random.Range(20.0f, 40.0f);
                footstep_Sound.Play();

                accumulated_Distance = 0f;
            }
        }
        else
        {
            accumulated_Distance = 0f;
        }

    }

    // 캐릭터 회전을 나타내는 함수
    private void CharacterRotation()
    {
        float yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, yRotation, 0f) * lookSensitivity;
        myRigid.MoveRotation(myRigid.rotation * Quaternion.Euler(characterRotationY));
    }

    // 상하 카메라 회전을 나타내는 함수
    private void CameraRotation()
    {
        float xRotation = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = xRotation * lookSensitivity;
        currentCameraRotationX -= cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX,
            -cameraRotationLimit, cameraRotationLimit);
        theCamera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
