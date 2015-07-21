using UnityEngine;
using System.Collections.Generic;

public class TurnState : IBattleState {

    // The state machine holding this state
    private BattleStateMachine bsm;

    // The combatants engaged in combat
    private List<GameObject> combatants;

    // The current decision made by an actor
    private IDecision decision;

    // The current actor holding the turn
    private GameObject actor;

    // Determines if we're waiting for an action to be decided
    private bool waitingForAction = false;

    public TurnState(BattleStateMachine battleStateMachine, List<GameObject> actors)
    {
        bsm = battleStateMachine;
        combatants = actors;
    }

    private T NextOf<T>(IList<T> list, T item)
    {
        return list[(list.IndexOf(item) + 1) == list.Count ? 0 : (list.IndexOf(item) + 1)];
    }

    public void Update()
    {
        if (!waitingForAction) 
        {
            // Determine who's turn it is
            actor = NextOf(combatants, actor);
            IHealth<int> actorHealth = actor.GetComponent<IHealth<int>>();

            // A dead actor can't make moves, so we'll simply return
            if (actorHealth.IsDead)
            {
                return;
            }
     
            if (actor.tag == "Player")
            {
                decision = new PlayerDecision(actor, combatants.FindAll(a => a.tag.Equals("Enemy")));
            }
            else
            {
                decision = new AIDecision(actor, combatants.FindAll(a => a.tag.Equals("Player")));
            }

            waitingForAction = true;
        }

        if (decision.IsReady)
        {
            bsm.Change(BattleState.Execute, decision.Action);
        }
    }

    public void OnEnter(IAction action = null)
    {
    }

    public void OnExit()
    {
        waitingForAction = false;
    }
}
