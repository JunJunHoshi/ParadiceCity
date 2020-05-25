using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    [SerializeField] public int bossid;
    public SetEnemyData setEnemy;
    public Fungus.Flowchart MyFlowChart;
    [SerializeField] Battle_Controller battle_Controller;
    [SerializeField] MoveController MasaoMove;
    public int death = 0;
    public CharaDataArray charaDataArray;

    public void Start()
    {
        setEnemy = GetComponent<SetEnemyData>();
        MyFlowChart = GetComponent<Fungus.Flowchart>();
        charaDataArray = GetComponentInParent<CharaDataArray>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (death==0)
        {
            MasaoMove.DeactivePlayer();
            MyFlowChart.SendFungusMessage("BattleStart");
            setEnemy.SetData();
        }
    }   
    public void BattleStart()
    {
        battle_Controller.Battle_Start();
        battle_Controller.BattleWithBoss(bossid+1);
    }
    public void OnReceiveDeath()
    {
        if (death==1)
        {
            Invoke("SendDeath", 3f);
        }
    }
    public void SendDeath()
    {
        MyFlowChart.SendFungusMessage("Death");
        death = 1;
    }
    public void DestroyMyself()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }
    public void StartSave()
    {
        charaDataArray.bossArray.BossDead[bossid] = death;
    }
    public void GetSave()
    {
        if (charaDataArray.bossArray.BossDead[bossid] == 1)
        {
            Destroy(gameObject);
        }
    }
}
