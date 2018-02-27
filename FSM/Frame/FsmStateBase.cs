using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FsmStateBase
{
    //该类的状态名
    public StateName stateName;

    //持有fsm
    public FsmSystem fsm;
    //该字典保存该状态在什么条件(Transition的实体)下可跳转到什么状态(StateName的实体)
    public Dictionary<Transition, StateName> transformDic = new Dictionary<Transition, StateName>();
    //在具体的子类中初始化用于状态转换的字典
    public abstract void InitDic();
    //状态刚进入时执行
    public virtual void Enter() { }
    //状态离开时执行
    public virtual void Exit() { }
    //在该状态下需执行的行为
    public abstract void Execute();
    //判断该状态下是否满足转换条件，如满足就转换
    public abstract void TurnToOtherState();
    //设置StateName的值
    public abstract void SetStateName();
    //通过转换条件获得状态名
    public StateName GetStateName(Transition tra)
    {
        if (transformDic.ContainsKey(tra))
        {
            return transformDic[tra];
        }
        return StateName.NullStateName;
    }
}

