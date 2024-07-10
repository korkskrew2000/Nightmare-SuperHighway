using UnityEngine;
public class FootstepChangeTrigger : MonoBehaviour {
    public AudioClip[] soundClips;
    FootstepSoundSystem footSys;

    void Awake() {
        footSys = FindFirstObjectByType<FootstepSoundSystem>();
    }

    public void SendFootstepSound()
	{
        Debug.Log("PlayingFootstepSound");
        footSys.walkGeneric = soundClips;
    }
}
