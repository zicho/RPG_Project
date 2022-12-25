using Entities;
using Godot;
using System;

namespace UI;
public partial class CharacterHud : PanelContainer
{
	// Called when the node enters the scene tree for the first time.
	public bool HasCurrentSelection { get; set; }
    public Character Character { get; private set; }

    private Vector2 _origPos;
	public override void _Ready()
	{
		_origPos = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(HasCurrentSelection) {
			Position = new Vector2(_origPos.x - 200, _origPos.y);
		} else {
			Position = _origPos;
		}
	}

	public void SetupHud(Character character) {
		Character = character;

        if (FindChild("CharacterName") is Label nameLabel) {
			nameLabel.Text = character.ActorName;
		}
	}
}
