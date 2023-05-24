using UnityEngine;

public enum StateList {

    Idle,
    Moving,

}

public enum ModeList {

    Walk,
    Run,
    Combat

}

public abstract class AbstractStateModeBehaviour<StateType, ModeType> : MonoBehaviour 
    where StateType : System.Enum 
    where ModeType : System.Enum {

    [SerializeField]
    public StateType State {

        get; protected set;
    
    }

    public ModeType Mode {

        get; protected set;
        
    }

    public delegate void StateChangeEvent( StateType oldState, StateType newState );
    public StateChangeEvent OnStateChange;

    public delegate void ModeChangeEvent( ModeType oldMode, ModeType newMode );
    public ModeChangeEvent OnModeChange;

    public virtual void ChangeState( StateType newState ) {

        State = newState;
        OnStateChange?.Invoke(State, newState);
    
    }

    public virtual void ChangeMode( ModeType newMode ) {

        Mode = newMode;
        OnModeChange?.Invoke( Mode, newMode );
    
    }

}
