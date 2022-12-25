using Entities;
using Godot;
using UI;

namespace Core;
public partial class CharacterHudHandler : VBoxContainer
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach(var n in GetChildren()) {
			RemoveChild(n);
			n.QueueFree();
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

    public void OnBattleStart(Character[] characters)
    {
        GD.Print("Go!!!");

		var characterHudScene = (PackedScene)ResourceLoader.Load("res://core/ui/character_hud.tscn");

		foreach(var character in characters) {
    		var hud = (CharacterHud)characterHudScene.Instantiate();
			hud.SizeFlagsHorizontal = (int)SizeFlags.ExpandFill;
			hud.SizeFlagsVertical= (int)SizeFlags.Expand;
    		AddChild(hud);
			hud.SetupHud(character);
		}
    }
}
