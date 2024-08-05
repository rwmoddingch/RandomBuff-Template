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
        public override SlugcatStats.Name BindSlug => null;
        public override Color TextCol => Color.yellow;
        public override string MissionName => "TEMPLATE MISSION";


        public TemplateMission()
        {
            gameSetting = new GameSetting(BindSlug)
            {
                conditions = new List<Condition>()
                {
                    new CardCondition() { type = BuffType.Positive, needCard = 50 },
                    new AchievementCondition() { achievementID = WinState.EndgameID.Chieftain }
                }
            };
            startBuffSet.Add(BuffEnums.CountDownBuff);
            startBuffSet.Add(BuffEnums.SimpleBuff);
            startBuffSet.Add(BuffEnums.TriggerableBuff);
            startBuffSet.Add(BuffEnums.StackableBuff);
        }

        public void RegisterMission()
        {
            BuffRegister.RegisterMission(BuffEnums.TemplateMission, new TemplateMission());

        }
    }
}
