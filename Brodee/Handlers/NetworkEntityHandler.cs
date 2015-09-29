﻿//using System;
//using System.Collections.Generic;
//using Brodee.Triggers;
//using PegasusGame;
//using UnityEngine;

//namespace Brodee
//{

//    public class NetworkEntityHandler : Releaser
//    {
//        private GameObject _parent;

//        public override HandlerType GetHandlerType => HandlerType.NetworkEntity;

//        private readonly Queue<string> _cardIdsReceived = new Queue<string>();

//        public override void Setup(GameObject parent)
//        {
//            _parent = parent;
//            Network.Get().RegisterNetHandler(PowerHistory.PacketID.ID, OnPowerHistory);
//        }

//        public override Trigger[] SpecificHandle(GameState previous, GameState next)
//        {
//            var trigger = new NetworkEntityTrigger();
//            var cardList = new List<string>();
//            while (_cardIdsReceived.Count > 0)
//            {
//                var cardId = _cardIdsReceived.Dequeue();
//                cardList.Add(cardId);
//            }
//            if (cardList.Count > 0)
//            {
//                trigger.CardIds = cardList.ToArray();
//                return new Trigger[] { trigger };
//            }
//            return EmptyTriggers;
//        }

//        public static bool IsRecordableCard(Entity entity)
//        {
//            var tagSet = entity.GetTags();
//            int tagValue;
//            if (tagSet.TryGetValue((int)GAME_TAG.CANT_PLAY, out tagValue))
//                return false;
//            if (tagSet.TryGetValue((int)GAME_TAG.REVEALED, out tagValue) && tagValue == 0)
//                return false;
//            if (tagSet.TryGetValue((int)GAME_TAG.CARDTYPE, out tagValue))
//            {
//                var cardType = (TAG_CARDTYPE)tagValue;
//                return cardType != TAG_CARDTYPE.HERO && cardType != TAG_CARDTYPE.HERO_POWER;
//            }
//            return true;
//        }

//        private void OnPowerHistory()
//        {
//            try
//            {
//                ConnectAPI.GetPowerHistory().ForEach(history =>
//                {
//                    if (history.Type == Network.PowerType.FULL_ENTITY)
//                    {
//                        var fullEnt = (Network.HistFullEntity)history;
//                        var newEntity = new Entity();
//                        newEntity.InitEntity(fullEnt.Entity);
//                        Logger.AppendLine($"OnPowerHistory - HistFullEntity:{newEntity}");
//                        foreach (var tag in fullEnt.Entity.Tags)
//                        {
//                            Logger.AppendLine($"    {Tags.DebugTag(tag.Name, tag.Value)}");
//                        }
//                        if (IsRecordableCard(newEntity))
//                        {
//                            _cardIdsReceived.Enqueue(fullEnt.Entity.CardID);
//                        }
//                    }
//                    else if (history.Type == Network.PowerType.SHOW_ENTITY)
//                    {
//                        var showEnt = (Network.HistShowEntity)history;
//                        var newEntity = new Entity();
//                        newEntity.InitEntity(showEnt.Entity);
//                        Logger.AppendLine($"OnPowerHistory - HistShowEntity:{newEntity}");
//                        foreach (var tag in showEnt.Entity.Tags)
//                        {
//                            Logger.AppendLine($"    {Tags.DebugTag(tag.Name, tag.Value)}");
//                        }
//                        if (IsRecordableCard(newEntity))
//                        {
//                            _cardIdsReceived.Enqueue(showEnt.Entity.CardID);
//                        }
//                    }
//                });
//            }
//            catch (Exception e)
//            {
//                Logger.AppendLine(e.Message);
//                Logger.AppendLine(Environment.NewLine);
//                Logger.AppendLine(e.StackTrace);
//                Logger.AppendLine(Environment.NewLine);
//            }
//        }
//    }

//    public class Releaser
//    {
//    }
//}