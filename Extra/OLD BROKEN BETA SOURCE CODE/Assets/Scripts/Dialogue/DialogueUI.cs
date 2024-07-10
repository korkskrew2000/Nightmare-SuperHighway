using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
	[SerializeField] GameObject dialogueBox;
	[SerializeField] private TMP_Text textLabel;

	public bool isTalking;
	public bool currentlyMidSentence;
	public AudioSource speakSound;
	public Animator animator;

	private float setSpeakingSpeed;
	private float maxDistance;
	private float skipDelay;
	private float skipTarget = 0.2f;
	public bool inResponses;

	private DialogueResponseHandler responseHandler;
	private DialogueTypeEffect typeEffect;
	private CrossHair crosshair;
	private Transform playerPos;

	void Start()
	{
		crosshair = Camera.main.GetComponent<CrossHair>();
		responseHandler = GetComponent<DialogueResponseHandler>();
		typeEffect = GetComponent<DialogueTypeEffect>();
		playerPos = GameManager.Instance.player.transform;
	}

	private void Update()
	{
		if (crosshair.crosshairState == CrossHair.State.NPC && isTalking == false && !inResponses)
		{
			if (Input.GetButtonDown("Interact"))
			{
				GameObject HitNPC = crosshair.npcHit.collider.gameObject;
				speakSound = HitNPC.GetComponent<AudioSource>();
				typeEffect.writingSpeed = HitNPC.GetComponent<DialogueTrigger>().speakingSpeed;
				setSpeakingSpeed = typeEffect.writingSpeed;
				maxDistance = HitNPC.GetComponent<DialogueTrigger>().maxDistanceAway;
				HitNPC.GetComponent<DialogueTrigger>().Speak();
			}
		}

		if (isTalking == true)
		{
			skipDelay += Time.deltaTime;
			DistanceFromSpeaker();
			if (Input.GetButtonDown("Interact") && skipDelay >= skipTarget && currentlyMidSentence)
			{
				typeEffect.writingSpeed = 100f;
			}
		}
	}
	void DistanceFromSpeaker()
	{
		GameObject HitNPC = crosshair.npcHit.collider.transform.root.gameObject;
		float dist = Vector3.Distance(HitNPC.transform.position, playerPos.position);
		if (dist > maxDistance)
		{
			speakSound.Stop();
			EndDialogue();
		}
	}

	public void ShowDialogue(DialogueObject dialogueObject)
	{
		if (dialogueObject.HasReponses)
		{
			StartCoroutine(GameManager.Instance.SpeakerResponses());
			inResponses = true;
		}
		animator.SetBool("IsOpen", true);
		isTalking = true;
		typeEffect.writingSpeed = setSpeakingSpeed;
		StartCoroutine(StepDialogue(dialogueObject));
	}

	IEnumerator StepDialogue(DialogueObject dialogueObject)
	{
		for (int i = 0; i < dialogueObject.Dialogue.Length; i++)
		{
			speakSound.Play();
			string dialogue = dialogueObject.Dialogue[i];
			currentlyMidSentence = true;
			yield return typeEffect.Type(dialogue, textLabel);


			speakSound.Stop();
			currentlyMidSentence = false;
			typeEffect.writingSpeed = setSpeakingSpeed;
			yield return new WaitUntil((() => Input.GetButtonDown("Interact")));
			if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasReponses) break;
		}

		if (dialogueObject.HasReponses)
		{
			responseHandler.ShowResponses(dialogueObject.DialogueResponse);
		}
		else
		{
			StartCoroutine(GameManager.Instance.SpeakerSetFree());
			inResponses = false;
			EndDialogue();
		}
		EndDialogue();
	}

	void EndDialogue()
	{
		skipDelay = 0f;
		currentlyMidSentence = false;
		speakSound.Stop();
		animator.SetBool("IsOpen", false);
		isTalking = false;
	}
}
