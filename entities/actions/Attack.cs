using Constants;
using Entities.Actions.Interfaces;

namespace Entities.Actions;

public class Attack : ActionBase, IAction
{
    public string Name { get => ActionNames.ATTACK; }
}