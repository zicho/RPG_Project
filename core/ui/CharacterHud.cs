using Entities;
using Entities.Interfaces;
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
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    // public override void _Process(double delta)
    // {
    //     if (HasCurrentSelection)
    //     {
    // 		SizeFlagsHorizontal = (int)SizeFlags.Fill;
    //     }
    //     else
    //     {
    // 		SizeFlagsHorizontal = (int)SizeFlags.ShrinkEnd;
    //     }
    // }

    public void OnActiveCharacterChanged(object sender, IActor character)
    {
        if (character != Character)
        {
            SizeFlagsHorizontal = (int)SizeFlags.ShrinkEnd;
        }
        else
        {
            SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
            GD.Print("Active character is: " + Character.ActorName);
            HasCurrentSelection = character == Character;
        }
    }

    public void SetupHud(Character character)
    {
        Character = character;

        if (FindChild("CharacterName") is Label nameLabel)
        {
            nameLabel.Text = character.ActorName;
        }
    }
}
