using Constants;
using Entities;
using Entities.Interfaces;
using Godot;
using Helpers;
using System;
using System.Linq;
using Tools;

namespace Core;
public partial class MainBattleHandler : Control
{
    // public BattleStateEnum BattleState { get; set; } = BattleStateEnum.SelectCharacter;

    private Marker _marker;
    private IActor _activeCharacter;
    private ItemList _actionList;

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

        if (FindChild("ActionList") is ItemList actionList)
        {
            _actionList = actionList;
        }

        BattleUiState = new FiniteStateMachine<BattleStateEnum>(BattleStateEnum.SelectCharacter, InfoMessages.CHOOSE_CHARACTER);

        Connect(nameof(AddCharactersToHud), new Callable(hud, nameof(ICharacterHUD.OnBattleStart)));
        var players = GetTree().GetNodesInGroup(Groups.CHARACTERS);
        EmitSignal(nameof(AddCharactersToHud), players);

        _marker = UiHandler.CreateMarker(GetTree().GetNodesInGroup(Groups.CHARACTERS), this);


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (BattleUiState.CurrentState == BattleStateEnum.SelectCharacter)
        {
            _marker.Visible = true;
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
                _activeCharacter = _marker.GetCharacter();
                BattleUiState.SetState(BattleStateEnum.WaitingForCharacterAction, string.Format(InfoMessages.CHARACTER_ACTION_QUERY, _activeCharacter.ActorName));
            }
        }
        else if (BattleUiState.CurrentState == BattleStateEnum.WaitingForCharacterAction) {
            _marker.Visible = false;
        }

        if (Input.IsActionJustPressed("ui_cancel"))
        {
            BattleUiState.GoBackToPrevState();
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
