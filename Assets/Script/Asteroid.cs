using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{   [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private float _rotationSpeed = 0.5f;

    [SerializeField]
    private GameObject _explosion;
    private SpawnManager _spawnManager;

    private void Start() {
        _spawnManager = GameObject.Find("Spawn Enemy").GetComponent<SpawnManager>();

        if (_spawnManager == null){
            Debug.LogError("Spawn manager is NULL");
        }
    }
    void Update()
    {
      transform.Rotate(new Vector3(0, 0, 1), 360 * _rotationSpeed * Time.deltaTime ); 
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Bullet"){
           Instantiate(_explosion, transform.position, Quaternion.identity);
           Destroy(other.gameObject);
           _spawnManager.StartSpawning();
           Destroy(this.gameObject);
        }
    }
}
