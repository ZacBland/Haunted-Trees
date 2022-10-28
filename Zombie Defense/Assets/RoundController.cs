using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundController : MonoBehaviour
{
    public float elapsedTime;
    public int round;
    public TextMeshProUGUI roundDisplay;
    public float spawnDelay;
    public int bottomSpeed;
    public int topSpeed;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        round = 1;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        setRound();
        setEnemyCharacteristics();
    }

    void setRound(){
        if(elapsedTime > 120) {
            round = 5;
        }
        else if(elapsedTime > 90) {
            round = 4;
        }
        else if(elapsedTime > 60) {
            round = 3;
        }
        else if(elapsedTime > 30) {
            round = 2;
        }
        roundDisplay.text = "Round: " + round;
    }
    
    void setEnemyCharacteristics(){
        if(round == 1){
            spawnDelay = 2f;
            bottomSpeed = 3;
            topSpeed = 4;
        }
        if(round == 2){
            spawnDelay = 1.2f;
            bottomSpeed = 3;
            topSpeed = 4;
        }
        if(round == 3){
            spawnDelay = 1.7f;
            bottomSpeed = 5;
            topSpeed = 7;
        }
        if(round == 4){
            spawnDelay = 0.4f;
            bottomSpeed = 3;
            topSpeed = 5;
        }
        if(round == 5){
            spawnDelay = 1f;
            bottomSpeed = 6;
            topSpeed = 9;
        }
    }
}
