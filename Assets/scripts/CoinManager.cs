using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public GameObject fastCoinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
   public void GenerateCoin(Vector3 stairTransform){
        Vector3 position = new Vector2(stairTransform.x + Random.Range(-0.5f,0.5f) , stairTransform.y + Random.Range(8f,9f) );
        Instantiate(fastCoinPrefab , position , Quaternion.identity);
    }
}
