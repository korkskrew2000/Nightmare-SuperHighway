using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DialogueResponseHandler : MonoBehaviour
{
    [SerializeField] RectTransform responseBox;
    [SerializeField] RectTransform responseButtonTemplate;
    [SerializeField] RectTransform responseContainer;

    DialogueUI dialogueUI;
    public PlayerAnimator hands;

    List<GameObject> tempResponseButtons = new List<GameObject>();

	private void Start()
	{
        dialogueUI = GetComponent<DialogueUI>();
	}

	public void ShowResponses(DialogueResponse[] responses)
	{
        float responseBoxHeight = 0;

        foreach (DialogueResponse response in responses)
		{
            
            GameObject responseButton = Instantiate(responseButtonTemplate.gameObject, responseContainer);
            responseButton.GetComponent<Animator>().SetBool("IsOpen", true);
            responseButton.GetComponent<TMP_Text>().text = response.ResponseText;
            responseButton.GetComponent<Button>().onClick.AddListener(() => OnPickResponse(response));
            tempResponseButtons.Add(responseButton);
            responseBoxHeight += responseButtonTemplate.sizeDelta.y;
            

        }
        responseBox.sizeDelta = new Vector2(responseBox.sizeDelta.x, responseBoxHeight);
        responseBox.GetComponent<Animator>().SetBool("IsOpen", true);
    }

    void OnPickResponse(DialogueResponse response)
	{
        responseBox.GetComponent<Animator>().SetBool("IsOpen", false);

        foreach(GameObject button in tempResponseButtons)
		{
            Destroy(button);
		}
        tempResponseButtons.Clear();

        dialogueUI.ShowDialogue(response.DialogueObject);
	}
}
