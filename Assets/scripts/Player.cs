using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    CoinManager coinManager;
    public GameObject startText;
    public bool isStart = false;
    public GameObject stairEffect;
    bool isDead = false;
    GameManager gameManager;
    ScoreManager scoreManager;
    public GameObject JumpEffect;
    public GameObject deadEffect;
    Vector2 playerPosition,dragPosition,touchPosition;
    bool isDragging = false;
    public float gravity = 1;
    public float jumpHeight;
    float jumpV;
    Rigidbody2D rb;
    StairsManager stairManager;

    void Awake()
    {
        Time.timeScale= 1f;
    }
    // Start is called before the first frame update
    void Start()
    {
        coinManager = GameObject.Find("CoinManager").GetComponent<CoinManager>();
        isStart = false;
        isDead = false;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        stairManager = GameObject.Find("stairs").GetComponent<StairsManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        WaitToTouch();
        if(isDead) return;
        if(!isStart) return;
        GetInput();
        MovePlayer();
        AddGravityToPlayer();
        DeadCheck();
    }

    void WaitToTouch(){
        if(!isStart){
             if(Input.GetMouseButtonDown(0)){
                 isStart = true;
                 startText.SetActive(false);
             }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "stair"){
            if(rb.velocity.y <=0 ){
                Jump();
                scoreManager.AddScore(1);
                DestroyStairAndMakeNewStair(other);
                Destroy( Instantiate(JumpEffect , transform.position , Quaternion.identity), 1f );
            }
        } else if(other.gameObject.tag == "coin"){
            scoreManager.AddScore(3);
            Destroy(other.gameObject.transform.parent.gameObject);
            Destroy( Instantiate(JumpEffect , transform.position , Quaternion.identity), 1f );
        }
    }
    public void Jump(){
        jumpV = gravity * jumpHeight;
        rb.velocity = new Vector2(0,jumpV);
        if(gravity < 2.4){
            gravity += 0.008f;
        }
    }
    void AddGravityToPlayer(){
        if(rb.velocity.y >= -300)
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y - (gravity * gravity));
    }
    void GetInput(){
        if(Input.GetMouseButtonDown(0)){
            isDragging = true;
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x , Input.mousePosition.y ));
            playerPosition = transform.position;
        } else if(Input.GetMouseButtonUp(0)){
            isDragging = false;
        }
    }

    void MovePlayer(){
        if( isDragging == true ){
            dragPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x , Input.mousePosition.y) );
            transform.position = new Vector2(playerPosition.x + (dragPosition.x - touchPosition.x) , transform.position.y);
            if(transform.position.x < -3.54){
                transform.position = new Vector2(-3.54f , transform.position.y);
            }else if(transform.position.x > 3.58){
                transform.position = new Vector2(3.54f, transform.position.y);
            }
        }
    }
    void DestroyStairAndMakeNewStair(Collider2D stair){

        if(Random.Range(4,8) == 6){
            coinManager.GenerateCoin(stair.transform.position);
        }

        stairManager.MakeNewStair();
        stairManager.ChangeBackgroundColor(stair);
        Destroy(stair.gameObject);
        GameObject stairEffectTemp = Instantiate(stairEffect , stair.gameObject.transform.position , Quaternion.identity);
        stairEffect.transform.localScale = new Vector2(stair.transform.localScale.x , stair.transform.localScale.y);
        Destroy(stairEffectTemp,0.5f);        
    }
    void DeadCheck(){
        if( isDead == false &&transform.position.y < Camera.main.transform.position.y - 8){
            gameManager.GameOver();
            isDead = true;
            Destroy( Instantiate(deadEffect , transform.position , Quaternion.identity), 1.5f );
            gameObject.SetActive(false);
        }
    }
}
