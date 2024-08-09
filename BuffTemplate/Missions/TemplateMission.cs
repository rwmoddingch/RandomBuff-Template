using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomBuff.Core.Buff;
using RandomBuff.Core.Entry;
using RandomBuff.Core.Game.Settings;
using RandomBuff.Core.Game.Settings.Conditions;
using RandomBuff.Core.Game.Settings.Missions;
using UnityEngine;

namespace BuffTemplate.Missions
{
    internal class TemplateMission : Mission, IMissionEntry
    {
        public override MissionID ID => BuffEnums.TemplateMission;

        // 如果BindSlug == null则为通用使命，反之则为专属使命
        // 注意：每一只猫只能有一个专属使命
        // If BindSlug == null, it is a universal mission; otherwise, it is a specific mission.
        // Note: Each cat can have only one specific mission.
        public override SlugcatStats.Name BindSlug => null;
        public override Color TextCol => Color.yellow;
        public override string MissionName => "TEMPLATE MISSION";


        public TemplateMission()
        {
            gameSetting = new GameSetting(BindSlug)
            {
                // 添加条件
                // add conditions
                conditions = new List<Condition>()
                {
                    new CardCondition() { type = BuffType.Positive, needCard = 50 },
                    new AchievementCondition() { achievementID = WinState.EndgameID.Chieftain }
                }
            };

            // 添加初始卡牌
            // Add initial cards.
            startBuffSet.Add(BuffEnums.CountDownBuff);
            startBuffSet.Add(BuffEnums.SimpleBuff);
            startBuffSet.Add(BuffEnums.TriggerableBuff);
            startBuffSet.Add(BuffEnums.StackableBuff);
        }

        public void RegisterMission()
        {
            // 注意：使命注册一定要在RegisterMission函数中进行
            // Note: Mission registration must be done within the RegisterMission function.
            BuffRegister.RegisterMission(BuffEnums.TemplateMission, new TemplateMission());

            // 注意：不能使用this
            // 以下是错误示范
            // Note: Do not use `this`
            // The following is an incorrect example.
            //BuffRegister.RegisterMission(BuffEnums.TemplateMission, this);

        }
    }
}
