using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsManager : MonoBehaviour
{
    float hueValue;
    
    public GameObject stairPrefab;
    int stairIndex = 0;
    float stairWidth = 3;
    float stairHeight = 0.8f;
    void Start()
    {
        InItColor();
        for(int i =0;i<2;i++){
            MakeNewStair();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void MakeNewStair(){
        int randomPositionX;
        stairWidth = Random.Range(1.7f , 4f);
        if(stairIndex == 0)
            randomPositionX = 0;
        else
            randomPositionX = Random.Range(-3,3);

        Vector2 newPosition = new Vector2(randomPositionX,(stairIndex*5) - 1.8f);
        GameObject newStair = Instantiate(stairPrefab , newPosition , Quaternion.identity);
        newStair.transform.SetParent(transform);
        newStair.transform.localScale = new Vector2(stairWidth , stairHeight);
        stairIndex++;
        SetColor(newStair);
    }
    void SetColor(GameObject newStair){
            hueValue += 0.11f;
            if(hueValue > 1){
            hueValue -= 1;
            }
        newStair.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hueValue , 0.6f,0.8f);
    }
    void InItColor(){
        hueValue = Random.Range(0,1f);
        Camera.main.backgroundColor = Color.HSVToRGB(hueValue ,0.6f,0.8f);
    }
    public void ChangeBackgroundColor(Collider2D Stair){
        Camera.main.backgroundColor = Stair.gameObject.GetComponent<SpriteRenderer>().color;
    }

}
