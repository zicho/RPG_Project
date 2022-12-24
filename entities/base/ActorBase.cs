using Constants;
using Entities.Actions;
using Entities.Actions.Interfaces;
using Entities.Interfaces;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities.Base;

public abstract partial class ActorBase : Node2D, IActor
{
    public string ActorName { get; set; } = "actor_name_unset";

    public List<IAction> Actions => new() {
        new Attack(),
        new Defend(),
        new UseItem(),
        new Escape(),
    };

    public override void _Ready()
    {
        AddToGroup(Groups.ACTORS);
    }
}
