using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{   [SerializeField]
    private float _speed = 3.0f;

    private AudioSource _audioSource;

    [SerializeField]
    private int _powerUpId; //0 = tripleshoot; 1 = speed; 2 = shields

    private void Start() {
        _audioSource = GameObject.Find("PowerUp Sound").GetComponent<AudioSource>();
    }
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);

        if (transform.position.y < -4){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player"){
            PlayerController playerScript = other.GetComponent<PlayerController>();

            if (playerScript != null){
                
                switch (_powerUpId){
                    case 0:
                        playerScript.ActivateTripleShoot();
                        break;
                    case 1:
                        playerScript.ActivateSpeedBoost();
                        break;
                    case 2:
                        playerScript.ActivateShield();
                        break;
                }
                _audioSource.Play();
            Destroy(this.gameObject);
            }
        }
    }
}
