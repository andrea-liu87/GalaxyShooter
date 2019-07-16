using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thruster : MonoBehaviour
{
    private SpriteRenderer _renderer;
    private void Start() {
        _renderer = GetComponent<SpriteRenderer>();
        StartCoroutine(FlickeringObject());
    }
    void Update()
    {
        
    }

    IEnumerator FlickeringObject (){
        while(true){
            _renderer.enabled = true;
            yield return new WaitForSeconds(0.01f);
            _renderer.enabled = false;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
