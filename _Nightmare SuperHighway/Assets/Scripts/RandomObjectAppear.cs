using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomObjectAppear : MonoBehaviour
{
    public GameObject[] appearObject;
    public GameObject activeObject;

    // Start is called before the first frame update
    void Start()
    {
        activeObject = appearObject[Random.Range(0, appearObject.Length)];
        activeObject.SetActive(true);
    }
}
