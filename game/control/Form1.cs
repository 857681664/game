using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using game.control;
using game.dto;
using game.entity;
using game.@event;
using game.monster.threestar;

namespace game
{
    
    public partial class Form1 : Form
    {
        
        private int skyKnightCount = 3;//天空骑士效果计数器
        private Const.PlayerBelongs playerBelongs;
        private MonsterEventArgs monsterEventArgs;//怪兽信息
        private LeftClickEventArgs leftClickEventArgs;//左键点击信息
        private MEAEventAgrs meaEventAgrs;
        private GameLabel[,] gameLabels;//游戏的格子
        private Random random;
        private GameData gameData;
        public Form1()
        {
            InitializeComponent();
            InitGameParam();
            InitGameLabel();
        }
        /// <summary>
        /// 初始化游戏的地图格子,i是列，j是行
        /// </summary>
        public void InitGameLabel()
        {
            gameLabels = new GameLabel[Const.LABEL_ROW, Const.LABEL_COL];
            for (int i = 0; i < Const.LABEL_ROW; i++)
            {
                for (int j = 0; j < Const.LABEL_COL; j++)
                {
                    gameLabels[i, j] = new GameLabel(i * (Const.LABEL_WIDTH + 2), j * (Const.LABEL_WIDTH + 2), Const.LABEL_WIDTH - 1, Const.LABEL_WIDTH - 1, i, j)
                    {
                        Visible = true
                    };
                    //给格子注册事件
                    gameLabels[i, j].ShowMonsterEvent += ShowMonster;
                    gameLabels[i, j].CallMonsterEvent += CallMonster;
                    gameLabels[i, j].MoveMonsterEvent += MoveMonster;
                    gameLabels[i, j].AttackMonsterEvent += AttackMonster;
                    gameLabels[i, j].UserEffectEvent += UserEffect;
                    gameLabels[i, j].MainPanelRefreshEvent += MainPanelRefresh;
                    gameLabels[i, j].LeftClickEventArgs = leftClickEventArgs;
                    gameLabels[i,j].SetMEAEventArgs(meaEventAgrs);
                    mainPanel.Controls.Add(gameLabels[i, j]);
                }
            }
            gameLabels[5, 0].BackColor = Color.Red;
            gameLabels[5, 0].Belongs = Const.PlayerBelongs.PlayerTwo;
            gameLabels[5, 9].BackColor = Color.Blue;
            gameLabels[5, 9].Belongs = Const.PlayerBelongs.PlayerOne;
            gameData.GameLabels = gameLabels;
        }
        /// <summary>
        /// 当怪兽执行攻击的时候，显示怪兽攻击范围
        /// </summary>
        private void MainPanelRefresh()
        {
            mainPanel.Refresh();
        }

        /// <summary>
        /// 初始化游戏开始的参数
        /// </summary>
        public void InitGameParam()
        {
            gameData = new GameData();
            random = new Random();
            leftClickEventArgs = new LeftClickEventArgs(Const.LeftClickEnum.None);
            meaEventAgrs = new MEAEventAgrs {Data = gameData};
            foreach (Control c in Controls)
            {
                if(c.GetType() == typeof(Label))
                {
                    if (c.Name.EndsWith("1"))
                    {
                        gameData.PlayerOne.LabelLinkedList.AddLast((Label) c);
//                        c.Text = "0";
                    }
                    else if (c.Name.EndsWith("2"))
                    {
                        gameData.PlayerTwo.LabelLinkedList.AddLast((Label) c);
//                        c.Text = "0";
                    }
                }
                if (c.GetType() == typeof(PictureBox))
                {
                    if (c.Name.EndsWith("One"))
                        gameData.PlayerOne.PictureBoxlList.AddLast((PictureBox) c);
                    else if(c.Name.EndsWith("Two"))
                        gameData.PlayerTwo.PictureBoxlList.AddLast((PictureBox) c);
                }
            }
        }
        /// <summary>
        /// 开始按钮，点击之后开始回合，且按钮不可点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void buttonStart_Click(object sender, EventArgs eventArgs)
        {
            if (gameData.PlayerOne.HisTurn)
            {
                playerBelongs = Const.PlayerBelongs.PlayerOne;
                TurnStart(gameData.PlayerOne);
            }
            else if (gameData.PlayerTwo.HisTurn)
            {
                playerBelongs = Const.PlayerBelongs.PlayerTwo;
                TurnStart(gameData.PlayerTwo);
            }
            buttonStart.Enabled = false;
            buttonEnd.Enabled = true;

        }
        /// <summary>
        /// 每个玩家的游戏总阶段，包括掷骰子，召唤怪兽，主要阶段
        /// </summary>
        /// <param name="player"></param>
        public void TurnStart(Player player)
        {
            //回合开始时重置怪兽的指令
            foreach (CardMonster card in player.CardLinkedList)
            {
                card.CanAttack = true;
                card.CanMove = true;
                card.CanEffective = true;
            }
            foreach (GameLabel label in gameLabels)
            {
                if(label.HasMonster)
                    label.Refresh();
            }
            WaveDice(player);
            
        }
        
