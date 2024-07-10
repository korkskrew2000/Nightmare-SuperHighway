using UnityEngine;

[System.Serializable]
public class DialogueResponse
{
    [SerializeField] string responseText;
    [SerializeField] DialogueObject dialogueObject;

    public string ResponseText => responseText;
    public DialogueObject DialogueObject => dialogueObject;
}
