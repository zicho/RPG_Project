using System;
using System.Collections.Generic;
using System.Linq;
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

    public void SetState(T newState, string infoText)
    {
        CurrentState = newState;
        InfoText = infoText;
        History.Add(new StateHistory(newState, infoText));
    }

    public void SetState(StateHistory history)
    {
        CurrentState = history.State;
        InfoText = history.InfoText;
    }

    public List<StateHistory> History { get; set; }

    public T PrevState { get; set; }

    public FiniteStateMachine(T initialState, string infoText)
    {
        CurrentState = initialState;
        InfoText = infoText;
    }

    public void GoBackToPrevState() {
        SetState(History.First());
    }

    public class StateHistory
    {
        public T State { get; set; }
        public string InfoText { get; set; }

        public StateHistory(T state, string infoText) {
            State = state;
            InfoText = infoText;
        }
    }
}