        /// <summary>
        /// 移动怪兽，判断是否可以移动，再移动
        /// </summary>
        /// <param name="e"></param>
        public void MoveMonster(MEAEventAgrs e)
        {
            int moveSum;
            e.NowGameLabel.Monster = e.LastGameLabel.Monster;
            e.NowGameLabel.HasMonster = true;
            e.LastGameLabel.HasMonster = false;
            e.LastGameLabel.Monster.IsMove = false;
            e.LastGameLabel.Monster = null;
            //moveSum = CalMoveSum(e);
            //moveSumLabel.Text = moveSum.ToString();
            e.LastGameLabel.Refresh();
            e.NowGameLabel.Refresh();
            MainPanelRefresh();
        }
        /// <summary>
        /// 计算怪兽移动所需要的步数
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int CalMoveSum(MEAEventAgrs e)
        {
            int[,] maxtra = new int[Const.LABEL_ROW,Const.LABEL_COL];
            for (int i = 0; i < Const.LABEL_ROW; i++)
            {
                for (int j = 0; j < Const.LABEL_COL;j++)
                {
                    
                }
            }
            return 0;
        }
        /// <summary>
        /// 怪兽攻击
        /// </summary>
        /// <param name="e"></param>
        public void AttackMonster(MEAEventAgrs e)
        {
            if (e.LastGameLabel.Monster.Belongs == Const.PlayerBelongs.PlayerOne)
            {
                if (e.LastGameLabel.Monster.Attack > e.NowGameLabel.Monster.Attack)
                {
                    gameData.PlayerTwo.DeathMonsters.AddLast(e.NowGameLabel.Monster);
                    gameData.PlayerTwo.CardLinkedList.Remove(e.NowGameLabel.Monster);
                    e.NowGameLabel.Monster = null;
                    e.NowGameLabel.HasMonster = false;
                    e.NowGameLabel.Refresh();
                }
                else if (e.LastGameLabel.Monster.Attack < e.NowGameLabel.Monster.Attack)
                {
                    gameData.PlayerOne.DeathMonsters.AddLast(e.LastGameLabel.Monster);
                    gameData.PlayerOne.CardLinkedList.Remove(e.LastGameLabel.Monster);
                    e.LastGameLabel.Monster = null;
                    e.LastGameLabel.HasMonster = false;
                    e.LastGameLabel.Refresh();
                }
                else
                {
                    gameData.PlayerOne.DeathMonsters.AddLast(e.NowGameLabel.Monster);
                    gameData.PlayerTwo.DeathMonsters.AddLast(e.LastGameLabel.Monster);
                    gameData.PlayerTwo.CardLinkedList.Remove(e.NowGameLabel.Monster);
                    gameData.PlayerOne.CardLinkedList.Remove(e.LastGameLabel.Monster);
                    e.LastGameLabel.Monster = null;
                    e.NowGameLabel.Monster = null;
                    e.LastGameLabel.HasMonster = false;
                    e.NowGameLabel.HasMonster = false;
                    e.LastGameLabel.Refresh();
                    e.NowGameLabel.Refresh();
                }
                e.LastGameLabel.Monster.IsAttack = false;
                MainPanelRefresh();
            }
            else if (e.LastGameLabel.Monster.Belongs == Const.PlayerBelongs.PlayerTwo)
            {
                if (e.LastGameLabel.Monster.Attack > e.NowGameLabel.Monster.Attack)
                {
                    gameData.PlayerOne.DeathMonsters.AddLast(e.NowGameLabel.Monster);
                    gameData.PlayerOne.CardLinkedList.Remove(e.NowGameLabel.Monster);
                    e.NowGameLabel.Monster = null;
                    e.NowGameLabel.HasMonster = false;
                    e.NowGameLabel.Refresh();
                }
                else if (e.LastGameLabel.Monster.Attack < e.NowGameLabel.Monster.Attack)
                {
                    gameData.PlayerTwo.DeathMonsters.AddLast(e.LastGameLabel.Monster);
                    gameData.PlayerTwo.CardLinkedList.Remove(e.LastGameLabel.Monster);
                    e.NowGameLabel.Monster = null;
                    e.NowGameLabel.HasMonster = false;
                    e.NowGameLabel.Refresh();
                }
                else
                {
                    gameData.PlayerTwo.DeathMonsters.AddLast(e.NowGameLabel.Monster);
                    gameData.PlayerOne.DeathMonsters.AddLast(e.LastGameLabel.Monster);
                    gameData.PlayerTwo.CardLinkedList.Remove(e.NowGameLabel.Monster);
                    gameData.PlayerOne.CardLinkedList.Remove(e.LastGameLabel.Monster);
                    e.LastGameLabel.Monster = null;
                    e.NowGameLabel.Monster = null;
                    e.LastGameLabel.HasMonster = false;
                    e.NowGameLabel.HasMonster = false;
                    e.LastGameLabel.Refresh();
                    e.NowGameLabel.Refresh();
                }
                e.LastGameLabel.Monster.IsAttack = false;
                MainPanelRefresh();
            }
            
        }
        /// <summary>
        /// 发动怪兽效果，完成后刷新怪兽信息和印章数量
        /// </summary>
        /// <param name="e"></param>
        public bool UserEffect(MEAEventAgrs e)
        {
//            bool IsEffect;
            Type t = e.LastGameLabel.Monster.GetType();
            object[] objects = { e };
            t.GetMethod("UserEffect").Invoke(e.LastGameLabel.Monster, objects);
//            Player player = gameData.BelongDictionary[e.LastGameLabel.Belongs];
            monsterTextBox.Text = e.NowGameLabel.Monster.ToString();
            e.LastGameLabel.LeftClickEventArgs.LeftClick = Const.LeftClickEnum.None;
            //            //如果发动失败左键类型不变
            //            if (!IsEffect)
            //                e.LastGameLabel.LeftClickEventArgs.LeftClick = Const.LeftClickEnum.SelectMonster;
            //            //发动成功
            //            else
            //            {
            //                if (t == typeof (YeQiPeng))
//            monsterTextBox.Text = e.NowGameLabel.Monster.ToString();
//                e.LastGameLabel.LeftClickEventArgs.LeftClick = Const.LeftClickEnum.None;
//            }
            
            return true;
        }
        /// <summary>
        /// 游戏开始掷骰子，并且召唤怪兽
        /// </summary>
        /// <param name="player"></param>
        public void WaveDice(Player player)
        {
            int twoStarSum = 0, threeStarSum = 0, fourStarSum = 0;
            CardMonster monster = null;
            foreach (PictureBox p in player.PictureBoxlList)
            {
                int number = random.Next(6);
                switch (number)
                {
                    case 0:
                        p.Image = Const.IMAGE_MAGIC;
                        player.MagicNumber++;
                        break;
                    case 1:
                        p.Image = Const.IMAGE_TRAP;
                        player.TrapNumber++;
                        break;
                    case 2:
                        p.Image = Const.IMAGE_MOVE;
                        player.MoveNumber++;
                        break;
                    case 3:
                        p.Image = Const.IMAGE_TWO_STAR;
                        twoStarSum++;
                        break;
                    case 4:
                        p.Image = Const.IMAGE_THREE_STAR;
                        threeStarSum++;
                        break;
                    case 5:
                        p.Image = Const.IMAGE_FOUR_STAR;
                        fourStarSum++;
                        break;
                }
                
            }
            //如果同样的星星印章数量 >= 2 则可以召唤对应的星级怪兽
            if (twoStarSum >= 2)
            {
                monster = CreateMonster(gameData.TwoStarMonsters,player);
                monsterEventArgs = new MonsterEventArgs(gameData.PlayerDictionary[player],monster);
                leftClickEventArgs.LeftClick = Const.LeftClickEnum.CallMonster;
            }
            else if (threeStarSum >= 2)
            {
                monster = CreateMonster(gameData.ThreeStarMonsters, player);
                monsterEventArgs = new MonsterEventArgs(gameData.PlayerDictionary[player], monster);
                leftClickEventArgs.LeftClick = Const.LeftClickEnum.CallMonster;
            }
            else if (fourStarSum >= 2)
            {
                monster = CreateMonster(gameData.FourStarMonsters, player);
                monsterEventArgs = new MonsterEventArgs(gameData.PlayerDictionary[player], monster);
                leftClickEventArgs.LeftClick = Const.LeftClickEnum.CallMonster;
            }
            player.LabelLinkedList.ElementAt(3).Text = player.MagicNumber.ToString();
            player.LabelLinkedList.ElementAt(2).Text = player.TrapNumber.ToString();
            player.LabelLinkedList.ElementAt(1).Text = player.MoveNumber.ToString();
        }

