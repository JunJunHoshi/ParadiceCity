using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Battle_Controller : MonoBehaviour
{
    [SerializeField] GameObject mainchara;
    [SerializeField] GameObject maincamera;
    [SerializeField] Text battle_log;
    [SerializeField] GetEnemyData getEnemyData;
    [SerializeField] Transform[] ButtonArray;
    [SerializeField] List<EnemyCollider> bossList;

    public Animator battle_animator;
    private int turn;
    public int BossNumber = 0;
    // Start is called before the first frame update
    void Start()
    {
        battle_animator = GetComponent<Animator>();
        turn = 0;
    }
    public void StopMasaru()
    {
        mainchara.GetComponent<MoveController>().DeactivePlayer();
    }
    public void GoMasaru()
    {
        mainchara.GetComponent<MoveController>().ActivePlayer();
    }

    public void MainCameraOff()
    {
        maincamera.SetActive(false);
    }

    public void MainCameraOn()
    {
        maincamera.SetActive(true);
    }

    public void Battle_Start()
    {
        battle_animator.SetBool("battle_start", true);
        battle_log.text = "戦闘開始!";
        getEnemyData.turn = 0;
    }

    public void Go_Next_Turn()
    {
        turn += 1;
    }

    public void Turn_Reset()
    {
        turn = 0;
    }
    public void Delete_Attack_State()
    {
        battle_animator.SetBool("attack", false);
    }
    public void Delete_Hojyo_State()
    {
        battle_animator.SetBool("hojyo", false);
    }
    public void Delete_Cure_State()
    {
        battle_animator.SetBool("cure", false);
    }
    public void Delete_Enemy_Damage_State()
    {
        battle_animator.SetBool("enemyattack", false);
    }
    public void Delete_Enemy_Stay_State()
    {
        battle_animator.SetBool("enemystay", false);
    }
    public void Decide_Enemy_Death()
    {
        if(getEnemyData.enemyHp <= 0)
        {
            battle_animator.SetBool("enemydeath", true);
        }
    }
    public void Announc_Death()
    {
        battle_log.text = getEnemyData.enemyName+"を倒した！\n";
        if (BossNumber > 0)
        {
            bossList[BossNumber - 1].death = 1;
            bossList[BossNumber - 1].OnReceiveDeath();
            BossNumber = 0;
        }
    }
    public void Battle_End()
    {//戦闘終了　敵の体力のリセット)(デバッグのため)　ボタンのリセット maincameraの復活 主人公の動き復活　を実装 
        turn = 0;
        BossNumber = 0;
        battle_animator.SetBool("battle_start", false);
        getEnemyData.enemyHp = 60;
        foreach(Transform buttonTra in ButtonArray)
        {
            for (int i = 0; i < buttonTra.childCount; ++i)
            {
                GameObject.Destroy(buttonTra.GetChild(i).gameObject);
            }
        }
        MainCameraOn();
        GoMasaru();        
    }   
    public void BattleWithBoss(int bossnum)
    {
        BossNumber = bossnum;
    }
}
