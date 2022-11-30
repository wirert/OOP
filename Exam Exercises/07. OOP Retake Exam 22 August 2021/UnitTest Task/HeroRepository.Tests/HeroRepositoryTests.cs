using System;
using NUnit.Framework;

[TestFixture]
public class HeroRepositoryTests
{
    private Hero hero;
    private HeroRepository repo;
    private string heroName;
    private int level;

    [SetUp]
    public void SetUp()
    {
        repo = new HeroRepository();
        heroName = "test";
        level = 5;
        hero = new Hero(heroName, level);
    }

    [Test]
    public void Ctor_Should_Initialize_ListOfHero()
    {
        Assert.IsNotNull(repo.Heroes);
    }

    [Test]
    public void CreateShouldAddHeroToCollection()
    { 
        string expectedOutputString = $"Successfully added hero {this.heroName} with level {level}";

        Assert.AreEqual(expectedOutputString, repo.Create(hero));
        Assert.AreEqual(1, repo.Heroes.Count);
    }

    [Test]
    public void CreateShouldThrowIfHeroIsNull()
    {
        hero = null;

        Assert.Throws<ArgumentNullException>(() => repo.Create(hero));
    }

    [Test]
    public void CreateShouldThrowIfHeroNameDuplicate()
    {
        repo.Create(hero);
        Hero hero2 = new Hero(heroName, 10);

        Assert.Throws<InvalidOperationException>(() => repo.Create(hero2));
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("    ")]
    public void RemoveShouldThrowIfNameIsNullOrWhiteSpace(string name)
    {
        repo.Create(hero);

        Assert.Throws<ArgumentNullException>(() => repo.Remove(name));
    }

    [Test]
    public void RemoveReturnBool()
    {
        repo.Create(hero);

        Assert.IsTrue(repo.Remove(this.heroName));
        Assert.AreEqual(0, repo.Heroes.Count);

        Assert.IsFalse(repo.Remove(this.heroName));
    }

    [Test]
    public void GetHeroWithHighestLevel_Test()
    {
        repo.Create(hero);
        var hero1 = new Hero("1", 10);
        var hero2 = new Hero("2", 100);
        var hero3 = new Hero("3", 1);

        repo.Create(hero1);
        repo.Create(hero2);
        repo.Create(hero3);

        Assert.AreEqual(hero2, repo.GetHeroWithHighestLevel());        
    }

    [Test]
    public void GetHeroReturnFirstOrDefaultHeroWithGivenName()
    {
        repo.Create(hero);
        repo.Create(new Hero("1", 10));

        Assert.AreEqual(hero, repo.GetHero(heroName));
        Assert.IsNull(repo.GetHero("InvalidName"));
    }
}