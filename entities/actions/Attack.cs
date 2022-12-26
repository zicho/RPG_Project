using System;
using Constants;
using Entities.Actions.Interfaces;

namespace Entities.Actions;

public class Attack : IAction
{
    public string Name => ActionNames.ATTACK;

    public ActionEnum Action => ActionEnum.ATTACK;

}