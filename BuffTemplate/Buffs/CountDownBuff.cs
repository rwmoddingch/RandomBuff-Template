using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomBuff.Core.Buff;
using RandomBuffUtils;

namespace BuffTemplate.Buffs
{

    internal class CountDownBuffData : CountableBuffData
    {
        public override BuffID ID => BuffEnums.CountDownBuff;
        public override int MaxCycleCount => 5;
    }

    internal class CountDownBuff : Buff<CountDownBuff,CountDownBuffData>
    {
        public override BuffID ID => BuffEnums.CountDownBuff;

        public CountDownBuff()
        {
            if (BuffCustom.TryGetGame(out var game))
            {
                foreach (var player in game.Players.Select(i => i.realizedCreature as Player))
                {
                    if(player == null) continue;

                    //这种更改玩家属性的方法有更好的兼容性
                    player.slugcatStats.Modify(this, PlayerUtils.Multiply, "runspeedFac", 1.5f);            //  player.slugcatStats.runspeedFac *= 1.5f;
                    player.slugcatStats.Modify(this, PlayerUtils.Multiply, "corridorClimbSpeedFac", 1.5f);  //  player.slugcatStats.corridorClimbSpeedFac *= 1.5f;
                    player.slugcatStats.Modify(this, PlayerUtils.Multiply, "poleClimbSpeedFac", 1.5f);      //  player.slugcatStats.poleClimbSpeedFac *= 1.5f;

                }
            }
        }

        public override void Destroy()
        {
            base.Destroy();

            //在删除时撤销所有对玩家属性的修改
            PlayerUtils.UndoAll(this);
        }
    }

    internal class CountDownBuffHook
    {
        public static void HookOn()
        {
            On.Player.ctor += Player_ctor;
        }

        private static void Player_ctor(On.Player.orig_ctor orig, Player self, AbstractCreature abstractCreature, World world)
        {
            orig(self, abstractCreature, world);
            if (self.isNPC) return;
            self.slugcatStats.Modify(CountDownBuff.Instance, PlayerUtils.Multiply, "runspeedFac", 1.5f);            //  player.slugcatStats.runspeedFac *= 1.5f;
            self.slugcatStats.Modify(CountDownBuff.Instance, PlayerUtils.Multiply, "corridorClimbSpeedFac", 1.5f);  //  player.slugcatStats.corridorClimbSpeedFac *= 1.5f;
            self.slugcatStats.Modify(CountDownBuff.Instance, PlayerUtils.Multiply, "poleClimbSpeedFac", 1.5f);      //  player.slugcatStats.poleClimbSpeedFac *= 1.5f;

        }
    }
}
