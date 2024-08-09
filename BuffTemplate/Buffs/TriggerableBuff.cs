using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RandomBuff.Core.Buff;

namespace BuffTemplate.Buffs
{
    internal class TriggerableBuffData : BuffData
    {
        public override BuffID ID => BuffEnums.TriggerableBuff;
    }
    internal class TriggerableBuff : Buff<TriggerableBuff,TriggerableBuffData>
    {
        public override BuffID ID => BuffEnums.TriggerableBuff;

        //在卡牌被触发的时候调用，返回true则删除卡牌，false则不删除
        //Called when the card is triggered. Return true to delete the card, false to not delete it
        public override bool Trigger(RainWorldGame game)
        {
            var alivePlayer = game.AlivePlayers.FirstOrDefault(i => i.realizedCreature != null && i.realizedCreature.room != null);

            //没有存货玩家
            //no alive player
            if (alivePlayer == null)
                return false;

            for (int i = 0; i < 3; i++)
            {
                AbstractSpear spear =
                    new AbstractSpear(alivePlayer.world, null, alivePlayer.pos, game.GetNewID(), true);
                spear.RealizeInRoom();
            }

            return true;
        }
    }

}
