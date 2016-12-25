using System;
using game.entity;
using game.@event;

namespace game.dto
{
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