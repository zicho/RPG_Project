using Constants;
using Entities.Base;
using Entities.Interfaces;
using Helpers;

namespace Entities;

public partial class Character : CharacterBase
{
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
