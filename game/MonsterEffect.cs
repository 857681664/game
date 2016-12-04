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
            return player.MagicNumber >= magicNum && player.TrapNumber >= trapNum;
        }


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

        public static bool SiWangEffect(MEAEventAgrs e)
        {
            Player player = e.LastGameLabel.Belongs == Const.PlayerBelongs.PlayerOne ? e.Data.PlayerTwo : e.Data.PlayerOne;
            player.LifePoint -= 500;
            player.LabelLinkedList.ElementAt(0).Text = player.LifePoint.ToString();
            return true;
        }
    }
}