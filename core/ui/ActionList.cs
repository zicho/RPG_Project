using Entities.Actions;
using Entities.Actions.Interfaces;
using Godot;
using System;
using System.Collections.Generic;

namespace UI;

public partial class ActionList : PanelContainer
{
    private int _selectedIndex;

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

        foreach (var action in actions)
        {
            ItemList.AddItem(action.Name);
        }

        _selectedIndex = 0;

        Focus();
    }

    public void Focus()
    {
        ItemList.Select(0);
    }

    public void NextItem()
    {
        _selectedIndex++;
        if (_selectedIndex > ItemList.ItemCount - 1)
        {
            _selectedIndex = 0;
        }

        ItemList.Select(_selectedIndex);
    }

    public void PrevItem()
    {
        _selectedIndex--;
        if (_selectedIndex < 0)
        {
            _selectedIndex = ItemList.ItemCount - 1;
        }

        ItemList.Select(_selectedIndex);
    }
}
