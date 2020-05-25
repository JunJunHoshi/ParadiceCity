using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEnemyManager : MonoBehaviour
{//Fieldの敵のデータを持って置き、それを頃合いに場所に適したSetEnemydataする
    [SerializeField] Transform masaoTra;
    [SerializeField] SetEnemyData setEnemyData;
    public List<int> enemyCommandList;

    public void Set(int imagenum, string name, int hp, int level, int attack, int defence, List<int> CommandList)
    {
        setEnemyData.enemyImageNum = imagenum;
        setEnemyData.enemyName = name;
        setEnemyData.enemyHp = hp;
        setEnemyData.enemyLevel = level;
        setEnemyData.enemyAttack = attack;
        setEnemyData.enemyDefence = defence;
        setEnemyData.enemyCommandList = CommandList;
    }

    public void EnemyDic(int num)
    {
        List<int> list;
        switch (num)
        {
            case 0:
                list = new List<int>() { 1, 2 };
                Set(0, "猟犬",40, 1, 20, 10, list);
                break;
            case 1:
                list = new List<int>() { 1 };
                Set(1, "子ぐま", 80, 3, 45, 23, list);
                break;
            case 2:
                list = new List<int>() { 1, 2, 3 };
                Set(2, "泥ネズミ", 180, 6, 68, 30, list);
                break;
            case 3:
                list = new List<int>() { 1, 2, 6 };
                Set(3, "悪臭蝙蝠", 200, 8, 80, 40, list);
                break;
            case 4:
                list = new List<int>() { 1 };
                Set(5, "凡人の歩兵", 100, 0, 50, 30, list);
                break;
            case 5:
                list = new List<int>() { 1,8 };
                Set(5, "太った重騎兵", 210, 0, 75, 30, list);
                break;
            case 6:
                list = new List<int>() { 1,10 };
                Set(5, "剣の達人", 180, 0, 100, 20, list);
                break;
            case 7:
                list = new List<int>() { 10 };
                Set(6, "歴戦の将軍", 10000, 0, 900, 500, list);
                break;

        }
    }

    public void SetEnemy()
    {//場所に応じてEnemyDicを実行し、そのデータをsetEnemyDataに渡す。そしてSetDataを実行
        if (masaoTra.position.x >= -65 && masaoTra.position.x <= -15 && masaoTra.position.y >= -90 && masaoTra.position.y <= -40) //最初の村付近
        {
            EnemyDic(Random.Range(0, 2));
        }

        if (masaoTra.position.x >= -110 && masaoTra.position.x <= -60 && masaoTra.position.y >= -90 && masaoTra.position.y <= -40)
        {
            EnemyDic(Random.Range(0, 3));
        }

        if (masaoTra.position.x >= -125 && masaoTra.position.x <= -60 && masaoTra.position.y >= 11 && masaoTra.position.y <= 60)
        {
            EnemyDic(Random.Range(2, 4));
        }

        if (masaoTra.position.x >= 350 && masaoTra.position.x <= 425 && masaoTra.position.y >= -30 && masaoTra.position.y <= 0)
        {
            EnemyDic(Random.Range(4, 6));
        }

        if (masaoTra.position.x >= 350 && masaoTra.position.x <= 425 && masaoTra.position.y >= 0 && masaoTra.position.y <= 70)
        {
            EnemyDic(Random.Range(5, 8));
        }
        setEnemyData.SetData();
    }
   
}
