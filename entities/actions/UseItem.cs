using System;
using Constants;
using Entities.Actions.Interfaces;

namespace Entities.Actions;

public class UseItem : IAction
{
    public string Name { get => ActionNames.USE_ITEM; }

    public ActionEnum Action => ActionEnum.USE_ITEM;
}