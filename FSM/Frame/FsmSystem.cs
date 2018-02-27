using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class FsmSystem
{
    private Dictionary<StateName, FsmStateBase> stateDic = new Dictionary<StateName, FsmStateBase>();
    private StateName currentStateName;
    private FsmStateBase currentFsmState;
    public void Update() {
        if (stateDic == null)
        {
            Debug.Log("状态机中没有状态");
        }
        else
        {
            if (currentFsmState == null)
            {
                currentFsmState = stateDic.Values.First() ;
                currentStateName = stateDic.Keys.First();
            }
        }
        currentFsmState.Execute();
        currentFsmState.TurnToOtherState();
    }

    public void AddState(FsmStateBase fsb) {
        if (fsb.stateName==StateName.NullStateName) {
            Debug.LogError("不允许添加空状态");
        }
        if (stateDic.ContainsKey(fsb.stateName))
        {
            Debug.LogError(fsb.stateName + "已存在，不允许重复添加添加");
        }
        stateDic.Add(fsb.stateName, fsb);
    }
    public void DeleteState(StateName sta) {
        if (sta == StateName.NullStateName)
        {
            Debug.LogError("无法删除空状态");
            return;
        }
        if (stateDic.ContainsKey(sta) == false)
        {
            Debug.LogError("无法删除不存在的状态：" + sta);
            return;
        }
    }

    public void PerformTransition(Transition trans)
    {
        if (trans == Transition.NullTransition)
        {
            Debug.LogError("无法执行空的转换条件");
        }
        StateName stateName=currentFsmState.GetStateName(trans);
        if (stateName == StateName.NullStateName)
        {
            Debug.LogError("当前状态" + stateName + "无法根据条件" + trans + "发生转换");
            return;
        }
        if (stateDic.ContainsKey(stateName)==false)
        {
            Debug.LogError("在状态机里面不存在该状态" + stateName + "，无法进行状态转换！");
            return;
        }
        currentFsmState.Exit();
        currentFsmState = stateDic[stateName];
        currentStateName = currentFsmState.stateName;
        currentFsmState.Enter();
    }
}

