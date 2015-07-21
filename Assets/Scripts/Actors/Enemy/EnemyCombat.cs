using UnityEngine;
using System.Collections;

public class EnemyCombat : MonoBehaviour
{
    private GameController gameController;

    void Awake()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            Debug.Log("Can't find Game Controller");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !gameController.inCombat)
        {
            gameController.AddCombatGroup(gameObject, CombatGroup.Enemy);
        }
    }
}
