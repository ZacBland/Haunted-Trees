using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundController : MonoBehaviour
{
    public float elapsedTime;
    public int round;
    public TextMeshProUGUI roundDisplay;
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
}
