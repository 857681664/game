using System;
using game.entity;
using game.@event;

namespace game.dto
{
    /// <summary>
    /// 消息监听的发送事件格式
    /// </summary>
    [Serializable]
    public class CustomListenDto
    {
        public string PlayerOne { get; set; }

        public string PlayerTwo { get; set; }

        public string MonsterOne { get; set; }

        public string MonsterTwo { get; set; }
        
        public string EventKind { get; set; }

        public int Attack { get; set; }

    }
}