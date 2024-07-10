using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color color1;
    public Color color2;
    public float neededDreamamount;
    float dreamAmount;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
        dreamAmount = GameManager.Instance.dreamValue;
        if(dreamAmount < neededDreamamount) {
            rend.material.SetColor("_Color", color1);
        } else {
            rend.material.SetColor("_Color", color2);
        }
    }
}
