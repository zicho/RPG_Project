using System;
using System.Collections.Generic;
using System.Linq;
using Godot;
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

    public void SetState(T newState, string infoText, bool addToHistory = true)
    {
        CurrentState = newState;
        InfoText = infoText;

        if (addToHistory) History.Add(new StateHistory(newState, infoText));
        UiHandler.SetStateInfoText(_currentState.ToString());
    }

    public void SetState(StateHistory history, bool addToHistory = true)
    {
        SetState(
            history.State,
            history.InfoText,
            addToHistory: addToHistory);
    }

    public List<StateHistory> History { get; set; } = new List<StateHistory>();

    public T PrevState { get; set; }

    public FiniteStateMachine(T initialState, string infoText)
    {
        SetState(initialState, infoText);
    }

    public void GoBackToPrevState()
    {
        if(History.Count == 1) return;
        History.RemoveAt(History.Count - 1);
        SetState(History[^1], addToHistory: false);
    }

    public class StateHistory
    {
        public T State { get; set; }
        public string InfoText { get; set; }

        public StateHistory(T state, string infoText)
        {
            State = state;
            InfoText = infoText;
        }
    }
}
