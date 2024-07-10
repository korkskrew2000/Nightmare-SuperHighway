using UnityEngine;

public class DialogueTrigger : MonoBehaviour {
    #region Public Variables
    [Range(0, 50)]
    public float speakingSpeed = 0f;
    public float maxDistanceAway = 10f;
    public DialogueObject dialogueObject;
    #endregion

    #region Private Variables
    AudioSource speakSound;
    Transform playerPos;
    CrossHair crosshair;
    #endregion

    private void Start() {
        speakSound = GetComponent<AudioSource>();
    }

    public void Speak() {
        FindObjectOfType<DialogueUI>().ShowDialogue(dialogueObject);
    }
}

