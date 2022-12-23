using Constants;
using Entities.Base;
using Godot;
using System;

namespace Entities;

public partial class Enemy : Actor
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        base._Ready();
        AddToGroup(Groups.ENEMIES);
        ActorName = "Enemy";
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }
}
