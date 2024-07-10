using System.Collections;
using UnityEngine;
/// <summary>
/// Moves CameraHolder up and down on moving and rotates according to player's direction.
/// </summary>
public class HeadBob : MonoBehaviour {
    public float bobbingSpeed = 10f;
    public float bobbingAmount = 0.1f;
    public float sprintBobbingSpeed = 15f;
    public float rotationSpeed = 2f;
    public float rotationAmountMultiplier = 2.5f;
    public float rotationResetMultiplier = 15f;
    public Movement controller;
    float defaultPosY = 0;
    float defaultRotZ = 0;
    float timer = 0;
    float rotTimer = 0;
    Transform cameraTarget;
    float velocityY = 0;
    float targetY = 0;
    float smoothTime = 0.2f;
    Quaternion desiredRot;

    void Start() {
        cameraTarget = this.transform;
        defaultPosY = transform.localPosition.y;
        defaultRotZ = transform.localRotation.z;
    }

    void Update() {
        //=================
        //Moving
        //=================
        if (controller.isMoving == true && GameManager.Instance.controllable && controller.isCrouching == false && !controller.cantStand) {
            StartCoroutine(YChange(1.3f));
            StartCoroutine(HeadBobMove(1f));
        }
        if (controller.isMoving == true && GameManager.Instance.controllable && controller.isCrouching == true && !controller.cantStand) {
            StartCoroutine(YChange(0.5f));
            StartCoroutine(HeadBobMove(2f));
        }
        //=================
        //Idle
        //=================
        if (controller.isMoving == false && controller.isCrouching == true && !controller.cantStand) {
            StartCoroutine(YChange(0.5f));
            StartCoroutine(HeadBobReset(2f));
        }
        if (controller.isMoving == false && controller.isCrouching == false && !controller.cantStand) {
            StartCoroutine(YChange(1.3f));
            StartCoroutine(HeadBobReset(1f));
        }
        //=================
        //cantStand Fix
        //=================
        if (controller.isMoving == true && controller.cantStand) {
            StartCoroutine(YChange(0.5f));
            StartCoroutine(HeadBobMove(3f));
        }
        if (controller.isMoving == false && controller.cantStand) {
            StartCoroutine(YChange(0.5f));
            StartCoroutine(HeadBobReset(1f));
        }

        float newY = Mathf.SmoothDamp(cameraTarget.localPosition.y, targetY, ref velocityY, smoothTime);
        cameraTarget.transform.localPosition = new Vector3(0, newY, 0);
    }

    IEnumerator YChange(float changeAmount) {
        targetY = Mathf.Lerp(targetY, changeAmount, Time.deltaTime * 7.0f);
        yield return targetY;
    }

    IEnumerator HeadBobMove(float crouchMultiplier) {
        
        if (controller.isSprinting)
        {
            timer += Time.deltaTime * sprintBobbingSpeed / crouchMultiplier;
        }
        else
        {
            timer += Time.deltaTime * bobbingSpeed / crouchMultiplier;
        }
        transform.localPosition = new Vector3(transform.localPosition.x,
            targetY + Mathf.Sin(timer) * bobbingAmount, transform.localPosition.z);
        rotTimer += Time.deltaTime * rotationSpeed / crouchMultiplier;
        desiredRot = Quaternion.Euler(0, 0, -controller.targetDir.x * rotationAmountMultiplier);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, desiredRot, rotationSpeed * Time.deltaTime);


        yield return null;
    }

    IEnumerator HeadBobReset(float resetSpeed) {
        timer = 0;
        transform.localPosition = new Vector3(transform.localPosition.x,
            Mathf.Lerp(transform.localPosition.y, targetY, Time.deltaTime * resetSpeed));
        rotTimer = 0;
        transform.localRotation = Quaternion.RotateTowards(transform.localRotation, Quaternion.identity, Time.deltaTime * resetSpeed * rotationResetMultiplier);
        yield return null;
    }
}