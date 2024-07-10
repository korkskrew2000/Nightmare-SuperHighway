using UnityEngine;

public class Billboard : MonoBehaviour
{
	private void LateUpdate()
	{
		this.transform.forward = new Vector3(Camera.main.transform.forward.x, this.transform.forward.y, Camera.main.transform.forward.z);
	}
}
