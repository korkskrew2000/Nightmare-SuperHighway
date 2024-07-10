using UnityEngine;


public class DistanceScale : MonoBehaviour {
    public float MinScale = 1;
    public float MaxScale = 3;
    public float MaxDistance = 25;
    public Transform playerTarget;
    Vector3 temp;
    private GameObject player;

    private void Start() {
        player = GameManager.Instance.player.gameObject;
        playerTarget = player.transform;
    }

    void Update() {
        //Scales the object based on Player's distance.
        temp = transform.localScale;
        float dist = Vector3.Distance(playerTarget.position, transform.position);
        if (dist < MaxDistance) {
            temp.x = ((MaxDistance - dist) / (MaxDistance / (MaxScale - MinScale))) + MinScale;
            temp.y = ((MaxDistance - dist) / (MaxDistance / (MaxScale - MinScale))) + MinScale;
            temp.z = ((MaxDistance - dist) / (MaxDistance / (MaxScale - MinScale))) + MinScale;
            transform.localScale = temp;
        }
        if (dist >= MaxDistance) {
            temp.x = MinScale;
            temp.y = MinScale;
            temp.z = MinScale;
            transform.localScale = temp;
        }
    }
}