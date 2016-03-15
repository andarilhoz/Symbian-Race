using UnityEngine;
using System.Collections;

public class PowerupManager : MonoBehaviour {
    public bool doublePoints;
    public bool safeMode;

    private bool powerupActive;

    private float powerupLenghtCounter;

    private ScoreManager theScoreManager;
    private PlatformGenerator thePlatformGenerator;

    private float normalPointsPerSecond;
    private float spikeRate;

    private PlatformDestroyer[] spikeList;

    private GameManager theGameManager;

    // Use this for initialization
    void Start () {
        theScoreManager = FindObjectOfType<ScoreManager>();
        thePlatformGenerator = FindObjectOfType<PlatformGenerator>();
        theGameManager = FindObjectOfType<GameManager>();
        spikeRate = 50f;
        normalPointsPerSecond = 1f;
        powerupLenghtCounter = 0;
        doublePoints = false;
        safeMode = false;

    }
	
	// Update is called once per frame
	void Update () {
        if (powerupLenghtCounter > 0)
        {
            if (theGameManager.powerupReset)
            {
                powerupLenghtCounter = 0;
                theGameManager.powerupReset = false;
            }
            if (doublePoints)
            {
                theScoreManager.pointsPerSecond = normalPointsPerSecond * 1;
                theScoreManager.shouldDouble = true;
            }

            if (safeMode)
            {
                thePlatformGenerator.randomSpikeThreshold = 0f;
            }

            if (powerupActive)
            {
                powerupLenghtCounter -= Time.deltaTime;
                if (powerupLenghtCounter <= 0 && !safeMode)
                {
                    theScoreManager.pointsPerSecond = normalPointsPerSecond;
                    theScoreManager.shouldDouble = false;
                }
                else if (powerupLenghtCounter <= 0 && safeMode == true)
                {
                    thePlatformGenerator.randomSpikeThreshold = spikeRate;
                }
                powerupActive = false;
            }

            if (!safeMode)
            {

                thePlatformGenerator.randomSpikeThreshold = spikeRate;
            }

        }
        else {
            safeMode = false;
            doublePoints = false;
        }

	}

    public void ActivatePowerup(bool points, bool safe, float time) {
        doublePoints = points;
        safeMode = safe;
        powerupLenghtCounter = time;

        if (safeMode) {
            spikeList = FindObjectsOfType<PlatformDestroyer>();

            for (int i = 0; i < spikeList.Length; i++)
            {
                if (spikeList[i].gameObject.name.Contains("spikes")){
                    spikeList[i].gameObject.SetActive(false);
                }
            }


        }
        if (doublePoints) {
            theScoreManager.AddScore(10000);
        }



        powerupActive = true;
    }



}
