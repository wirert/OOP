namespace Heroes.Utilities.Messages
{
    public static class ExceptionMessages
    {
        public const string InvalidWeaponName = "Weapon type cannot be null or empty.";
        public const string InvalidWeaponDurability = "Durability cannot be below 0.";
        public const string InvalidHeroName = "Hero name cannot be null or empty.";
        public const string InvalidHeroHealth = "Hero health cannot be below 0.";
        public const string InvalidHeroArmour = "Hero armour cannot be below 0.";
        public const string InvalidWeapon = "Weapon cannot be null.";
        public const string HeroNameAlreadyExist = "The hero {0} already exists.";
        public const string InvalidHeroType = "Invalid hero type.";
        public const string WeaponAlreadyExist = "The weapon {0} already exists.";
        public const string InvalidWeaponType = "Invalid weapon type.";
        public const string HeroDoesNotExist = "Hero {0} does not exist.";
        public const string WeaponDoesNotExist = "Weapon {0} does not exist.";
        public const string HeroAlreadyHaveWeapon = "Hero {0} is well-armed.";
    }
}
