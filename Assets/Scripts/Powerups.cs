using UnityEngine;
using System.Collections;

public class Powerups : MonoBehaviour {

    public bool doublePoints;
    public bool safeMode;

    public float powerupLength;

    public Sprite[] powerupSprites;

    private PowerupManager thePowerManager;
    // Use this for initialization
	void Start () {
        thePowerManager = FindObjectOfType<PowerupManager>();	
	}

    void Awake() {
        int powerupSelector = Random.Range(0, 2);
        switch (powerupSelector) {
            case 0: doublePoints = true;
                break;
            case 1: safeMode = true;
                break;
        }
        GetComponent<SpriteRenderer>().sprite = powerupSprites[powerupSelector];
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.name == "Player") {
            thePowerManager.ActivatePowerup(doublePoints, safeMode, powerupLength);
        }
        gameObject.SetActive(false);
    }

}
