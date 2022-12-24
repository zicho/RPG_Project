using Entities.Actions;
using Entities.Actions.Interfaces;
using Godot;
using System;
using System.Collections.Generic;

namespace UI;

public partial class ActionList : PanelContainer
{
    public ItemList ItemList { get; private set; }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (FindChild("ItemList") is ItemList actionList)
        {
            ItemList = actionList;
        }
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(double delta)
    {
    }

    public void SetActions(List<IAction> actions)
    {
        ItemList.Clear();
		GD.Print(actions.Count);

        foreach (var action in actions)
        {
			GD.Print(action.Name);
            ItemList.AddItem(action.Name);
        }
    }
}
