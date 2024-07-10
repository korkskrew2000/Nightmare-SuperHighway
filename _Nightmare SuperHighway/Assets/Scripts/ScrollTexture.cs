using UnityEngine;

public class ScrollTexture : MonoBehaviour {
    public float scrollX = 0.5f;
    public float scrollY = 0.5f;
    Material scroll;

    void Start()
    {
        scroll = GetComponent<MeshRenderer>().material;
    }


    void Update() {
        float OffsetX = Time.time * scrollX;
        float OffsetY = Time.time * scrollY;
        scroll.SetVector("_MainTexture", new Vector2(OffsetX, OffsetY));
    }
}
