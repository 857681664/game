using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using game.control;
using game.dto;
using game.entity;
using game.@event;

namespace game
{
    /// <summary>
    /// 游戏的主要窗口
    /// </summary>
    public partial class Form1 : Form
    {
        
        private Const.PlayerBelongs playerBelongs;//玩家所属
        private MonsterEventArgs monsterEventArgs;//怪兽信息
        private LeftClickEventArgs leftClickEventArgs;//左键点击信息
        private MEAEventAgrs meaEventAgrs;//攻击移动效果事件信息
        private GameLabel[,] gameLabels;//游戏的格子
        private Random random;
        private GameData gameData;//游戏数据
        private IConnectionFactory factory;//连接工厂
        private IConnection connection;//连接
        private ISession session;//会话
        private IMessageProducer producer;//生产者
        private CustomListenDto dto;//
        private SqlOperate sqlOperate;
        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            InitGameParam();
            InitGameLabel();
            InitProducter();
            InitCustom();
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
                    gameLabels[i, j].SendMessageEvent += NotPointMonsterSendMessage;
                    gameLabels[i, j].MainPanelRefreshEvent += MainPanelRefresh;
                    gameLabels[i, j].LeftClickEventArgs = leftClickEventArgs;
                    gameLabels[i,j].SetMEAEventArgs(meaEventAgrs);
                    mainPanel.Controls.Add(gameLabels[i, j]);
                }
            }
            //玩家2的生命格子
            gameLabels[5, 0].BackColor = Color.Red;
            gameLabels[5, 0].Belongs = Const.PlayerBelongs.PlayerTwo;
            //玩家1的生命格子
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
            gameInfoTBox.Text = "";
            factory = new ConnectionFactory("tcp://localhost:61616");
            connection = factory.CreateConnection();
            session = connection.CreateSession();
            gameData = new GameData();
            random = new Random();
            leftClickEventArgs = new LeftClickEventArgs(Const.LeftClickEnum.None);
            meaEventAgrs = new MEAEventAgrs {Data = gameData};
            dto = new CustomListenDto();
            sqlOperate = new SqlOperate();
            int nowLength = gameData.TwoStarMonsters.Count + gameData.ThreeStarMonsters.Count +
                            gameData.FourStarMonsters.Count;
            sqlOperate.FillMonsterTable(gameData.TwoStarMonsters, gameData.ThreeStarMonsters, gameData.FourStarMonsters, nowLength);
            //把玩家1和玩家2对应的Label和PictureBox加入到相应的list中
            foreach (Control c in Controls)
            {
                if(c.GetType() == typeof(Label))
                {
                    if (c.Name.EndsWith("1"))
                        gameData.PlayerOne.LabelLinkedList.AddLast((Label) c);
                    else if (c.Name.EndsWith("2"))
                        gameData.PlayerTwo.LabelLinkedList.AddLast((Label) c);
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
            //如果是玩家1的回合
            if (gameData.PlayerOne.HisTurn)
            {
                playerBelongs = Const.PlayerBelongs.PlayerOne;
                Const.Turn = Const.PlayerTurn.TurnOne;
                TurnStart(gameData.PlayerOne);
            }
            //如果是玩家2的回合
            else if (gameData.PlayerTwo.HisTurn)
            {
                playerBelongs = Const.PlayerBelongs.PlayerTwo;
                Const.Turn = Const.PlayerTurn.TurnTwo;
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
                //如果怪兽还被影响
                if (card.EffectTurn != 0)                
                    card.EffectTurn--;
                //如果怪兽影响结束，回复怪兽本来的数值
                if (card.EffectTurn == 0)
                {
                    card.IsEffected = false;
                    switch (card.PointKind)
                    {
                        case Const.PointKindEnum.DecreaseAttack:
                            card.Attack += card.EffectNumber;
                            break;
                        case Const.PointKindEnum.IncreaseAttack:
                            card.Attack -= card.EffectNumber;
                            break;
                    }
                }
            }
            //刷新所有的游戏格子
            foreach (GameLabel label in gameLabels)
            {
                if (label.HasMonster)
                    label.Refresh();
            }
            WaveDice(player);
        }

        /// <summary>
        /// 移动怪兽，判断是否可以移动，再移动
        /// </summary>
        /// <param name="e"></param>
        public bool MoveMonster(MEAEventAgrs e)
        {
            int moveSum;
            Player player = gameData.BelongDictionary[e.LastGameLabel.Belongs];
            moveSum = CalMoveSum(e);
            if (moveSum == 0)
                MessageBox.Show("无法到达");
            else if (player.MoveNumber < moveSum)
                MessageBox.Show("移动印章不足，无法移动");
            else
            {
                
                player.MoveNumber -= moveSum;//移动印章数减少
                player.LabelLinkedList.ElementAt(1).Text = player.MoveNumber.ToString();
                e.LastGameLabel.Monster.CanMove = false;//怪兽设置不可移动
                e.NowGameLabel.Monster = e.LastGameLabel.Monster;
                e.NowGameLabel.HasMonster = true;
                e.LastGameLabel.HasMonster = false;
                e.LastGameLabel.Monster = null;
                SendMessage(e, "move");
            }
            if (e.LastGameLabel.Monster != null) e.LastGameLabel.Monster.IsMove = false;
            e.LastGameLabel.Refresh();
            e.NowGameLabel.Refresh();
            e.LastGameLabel.MyRefresh();
            return true;
        }

        /// <summary>
        /// 计算怪兽移动所需要的步数
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public int CalMoveSum(MEAEventAgrs e)
        {
            int[,] matrix = new int[Const.LABEL_COL, Const.LABEL_ROW];
            for (int i = 0; i < Const.LABEL_ROW; i++)
            {
                for (int j = 0; j < Const.LABEL_COL; j++)
                {
//                    matrix[j, i] = 0;
                    if (gameData.PlayerOne.HisTurn)
                    {
                        if (gameLabels[i, j].HasMonster && gameLabels[i, j].Monster.Belongs == Const.PlayerBelongs.PlayerTwo)
                            matrix[j, i] = 1;
                    }
                    else
                    {
                        if (gameLabels[i, j].HasMonster && gameLabels[i, j].Monster.Belongs == Const.PlayerBelongs.PlayerOne)
                            matrix[j, i] = 1;
                    }
                }
            }
            int stepSum = CalMinimumStep.MiniMumStep(matrix, e.LastGameLabel.I, e.LastGameLabel.J, e.NowGameLabel.I,
                e.NowGameLabel.J);
            return stepSum;
        }

        /// <summary>
        /// 怪兽攻击
        /// </summary>
        /// <param name="e"></param>
        public void AttackMonster(MEAEventAgrs e)
        {
            if (e.LastGameLabel.Monster.Belongs == Const.PlayerBelongs.PlayerOne)
            {
                //如果是生命格子
                if (e.NowGameLabel.I == 5 && e.NowGameLabel.J == 0)
                {
                    e.Data.PlayerTwo.LifePoint -= e.LastGameLabel.Monster.Attack;
                    playerTwoLPLabel2.Text = e.Data.PlayerTwo.LifePoint.ToString();
                    SendMessage(e, "attackPlayer");
                    if (e.Data.PlayerTwo.LifePoint <= 0)
                        MessageBox.Show("鲍东赢了，良俊输了", "提示");
                }
                else
                {
                    SendMessage(e, "attack");
                    if (e.LastGameLabel.Monster.Attack > e.NowGameLabel.Monster.Attack)
                    {
                        gameData.PlayerTwo.DeathMonsters.AddLast(e.NowGameLabel.Monster);
                        gameData.PlayerTwo.CardLinkedList.Remove(e.NowGameLabel.Monster);
                        e.NowGameLabel.Monster = null;
                        e.NowGameLabel.HasMonster = false;
                    }
                    else if (e.LastGameLabel.Monster.Attack < e.NowGameLabel.Monster.Attack)
                    {
                        gameData.PlayerOne.DeathMonsters.AddLast(e.LastGameLabel.Monster);
                        gameData.PlayerOne.CardLinkedList.Remove(e.LastGameLabel.Monster);
                        e.LastGameLabel.Monster = null;
                        e.LastGameLabel.HasMonster = false;
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
                    }
                }
                
            }
            else if (e.LastGameLabel.Monster.Belongs == Const.PlayerBelongs.PlayerTwo)
            {
                if (e.NowGameLabel.I == 5 && e.NowGameLabel.J == 9)
                {
                    e.Data.PlayerTwo.LifePoint -= e.LastGameLabel.Monster.Attack;
                    playerOneLPLabel1.Text = e.Data.PlayerTwo.LifePoint.ToString();
                    SendMessage(e, "attackPlayer");
                    if (e.Data.PlayerOne.LifePoint <= 0)
                        MessageBox.Show("良俊赢了，鲍东输了", "提示");
                }
                else
                {
                    SendMessage(e, "attack");
                    if (e.LastGameLabel.Monster.Attack > e.NowGameLabel.Monster.Attack)
                    {
                        gameData.PlayerOne.DeathMonsters.AddLast(e.NowGameLabel.Monster);
                        gameData.PlayerOne.CardLinkedList.Remove(e.NowGameLabel.Monster);
                        e.NowGameLabel.Monster = null;
                        e.NowGameLabel.HasMonster = false;
                    }
                    else if (e.LastGameLabel.Monster.Attack < e.NowGameLabel.Monster.Attack)
                    {
                        gameData.PlayerTwo.DeathMonsters.AddLast(e.LastGameLabel.Monster);
                        gameData.PlayerTwo.CardLinkedList.Remove(e.LastGameLabel.Monster);
                        e.NowGameLabel.Monster = null;
                        e.NowGameLabel.HasMonster = false;
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
                    }
                }
                
            }

            if (e.LastGameLabel.Monster != null)
            {
                e.LastGameLabel.Monster.IsAttack = false;
                e.LastGameLabel.Monster.IsMove = false;
                e.LastGameLabel.Monster.CanAttack = false;
                e.LastGameLabel.Monster.CanEffective = false;
            }
            MainPanelRefresh();
            e.LastGameLabel.Refresh();
            e.NowGameLabel.Refresh();
        }

        /// <summary>
        /// 发动怪兽效果，完成后刷新怪兽信息和印章数量
        /// </summary>
        /// <param name="e"></param>
        public bool UserEffect(MEAEventAgrs e)
        {
            bool IsEffect;
            Type t = e.LastGameLabel.Monster.GetType();
            object[] objects = {e};
            IsEffect = (bool) t.GetMethod("UserEffect").Invoke(e.LastGameLabel.Monster, objects);
            if (!IsEffect)
            {
                MessageBox.Show("发动失败,条件没有满足或者选择怪兽已经有BUFF");
                e.LastGameLabel.LeftClickEventArgs.LeftClick = Const.LeftClickEnum.SelectMonster;
            }
            else
            {
                e.LastGameLabel.Monster.CanAttack = false;
                e.LastGameLabel.Monster.CanEffective = false;
                e.LastGameLabel.Refresh();
                e.NowGameLabel.Refresh();
                if(e.NowGameLabel.HasMonster)
                    monsterTextBox.Text = e.NowGameLabel.Monster.ToString();
                e.LastGameLabel.LeftClickEventArgs.LeftClick = Const.LeftClickEnum.None;
                SendMessage(e, "pointEffect");
            }

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
                monster = CreateMonster(gameData.TwoStarMonsters, player);
            else if (threeStarSum >= 2)
                monster = CreateMonster(gameData.ThreeStarMonsters, player);
            else if (fourStarSum >= 2)
                monster = CreateMonster(gameData.FourStarMonsters, player);
            monsterEventArgs = new MonsterEventArgs(gameData.PlayerDictionary[player], monster);
            leftClickEventArgs.LeftClick = Const.LeftClickEnum.CallMonster;
            if (monster != null) monster.Belongs = gameData.PlayerDictionary[player];
            player.LabelLinkedList.ElementAt(3).Text = player.MagicNumber.ToString();
            player.LabelLinkedList.ElementAt(2).Text = player.TrapNumber.ToString();
            player.LabelLinkedList.ElementAt(1).Text = player.MoveNumber.ToString();
        }

        /// <summary>
        /// 创建怪兽
        /// </summary>
        /// <param name="monsterList"></param>
        /// <returns></returns>
        public CardMonster CreateMonster(List<CardMonster> monsterList, Player player)
        {
            int index = random.Next(monsterList.Count);
            CardMonster monster = (CardMonster) monsterList.ElementAt(index).Clone();
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
        public bool CallMonster(MEAEventAgrs sender)
        {
            for (var i = 0; i < Const.LABEL_ROW; i++)
            {
                for (var j = 0; j < Const.LABEL_COL; j++)
                {
                    if (sender.LastGameLabel.I + 1 <= 10 && sender.LastGameLabel.I - 1 >= 0 && sender.LastGameLabel.J + 1 <= 10 && sender.LastGameLabel.J - 1 >= 0)
                    {
                        if (gameLabels[sender.LastGameLabel.I, sender.LastGameLabel.J].Belongs == Const.PlayerBelongs.None && (gameLabels[sender.LastGameLabel.I, sender.LastGameLabel.J + 1].Belongs == playerBelongs || gameLabels[sender.LastGameLabel.I, sender.LastGameLabel.J - 1].Belongs == playerBelongs || gameLabels[sender.LastGameLabel.I - 1, sender.LastGameLabel.J].Belongs == playerBelongs || gameLabels[sender.LastGameLabel.I + 1, sender.LastGameLabel.J].Belongs == playerBelongs)) //如果点击的格子四周是己方的格子
                        {
                            sender.LastGameLabel.SetMonsterEventArgs(monsterEventArgs); //设置怪兽
                            sender.LastGameLabel.Monster = monsterEventArgs.Monster;
                            playerBelongs = monsterEventArgs.Player; //更改玩家所属                           
                            //把点击格子四周的所属修改，且刷新（无法覆盖属于敌方的格子）
                            if (gameLabels[sender.LastGameLabel.I - 1, sender.LastGameLabel.J].Belongs == Const.PlayerBelongs.None)
                                gameLabels[sender.LastGameLabel.I - 1, sender.LastGameLabel.J].Belongs = playerBelongs;
                            if (gameLabels[sender.LastGameLabel.I + 1, sender.LastGameLabel.J].Belongs == Const.PlayerBelongs.None)
                                gameLabels[sender.LastGameLabel.I + 1, sender.LastGameLabel.J].Belongs = playerBelongs;
                            if (gameLabels[sender.LastGameLabel.I, sender.LastGameLabel.J - 1].Belongs == Const.PlayerBelongs.None)
                                gameLabels[sender.LastGameLabel.I, sender.LastGameLabel.J - 1].Belongs = playerBelongs;
                            if (gameLabels[sender.LastGameLabel.I, sender.LastGameLabel.J + 1].Belongs == Const.PlayerBelongs.None)
                                gameLabels[sender.LastGameLabel.I, sender.LastGameLabel.J + 1].Belongs = playerBelongs;
                            gameLabels[sender.LastGameLabel.I - 1, sender.LastGameLabel.J].Refresh();
                            gameLabels[sender.LastGameLabel.I + 1, sender.LastGameLabel.J].Refresh();
                            gameLabels[sender.LastGameLabel.I, sender.LastGameLabel.J - 1].Refresh();
                            gameLabels[sender.LastGameLabel.I, sender.LastGameLabel.J + 1].Refresh();
                            SendMessage(sender, "call");
                            if (monsterEventArgs.Monster.EffectKind == Const.EffectKindEnum.AfterCall)
                            {
                                Type t = monsterEventArgs.Monster.GetType();
                                object[] objects = { meaEventAgrs };
                                t.GetMethod("UserEffect").Invoke(monsterEventArgs.Monster, objects);
                                SendMessage(sender, "aftercall");
                            }
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
            Pen redPen = new Pen(Color.Red, 4);
            Pen bluePen = new Pen(Color.Blue, 4);
            if (meaEventAgrs.LastGameLabel != null && meaEventAgrs.LastGameLabel.Monster != null)
            {
                if (meaEventAgrs.LastGameLabel.Monster.IsAttack)
                {
                    Pen pen = gameData.PlayerOne.HisTurn ? redPen : bluePen;
                    for (int i = 0; i < Const.LABEL_ROW; i++)
                    {
                        for (int j = 0; j < Const.LABEL_COL; j++)
                        {
                            g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I - 1, meaEventAgrs.LastGameLabel.J].I*30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I - 1, meaEventAgrs.LastGameLabel.J].J*30, 28, 28);
                            g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I + 1, meaEventAgrs.LastGameLabel.J].I*30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I + 1, meaEventAgrs.LastGameLabel.J].J*30, 28, 28);
                            g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J - 1].I*30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J - 1].J*30, 28, 28);
                            g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J + 1].I*30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J + 1].J*30, 28, 28);
                        }
                    }
                }
                else if (meaEventAgrs.LastGameLabel.Monster.IsMove)
                {
                    Pen pen = gameData.PlayerOne.HisTurn ? redPen : bluePen;
                    g.DrawRectangle(pen, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J].I*30, meaEventAgrs.Data.GameLabels[meaEventAgrs.LastGameLabel.I, meaEventAgrs.LastGameLabel.J].J*30, 28, 28);
                }
            }
        }

        /// <summary>
        /// 初始化消息队列生产者
        /// </summary>
        private void InitProducter()
        {
            producer = session.CreateProducer(new ActiveMQQueue("game"));
        }

        /// <summary>
        /// 发送消息给消费者监听器，根据行动的种类发送不同种类的消息
        /// </summary>
        /// <param name="e"></param>
        /// <param name="eventKind"></param>
        public void SendMessage(MEAEventAgrs e, string eventKind)
        {
            IObjectMessage message;
            if (eventKind.Equals("move"))
            {
                dto.PlayerOne = gameData.BelongDictionary[e.NowGameLabel.Monster.Belongs].NickName;
                dto.MonsterOne = e.NowGameLabel.Monster.Name;
                dto.EventKind = eventKind;
            }
            else if (eventKind.Equals("call"))
            {
                dto.MonsterOne = e.LastGameLabel.Monster.Name;
                dto.PlayerOne = gameData.BelongDictionary[e.LastGameLabel.Monster.Belongs].NickName;
                dto.EventKind = eventKind;
            }
            else if (eventKind.Equals("attack"))
            {
                dto.MonsterOne = e.LastGameLabel.Monster.Name;
                dto.MonsterTwo = e.NowGameLabel.Monster.Name;
                dto.PlayerOne = gameData.BelongDictionary[e.LastGameLabel.Monster.Belongs].NickName;
                dto.PlayerTwo = gameData.BelongDictionary[e.NowGameLabel.Monster.Belongs].NickName;
                dto.EventKind = eventKind;
            }
            else if (eventKind.Equals("aftercall"))
            {
                dto.MonsterOne = e.LastGameLabel.Monster.Name;
                dto.PlayerOne = gameData.BelongDictionary[e.LastGameLabel.Monster.Belongs].NickName;
                dto.EventKind = eventKind;
            }
            else if (eventKind.Equals("attackPlayer"))
            {
                dto.MonsterOne = e.LastGameLabel.Monster.Name;
                dto.Attack = e.LastGameLabel.Monster.Attack;
                dto.PlayerOne = gameData.BelongDictionary[e.LastGameLabel.Monster.Belongs].NickName;
                dto.PlayerTwo = gameData.BelongDictionary[e.NowGameLabel.Belongs].NickName;
                dto.EventKind = eventKind;
            }
            else
            {
                //如果是kill效果dto的第二个名字要从墓地中获取
                if (e.LastGameLabel.Monster.PointKind == Const.PointKindEnum.KillMonster)
                {
                    Player player = e.LastGameLabel.Monster.Belongs == Const.PlayerBelongs.PlayerOne
                        ? gameData.PlayerTwo
                        : gameData.PlayerOne;
                    dto.MonsterTwo = player.DeathMonsters.ElementAt(player.DeathMonsters.Count - 1).Name;
                    dto.PlayerTwo = player.NickName;
                }
                else
                {
                    dto.MonsterTwo = e.NowGameLabel.Monster.Name;
                    dto.PlayerTwo = gameData.BelongDictionary[e.NowGameLabel.Monster.Belongs].NickName;
                }
                dto.MonsterOne = e.LastGameLabel.Monster.Name;
                dto.PlayerOne = gameData.BelongDictionary[e.LastGameLabel.Monster.Belongs].NickName;
                dto.EventKind = eventKind;
            }
            message = producer.CreateObjectMessage(dto);
            producer.Send(message);
        }

        /// <summary>
        /// 初始化消费者，并添加消息监听者
        /// </summary>
        private void InitCustom()
        {
            connection.Start();
            IMessageConsumer custom = session.CreateConsumer(new ActiveMQQueue("game"));
            custom.Listener += CustomOnListener;
        }

        /// <summary>
        /// 消息监听者对监听消息的处理，根据当前玩家的回合改变当前消息的颜色
        /// </summary>
        /// <param name="m"></param>
        private void CustomOnListener(IMessage m)
        {
            IObjectMessage message = (IObjectMessage) m;
            dto = (CustomListenDto) message.Body;
            string info;
            if (dto.EventKind.Equals("move"))
                info = dto.PlayerOne + "：" + dto.MonsterOne + "-> 移动\r\n";
            else if (dto.EventKind.Equals("call"))
                info = dto.PlayerOne + "：" + dto.MonsterOne + "-> 召唤\r\n";
            else if (dto.EventKind.Equals("attack"))
                info = dto.PlayerOne + "：" + dto.MonsterOne + "-> 攻击" + "-> " + dto.PlayerTwo + "：" + dto.MonsterTwo + "\r\n";
            else if (dto.EventKind.Equals("notPointEffect") || dto.EventKind.Equals("aftercall"))
                info = dto.PlayerOne + "：" + dto.MonsterOne + "-> 发动效果\r\n";
            else if (dto.EventKind.Equals("attackPlayer"))
                info = dto.PlayerOne + "：" + dto.MonsterOne + "->对" + dto.PlayerTwo + "造成了" + dto.Attack + "点伤害";
            else
                info = dto.PlayerOne + "：" + dto.MonsterOne + "-> 发动效果" + "-> " + dto.PlayerTwo + "：" + dto.MonsterTwo + "\r\n";
            if (gameData.PlayerOne.HisTurn)
            {
                gameInfoTBox.SelectionColor = Color.Blue;
                gameInfoTBox.SelectedText = info;
            }
            else if (gameData.PlayerTwo.HisTurn)
            {
                gameInfoTBox.SelectionColor = Color.Red;
                gameInfoTBox.SelectedText = info;
            }
        }

        /// <summary>
        /// 非指向性怪兽发动时创建消息
        /// </summary>
        /// <param name="e"></param>
        public void NotPointMonsterSendMessage(MEAEventAgrs e)
        {
            IObjectMessage message;
            dto.PlayerOne = gameData.BelongDictionary[e.LastGameLabel.Monster.Belongs].NickName;
            dto.MonsterOne = e.LastGameLabel.Monster.Name;
            dto.EventKind = "notPointEffect";
            message = producer.CreateObjectMessage(dto);
            producer.Send(message);
        }

        private void 查看全部ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            MonsterList form = new MonsterList(gameData);
            form.Show();
        }
    }
}
