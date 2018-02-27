using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//保存状态机中可存在的状态
public enum StateName
{
    NullStateName,
    Patrol,
    Trace,
    Attack,
    Death
}

//状态中可存在的转换条件
public enum Transition
{
    NullTransition,
    SeePlayer,//patrol->trace
    LostPlayer,//trace->patrol
    NearPlayer,//trace->Attack
    HpZero,//any->death
}

