using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnAwake : MonoBehaviour
{
    public GameObject[] obj;
    void Awake()
    {
        foreach(GameObject gameOBJ in obj)
		{
            gameOBJ.SetActive(true);
		}
    }
}
