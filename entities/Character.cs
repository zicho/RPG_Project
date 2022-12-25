using System.Collections.Generic;
using Constants;
using Entities.Actions;
using Entities.Actions.Interfaces;
using Entities.Base;
using Entities.Interfaces;
using Godot;
using Helpers;

namespace Entities;

public partial class Character : ActorBase, ICharacter
{
	public override List<IAction> Actions { get; set; } = new() {
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
		GD.Print(ActorName + " has " + Actions.Count + " actions!");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
