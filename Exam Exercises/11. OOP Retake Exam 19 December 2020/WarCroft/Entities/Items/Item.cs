namespace WarCroft.Entities.Items
{
	using Characters.Contracts;

	// Christmas came early this year - this class is already implemented for you!
	public abstract class Item
	{
		protected Item(int weight)
		{
			this.Weight = weight;
		}

		public int Weight { get; private set; }

		public virtual void AffectCharacter(Character character)
		{
			character.EnsureAlive();
		}
	}
}
