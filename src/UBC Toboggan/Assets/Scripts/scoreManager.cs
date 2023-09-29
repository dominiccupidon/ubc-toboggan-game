using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    public static float score = 0f;
    
    public Text scoreText;
    public GameObject flipBonusText;
    public GameObject airBonusText;
    public GameObject player;

    public float flipBonus = 5f;
    public float airTimeMultiplier = 1f;

    float flipScore = 0f;
    float airScore = 0f;
    bool showingBonus = false;

    playerManager playerManagerScript;

    void Start() {
        flipBonusText.SetActive(false);
        airBonusText.SetActive(false);

        score = 0;

        playerManagerScript = player.GetComponent<playerManager>();
    }

    void Update() {
        scoreText.text = score.ToString();

        // begin showing score text when a point is gained in the air
        if (!playerManagerScript.grounded && !showingBonus && (flipScore > 0f || airScore > 0f)) {
            updateFlipText();
            updateAirText();

            flipBonusText.SetActive(true);
            airBonusText.SetActive(true);
            showingBonus = true;
        }
        
        // handle removing the bonus text if player is on ground for x seconds
        if (showingBonus && playerManagerScript.grounded) {
            showingBonus = false;

            score += airScore;
            score += flipScore;

            flipScore = 0f;
            airScore = 0f;
            
            flipBonusText.SetActive(false);
            airBonusText.SetActive(false);
        }
    }

    // called everytime the player does a full 360 flip in the air
    public void addFlipScore() {
        flipScore += flipBonus;
        updateFlipText();
    }

    // called once every second the player is in the air
    public void addAirTimeScore(int seconds) {
        // add and display points
        float addedScore = seconds * airTimeMultiplier;
        airScore += addedScore;
        updateAirText();
    }

    void updateFlipText() {
        flipBonusText.GetComponent<Text>().text = "Flip Bonus: +" + flipScore.ToString();
    }

    void updateAirText() {
        airBonusText.GetComponent<Text>().text = "Air Bonus: +" + airScore.ToString();
    }

}
