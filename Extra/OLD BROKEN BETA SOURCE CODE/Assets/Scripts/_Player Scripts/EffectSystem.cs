using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectSystem : MonoBehaviour {
    public bool attackEffect = false;
    public EquippedEffectState equippedEffectState;
    public EffectSlapStatus slapStatus;
    public GameObject EffectSlapSelect;

    [HideInInspector] public Canvas canvas;
    [HideInInspector] public PlayerAnimator handScript;


    public enum EquippedEffectState {
        EquippedNone,
        EquippedAttackEffect,
    }
    
    public enum EffectSlapStatus {
        Locked,
        Unlocked
    }

    void Start()
    {
        canvas = FindObjectOfType<Canvas>(CompareTag("MainCanvas"));
        handScript = FindObjectOfType<PlayerAnimator>();
        attackEffect = GameManager.Instance.haveEffect_Slap;
        
    }

   
    void Update()
    {
        if (attackEffect == true) {
            slapStatus = EffectSlapStatus.Unlocked;
        } else {
            slapStatus = EffectSlapStatus.Locked;
        }

        switch (slapStatus) {
            case EffectSlapStatus.Locked:
                EffectSlapSelect.gameObject.SetActive(false);
                break;
            case EffectSlapStatus.Unlocked:
                EffectSlapSelect.gameObject.SetActive(true);
                break;
        }

        switch (equippedEffectState) {
            case EquippedEffectState.EquippedNone:

            break;
            case EquippedEffectState.EquippedAttackEffect:

            break;
        }
    }

    public void ChangeSlap() {
        handScript.handState = PlayerAnimator.HandState.Slap;
    }

    public void ChangeNone() {
        handScript.handState = PlayerAnimator.HandState.None;
    }
}
