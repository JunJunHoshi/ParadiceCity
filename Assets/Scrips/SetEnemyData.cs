using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetEnemyData : MonoBehaviour
{//だたserializefieldにある色々な変数をGetEnemydataに反映するだけ
    [SerializeField] public int enemyImageNum;
    [SerializeField] public string enemyName;
    [SerializeField] public int enemyHp;
    [SerializeField] public int enemyLevel;
    [SerializeField] public int enemyAttack;
    [SerializeField] public int enemyDefence;
    [SerializeField] public List<int> enemyCommandList;
    [SerializeField] public GetEnemyData getEnemyData;
    public void SetData()
    {
        getEnemyData.enemyName = enemyName;
        getEnemyData.enemyHp = enemyHp;
        getEnemyData.enemyLevel = enemyLevel;
        getEnemyData.enemyAttack = enemyAttack;
        getEnemyData.enemyDefence = enemyDefence;
        getEnemyData.enemyCommandList = enemyCommandList;
        getEnemyData.SetEnemyImage(enemyImageNum);
    }
}
