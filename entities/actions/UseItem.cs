using Constants;
using Entities.Actions.Interfaces;

namespace Entities.Actions;

public class UseItem : ActionBase, IAction
{
    public string Name { get => ActionNames.USE_ITEM; }
}