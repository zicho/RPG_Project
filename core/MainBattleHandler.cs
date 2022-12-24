using Constants;
using Entities;
using Entities.Base;
using Entities.Interfaces;
using Godot;
using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools;
using UI;

namespace Core;
public partial class MainBattleHandler : Control
{
    // public BattleStateEnum BattleState { get; set; } = BattleStateEnum.SelectCharacter;

    private Marker _marker;
    private IActor _activeCharacter;
    private ActionList _actionList;

    public FiniteStateMachine<BattleStateEnum> BattleUiState { get; private set; }

    public enum BattleStateEnum
    {
        SelectCharacter,
        WaitingForCharacterAction
    }

    [Signal]
    public delegate void AddCharactersToHudEventHandler(Character[] characters);

    public override void _Ready()
    {
        var hud = GetNode<VBoxContainer>("Character_HUDS");

        if (FindChild("InfoLabel") is Label infoLabel)
        {
            UiHandler.InfoLabel = infoLabel;

            UiHandler.SetInfoText(InfoMessages.CHOOSE_CHARACTER);
        }

        if (FindChild("StateInfoLabel") is Label stateInfoLabel)
        {
            UiHandler.StateInfoLabel = stateInfoLabel;
            UiHandler.SetStateInfoText(nameof(BattleStateEnum.SelectCharacter));
        }

        if (FindChild("ActionList") is ActionList actionList)
        {
            _actionList = actionList;
            _actionList.Visible = false;
        }

        BattleUiState = new FiniteStateMachine<BattleStateEnum>(BattleStateEnum.SelectCharacter, InfoMessages.CHOOSE_CHARACTER);

        Connect(nameof(AddCharactersToHud), new Callable(hud, nameof(ICharacterHUD.OnBattleStart)));
        var players = GetTree().GetNodesInGroup(Groups.CHARACTERS);
        EmitSignal(nameof(AddCharactersToHud), players);

        var selectableActors = new List<IActor>();

        foreach (var c in GetTree().GetNodesInGroup(Groups.CHARACTERS))
        {
            var actor = c as IActor;
            GD.Print("Main: " + actor.Actions.Count);
            selectableActors.Add(actor);
        }

        _marker = UiHandler.CreateMarker(selectableActors, this);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (BattleUiState.CurrentState == BattleStateEnum.SelectCharacter)
        {
            if (Input.IsActionJustPressed("ui_left"))
            {
                _marker.NextTarget();
            }
            else if (Input.IsActionJustPressed("ui_right"))
            {
                _marker.PrevTarget();
            }

            if (Input.IsActionJustPressed("ui_accept"))
            {
                _activeCharacter = _marker.GetActor();
                BattleUiState.SetState(BattleStateEnum.WaitingForCharacterAction, string.Format(InfoMessages.CHARACTER_ACTION_QUERY, _activeCharacter.ActorName));
                _actionList.Visible = true;
                _actionList.SetActions(_activeCharacter.Actions);
            }
        }
        else if (BattleUiState.CurrentState == BattleStateEnum.WaitingForCharacterAction)
        {
            if (Input.IsActionJustPressed("ui_up"))
            {
                _actionList.PrevItem();
            }
            else if (Input.IsActionJustPressed("ui_down"))
            {
                _actionList.NextItem();
            }
            else if (Input.IsActionJustPressed("ui_accept"))
            {
                // _actionList.ChooseAction();
                UiHandler.CreateMarker();
            }
        }

        if (Input.IsActionJustPressed("ui_cancel"))
        {
            BattleUiState.GoBackToPrevState();
            _actionList.Visible = false;
        }

        // if (Input.IsActionJustPressed("ui_cancel"))
        // {
        //     if (BattleState == BattleStateEnum.Selecting)
        //     {
        //         BattleState = BattleStateEnum.Waiting;
        //         RemoveChild(_marker);
        //     }
        //     else
        //     {
        //         GetTree().Quit();
        //     }
        // }
    }
}
