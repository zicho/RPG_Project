using Entities;
using Godot;

namespace Core
{
    internal interface ICharacterHUD
    {
        public void OnBattleStart(Character[] players);
        public void AddCharacter(Character character);
    }
}