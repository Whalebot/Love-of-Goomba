using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMasterPointsMechanic : MonoBehaviour
{
    public Text pointsDisplay;
    [SerializeField]
    public int GameMasterPointsForSpawning;
    public static GameObject [] spawnerButtons;
    // Start is called before the first frame update
    void Start(){
        spawnerButtons = GameObject.FindGameObjectsWithTag("SpawnerButton");
        pointsDisplay.text = GameMasterPointsForSpawning.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        checkForAffordance();
    }

    public void substractPoints(GameObject currentSpawnableObject){
        GameMasterPointsForSpawning -= currentSpawnableObject.GetComponent<SpawnableObjectCost>().cost;
        pointsDisplay.text = GameMasterPointsForSpawning.ToString();
    }

    public void giveBackLostPoints(GameObject currentSpawnableObject){
        GameMasterPointsForSpawning += currentSpawnableObject.GetComponent<SpawnableObjectCost>().cost;
        pointsDisplay.text = GameMasterPointsForSpawning.ToString();
    }

    void checkForAffordance(){
        foreach(GameObject spawnerButton in GameMasterPointsMechanic.spawnerButtons){
            int cost = spawnerButton.GetComponent<ObjectSpawner>().spawnableGO.GetComponent<SpawnableObjectCost>().cost;
            Button tempSpawnerButton = spawnerButton.GetComponent<Button>();
            if(GameMasterPointsForSpawning-cost<0){
                tempSpawnerButton.interactable = false;
            }
            else{
                tempSpawnerButton.interactable = true;
            }
        }
    }
}
