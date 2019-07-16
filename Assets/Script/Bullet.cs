using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveUp = new Vector3(0, 1, 0);
        transform.Translate(moveUp * _speed * Time.deltaTime);

        if (transform.position.y >= 5){

            if (this.transform.parent != null){
            Destroy(this.transform.parent.gameObject);}

            Destroy(this.gameObject);
        }
    }
}
