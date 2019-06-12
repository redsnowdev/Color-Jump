using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyCoin : MonoBehaviour
{

    void Update()
    {
        if(transform.position.y <= Camera.main.transform.position.y - 9){
            Destroy(gameObject);
        }
    }
}
