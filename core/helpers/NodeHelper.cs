using System.Collections.Generic;
using Godot;

namespace Helpers;

public static class NodeHelper<T> where class {
    public static List<T> NodesToType(Godot.Collections.Array<Node> nodes) {
        
        var typesToReturn = new List<T>();
        
        foreach(var n in nodes) {
            typesToReturn.Add((T)n);
        }        
    }
}