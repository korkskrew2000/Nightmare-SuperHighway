using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsPaperRandom : MonoBehaviour
{
    public Material[] randomImage;
    void Start()
    {

        Material thisMaterial = randomImage[Random.Range(0, randomImage.Length)];
        this.gameObject.GetComponent<Renderer>().material = thisMaterial;
    }

    void Update()
    {
        
    }
}
