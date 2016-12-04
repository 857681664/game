namespace game.dto
{
    /// <summary>
    /// 封装怪兽信息的传输数据格式
    /// </summary>
    public class MonsterDto
    {
        public int Attack { get; set; }

        public int Defense { get; set; }

        public int Number { get; set; }

        public int Star { get; set; }

        public string Descripe { get; set; }

        public string Name { get; set; }

        public string Effect { get; set; }

        public Const.PropEnum Prop { get; set; }

        public Const.EffectKindEnum EffectKind { get; set; }

        public Const.PointKindEnum PointKind { get; set; }
    }
}