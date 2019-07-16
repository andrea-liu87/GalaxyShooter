using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_prefab : MonoBehaviour
{
    private AudioSource _audioSource;

    void Start()
    {
        _audioSource = GameObject.Find("Explosion Sound").GetComponent<AudioSource>();
        _audioSource.Play();
        Destroy(this.gameObject, 0.5f);
    }
}
