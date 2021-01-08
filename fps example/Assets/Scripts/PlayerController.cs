using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigid;

    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    private bool isWalk = false;
    private bool isRun = false;
    private Vector3 lastPos;

    [SerializeField] private float crouchSpeed;
    [SerializeField] private float crouchPosY;
    private float originPosY;
    private float applyCrouchPosY;
    private float applySpeed;
    private bool isCrouch = false;

    [SerializeField] private float originJumpForce;
    [SerializeField] private float crouchJumpForce;
    private float applyJumpForce;
    private bool isGround = true;
    private CapsuleCollider capsuleCollider;

    [SerializeField] private float lookSensitivity;
    [SerializeField] private float cameraRotationLimit;
    private float currentCameraRotationX = 45f;
    [SerializeField] private Camera camera;
    private GunController gunController;
    private CrossHair crossHair;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        gunController = FindObjectOfType<GunController>();
        crossHair = FindObjectOfType<CrossHair>();

        applySpeed = walkSpeed;
        originPosY = camera.transform.localPosition.y;
        applyCrouchPosY = originPosY;
        applyJumpForce = originJumpForce;
    }

    // Update is called once per frame
    void Update()
    {
        IsGround();
        TryRun();
        TryCrouch();
        TryJump();
        Move();
        CameraRotation();
        CharacterRotation();
    }
    void FixedUpdate()
    {
        MoveCheck();
    }

    private void TryJump()
    {
        applyJumpForce = isCrouch ? crouchJumpForce : originJumpForce;
        if(Input.GetKeyDown(KeyCode.Space)&&isGround)
        {
            rigid.velocity = transform.up * applyJumpForce;
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
        gunController.CancelFineSight();
        isRun = true;
        crossHair.RunningAnimation(isRun);
        applySpeed = runSpeed;
    }

    private void RunningCancel()
    {
        isRun = false;
        crossHair.RunningAnimation(isRun);
        applySpeed = walkSpeed;
    }

    private void TryCrouch()
    {
        if(Input.GetKey(KeyCode.LeftControl))
        {
            isCrouch = true;
            applyCrouchPosY = crouchPosY;
            applySpeed = crouchSpeed;
            crossHair.CrouchingAnimation(isCrouch);
        }
        if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            isCrouch = false;
            applyCrouchPosY = originPosY;
            applySpeed = walkSpeed;
            crossHair.CrouchingAnimation(isCrouch);
        }
        StartCoroutine(CrouchCoroutine());
    }

    IEnumerator CrouchCoroutine()
    {
        float _posY = camera.transform.localPosition.y;
        int count = 0;
        while(_posY!=applyCrouchPosY)
        {
            count++;
            _posY = Mathf.Lerp(_posY, applyCrouchPosY, 0.3f);
            camera.transform.localPosition = new Vector3(0, _posY, 0);
            if (count > 15) break;
            yield return null;
        }
        camera.transform.localPosition = new Vector3(0, applyCrouchPosY, 0);
    }

    private void IsGround()
    {
        isGround = Physics.Raycast(transform.position, Vector3.down, capsuleCollider.bounds.extents.y + 0.1f);
        crossHair.RunningAnimation(!isGround);
    }

    private void Move()
    {

        float _moveDirX = Input.GetAxisRaw("Horizontal");
        float _moveDirZ = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _moveDirX;
        Vector3 _moveVertical = transform.forward * _moveDirZ;

        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * applySpeed;
        rigid.MovePosition(transform.position + _velocity * Time.deltaTime);
    }

    private void MoveCheck()
    {
        if (!isRun && !isCrouch && isGround) 
        {
            Debug.Log(Vector3.Distance(lastPos, transform.position));
            if (Vector3.Distance(lastPos, transform.position) >= 0.01f)
                isWalk = true;
            else isWalk = false;

            crossHair.WalkingAnimation(isWalk);
            lastPos = transform.position;
        }
    }

    private void CharacterRotation()
    {
        float _yRotation = Input.GetAxisRaw("Mouse X");
        Vector3 _characterRotationY = new Vector3(0f, _yRotation, 0f) * lookSensitivity;
        rigid.MoveRotation(rigid.rotation * Quaternion.Euler(_characterRotationY));
    }

    private void CameraRotation()
    {
        float _xRotation = Input.GetAxisRaw("Mouse Y");
        float _cameraRotationX = _xRotation * lookSensitivity;
        currentCameraRotationX -= _cameraRotationX;
        currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);
        camera.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0f, 0f);
    }
}
