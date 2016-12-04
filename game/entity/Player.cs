using System.Collections.Generic;
using System.Windows.Forms;

namespace game.entity
{
    /// <summary>
    /// 玩家类
    /// </summary>
    public class Player
    {
        public int LifePoint { get; set; }

        public bool HisTurn { get; set; }

        public int MagicNumber { get; set; }

        public int TrapNumber { get; set; }

        public int MoveNumber { get; set; }

        public LinkedList<Label> LabelLinkedList { get; set; }

        public LinkedList<CardMonster> CardLinkedList { get; set; }

        public LinkedList<PictureBox> PictureBoxlList { get; set; }

        public LinkedList<CardMonster> DeathMonsters { get; set; } 

        public Player()
        {
            MagicNumber = 0;
            TrapNumber = 0;
            MoveNumber = 0;
            LifePoint = 4000;
            CardLinkedList = new LinkedList<CardMonster>();
            LabelLinkedList = new LinkedList<Label>();
            PictureBoxlList = new LinkedList<PictureBox>();
            DeathMonsters = new LinkedList<CardMonster>();
        }
    }
}
