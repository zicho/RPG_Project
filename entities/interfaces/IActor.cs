
using System.Collections.Generic;
using Entities.Actions.Interfaces;
using Godot;

namespace Entities.Interfaces;
public interface IActor
{
    string ActorName { get; set; }
    List<IAction> Actions { get; }
}