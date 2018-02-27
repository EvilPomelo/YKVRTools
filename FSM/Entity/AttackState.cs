using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AttackState : EnemyState
{
    public AttackState(Transform Player, Transform Npc, FsmSystem Fsm) : base(Player, Npc, Fsm)
    {
    }

    public override void Execute()
    {
        Debug.Log("Attack");
    }

    public override void InitDic()
    {
        transformDic.Add(Transition.SeePlayer,StateName.Trace);
        transformDic.Add(Transition.LostPlayer, StateName.Patrol);
    }

    public override void SetStateName()
    {
        stateName = StateName.Attack;
    }

    public override void TurnToOtherState()
    {
        float distance = Vector3.Distance(player.position, npc.position);
        if (distance> 6)
        {
            fsm.PerformTransition(Transition.LostPlayer);
        }
        if (distance>3&& distance <6)
        {
            fsm.PerformTransition(Transition.SeePlayer);
        }
    }
}
