using Constants;
using Entities.Interfaces;
using Godot;
using System;
using System.Linq;

namespace Entities.Base;

public abstract partial class Actor : Node2D, IActor
{
    public string ActorName { get; set; } = "actor_name_unset";

    public override void _Ready()
    {
        AddToGroup(Groups.ACTORS);
    }
}
