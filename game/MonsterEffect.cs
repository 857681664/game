using System.Linq;
using System.Windows.Forms;
using game.entity;
using game.@event;

namespace game
{
    /// <summary>
    /// 怪兽发动效果的类
    /// </summary>
    public class MonsterEffect
    {
        public static bool CanEffect(MEAEventAgrs e, int magicNum, int trapNum)
        {
            Player player = e.Data.BelongDictionary[e.LastGameLabel.Monster.Belongs];
            if (player.MagicNumber >= magicNum && player.TrapNumber >= trapNum)
            {
                player.MagicNumber -= magicNum;
                player.TrapNumber -= trapNum;
                player.LabelLinkedList.ElementAt(3).Text = player.MagicNumber.ToString();
                player.LabelLinkedList.ElementAt(2).Text = player.TrapNumber.ToString();
                player.LabelLinkedList.ElementAt(1).Text = player.MoveNumber.ToString();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 北郡牧师效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool BeiJunEffect(MEAEventAgrs e)
        {
            Player player = e.LastGameLabel.Belongs == Const.PlayerBelongs.PlayerOne ? e.Data.PlayerTwo : e.Data.PlayerOne;
            foreach (CardMonster monster in player.CardLinkedList)
            {
                monster.Attack -= 200;
                monster.Defense -= 200;
            }
            return true;
        }
        /// <summary>
        /// 死亡领主效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool SiWangEffect(MEAEventAgrs e)
        {
            Player player = e.LastGameLabel.Belongs == Const.PlayerBelongs.PlayerOne ? e.Data.PlayerTwo : e.Data.PlayerOne;
            player.LifePoint -= 500;
            player.LabelLinkedList.ElementAt(0).Text = player.LifePoint.ToString();
            return true;
        }
        
        /// <summary>
        /// 宴会技师效果
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool YanHuiEffect(MEAEventAgrs e)
        {
            e.NowGameLabel.Monster.Attack -= 200;
            return true;
        }
    }
}