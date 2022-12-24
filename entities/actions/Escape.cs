using Constants;
using Entities.Actions.Interfaces;

namespace Entities.Actions;

public class Escape : IAction
{
    public string Name { get => ActionNames.ESCAPE; }
}