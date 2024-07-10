using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubMenuScript : MonoBehaviour
{
    public bool subMenuOpen;
    public GameObject effectMenu;

    public GameObject noneEffect, attackEffect;
    
    void Start()
    {
        
    }


    void Update() {
        if (Input.GetButtonDown("SubMenu") && !GameManager.Instance.pauseMenu) {

            GameManager.Instance.subMenu = !GameManager.Instance.subMenu;
            if (GameManager.Instance.subMenu) {
                OpenSubmenu();
            }
            if (!GameManager.Instance.subMenu) {
                CloseSubmenu();
            }
        }
    }

    void OpenSubmenu() {
        StartCoroutine(GameManager.Instance.GamePause());
        effectMenu.gameObject.SetActive(true);
    }

    void CloseSubmenu() {
        StartCoroutine(GameManager.Instance.GameUnpause());
        effectMenu.gameObject.SetActive(false);
    }

}
