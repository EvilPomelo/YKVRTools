using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class PatrolState : EnemyState
{
    public PatrolState(Transform Player, Transform Npc, FsmSystem Fsm) : base(Player, Npc, Fsm)
    {
        
    }

    public override void Execute()
    {
        Debug.Log("执行patrol");
    }

    public override void InitDic()
    {
        transformDic.Add(Transition.SeePlayer, StateName.Trace);
    }

    public override void SetStateName()
    {
        stateName=StateName.Patrol;
    }

    public override void TurnToOtherState()
    {
        if (Vector3.Distance(player.position, npc.position) < 6)
        {
            fsm.PerformTransition(Transition.SeePlayer);
        }
    }
}