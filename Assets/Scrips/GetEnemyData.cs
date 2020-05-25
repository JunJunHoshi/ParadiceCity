using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetEnemyData : MonoBehaviour
{//フィールドで探索をする際に、Enemyのデータを渡されるのでそれを拾って反映してあげてください。技は全てここに乗っています。
    [SerializeField] public List<Sprite> enemyImageList;
    [SerializeField] public string enemyName;
    [SerializeField] public int enemyHp;
    [SerializeField] public int enemyLevel;
    [SerializeField] public int enemyAttack;
    [SerializeField] public int enemyDefence;
    [SerializeField] public List<int> enemyCommandList;
    [SerializeField] public GetBattleCharaData getBattleCharaData;
    [SerializeField] public Text LogText;
    [SerializeField] public Text enemyText;
    [SerializeField] public Image enemyImage;
    [SerializeField] public Animator battle_Animator;

    public int turn = 0;
    public bool hunny = false;

    public void SetEnemyText()
    {
        enemyText.text = enemyName;
    }
    public void SetEnemyImage(int imagenum)
    {
        enemyImage.sprite = enemyImageList[imagenum];
    }

    public void ExecuteEnemyAttack()
    {
        while (enemyHp>0)
        {
            int targetnum = Random.Range(0, getBattleCharaData.CharaObjArray.Length);
            int damage = 0;
            if (getBattleCharaData.CharaObjArray[targetnum] != null && !getBattleCharaData.CharaObjArray[targetnum].GetComponent<BasicData>().dead)
            {//それ攻撃していい奴やで
                BattleChara targetChara = getBattleCharaData.BattleCharaArray[targetnum];
                switch (enemyCommandList[turn])
                {
                    case 1: //ただの攻撃
                        damage = NaturalAttack(enemyAttack, targetChara);
                        getBattleCharaData.UpdateHp(targetnum);
                        LogText.text = enemyName + "の攻撃！\n" + targetChara.name + "に" + damage.ToString() + "のダメージ";
                        AttackEnd();
                        break;
                    case 2://かみつく
                        damage = NaturalAttack(enemyAttack, targetChara);
                        targetChara.defence -= 10;
                        if (targetChara.defence <= 0) targetChara.defence = 0;
                        getBattleCharaData.UpdateHp(targetnum);
                        LogText.text = enemyName + "は牙を剥きだした！\n" + targetChara.name + "に" + damage.ToString() + "のダメージ\n" + targetChara.name + "の防御力がすこし下がった";
                        AttackEnd();
                        break;
                    case 3://どろかけ
                        LogText.text = enemyName + "はヘドロをまき散らす！\nこれは汚い！\n";
                        AllAttack(enemyAttack);
                        AttackEnd();
                        break;
                    case 4://爪を研ぐ
                        LogText.text = $"{enemyName}は鋭い爪を光らせた....";
                        StayEnd();
                        break;
                    case 5://熊の乱舞
                        if (hunny)
                        {
                            LogText.text = $"{enemyName}はなんと蜂蜜をおいしそうに貪っている！";
                            hunny = false;
                            StayEnd();
                        }
                        else
                        {
                            LogText.text = $"「ハチミツタベタイイイイ」\n{enemyName}は暴れしだした!!!";
                            AllAttack(enemyAttack * 100);
                            AttackEnd();
                        }
                        break;
                    case 6://臭い息
                        damage = NaturalAttack(enemyAttack, targetChara);
                        targetChara.attack -= 10;
                        if (targetChara.attack <= 0) targetChara.attack = 0;
                        getBattleCharaData.UpdateHp(targetnum);
                        LogText.text = enemyName + "は臭い息を吐き出した！\n鼻がよじれそうだ...\n" + targetChara.name + "に" + damage.ToString() + "のダメージ\n" + targetChara.name + "の攻撃力がすこし下がった";
                        AttackEnd();
                        break;
                    case 7://硬化魔法
                        enemyDefence += 150;
                        LogText.text = "【硬化魔法】 " + enemyName + "は身に頑強なバリアをまとった\n" + enemyName +"の防御力が上がった\n";
                        StayEnd();
                        break;
                    case 8://ぶちかまし
                        LogText.text = $"{enemyName}のぶちかまし!\n";
                        damage = NaturalAttack(enemyDefence * 3,targetChara);
                        getBattleCharaData.UpdateHp(targetnum);
                        LogText.text += $"{targetChara.name}に{damage}のダメージ\n";
                        AttackEnd();
                        break;
                    case 9://発砲
                        LogText.text = $"{enemyName}は発砲した!\n";
                        damage = NaturalAttack(300, targetChara);
                        getBattleCharaData.UpdateHp(targetnum);
                        LogText.text += $"{targetChara.name}に{damage}のダメージ\n";
                        AttackEnd();
                        break;
                    case 10://なぎはらい
                        LogText.text = enemyName + "のなぎはらい！\nイッテーー！\n";
                        AllAttack(enemyAttack);
                        AttackEnd();
                        break;
                }
                if (turn + 1 < enemyCommandList.Count)
                    turn += 1;
                else
                    turn = 0;
                break;
            }
            else continue;
        }
    }
    public void AttackEnd()
    {
        battle_Animator.SetBool("enemyattack", true);
    }
    public void StayEnd()
    {
        battle_Animator.SetBool("enemystay", true);
    }
    public int NaturalAttack(int attack, BattleChara targetChara)
    {//普通の攻撃をここですべて計算し実際のHPに反映。そしてダメージ数を返す。
        int damage = 0;
        damage = attack - targetChara.defence;
        if (damage < 0) damage = 0;
        targetChara.CurrentHp -= damage;
        if (targetChara.CurrentHp <= 0) targetChara.CurrentHp = 0;
        return damage;
    }
    public void AllAttack(int attack)
    {//こっちはログまで管理しているからケア
        for(int i = 0; i < getBattleCharaData.CharaObjArray.Length; i++)
        {
            if (getBattleCharaData.CharaObjArray[i]!=null && !getBattleCharaData.CharaObjArray[i].GetComponent<BasicData>().dead)
            {
                int damage = 0;
                damage = NaturalAttack(attack, getBattleCharaData.BattleCharaArray[i]);
                LogText.text += getBattleCharaData.BattleCharaArray[i].name + "に" + damage + "のダメージ\n";
            }
        }
    }

}
