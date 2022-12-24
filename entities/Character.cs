using System.Collections.Generic;
using Constants;
using Entities.Actions;
using Entities.Actions.Interfaces;
using Entities.Base;
using Godot;
using Helpers;

namespace Entities;

public partial class Character : CharacterBase
{
    public new List<IAction> Actions => new() {
        new Attack(),
        new Defend(),
        new UseItem(),
        new Escape(),
    };

    public override void _Ready()
	{
		base._Ready();
		ActorName = NameGenerator.GetRandomName();
		AddToGroup(Groups.CHARACTERS);
		GD.Print(Actions.Count);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
