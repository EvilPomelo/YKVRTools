using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TraceState : EnemyState
{
    public TraceState(Transform Player, Transform Npc, FsmSystem Fsm) : base(Player, Npc, Fsm)
    {

    }

    public override void Execute()
    {
        Debug.Log("正在执行Trace");
    }

    public override void InitDic()
    {
        transformDic.Add(Transition.LostPlayer, StateName.Patrol);
        transformDic.Add(Transition.NearPlayer, StateName.Attack);
    }

    public override void SetStateName()
    {
        stateName = StateName.Trace;
    }

    public override void TurnToOtherState()
    {
        if (Vector3.Distance(npc.position,player.position)>6)
        {
            fsm.PerformTransition(Transition.LostPlayer);
        }
        if (Vector3.Distance(npc.position, player.position) < 3)
        {
            fsm.PerformTransition(Transition.NearPlayer);
        }
    }
}

