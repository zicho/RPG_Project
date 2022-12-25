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

    public event EventHandler<IActor> ActiveCharacterChangedEventHandler;

    public FiniteStateMachine<BattleStateEnum> BattleUiState { get; private set; }

    public enum BattleStateEnum
    {
        SelectCharacter,
        WaitingForCharacterAction,
        ChooseAttackTarget
    }

    [Signal]
    public delegate void AddCharactersToHudEventHandler(Character[] characters);

    public override void _Ready()
    {
        var hudContainer = GetNode<VBoxContainer>("Character_HUDS");

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

        BattleUiState.StateChangedEventHandler += OnBattleStateChanged;

        Connect(nameof(AddCharactersToHud), new Callable(hudContainer, nameof(CharacterHudHandler.OnBattleStart)));
        var players = GetTree().GetNodesInGroup(Groups.CHARACTERS);
        EmitSignal(nameof(AddCharactersToHud), players);

        foreach (var charHud in hudContainer.GetChildren().Cast<CharacterHud>())
        {
            ActiveCharacterChangedEventHandler += charHud.OnActiveCharacterChanged;
        }

        _marker = UiHandler.CreateMarker(GetTree().GetNodesInGroup(Groups.CHARACTERS), this);
        ActiveCharacterChangedEventHandler.Invoke(this, _marker.GetActor());
    }

    private void OnBattleStateChanged(object sender, BattleStateEnum e)
    {
        if (e == BattleStateEnum.SelectCharacter)
        {
            _marker = UiHandler.CreateMarker(GetTree().GetNodesInGroup(Groups.CHARACTERS), this);
            ActiveCharacterChangedEventHandler.Invoke(this, _marker.GetActor());
            GD.Print("Choose character phase");
            _actionList.Visible = false;
        }
        else if (e == BattleStateEnum.WaitingForCharacterAction)
        {
            GD.Print("Choose action phase");
            _actionList.Visible = true;
            _marker.Visible = false;
            _actionList.SetActions(_activeCharacter.Actions);
        }
        else if (e == BattleStateEnum.ChooseAttackTarget)
        {
            _actionList.Visible = false;
            GD.Print("Choose enemy phase");
            _marker = UiHandler.CreateMarker(GetTree().GetNodesInGroup(Groups.ENEMIES), this);
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (BattleUiState.CurrentState == BattleStateEnum.SelectCharacter)
        {
            if (Input.IsActionJustPressed("ui_right"))
            {
                _marker.NextTarget();
                GD.Print("Changing character!");
                ActiveCharacterChangedEventHandler.Invoke(this, _marker.GetActor());
            }
            else if (Input.IsActionJustPressed("ui_left"))
            {
                _marker.PrevTarget();
                GD.Print("Changing character!");
                ActiveCharacterChangedEventHandler.Invoke(this, _marker.GetActor());
            }

            if (Input.IsActionJustPressed("ui_accept"))
            {
                _activeCharacter = _marker.GetActor();
                BattleUiState.SetState(BattleStateEnum.WaitingForCharacterAction, string.Format(InfoMessages.CHARACTER_ACTION_QUERY, _activeCharacter.ActorName));
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
                BattleUiState.SetState(BattleStateEnum.ChooseAttackTarget, InfoMessages.CHOOSE_ATTACK_TARGET);
            }
        }

        else if (BattleUiState.CurrentState == BattleStateEnum.ChooseAttackTarget)
        {
            if (Input.IsActionJustPressed("ui_right"))
            {
                _marker.NextTarget();
            }
            else if (Input.IsActionJustPressed("ui_left"))
            {
                _marker.PrevTarget();
            }
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
