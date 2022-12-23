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

    public FiniteStateMachine<BattleStateEnum> BattleState { get; private set; }

    public enum BattleStateEnum
    {
        SelectCharacter,
        WaitingForCharacterAction
    }

    [Signal]
    public delegate void AddCharactersToHudEventHandler(Character[] characters);

    public override void _Ready()
    {
        if (FindChild("InfoLabel") is Label infoLabel)
        {
            UiHandler.InfoLabel = infoLabel;

            UiHandler.SetInfoText(InfoMessages.CHOOSE_CHARACTER);
        }

        var hud = GetNode<VBoxContainer>("Character_HUDS");

        BattleState = new FiniteStateMachine<BattleStateEnum>(BattleStateEnum.SelectCharacter, InfoMessages.CHOOSE_CHARACTER);

        Connect(nameof(AddCharactersToHud), new Callable(hud, nameof(ICharacterHUD.OnBattleStart)));
        var players = GetTree().GetNodesInGroup(Groups.CHARACTERS);
        EmitSignal(nameof(AddCharactersToHud), players);

        _marker = UiHandler.CreateMarker(GetTree().GetNodesInGroup(Groups.CHARACTERS), this);


    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (BattleState.CurrentState == BattleStateEnum.SelectCharacter)
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
                _marker.GetTargetInfo();

                BattleState.SetState(BattleStateEnum.WaitingForCharacterAction, InfoMessages.CHARACTER_ACTION_QUERY);
            }


        }

        // if (Input.IsActionJustPressed("ui_cancel"))
        // {
        //     BattleState.GoBackToPrevState();
        // }

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
