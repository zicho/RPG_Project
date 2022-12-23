using Godot;
using Tools;

namespace Helpers;

public static class ToolHandler
{
    public static Marker CreateMarker(Godot.Collections.Array<Node> targets)
    {
        var markerScene = (PackedScene)ResourceLoader.Load("res://tools/marker.tscn");

        var marker = (Marker)markerScene.Instantiate();
        marker.Targets = targets;

        return marker;
    }
}