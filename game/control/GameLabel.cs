using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using game.dto;
using game.entity;
using game.@event;
using game.monster.fourstar;

namespace game.control
{
    /// <summary>
    /// 游戏格子，用来显示玩家所属的领地
    /// </summary>
    public class GameLabel : Label
    {
        private ContextMenuStrip rightContextMenu;
        private ToolStripMenuItem moveMenuItem;
        private ToolStripMenuItem attackMenuItem;
        private ToolStripMenuItem effectMenuItem;
        private MonsterEventArgs monsterEventArgs;//怪兽信息
        private MEAEventAgrs meaEventAgrs;
        private bool isMouseIn;

        public delegate bool UserEffectHandle(MEAEventAgrs e);
        public delegate void ShowMonsterHandle(CardMonster monster);
        public delegate bool CallMonsterHandle(GameLabel sender);
        public delegate void MoveMonsterHandle(MEAEventAgrs e);
        public delegate void AttackMonsterHandle(MEAEventAgrs e);
        public delegate void MainPanelRefreshHandle();
        

        public event ShowMonsterHandle ShowMonsterEvent;//显示怪兽信息事件

        public event CallMonsterHandle CallMonsterEvent;//召唤怪兽事件

        public event MoveMonsterHandle MoveMonsterEvent;//移动怪兽

        public event AttackMonsterHandle AttackMonsterEvent;//攻击怪兽

        public event UserEffectHandle UserEffectEvent;//发动效果事件

        public event MainPanelRefreshHandle MainPanelRefreshEvent;//刷新怪兽的攻击范围显示

        public CardMonster Monster { get; set; }

        public Const.PlayerBelongs Belongs { get; set; } //属性

        public bool HasMonster { get; set; } //属性

        public LeftClickEventArgs LeftClickEventArgs { get; set; }

        //I和J表示该格子的位置
        public int I { get; set; }

        public int J { get; set; }

        /// <summary>
        /// 游戏格子构造方法
        /// </summary>
        /// <param name="x">横轴坐标</param>
        /// <param name="y">纵轴坐标</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public GameLabel(int x,int y,int width,int height,int i, int j)
        {
            I = i;
            J = j;
            Width = width;
            Height = height;
            Location = new Point(x + 1, y + 1);
            BorderStyle = BorderStyle.FixedSingle;
            Belongs = Const.PlayerBelongs.None;
            HasMonster = false;
            isMouseIn = false;
        }

