using UnityEngine;

public class ScrollTexture : MonoBehaviour {
    public float scrollX = 0.5f;
    public float scrollY = 0.5f;

    void Update() {
        float OffsetX = Time.time * scrollX;
        float OffsetY = Time.time * scrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
    }
}
