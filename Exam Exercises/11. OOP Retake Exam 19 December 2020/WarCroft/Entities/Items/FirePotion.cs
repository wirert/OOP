namespace WarCroft.Entities.Items
{
    using Characters.Contracts;

    public class FirePotion : Item
    {
        private const int DEFAULT_WEIGHT = 5;

        public FirePotion() : base(DEFAULT_WEIGHT)
        {
        }

        public override void AffectCharacter(Character character)
        {
            base.AffectCharacter(character);
            character.Health -= 20;
        }
    }
}
