using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppleTree : MonoBehaviour
{
    public ScoreCounter scoreCounter;

    //instantiate apples
    public GameObject applePrefab;

    //speed at which apple tree moves
    float[] speedArray = { 2f, 4f, 6f };
    float speed;

    //distance when apple tree turns around
    float leftAndRightEdge = 20f;
    private bool movingRight = true;

    //chance that it changes direction
    float[] changeDirArray = { 0.01f, 0.02f, 0.04f };
    float changeDirChance;

    //seconds between apple instantiation
    float[] appleDropDelayArray = { 1.5f, 1f, 0.5f };
    float appleDropDelay;
    
    //difficulty
    int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreCounter = scoreGO.GetComponent<ScoreCounter>();

        //start dropping apples
        Invoke("DropApple", 2f);
    }

    public float GetDifficulty()
    {
        if (scoreCounter.score < 2000)
        {
            difficulty = 0;
        }
        else if (scoreCounter.score >= 2000 && scoreCounter.score < 4000)
        {
            difficulty = 1;
        }
        else if (scoreCounter.score >= 4000)
        {
            difficulty = 2;
        }

        if (difficulty < 0) difficulty = 0;
        if (difficulty >= speedArray.Length) difficulty = speedArray.Length - 1;

        return difficulty;
    }
    void DropApple()
    {
        //get difficulty and set drop speed
        difficulty = (int)GetDifficulty();
        appleDropDelay = appleDropDelayArray[difficulty];

        GameObject apple = Instantiate<GameObject>(applePrefab);
        apple.transform.position = transform.position;
        Invoke( "DropApple", appleDropDelay );
    }

    // Update is called once per frame
    void Update()
    {
        // Update difficulty and speed
        difficulty = (int)GetDifficulty();
        speed = speedArray[difficulty];

        // Basic movement
        Vector3 pos = transform.position;

        if (movingRight)
        {
            pos.x += speed * Time.deltaTime;
        }
        else
        {
            pos.x -= speed * Time.deltaTime;
        }

        transform.position = pos;

        // Turn around if at the edge
        if (pos.x < -leftAndRightEdge)
        {
            movingRight = true;
        }
        else if (pos.x > leftAndRightEdge)
        {
            movingRight = false;
        }

        //Debug.Log($"Position: {pos}, Speed: {speed}, Change: {changeDirChance}, Delay: {appleDropDelay}");
    }

    void FixedUpdate()
    {
        //get difficulty
        difficulty = (int)GetDifficulty();
        changeDirChance = changeDirArray[difficulty];

        if (Random.value < changeDirChance)
        {
            movingRight = !movingRight;
        }
    }

}
