using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour {
    #region Public Variables
    public float interactionDistance = 5f;
    public MouseLooker mouselook;
    public ObjPhysicsSystem pickUp;
    public Sprite[] crosshairImageList;
    public Image imageCrosshair;
    public State crosshairState;
    public RaycastHit normalHit;
    public RaycastHit npcHit;
    #endregion

    #region Private Variables
    Camera playerCam;
    LayerMask playerIgnore = 1 << 3;
    LayerMask interactionLayer;
    LayerMask npcLayer;
    #endregion

    public enum State {
        Normal,
        Teleporter,
        Interactable,
        InteractablePickedUp,
        NPC,
    }

    void Start() {
        imageCrosshair = imageCrosshair.GetComponent<Image>();
        interactionLayer = LayerMask.NameToLayer("Interactable");
        npcLayer = LayerMask.NameToLayer("NPC");
        playerCam = Camera.main;
    }

    //this is probably not any better than the system i used earlier who cares fuck
    void Update() {
        ChangeCrosshair();
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out normalHit, interactionDistance, ~playerIgnore)) {
            crosshairState = State.Normal;

            if (normalHit.transform.gameObject.layer == interactionLayer) {
                crosshairState = State.Interactable;
            }

            if (normalHit.transform.gameObject.layer == npcLayer) {
                crosshairState = State.NPC;
                npcHit = normalHit;
            }

            if (pickUp.currentlyPickedUpObject != null) {
                crosshairState = State.InteractablePickedUp;
            }

            if (normalHit.transform.CompareTag("Teleporter")) {
                crosshairState = State.Teleporter;
            }

        } else {
            crosshairState = State.Normal;
        }

        if (crosshairState == State.Normal) {
            imageCrosshair.color = new Color(imageCrosshair.color.r, imageCrosshair.color.g, imageCrosshair.color.b, 0f);
        } else {
            imageCrosshair.color = new Color(imageCrosshair.color.r, imageCrosshair.color.g, imageCrosshair.color.b, 1f);
        }
    }

    public void ChangeCrosshair() {
        switch (crosshairState) {
            case State.Normal:
                imageCrosshair.sprite = crosshairImageList[0];
                imageCrosshair.color = new Color(imageCrosshair.color.r, imageCrosshair.color.g, imageCrosshair.color.b, 0f);
                break;
            case State.Interactable:
                imageCrosshair.sprite = crosshairImageList[1];
                break;
            case State.InteractablePickedUp:
                imageCrosshair.sprite = crosshairImageList[1];
                break;
            case State.Teleporter:
                imageCrosshair.sprite = crosshairImageList[4];
                break;
            case State.NPC:
                imageCrosshair.sprite = crosshairImageList[3];
                break;
        }
    }
}