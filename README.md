# SOStateMachine
State Machine with ScriptableObjects

This is one of two projects of mine that are "scriptable object state machines".

The other is [Simple SO State Machine](https://github.com/hunterdyar/Simple-Scriptable-Object-State-Machine).

## Difference
This one has conditions & transitions. You can setup how the machine flows between states.

That one is simpler, you don't set up any transtions or anything, the state-flow has to be controlled elsewhere (it uses a 'stack' of states, and can return to previous).
That one uses sub-assets for the states, which makes project management and organization easier.

Neither support monobehaviour instancing, and are intended for more 'global' asset-level state systems. 
