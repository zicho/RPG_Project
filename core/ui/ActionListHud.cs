using Entities.Actions;
using Entities.Actions.Interfaces;
using Godot;
using System;
using System.Collections.Generic;

namespace UI;

public partial class ActionListHud : PanelContainer
{
	private int _selectedIndex;

	public ItemList ListHud { get; private set; }
 
	public List<IAction> Actions { get; } = new List<IAction>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		if (FindChild("ItemList") is ItemList listHud)
		{
			ListHud = listHud;
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetActions(List<IAction> actions)
	{
		ListHud.Clear();

		foreach (var action in actions)
		{
			ListHud.AddItem(action.Name);
			Actions.Add(action);
		}

		_selectedIndex = 0;

		Focus();
	}

	public ActionEnum? SelectedAction => Actions[_selectedIndex].Action;

	public void Focus()
	{
		ListHud.Select(0);
	}

	public void NextItem()
	{
		_selectedIndex++;
		if (_selectedIndex > ListHud.ItemCount - 1)
		{
			_selectedIndex = 0;
		}

		ListHud.Select(_selectedIndex);
	}

	public void PrevItem()
	{
		_selectedIndex--;
		if (_selectedIndex < 0)
		{
			_selectedIndex = ListHud.ItemCount - 1;
		}

		ListHud.Select(_selectedIndex);
	}
}
