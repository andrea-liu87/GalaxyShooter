using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private PlayerController player_script;
    private Animator _animator;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        player_script =  GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        _animator = GetComponent<Animator>();
        _audioSource = GameObject.Find("Explosion Sound").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate (new Vector3(0, -1, 0) * _speed * Time.deltaTime);

        if (transform.position.y < -4){
            float randomValue = Random.Range(-8, 8);
            transform.position = new Vector3 (randomValue, 6, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet"){
            Destroy(other.gameObject);
            if (player_script != null){
                player_script.addScore(10);
            }
            _animator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(this.gameObject, 0.3f);
        }

        if (other.tag == "Player"){
            if (player_script != null){
                player_script.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();
            Destroy(this.gameObject, 0.3f);
        }
    }
}
