using System;

namespace Entities.Actions.Interfaces;

public interface IAction
{
    public string Name { get; }
    public ActionEnum Action { get; }
}