        /// <summary>
        /// 如果当前格子有怪兽，在格子的中央绘制一个方块
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            base.OnPaint(e);
            if (HasMonster)
            {
                BackColor = Color.FromArgb(240, 240, 240);
                Image = Monster.MonsterImage;
                g.DrawLine(!Monster.CanMove ? new Pen(Color.Green, 3) : new Pen(Color.FromArgb(240, 240, 240), 3),2, 0, 2, 6);
                g.DrawLine( !Monster.CanAttack ? new Pen(Color.DarkRed, 3) : new Pen(Color.FromArgb(240, 240, 240), 3), 10, 0, 10, 6);
                g.DrawLine(!Monster.CanEffective? new Pen(Color.BlueViolet, 3): new Pen(Color.FromArgb(240, 240, 240), 3), 20, 0, 20, 6);
            }
            else
            {
                if (Belongs == Const.PlayerBelongs.PlayerOne)
                    BackColor = Color.Blue;
                else if(Belongs == Const.PlayerBelongs.PlayerTwo)
                    BackColor = Color.Red;
                Image = null;
            }
            if (isMouseIn)
            {
                g.DrawRectangle(new Pen(Color.Firebrick),e.ClipRectangle.X,e.ClipRectangle.Y,Width - 3,Height - 3);
            }
        }
        /// <summary>
        /// 鼠标点击事件，如果点击的格子没有怪兽且随机到一只，便可以点击召唤
        /// 如果点击的格子没有怪兽，则触发怪兽移动
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (e.Button == MouseButtons.Left)
            {
                if (!HasMonster && LeftClickEventArgs.LeftClick == Const.LeftClickEnum.CallMonster && CallMonsterEvent(this) )
                {
                    HasMonster = true;
                    Monster = monsterEventArgs.Monster;
                    Belongs = monsterEventArgs.Player;
                    Monster.Belongs = Belongs;
                    LeftClickEventArgs.LeftClick = Const.LeftClickEnum.None;
                }
                else if (LeftClickEventArgs.LeftClick == Const.LeftClickEnum.MoveMonster)
                {
                    if (Belongs == Const.PlayerBelongs.None)
                    {
                        MessageBox.Show("无法移动至该格子，请重新点击");
                        LeftClickEventArgs.LeftClick = Const.LeftClickEnum.MoveMonster;
                    }
                    else
                    {
                        meaEventAgrs.NowGameLabel = this;
                        MoveMonsterEvent?.Invoke(meaEventAgrs);
                        Monster.CanMove = false;
                        LeftClickEventArgs.LeftClick = Const.LeftClickEnum.None;
                    }                    
                }
                else if (LeftClickEventArgs.LeftClick == Const.LeftClickEnum.AttackMonster)
                {
                    meaEventAgrs.NowGameLabel = this;
                    if (HasMonster && meaEventAgrs.LastGameLabel.Monster.Belongs == meaEventAgrs.NowGameLabel.Monster.Belongs)
                    {
                        MessageBox.Show("请选择敌方怪兽", "提示");
                        LeftClickEventArgs.LeftClick = Const.LeftClickEnum.AttackMonster;
                    }
                    else if (!meaEventAgrs.NowGameLabel.HasMonster)
                    {
                        MessageBox.Show("请点击有怪兽的格子", "提示");
                        LeftClickEventArgs.LeftClick = Const.LeftClickEnum.AttackMonster;
                    }
                    else
                    {
                        if (Math.Abs(meaEventAgrs.LastGameLabel.I - meaEventAgrs.NowGameLabel.I) != 1 &&
                            Math.Abs(meaEventAgrs.LastGameLabel.J - meaEventAgrs.NowGameLabel.J) != 1)
                        {
                            MessageBox.Show("不在攻击范围内", "提示");
                            meaEventAgrs.LastGameLabel.Monster.IsAttack = false;
                            LeftClickEventArgs.LeftClick = Const.LeftClickEnum.AttackMonster;
                        }
                        else
                        {
                            AttackMonsterEvent?.Invoke(meaEventAgrs);
                            LeftClickEventArgs.LeftClick = Const.LeftClickEnum.None;
                        }
                        
                    }
                }
                else if (LeftClickEventArgs.LeftClick == Const.LeftClickEnum.SelectMonster)
                {
                    meaEventAgrs.NowGameLabel = this;
                    UserEffectEvent?.Invoke(meaEventAgrs);
                }
            }
            //如果是右键则弹出右键菜单
            //二星怪的效果项禁用，移动一回合只能一次，攻击和效果一回合选一个发动
            else if (e.Button == MouseButtons.Right)
            {
                if (LeftClickEventArgs.LeftClick != Const.LeftClickEnum.None)
                {
                    if (Monster != null && Monster.IsAttack)
                    {
                        Monster.IsAttack = false;
                        LeftClickEventArgs.LeftClick = Const.LeftClickEnum.None;
                        MainPanelRefreshEvent?.Invoke();
                    }
                    else if (Monster != null && Monster.IsMove)
                    {
                        Monster.IsMove = false;
                        LeftClickEventArgs.LeftClick = Const.LeftClickEnum.None;
                        MainPanelRefreshEvent?.Invoke();
                    }
                        
                }
                rightContextMenu = Const.CreateRightContentMenu();
                moveMenuItem = Const.MoveMenuItem;
                moveMenuItem.Click += MoveMenuItem_Click;
                attackMenuItem = Const.AttackMenuItem;
                attackMenuItem.Click += AttackMenuItem_Click;
                effectMenuItem = Const.EffectMenuItem;
                effectMenuItem.Click += EffectMenuItem_Click;
                rightContextMenu.Items.Add(moveMenuItem);
                rightContextMenu.Items.Add(attackMenuItem);
                rightContextMenu.Items.Add(effectMenuItem);
                if (HasMonster)
                {
                    if (Monster.Star == 2)
                        effectMenuItem.Enabled = false;
                    if (!Monster.CanAttack)
                        attackMenuItem.Enabled = false;
                    if (!Monster.CanMove)
                        moveMenuItem.Enabled = false;
                    if (!Monster.CanEffective)
                        effectMenuItem.Enabled = false;
                    rightContextMenu.Show(this, new Point(e.X, e.Y));
                }
            }
            Refresh();
        }
        /// <summary>
        /// 右键发动效果，如果印章足够则左键点击变为怪兽点击，否则弹出印章不足
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EffectMenuItem_Click(object sender, EventArgs e)
        {
            meaEventAgrs.LastGameLabel = this;
            Type t = meaEventAgrs.LastGameLabel.Monster.Star == 3 ? typeof (ThreeStarMonster) : typeof (FourStarMonster);
//            bool canEffect = (bool)t.GetMethod("CanEffect")
//                .Invoke(meaEventAgrs.LastGameLabel.Monster,
//                    new object[]
//                    {
//                        meaEventAgrs,
//                        (int)t.GetProperty("NeedMagic").GetValue(meaEventAgrs.LastGameLabel.Monster,null),
//                        (int)t.GetProperty("NeedTrap").GetValue(meaEventAgrs.LastGameLabel.Monster,null)
//                    });
//            if (!canEffect)
//            {
//                MessageBox.Show("印章不足，发动失败");
//            }
//            else
//            {
                object[] objects = { meaEventAgrs };
                if (meaEventAgrs.LastGameLabel.Monster.EffectKind == Const.EffectKindEnum.NotPoint)
                {
                    t.GetMethod("UserEffect").Invoke(meaEventAgrs.LastGameLabel.Monster, objects);
                    Monster.CanEffective = false;
                    Monster.CanAttack = false;
                    Refresh();
                }
                else if (meaEventAgrs.LastGameLabel.Monster.EffectKind == Const.EffectKindEnum.Point)
                {
                    LeftClickEventArgs.LeftClick = Const.LeftClickEnum.SelectMonster;
                }
//            }
            
//            var canEffect = (bool)t.GetMethod("CanEffect").Invoke(meaEventAgrs.LastGameLabel.Monster, objects);
//            if (!canEffect)
//                MessageBox.Show("印章不足，发动失败", "提示");
//            else
//            {
//                if (t == typeof (WuQiHao))
//                    t.GetMethod("UserEffect").Invoke(meaEventAgrs.LastGameLabel.Monster, objects);
//                else
//                    LeftClickEventArgs.LeftClick = Const.LeftClickEnum.SelectMonster;
//            }
            
        }

        private void AttackMenuItem_Click(object sender, EventArgs e)
        {
            LeftClickEventArgs.LeftClick = Const.LeftClickEnum.AttackMonster;
            meaEventAgrs.LastGameLabel = this;
            Monster.IsAttack = true;
            MainPanelRefreshEvent?.Invoke();
        }

        private void MoveMenuItem_Click(object sender, EventArgs e)
        {
            LeftClickEventArgs.LeftClick = Const.LeftClickEnum.MoveMonster;
            Monster.IsMove = true;
            meaEventAgrs.LastGameLabel = this;
            MainPanelRefreshEvent?.Invoke();
        }

        /// <summary>
        /// 鼠标进入时，如果该格子里有怪兽，则显示怪兽信息
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (HasMonster)
                ShowMonsterEvent.Invoke(Monster);
            isMouseIn = true;
            Refresh();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isMouseIn = false;
            Refresh();
        }

        /// <summary>
        /// 给该格子设置怪兽信息
        /// </summary>
        /// <param name="e"></param>
        public void SetMonsterEventArgs(MonsterEventArgs e)
        {
            monsterEventArgs = e;
        }

        public void SetMEAEventArgs(MEAEventAgrs e)
        {
            meaEventAgrs = e;
        }
    }
}
