using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using game.control;
using game.entity;
using game.@event;
using game.monster.fourstar;
using game.monster.threestar;
using game.monster.twostar;

namespace game.dto
{
    /// <summary>
    /// 游戏数据类，包括卡片的集合，玩家的信息
    /// </summary>
    public class GameData
    {

        public GameLabel[,] GameLabels { get; set; } //游戏的格子

        public Dictionary<Player, Const.PlayerBelongs> PlayerDictionary { get; set; }

        public Dictionary<Const.PlayerBelongs,Player> BelongDictionary { get; set; } 

        public Dictionary<Player,List<Image>> ImageDictionary { get; set; } 

        public List<CardMonster> TwoStarMonsters { get; set; }

        public List<CardMonster> ThreeStarMonsters { get; set; }

        public List<CardMonster> FourStarMonsters { get; set; }

        public Player PlayerOne { get; set; }

        public Player PlayerTwo { get; set; }

        public GameData()
        {
            TwoStarMonsters = new List<CardMonster>();
            ThreeStarMonsters = new List<CardMonster>();
            FourStarMonsters = new List<CardMonster>();
            PlayerDictionary = new Dictionary<Player, Const.PlayerBelongs>();
            BelongDictionary = new Dictionary<Const.PlayerBelongs, Player>();
            ImageDictionary = new Dictionary<Player, List<Image>>();
            PlayerOne = new Player();
            PlayerTwo = new Player();
            PlayerOne.HisTurn = true;
            PlayerTwo.HisTurn = false;
            PlayerDictionary.Add(PlayerOne,Const.PlayerBelongs.PlayerOne);
            PlayerDictionary.Add(PlayerTwo,Const.PlayerBelongs.PlayerTwo);
            BelongDictionary.Add(Const.PlayerBelongs.PlayerOne,PlayerOne);
            BelongDictionary.Add(Const.PlayerBelongs.PlayerTwo,PlayerTwo);
            ImageDictionary.Add(PlayerOne,Const.BlueImages);
            ImageDictionary.Add(PlayerTwo,Const.RedImages);
            InitCards();
        }

        public void InitCards()
        {
            TwoStarMonsters = InitGameMonster.AddTwoStarMonsters();
            ThreeStarMonsters = InitGameMonster.AddThreeStarMonsters();
            FourStarMonsters = InitGameMonster.AddfourStarMonsters();
        }

        
    }
}
