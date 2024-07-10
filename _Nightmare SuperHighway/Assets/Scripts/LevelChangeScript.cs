using UnityEngine;

public class LevelChangeScript : MonoBehaviour {
    //Put this script onto an object that teleports you, levelValue number for the corresponding level
    //can be found inside File > Build Settings > Scenes In Build (that number on far right)
    public int levelValue;
    //Which spawn point you want it to be.
    public int spawnPointNumber = 0;
    //Whatever number added to dream value, keep it as 1, 2, 3... rather than 1.4, 2.7...
    //Can also be subtracted from (add a minus)
    public float addToDreamValue;
    public float fadeSpeed = 1.5f;
    [Space(5)]
    public bool forceTriggerChange;
    private bool used = false;

    private void OnTriggerEnter(Collider other)
    {
        if (forceTriggerChange && other.gameObject.CompareTag("Player"))
        {

            if (!used)
            {
                LevelChangeManager.instance.StartCoroutine(LevelChangeManager.instance.ForceLoadLevel(levelValue, spawnPointNumber));
                used = true;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (this.gameObject.GetComponent<Collider>() != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(this.gameObject.GetComponent<Collider>().bounds.center, this.gameObject.GetComponent<Collider>().bounds.size);
        }
    }
}