        /// <summary>
        /// 创建怪兽
        /// </summary>
        /// <param name="monsterList"></param>
        /// <returns></returns>
        public CardMonster CreateMonster(List<CardMonster> monsterList,Player player)
        {
            int index = random.Next(monsterList.Count);
            CardMonster monster = (CardMonster)monsterList.ElementAt(index).Clone();
            //第一回合召唤的怪兽无法执行指令
            monster.CanAttack = false;
            monster.CanMove = false;
            monster.CanEffective = false;
            monster.MonsterImage = gameData.ImageDictionary[player].ElementAt((int) monster.Prop);
            player.CardLinkedList.AddLast(monster);
            MessageBox.Show(monster.Name + "可以召唤，请点击己方格子召唤", "提示");
            return monster;
        }

        /// <summary>
        /// 游戏格子点击之后放怪兽在格子上,并且把点击信息改成召唤怪兽
        /// </summary>
        /// <param name="sender"></param>
        /// <returns></returns>
        public bool CallMonster(GameLabel sender)
        {
            for (var i = 0; i < Const.LABEL_ROW; i++)
            {
                for (var j = 0; j < Const.LABEL_COL; j++)
                {
                    if (sender.I + 1 <= 10 && sender.I - 1 >= 0 && sender.J + 1 <= 10 && sender.J - 1 >= 0)
                    {
                        if (gameLabels[sender.I, sender.J].Belongs == Const.PlayerBelongs.None &&
                            (gameLabels[sender.I, sender.J + 1].Belongs == playerBelongs ||
                             gameLabels[sender.I, sender.J - 1].Belongs == playerBelongs ||
                             gameLabels[sender.I - 1, sender.J].Belongs == playerBelongs ||
                             gameLabels[sender.I + 1, sender.J].Belongs == playerBelongs)) //如果点击的格子四周是己方的格子
                        {
                            sender.SetMonsterEventArgs(monsterEventArgs); //设置怪兽
                            playerBelongs = monsterEventArgs.Player; //更改玩家所属                           
                            //把点击格子四周的所属修改，且刷新（无法覆盖属于敌方的格子）
                            if(gameLabels[sender.I - 1, sender.J].Belongs == Const.PlayerBelongs.None)
                                gameLabels[sender.I - 1, sender.J].Belongs = playerBelongs;
                            if (gameLabels[sender.I + 1, sender.J].Belongs == Const.PlayerBelongs.None)
                                gameLabels[sender.I + 1, sender.J].Belongs = playerBelongs;
                            if (gameLabels[sender.I, sender.J - 1].Belongs == Const.PlayerBelongs.None)
                                gameLabels[sender.I, sender.J - 1].Belongs = playerBelongs;
                            if (gameLabels[sender.I, sender.J + 1].Belongs == Const.PlayerBelongs.None)
                                gameLabels[sender.I, sender.J + 1].Belongs = playerBelongs;
                            gameLabels[sender.I - 1, sender.J].Refresh();
                            gameLabels[sender.I + 1, sender.J].Refresh();
                            gameLabels[sender.I, sender.J - 1].Refresh();
                            gameLabels[sender.I, sender.J + 1].Refresh();
                            return true;
                        }
                    }
                }
            }
            MessageBox.Show("无法在此格子召唤怪兽，请重新点击", "提示");
            return false;
        }
        /// <summary>
        /// 显示怪兽信息
        /// </summary>
        /// <param name="number"></param>
        /// <param name="star"></param>
        public void ShowMonster(CardMonster monster)
        {
            monsterTextBox.Text = monster.ToString();
        }

