namespace WarCroft.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Constants;
    using Entities.Characters;
    using Entities.Characters.Contracts;
    using Entities.Items;

    public class WarController
    {
        private HashSet<Character> characterParty;
        private HashSet<Item> itemPool;


        public WarController()
        {
            characterParty = new HashSet<Character>();
            itemPool = new HashSet<Item>();
        }

        public string JoinParty(string[] args)
        {
            string characterType = args[0];
            string name = args[1];

            Character character = null;

            if (characterType == "Warrior")
            {
                character = new Warrior(name);
            }
            else if (characterType == "Priest")
            {
                character = new Priest(name);
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType, characterType));
            }

            characterParty.Add(character);

            return string.Format(SuccessMessages.JoinParty, name);
        }

        public string AddItemToPool(string[] args)
        {
            string itemName = args[0];

            Item item = null;

            if (itemName == "FirePotion")
            {
                item = new FirePotion();
            }
            else if (itemName == "HealthPotion")
            {
                item = new HealthPotion();
            }
            else
            {
                throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
            }
            itemPool.Add(item);

            return string.Format(SuccessMessages.AddItemToPool, itemName);
        }

        public string PickUpItem(string[] args)
        {
            string characterName = args[0];

            var character = FindCharacterInPartyIfExist(characterName);

            if (!itemPool.Any())
            {
                throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

            var item = itemPool.Last();

            itemPool.Remove(item);
            character.Bag.AddItem(item);

            return string.Format(SuccessMessages.PickUpItem, characterName, item.GetType().Name);
        }

        public string UseItem(string[] args)
        {
            string characterName = args[0];
            string itemName = args[1];

            var character = FindCharacterInPartyIfExist(characterName);

            var item = character.Bag.GetItem(itemName);

            character.UseItem(item);

            return string.Format(SuccessMessages.UsedItem, characterName, itemName);
        }

        public string GetStats()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var character in characterParty
                .OrderByDescending(ch => ch.IsAlive)
                .ThenByDescending(ch => ch.Health))
            {
                sb.AppendLine(character.ToString());
            }

            return sb.ToString().Trim();
        }

        public string Attack(string[] args)
        {
            string attackerName = args[0];
            string receiverName = args[1];

            var attacker = FindCharacterInPartyIfExist(attackerName);
            var reciever = FindCharacterInPartyIfExist(receiverName);

            if (!(attacker is IAttacker))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attackerName));
            }

            var warrior = attacker as IAttacker;
            warrior.Attack(reciever);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format(SuccessMessages.AttackCharacter, attackerName, receiverName, attacker.AbilityPoints, receiverName, reciever.Health, reciever.BaseHealth, reciever.Armor, reciever.BaseArmor));

            if (!reciever.IsAlive)
            {
                sb.AppendLine(string.Format(SuccessMessages.AttackKillsCharacter, receiverName));
            }

            return sb.ToString().Trim();
        }

        public string Heal(string[] args)
        {
            string healerName = args[0];
            string healingReceiverName = args[1];

            var healer = FindCharacterInPartyIfExist(healerName);
            var reciever = FindCharacterInPartyIfExist(healingReceiverName);

            if (!(healer is IHealer))
            {
                throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));
            }

            var healerAsHealer = (IHealer)healer;

            healerAsHealer.Heal(reciever);

            return string.Format(SuccessMessages.HealCharacter, healerName, healingReceiverName, healer.AbilityPoints, healingReceiverName, reciever.Health);
        }

        private Character FindCharacterInPartyIfExist(string name)
        {
            var character = characterParty.FirstOrDefault(ch => ch.Name == name);

            if (character == null)
            {
                throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, name));
            }

            return character;
        }
    }
}
