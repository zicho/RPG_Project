using Constants;
using Entities.Actions.Interfaces;

namespace Entities.Actions;

public class Defend : IAction
{
    public string Name { get => ActionNames.DEFEND; }
}