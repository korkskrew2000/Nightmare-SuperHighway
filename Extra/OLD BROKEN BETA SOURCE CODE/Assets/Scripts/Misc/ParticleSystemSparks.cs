using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystemSparks : MonoBehaviour
{
    ParticleSystem particle;
    AudioSource audios;
    public GameObject lights;
    public int minWait = 1;
    public int maxWait = 10;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
        audios = GetComponent<AudioSource>();
        StartCoroutine(PlayRandomTime());
    }

    public IEnumerator PlayRandomTime() {
        while (true) {
            int wait = Random.Range(minWait, maxWait);
            yield return new WaitForSeconds(wait);
            particle.Play();
            audios.Play();
        }
    }
}
