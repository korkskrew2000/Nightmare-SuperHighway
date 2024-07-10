using UnityEngine;
public class FootstepChangeTrigger : MonoBehaviour {
    public AudioClip[] soundClips;
    FootstepSoundSystem footSys;

    void Awake() {
        footSys = FindObjectOfType<FootstepSoundSystem>();
    }

    public void SendFootstepSound()
	{
        Debug.Log("Pillu");
        footSys.walkSounds = soundClips;
    }
}
