using Constants;
using Entities.Interfaces;
using Godot;
using Helpers;
using System;

namespace Tools;
public partial class Marker : Node2D
{
    // Called when the node enters the scene tree for the first time.

    public Godot.Collections.Array<Node> Targets { get; set; }
    private int _targetIndex = 0;

    public override void _Ready()
    {
        MarkTarget(Targets[_targetIndex]);
        if (FindChild("Anim") is AnimationPlayer animPlayer)
        {
            animPlayer.Play("bounce");
        }
    }

    private void MarkTarget(Node node)
    {
        var target = node as Node2D;

        var targetPos = (target).Position;
        targetPos.y -= 100;

        Position = targetPos;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {

    }

    public void NextTarget()
    {
        _targetIndex++;
        if (_targetIndex > Targets.Count - 1)
        {
            _targetIndex = 0;
        }
        MarkTarget(Targets[_targetIndex]);
    }

    public void PrevTarget()
    {
        _targetIndex--;
        if (_targetIndex < 0)
        {
            _targetIndex = Targets.Count - 1;
        }

        MarkTarget(Targets[_targetIndex]);
    }

    public IActor GetActor() => Targets[_targetIndex] as IActor;
	public ICharacter GetCharacter() => Targets[_targetIndex] as ICharacter;
}
