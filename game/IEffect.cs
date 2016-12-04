using game.@event;

namespace game
{
    /// <summary>
    /// 发动效果的接口
    /// </summary>
    public interface IEffect
    {
        bool UserEffect(MEAEventAgrs e);
    }
}
