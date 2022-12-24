using Constants;
using Entities.Actions;
using Entities.Actions.Interfaces;
using Entities.Interfaces;
using Godot;
using System;
using System.Collections.Generic;

namespace Entities.Base;

public abstract partial class ActorBase : Node2D, IActor
{
    public string ActorName { get; set; } = "actor_name_unset";
    public virtual List<IAction> Actions { get; set; } = new();

    public override void _Ready()
    {
        AddToGroup(Groups.ACTORS);
    }
}
