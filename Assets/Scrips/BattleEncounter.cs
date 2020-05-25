using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEncounter : MonoBehaviour
{
    [SerializeField] public int encindex;
    [SerializeField] public GetMapPos getMapPos;
    [SerializeField] public Battle_Controller battleController;
    [SerializeField] public FieldEnemyManager fieldEnemyManager;

    // Start is called before the first frame update
    void Start()
    {
        encindex = 200;
    }

    // Update is called once per frame
    public void WalkInMap()
    {
        if(getMapPos.World == 1)
        {
            encindex -= 1;
        }
        if(encindex < 50)
        {
            fieldEnemyManager.SetEnemy();
            battleController.Battle_Start();
        }
    }
    public void ResetEnc()
    {
        encindex = Random.Range(200, 700);
    }
}
