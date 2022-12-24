using Constants;
using Entities.Actions.Interfaces;

namespace Entities.Actions;

public class UseItem : IAction
{
    public string Name { get => ActionNames.USE_ITEM; }
}