using Godot;
using Tools;

namespace Helpers;

public static class UiHandler
{
    public static Label InfoLabel { get; set; }
	public static Label StateInfoLabel { get; set; }
    public static Marker CreateMarker(Godot.Collections.Array<Node> targets, Node parent)
    {
        var markerScene = (PackedScene)ResourceLoader.Load("res://tools/marker.tscn");

        var marker = (Marker)markerScene.Instantiate();
        marker.Targets = targets;

        parent.AddChild(marker);

        return marker;
    }

    public static void SetInfoText(string text) => InfoLabel.Text = text;
    public static void SetStateInfoText(string text) => StateInfoLabel.Text = text;
}