        /// <summary>
        ///结束按钮点击，结束该玩家的回合 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void buttonEnd_Click(object sender, EventArgs eventArgs)
        {
            gameData.PlayerOne.HisTurn = !gameData.PlayerOne.HisTurn;
            gameData.PlayerTwo.HisTurn = !gameData.PlayerTwo.HisTurn;
            buttonStart.Enabled = true;
            buttonEnd.Enabled = false;
            if (gameData.PlayerTwo.HisTurn)
            {
                pictureBox7.BackColor = Color.Red;
                pictureBox8.BackColor = Color.FromArgb(240, 240, 240);
            }
            else if (gameData.PlayerOne.HisTurn)
            {
                pictureBox7.BackColor = Color.FromArgb(240, 240, 240);
                pictureBox8.BackColor = Color.Blue;
            }   
            
        }
        /// <summary>
        /// 当怪兽执行攻击指令时，高亮显示怪兽可以攻击的范围
        /// 当怪兽执行移动时，高亮显示当前执行指令的怪兽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen redPen = new Pen(Color.Red,4);
            Pen bluePen = new Pen(Color.Blue,4);
            if (meaEventAgrs.LastGameLabel != null && meaEventAgrs.LastGameLabel.Monster != null)
            {
                if (meaEventAgrs.LastGameLabel.Monster.IsAttack)
                {
                    Pen pen = gameData.PlayerOne.HisTurn ? redPen : bluePen;
                    for (int i = 0; i < Const.LABEL_ROW; i++)
                    {
                        for (int j = 0; j < Const.LABEL_COL; j++)
                        {
                            g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I - 1, meaEventAgrs.LastGameLabel.J].I * 30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I - 1, meaEventAgrs.LastGameLabel.J].J * 30, 28, 28);
                            g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I + 1, meaEventAgrs.LastGameLabel.J].I * 30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I + 1, meaEventAgrs.LastGameLabel.J].J * 30, 28, 28);
                            g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J - 1].I * 30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J - 1].J * 30, 28, 28);
                            g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J + 1].I * 30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J + 1].J * 30, 28, 28);
                        }
                    }
                }
                else if (meaEventAgrs.LastGameLabel.Monster.IsMove)
                {
                    Pen pen = gameData.PlayerOne.HisTurn ? redPen : bluePen;
                    g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J].I * 30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J].J * 30, 28, 28);
                }
            }
        }
    }
}
