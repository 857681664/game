using System;
using game.entity;

namespace game
{
    /// <summary>
    /// 怪兽信息类，包含怪兽信息和所属玩家信息
    /// </summary>
    public class MonsterEventArgs : EventArgs
    {
        public Const.PlayerBelongs Player { get; set; }

        public CardMonster Monster { get; set; }
        public MonsterEventArgs(Const.PlayerBelongs player, CardMonster monster)
        {
            Player = player;
            Monster = monster;
        }
    }
}