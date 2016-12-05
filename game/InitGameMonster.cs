using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using game.dto;
using game.entity;

namespace game
{
    //从xml文件中读取怪兽的配置信息，并实例化每个怪兽的对象，将其加入怪兽池中
    public static class InitGameMonster
    {
        private static XmlDocument document;
        private static XmlNode root;
        private static XmlNode monsterNode;
        private static List<CardMonster> twoStarMonsters;
        private static List<CardMonster> threeStarMonsters;
        private static List<CardMonster> fourStarMonsters;

        static InitGameMonster()
        {
            document = new XmlDocument();
            document.Load("H:\\c#\\game\\game\\data.xml");
            root = document.DocumentElement;
            monsterNode = root.FirstChild;
            twoStarMonsters = new List<CardMonster>();
            threeStarMonsters = new List<CardMonster>();
            fourStarMonsters = new List<CardMonster>();
        }

       /// <summary>
       /// 添加二星怪兽
       /// </summary>
       /// <returns></returns>
        public static List<CardMonster> AddTwoStarMonsters()
        {
            XmlNode twoStarNode = monsterNode.ChildNodes[0];
            XmlNodeList items = twoStarNode.ChildNodes;
            for (int i = 0;i < items.Count;i++)
            {
                MonsterDto dto = new MonsterDto();
                dto.Number = Convert.ToInt32(items[i].Attributes["number"].Value);
                dto.Star = Convert.ToInt32(items[i].Attributes["star"].Value);
                dto.Name = items[i].Attributes["name"].Value;
                dto.Descripe = items[i].Attributes["descripe"].Value;
                dto.Attack = Convert.ToInt32(items[i].Attributes["attack"].Value);
                dto.Defense = Convert.ToInt32(items[i].Attributes["defense"].Value);
                dto.Prop = (Const.PropEnum)Enum.Parse(typeof(Const.PropEnum), items[i].Attributes["prop"].Value);
                TwoStarMonster monster = BuilderTwoStarMonster(dto);
                twoStarMonsters.Add(monster);
            }
            return twoStarMonsters;
        }

        /// <summary>
        /// 添加三星怪兽
        /// </summary>
        /// <returns></returns>
        public static List<CardMonster> AddThreeStarMonsters()
        {
            XmlNode threeStarNode = monsterNode.ChildNodes[1];
            XmlNodeList items = threeStarNode.ChildNodes;
            for (int i = 0; i < items.Count; i++)
            {
                XmlNode effectNode = items[i].FirstChild;
                if (effectNode.Attributes != null)
                {
                    MonsterDto dto = new MonsterDto();
                    dto.Number = Convert.ToInt32(items[i].Attributes["number"].Value);
                    dto.Star = Convert.ToInt32(items[i].Attributes["star"].Value);
                    dto.Name = items[i].Attributes["name"].Value;
                    dto.Descripe = items[i].Attributes["descripe"].Value;
                    dto.Effect = items[i].Attributes["effect"].Value;
                    dto.Attack = Convert.ToInt32(items[i].Attributes["attack"].Value);
                    dto.Defense = Convert.ToInt32(items[i].Attributes["defense"].Value);
                    dto.NeedMagic = Convert.ToInt32(effectNode.Attributes["magic"].Value);
                    dto.NeedTrap = Convert.ToInt32(effectNode.Attributes["trap"].Value);
                    dto.Prop = (Const.PropEnum)Enum.Parse(typeof(Const.PropEnum), items[i].Attributes["prop"].Value);
                    dto.EffectKind = (Const.EffectKindEnum)Enum.Parse(typeof(Const.EffectKindEnum), effectNode.Attributes["effectKind"].Value);
                    dto.PointKind = (Const.PointKindEnum)Enum.Parse(typeof(Const.PointKindEnum), effectNode.Attributes["pointKind"].Value);
                    ThreeStarMonster monster = (ThreeStarMonster)BuilderThreeAndFourStarMonster(
                                            typeof(ThreeStarMonster), dto, effectNode.Attributes["effectName"].Value);

                    threeStarMonsters.Add(monster);
                }
            }
            return threeStarMonsters;
        }
        /// <summary>
        /// 添加四星怪兽
        /// </summary>
        /// <returns></returns>
        public static List<CardMonster> AddfourStarMonsters()
        {
            XmlNode fourStarNode = monsterNode.ChildNodes[2];
            XmlNodeList items = fourStarNode.ChildNodes;
            for (int i = 0; i < items.Count; i++)
            {
                XmlNode effectNode = items[i].FirstChild;
                if (effectNode.Attributes != null)
                {
                    MonsterDto dto = new MonsterDto();
                    dto.Number = Convert.ToInt32(items[i].Attributes["number"].Value);
                    dto.Star = Convert.ToInt32(items[i].Attributes["star"].Value);
                    dto.Name = items[i].Attributes["name"].Value;
                    dto.Descripe = items[i].Attributes["descripe"].Value;
                    dto.Effect = items[i].Attributes["effect"].Value;
                    dto.Attack = Convert.ToInt32(items[i].Attributes["attack"].Value);
                    dto.Defense = Convert.ToInt32(items[i].Attributes["defense"].Value);
                    dto.NeedMagic = Convert.ToInt32(effectNode.Attributes["magic"].Value);
                    dto.NeedTrap = Convert.ToInt32(effectNode.Attributes["trap"].Value);
                    dto.Prop = (Const.PropEnum)Enum.Parse(typeof(Const.PropEnum), items[i].Attributes["prop"].Value);
                    dto.EffectKind = (Const.EffectKindEnum)Enum.Parse(typeof(Const.EffectKindEnum), effectNode.Attributes["effectKind"].Value);
                    dto.PointKind = (Const.PointKindEnum)Enum.Parse(typeof(Const.PointKindEnum), effectNode.Attributes["pointKind"].Value);
                    FourStarMonster monster = (FourStarMonster)BuilderThreeAndFourStarMonster(
                                            typeof(FourStarMonster),  dto,effectNode.Attributes["effectName"].Value);

                    fourStarMonsters.Add(monster);
                }
            }
            return fourStarMonsters;
        }

        /// <summary>
        /// 反射实例化二星怪兽
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        private static TwoStarMonster BuilderTwoStarMonster(MonsterDto dto)
        {
            Type t = typeof(TwoStarMonster);
            object[] objs = { dto };
            TwoStarMonster monster = (TwoStarMonster)Activator.CreateInstance(t, objs);
            return monster;
        }

        /// <summary>
        /// 反射实例化三星怪兽
        /// </summary>
        /// <param name="type"></param>
        /// <param name="dto"></param>
        /// <param name="effectName"></param>
        /// <returns></returns>
        private static CardMonster BuilderThreeAndFourStarMonster(Type type, MonsterDto dto,  string effectName)
        {
            CardMonster monster;
            Type monsterEffect = typeof (MonsterEffect);
            MethodInfo method = monsterEffect.GetMethod(effectName);
            MethodInfo canEffectMethod = monsterEffect.GetMethod("CanEffect");
            Delegate effectDelegate = Delegate.CreateDelegate(type.GetNestedType("EffectHandle"), null, method);
            Delegate canDelegate = Delegate.CreateDelegate(type.GetNestedType("CanEffectHandle"), null, canEffectMethod);
            object[] objs = { dto, effectDelegate , canDelegate };
            if(type == typeof(ThreeStarMonster))
                monster = (ThreeStarMonster)Activator.CreateInstance(type, objs);
            else
                monster = (FourStarMonster)Activator.CreateInstance(type, objs);
            return monster;
        }
    }
}