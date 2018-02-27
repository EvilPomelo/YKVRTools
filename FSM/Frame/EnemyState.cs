using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//敌人状态机需要识别敌人自身和玩家的游戏物体
public abstract class EnemyState : FsmStateBase
{
    public Transform player;
    public Transform npc;

    //在构造函数内初始化这两个游戏物体
    public EnemyState(Transform Player, Transform Npc,FsmSystem Fsm)
    {
        player = Player;
        npc = Npc;
        fsm = Fsm;
        SetStateName();
        InitDic();
    }

}
