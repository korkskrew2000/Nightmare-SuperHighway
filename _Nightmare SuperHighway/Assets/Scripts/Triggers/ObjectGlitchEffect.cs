using UnityEngine;

public class ObjectGlitchEffect : MonoBehaviour {
    float displacementAmount;
    float dissolveAmount;
    float dissolveNoise;
    MeshRenderer MeshRenderer;
    bool glitchFuck = false;
    public AudioSource glitchSound;
    bool setMat = true;

    void Start() {
        MeshRenderer = GetComponent<MeshRenderer>();
    }

    void Update() {
		if (setMat) SetMaterials();

        if (glitchFuck == true) {
            dissolveNoise = 200f;
            displacementAmount += Mathf.SmoothStep(0f, 0.5f, 0.5f) * Time.deltaTime;
            dissolveAmount += Mathf.SmoothStep(0f, 0.5f, 0.45f) * Time.deltaTime;
            transform.Translate(new Vector3(0.01f, 0.01f, 0.01f) * Time.deltaTime);
            this.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f) * Time.deltaTime;

            if (dissolveAmount >= 1.6f) {
                Destroy(this.gameObject);
            }
        }
    }
    private void OnTriggerEnter(Collider other) {
        if (glitchFuck == false) {
            glitchFuck = true;
            glitchSound.Play();
        }
    }

    void SetMaterials()
	{
        MeshRenderer.material.SetFloat("_Amount", displacementAmount);
        MeshRenderer.material.SetFloat("_Dissolve", dissolveAmount);
        MeshRenderer.material.SetFloat("_DissolveNoise", dissolveNoise);
        setMat = false;
    }
}

