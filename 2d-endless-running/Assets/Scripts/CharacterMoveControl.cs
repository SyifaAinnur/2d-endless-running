using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMoveControl : MonoBehaviour
{
    [Header("Movement")]
    public float moveAccel;
    public float maxSpeed;
    private Rigidbody2D rig;

    //jumping
    [Header("Jump")]
    public float jumpAccel;
    private bool isJumping;
    private bool isOnGround;

    //raycast
    [Header("Ground Raycast")]
    public float groundRaycastDistance;
    public LayerMask groundLayerMask;

    private float lastPositionX;

    //score
    [Header("Scoring")]
    public ScoreController score;
    public float scoringRatio;
    
    //gameover
    [Header("GameOver")]
    public GameObject gameOverScreen;
    public float fallPositionY;

    //camera
    [Header("Camera")]
    public CameraMoveController gameCamera;
    //anim
    private Animator anim;

    //sounds
    private CharacterSoundController sound;
    // Start is called before the first frame update
    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sound = GetComponent<CharacterSoundController>();
        lastPositionX = transform.position.x;
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //raycast ground
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        if (hit){
            if(!isOnGround && rig.velocity.y <= 0){
                isOnGround = true;
                
            }
        }
        else{
            isOnGround = false;
        }

        //calculate velocity vector
        Vector2 velocityVector = rig.velocity;
        if(isJumping){
            velocityVector.y += jumpAccel;
            isJumping = false;
        }
        velocityVector.x = Mathf.Clamp(velocityVector.x + moveAccel * Time.deltaTime, 0.0f, maxSpeed);

        rig.velocity = velocityVector;
    }
    private void OnDrawGizmos(){
        Debug.DrawLine(transform.position, transform.position +(Vector3.down * groundRaycastDistance), Color.white );
    }
    private void Update(){
        //inputan
        if(Input.GetMouseButtonDown(0)){
            if(isOnGround){
                isJumping = true;
                sound.PlayJump();
            }
        }

        //read input
        if(Input.GetMouseButtonDown(0)){
            if(isOnGround){
                isJumping = true;
                sound.PlayJump();
            }
        }
        //ganti animasi
        anim.SetBool("isOnGround", isOnGround);

        //calculate score
        int distancePassed = Mathf.FloorToInt(transform.position.x - lastPositionX);
        int scoreIncrement = Mathf.FloorToInt(distancePassed / scoringRatio);

        if(scoreIncrement > 0){
            score.IncreaseCurrentScore(scoreIncrement);
            lastPositionX += distancePassed;
        }
        //gameover
        if(transform.position.y < fallPositionY){
            GameOver();
        }
        
    }
    private void GameOver(){
            //mengset highscore
            score.FinishScoring();

            //camera stop
            gameCamera.enabled = false;

            //show gameover
            gameOverScreen.SetActive(true);

            //disable this too
            this.enabled = false;
        }    
    
}
