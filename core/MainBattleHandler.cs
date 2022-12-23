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
    public BattleStateEnum BattleState { get; set; } = BattleStateEnum.Waiting;

    private Marker _marker;

    public enum BattleStateEnum
    {
        Waiting,
        Selecting
    }

    [Signal]
    public delegate void AddCharactersToHudEventHandler(Character[] characters);

    public override void _Ready()
    {
        var hud = GetNode<VBoxContainer>("Character_HUDS");
        Connect(nameof(AddCharactersToHud), new Callable(hud, nameof(ICharacterHUD.OnBattleStart)));
        var players = GetTree().GetNodesInGroup(Groups.CHARACTERS);
        EmitSignal(nameof(AddCharactersToHud), players);
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed("ui_accept"))
        {
            if (BattleState == BattleStateEnum.Waiting)
            {
                BattleState = BattleStateEnum.Selecting;
                _marker = ToolHandler.CreateMarker(GetTree().GetNodesInGroup(Groups.ENEMIES));
                AddChild(_marker);
            }
        }

        if (Input.IsActionJustPressed("ui_cancel"))
        {
            if (BattleState == BattleStateEnum.Selecting)
            {
                BattleState = BattleStateEnum.Waiting;
                RemoveChild(_marker);
            }
        }
    }
}
