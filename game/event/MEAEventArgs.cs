using System;
using game.control;
using game.dto;
using game.entity;

namespace game.@event
{
    /// <summary>
    /// 点击移动，攻击，发动效果的事件信息，包括点击的2个格子和怪兽
    /// </summary>
    
    public class MEAEventAgrs : EventArgs
    {
        public GameLabel LastGameLabel { get; set; }

        public GameLabel NowGameLabel { get; set; }

        public GameData Data { get; set; }

    }
}