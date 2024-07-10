using UnityEngine;

[System.Serializable]
public class Character {
	public float jumpHeight = 3f;
	public float walkingSpeed = 7f;
	public float walkingAcceleration = 5f;
	public float runningSpeed = 12f;
	public float crouchSpeed = 4f;
	public float gravity = 30f;
	public float maxFallingSpeed = 30f;
	[Header("-1 = None")]
	public int preferredSpawnPoint = -1; 
	//need a way to select animation type
}
