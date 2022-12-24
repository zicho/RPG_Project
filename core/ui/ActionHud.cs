using Entities.Actions;
using Entities.Actions.Interfaces;
using Godot;
using System;
using System.Collections.Generic;

namespace UI;

public partial class ActionHud : PanelContainer
{
    public ItemList ActionList { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (FindChild("ActionList") is ItemList actionList)
        {
            ActionList = actionList;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void SetActions(List<IAction> actions)
    {
        ActionList.Clear();

        foreach (var action in actions)
        {
            ActionList.AddItem(action.Name);
        }
    }
}
