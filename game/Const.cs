using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using game.dto;

namespace game
{
    /// <summary>
    /// 游戏常量类，封装常量
    /// </summary>
    public class Const
    {
        
        //方向
        public enum Direction { Up, Down, Right, Left }
        //右键菜单
        private static ContextMenuStrip rightContextMenu;
        public static ToolStripMenuItem MoveMenuItem { get; set; }
        public static ToolStripMenuItem AttackMenuItem { get; set; }
        public static ToolStripMenuItem EffectMenuItem { get; set; }

        //怪兽buff类型
        public enum MonsterBuff { AttackIncrease, AttackDecrease, }
        //左键点击事件的类型
        public enum LeftClickEnum { CallMonster, MoveMonster, SelectMonster, AttackMonster, SkyKnight, None }
        //卡的种类
        public enum KindEnum { Magic,Monster }
        //暗，光，火，水，地，天，恶魔,天使，鬼，木
        public enum PropEnum { Dark, Light, Fire, Water, Ground, Fly, Demon, Angel, Ghost, Tree }
        //所属玩家
        public enum PlayerBelongs { PlayerOne, PlayerTwo, None }

        //效果类别：指向和非指向
        public enum EffectKindEnum { Point ,NotPoint }

        //效果类别：加攻击，减攻击，消灭随从，复活
        public enum PointKindEnum { IncreaseAttack, DecreaseAttack, KillMonster, ReCall, DecreaseLifePoint }


        //怪兽卡对应属性名
        public static List<string> PropList = new List<string>() {"暗","光","火","水","地","天空","恶魔","天使","鬼","木"}; 
        //格子宽度
        public static int LABEL_WIDTH = 28;
        //格子行数
        public static int LABEL_ROW = 11;
        //格子列数
        public static int LABEL_COL = 10;
        //魔法印章图片
        public static Image IMAGE_MAGIC = Image.FromFile("H:\\c#\\game\\game\\graphics\\mofa.png");
        //陷阱印章图片
        public static Image IMAGE_TRAP = Image.FromFile("H:\\c#\\game\\game\\graphics\\xianjin.png");
        //移动印章图片
        public static Image IMAGE_MOVE = Image.FromFile("H:\\c#\\game\\game\\graphics\\qianjin.png");
        //二星图片
        public static Image IMAGE_TWO_STAR = Image.FromFile("H:\\c#\\game\\game\\graphics\\erxing.png");
        //三星图片
        public static Image IMAGE_THREE_STAR = Image.FromFile("H:\\c#\\game\\game\\graphics\\sanxing.png");
        //四星图片
        public static Image IMAGE_FOUR_STAR = Image.FromFile("H:\\c#\\game\\game\\graphics\\sixing.png");

        //怪兽属性图片
        public static Image IMAGE_RED_DARK = Image.FromFile("H:\\c#\\game\\game\\graphics\\darkred.png");
        public static Image IMAGE_BLUE_DARK = Image.FromFile("H:\\c#\\game\\game\\graphics\\darkblue.png");
        public static Image IMAGE_RED_LIGHT = Image.FromFile("H:\\c#\\game\\game\\graphics\\lightred.png");
        public static Image IMAGE_BLUE_LIHT = Image.FromFile("H:\\c#\\game\\game\\graphics\\lightblue.png");
        public static Image IMAGE_RED_FIRE = Image.FromFile("H:\\c#\\game\\game\\graphics\\firered.png");
        public static Image IMAGE_BLUE_FIRE = Image.FromFile("H:\\c#\\game\\game\\graphics\\fireblue.png");
        public static Image IMAGE_RED_WATER = Image.FromFile("H:\\c#\\game\\game\\graphics\\waterred.png");
        public static Image IMAGE_BLUE_WATER = Image.FromFile("H:\\c#\\game\\game\\graphics\\waterblue.png");
        public static Image IMAGE_RED_GROUND = Image.FromFile("H:\\c#\\game\\game\\graphics\\groundred.png");
        public static Image IMAGE_BLUE_GROUND = Image.FromFile("H:\\c#\\game\\game\\graphics\\groundblue.png");
        public static Image IMAGE_RED_SKY = Image.FromFile("H:\\c#\\game\\game\\graphics\\skyred.png");
        public static Image IMAGE_BLUE_SKY = Image.FromFile("H:\\c#\\game\\game\\graphics\\skyblue.png");
        public static Image IMAGE_RED_EVIL = Image.FromFile("H:\\c#\\game\\game\\graphics\\evilred.png");
        public static Image IMAGE_BLUE_EVIL = Image.FromFile("H:\\c#\\game\\game\\graphics\\evilblue.png");
        public static Image IMAGE_RED_ANGEL = Image.FromFile("H:\\c#\\game\\game\\graphics\\angelred.png");
        public static Image IMAGE_BLUE_ANGEL = Image.FromFile("H:\\c#\\game\\game\\graphics\\angelblue.png");
        public static Image IMAGE_RED_GHOST = Image.FromFile("H:\\c#\\game\\game\\graphics\\ghostred.png");
        public static Image IMAGE_BLUE_GHOST = Image.FromFile("H:\\c#\\game\\game\\graphics\\ghostblue.png");
        public static Image IMAGE_RED_TREE = Image.FromFile("H:\\c#\\game\\game\\graphics\\treered.png");
        public static Image IMAGE_BLUE_TREE = Image.FromFile("H:\\c#\\game\\game\\graphics\\treeblue.png");
        //右键菜单的图标
        public static Image AttackMenuItemImage = Image.FromFile("H:\\c#\\game\\game\\graphics\\attackmenuitem.png");
        public static Image MoveMenuItemImage = Image.FromFile("H:\\c#\\game\\game\\graphics\\movemenuitem.png");
        public static Image EffectMenuItemImage = Image.FromFile("H:\\c#\\game\\game\\graphics\\effectmenuitem.png");

        public static List<Image> RedImages = new List<Image>() { IMAGE_RED_DARK,IMAGE_RED_LIGHT, IMAGE_RED_FIRE, IMAGE_RED_WATER, IMAGE_RED_GROUND, IMAGE_RED_SKY, IMAGE_RED_EVIL, IMAGE_RED_ANGEL, IMAGE_RED_GHOST, IMAGE_RED_TREE }; 
        public static List<Image> BlueImages = new List<Image>() { IMAGE_BLUE_DARK , IMAGE_BLUE_LIHT , IMAGE_BLUE_FIRE,IMAGE_BLUE_WATER, IMAGE_BLUE_GROUND, IMAGE_BLUE_SKY, IMAGE_BLUE_EVIL, IMAGE_BLUE_ANGEL, IMAGE_BLUE_GHOST, IMAGE_BLUE_TREE };


   
        //创建右键菜单
        public static ContextMenuStrip CreateRightContentMenu()
        {
            rightContextMenu = new ContextMenuStrip();
            MoveMenuItem = GetStripMenuItem("移动", MoveMenuItemImage);
            AttackMenuItem = GetStripMenuItem("攻击", AttackMenuItemImage);
            EffectMenuItem = GetStripMenuItem("发动", EffectMenuItemImage);
            return rightContextMenu;
        }
        /// <summary>
        /// 创建右键菜单
        /// </summary>
        /// <param name="text">菜单内容</param>
        /// <param name="image">菜单图标</param>
        /// <returns></returns>
        private static ToolStripMenuItem GetStripMenuItem(String text, Image image)
        {
            ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(text,image);
            return toolStripMenuItem;
        }
    }
}
