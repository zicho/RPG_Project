using System;
using System.Collections.Generic;
using Godot;

namespace Helpers;

public static class NameGenerator
{
    private static readonly List<string> _names = new() {
        "Lozo",
        "Susattos",
        "Emna",
        "Iarnays",
        "Rundun",
        "Fianmas",
        "Oca",
        "Xarthul",
        "Argol",
        "Ergard",
        "Jevalek",
        "Urnin",
        "Lorgil",
        "Xisrand"
    };

    public static string GetRandomName()
    {
        var random = new Random();
        int index = random.Next(_names.Count);
        return _names[index];
    }
}