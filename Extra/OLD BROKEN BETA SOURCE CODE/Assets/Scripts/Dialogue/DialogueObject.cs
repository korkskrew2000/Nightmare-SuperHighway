using UnityEngine;
[CreateAssetMenu(menuName = "Dialogue/DialogueDataObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] [TextArea] private string[] dialogue;
    [SerializeField] private DialogueResponse[] responses;

    public string[] Dialogue => dialogue;

    public bool HasReponses => DialogueResponse != null && DialogueResponse.Length > 0;

    public DialogueResponse[] DialogueResponse => responses;
}
