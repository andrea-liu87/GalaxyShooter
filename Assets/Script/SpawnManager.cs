using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemy;

    [SerializeField]
    private GameObject[] _powerUp;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _stopSpawning = false;


    public void StartSpawning() {
        StartCoroutine(enemyRoutine());
        StartCoroutine(powerUpRoutine());
    }

    IEnumerator enemyRoutine(){
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false){
            Vector3 spawnPos = new Vector3(Random.Range(-8,8), 6, 0);
            GameObject newEnemy = Instantiate(_enemy, spawnPos, Quaternion.identity);
             newEnemy.transform.parent = _enemyContainer.transform;
             yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator powerUpRoutine(){
        yield return new WaitForSeconds(2.0f);
        while (_stopSpawning == false){
            Vector3 spawnPos = new Vector3(Random.Range(-8,8), 6, 0);
            int powerUpId = Random.Range(0,3);
            GameObject newPowerUp = Instantiate(_powerUp[powerUpId], spawnPos, Quaternion.identity );
            yield return new WaitForSeconds(Random.Range(5,8));
        }
    }

    public void OnPlayerDeath() {
        _stopSpawning = true;
    }
}
