using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{   [SerializeField]
    private float _speed = 3.5f;
    private float _speedMultiplier = 2.0f;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _score = 0;

    [SerializeField]
    private GameObject _bullet;

    [SerializeField]
    private GameObject _tripleShoot;
    private bool _isTripleShootActive = false;
    private bool _isShieldActive = false;

    [SerializeField]
    private float _fireRate = 0.25f;
    
    private float _canFire = -1f;

    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shieldVisualizer;

    private ui_manager UIManager;
    private Game_Manager _gameManager;

    [SerializeField]
    private GameObject _rightEngine;

    [SerializeField]
    private GameObject _leftEngine;

    [SerializeField]
    private Explosion_prefab _explosion;
    private AudioSource _audioSource;

    [SerializeField]
    private AudioClip _bulletShot;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0,-2.5f,0);
        _spawnManager = GameObject.Find("Spawn Enemy").GetComponent<SpawnManager>();
        UIManager = GameObject.Find("Canvas").GetComponent<ui_manager>();
        _gameManager = GameObject.Find("Game Manager").GetComponent<Game_Manager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null){
            Debug.LogError("The _spawnManager is null");
        }

        if (UIManager == null){
            Debug.LogError("UI Manager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

#if UNITY_ANDROID
        if ((Input.GetKeyDown(KeyCode.Space) || CrossPlatformInputManager.GetButtonDown("Fire"))
        && Time.time > _canFire){
            ShootBullet();
        }
#else 
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        && Time.time > _canFire){
            ShootBullet();
        }
#endif
    }

    void CalculateMovement() {
        float moveHorizontal = CrossPlatformInputManager.GetAxis("Horizontal");
        float moveVertical = CrossPlatformInputManager.GetAxis("Vertical");

        Vector3 translation = new Vector3(moveHorizontal, moveVertical, 0);
        transform.Translate(translation * _speed * Time.deltaTime);

        if (transform.position.y >= 0){
            transform.position = new Vector3(transform.position.x, 0, 0);
        } else if (transform.position.y < -3.8f){
            transform.position = new Vector3(transform.position.x, -3.8f, 0);
        }

        if (transform.position.x >= 11){
            transform.position = new Vector3(-11, transform.position.y, 0);
        } else if (transform.position.x < -11){
            transform.position = new Vector3(11, transform.position.y, 0);
        }
    }

    void ShootBullet(){
        _canFire = Time.time + _fireRate;
        if (_isTripleShootActive == true){
            Instantiate(_tripleShoot, transform.position, Quaternion.identity);
        } else {
        Instantiate(_bullet, transform.position, Quaternion.identity);
        }
        _audioSource.clip = _bulletShot;
        _audioSource.Play();
    }

    public void Damage() {
        if (_isShieldActive == true){
            _isShieldActive = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;
        UIManager.reduceLiveDisplay(_lives);
        if (_lives == 2){
            _leftEngine.SetActive(false);
        }

        if (_lives == 1){
            _rightEngine.SetActive(false);
        }


        if (_lives == 0){
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosion, transform.position, Quaternion.identity);
            _gameManager.GameOver();
            Destroy(this.gameObject, 0.5f);
        }
    }

    public void ActivateTripleShoot () {
        _isTripleShootActive = true;
        StartCoroutine(TripleShootDeactivate());
    }

    IEnumerator TripleShootDeactivate(){
        yield return new WaitForSeconds(10);
        _isTripleShootActive = false;
    }


    public void ActivateSpeedBoost () {
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostDeactivate());
    }

    IEnumerator SpeedBoostDeactivate() {
        yield return new WaitForSeconds(5);
        _speed /= _speedMultiplier;
    }

    public void ActivateShield() {
        _isShieldActive = true;
        _shieldVisualizer.SetActive(true);
    }

    public void addScore(int point) {
        _score += point;
        UIManager.setScoreText(_score);
    }
}
