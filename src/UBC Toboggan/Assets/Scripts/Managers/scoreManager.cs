using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreManager : MonoBehaviour
{
    float score = 0f;
    
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

    public GameObject finishLine;
    FinishLine finishScript;

    void Start() {
        UIManager.Instance.scoreManager = this;

        flipBonusText.SetActive(false);
        airBonusText.SetActive(false);

        score = 0;

        playerManagerScript = player.GetComponent<playerManager>();
        finishScript = finishLine.GetComponent<FinishLine>();
    }

    void Update() {
        scoreText.text = score.ToString();

        // begin showing score text when a point is gained in the air
        if (!playerManagerScript.grounded && !showingBonus && (flipScore > 0f || airScore > 0f) && !finishScript.levelEndTriggered) {
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


    public bool HideScore()
    {
        flipBonusText.SetActive(false);
        airBonusText.SetActive(false);
        scoreText.gameObject.SetActive(false);
        return showingBonus;
    }

    public void ShowScore(bool wasShowingScore)
    {
        flipBonusText.SetActive(wasShowingScore);
        airBonusText.SetActive(wasShowingScore);
        scoreText.gameObject.SetActive(true);
    }

    public float GetLevelScore()
    {
        float levelScore = score;
        float cumulativeScore = PlayerPrefs.GetFloat("Score", 0) + levelScore;
        PlayerPrefs.SetFloat("Score", cumulativeScore);
        score = 0f;
        return levelScore;
    }

    public float GetFinalScore()
    {
        float cumulativeScore = PlayerPrefs.GetFloat("Score", 0) + score;
        PlayerPrefs.SetFloat("Score", cumulativeScore);
        return cumulativeScore;
    }

    void updateFlipText() {
        flipBonusText.GetComponent<Text>().text = "Flip Bonus: +" + flipScore.ToString();
    }

    void updateAirText() {
        airBonusText.GetComponent<Text>().text = "Air Bonus: +" + airScore.ToString();
    }

}
