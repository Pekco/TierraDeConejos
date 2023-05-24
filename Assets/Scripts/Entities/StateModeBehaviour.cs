using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateModeBehaviour : AbstractStateModeBehaviour<StateList, ModeList> {

    private void Start( ) {

        ChangeState( StateList.Idle );
        ChangeMode( ModeList.Walk );

    }

}
