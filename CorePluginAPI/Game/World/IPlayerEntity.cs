﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using QuantumCore.API.Core.Models;
using QuantumCore.API.Game.Types;

namespace QuantumCore.API.Game.World
{
    public interface IPlayerEntity : IEntity
    {
        string Name { get; }
        IGameConnection Connection { get; }
        PlayerData Player { get; }
        IInventory Inventory { get; }
        IEntity Target { get; set; }
        IList<Guid> Groups { get; }
        IShop Shop { get; set; }
        IQuickSlotBar QuickSlotBar { get; }
        IQuest CurrentQuest { get; set; }
        Dictionary<string, IQuest> Quests { get; }
        EAntiFlags AntiFlagClass { get; }
        EAntiFlags AntiFlagGender { get; }

        Task Load();
        T GetQuestInstance<T>() where T : IQuest;
        void Respawn(bool town);
        uint CalculateAttackDamage(uint baseDamage);
        uint GetHitRate();
        void AddPoint(EPoints point, int value);
        void SetPoint(EPoints point, uint value);
        void DropItem(ItemInstance item, byte count);
        void Pickup(IGroundItem groundItem);
        void DropGold(uint amount);
        ItemInstance GetItem(byte window, ushort position);
        bool IsSpaceAvailable(ItemInstance item, byte window, ushort position);
        bool IsEquippable(ItemInstance item);
        bool DestroyItem(ItemInstance item);
        void RemoveItem(ItemInstance item);
        void SetItem(ItemInstance item, byte window, ushort position);
        void SendBasicData();
        void SendPoints();
        void SendInventory();
        void SendItem(ItemInstance item);
        void SendRemoveItem(byte window, ushort position);
        void SendCharacter(IConnection connection);
        void SendCharacterAdditional(IConnection connection);
        void SendCharacterUpdate();
        void SendChatMessage(string message);
        void SendChatCommand(string message);
        void SendChatInfo(string message);
        void SendTarget();
        void Show(IConnection connection);
        void Disconnect();
        string ToString();
    }
}
