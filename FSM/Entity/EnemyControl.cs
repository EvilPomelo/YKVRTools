using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour {
    public Transform player;
    public FsmSystem enemyFsm;
    // Use this for initialization
    void Start () {
        enemyFsm = new FsmSystem();
        FsmStateBase patrol = new PatrolState(player,transform,enemyFsm);
        FsmStateBase trace =new TraceState(player, transform, enemyFsm);
        FsmStateBase attack = new AttackState(player, transform, enemyFsm);
        enemyFsm.AddState(patrol);
        enemyFsm.AddState(trace);
        enemyFsm.AddState(attack);
    }
	
	// Update is called once per frame
	void Update () {
        enemyFsm.Update();
	}
}
