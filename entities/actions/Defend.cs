using System;
using Constants;
using Entities.Actions.Interfaces;

namespace Entities.Actions;

public class Defend : IAction
{
    public string Name { get => ActionNames.DEFEND; }

    public ActionEnum Action => ActionEnum.DEFEND;
}