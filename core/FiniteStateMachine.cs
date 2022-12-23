using System;
using Helpers;

namespace Core;

public class FiniteStateMachine<T> where T : Enum
{
    private T _currentState;
    private string _infoText;

    public T CurrentState
    {
        get => _currentState; private set
        {
            PrevState = _currentState;
            _currentState = value;
        }
    }

    public string InfoText
    {
        get => _infoText; set
        {
            _infoText = value;
            UiHandler.SetInfoText(value);
        }
    }

    public void SetState(T newState, string infoText) {
        CurrentState = newState;
        InfoText = infoText;
    }

    public T PrevState { get; set; }

    public FiniteStateMachine(T initialState, string infoText)
    {
        CurrentState = initialState;
        InfoText = infoText;
    }

    public class StateHistory
    {
        public T State { get; set; }
        public string InfoText { get; set; }
    }
}