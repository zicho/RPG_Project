using Constants;
using Entities.Interfaces;
using Godot;
using System;
using System.Linq;

namespace Entities.Base;

public abstract partial class CharacterBase : ActorBase, ICharacter
{
    public override void _Ready()
    {
        base._Ready();
        AddToGroup(Groups.CHARACTERS);
    }
}
