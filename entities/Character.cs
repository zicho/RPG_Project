using System.Collections.Generic;
using Constants;
using Entities.Actions;
using Entities.Base;
using Helpers;

namespace Entities;

public partial class Character : CharacterBase
{
	public List<IAction> Actions = new() {
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
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
