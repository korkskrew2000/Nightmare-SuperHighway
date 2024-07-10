using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueTypeEffect : MonoBehaviour
{
	public float writingSpeed = 50f;

    public Coroutine Type(string textToType, TMP_Text textLabel)
	{
		return StartCoroutine(TypeOutText(textToType, textLabel));
	}

	IEnumerator TypeOutText(string textToType, TMP_Text textLabel)
	{
		textLabel.text = string.Empty;

		float time = 0;
		int charIndex = 0;

		while(charIndex < textToType.Length)
		{
			time += Time.deltaTime * writingSpeed;
			charIndex = Mathf.FloorToInt(time);
			charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

			textLabel.text = textToType.Substring(0, charIndex);

			yield return null;
		}

		textLabel.text = textToType;

	}
}
