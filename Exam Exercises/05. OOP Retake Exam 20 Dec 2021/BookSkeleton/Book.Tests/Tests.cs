namespace Book.Tests
{
    using System;

    using NUnit.Framework;
    using static System.Net.Mime.MediaTypeNames;

    [TestFixture]
    public class Tests
    {
        private Book book;
        string bookName;
        string author;


        [SetUp]
        public void SetUp()
        {
            bookName = "Test Name";
            author = "Test Author";
            book = new Book(bookName, author);
        }

        [Test]
        public void Ctor_Test_Set_Values()
        {
            Assert.AreEqual(bookName, book.BookName);
            Assert.AreEqual(author, book.Author);
            Assert.AreEqual(0, book.FootnoteCount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void BookNameThrowIfNullOrEmpty(string name)
        {
            Assert.Throws<ArgumentException>(() => new Book(name, author));
        }

        [TestCase(null)]
        [TestCase("")]
        public void AuthorThrowIfNullOrEmpty(string wrongAuthor)
        {
            Assert.Throws<ArgumentException>(() => new Book(bookName, wrongAuthor));
        }

        [TestCase("   ")]
        [TestCase("1")]
        [TestCase("a    dasfdaf dfadas               fasdfdasdas")]
        public void AuthorShouldSetValue(string author)
        {
            Book testBook = new Book(bookName, author);

            Assert.AreEqual(author, testBook.Author);
        }

        [Test]
        public void AddFootnoteTest()
        {
            int noteNumber = 1;
            book.AddFootnote(noteNumber, "Note");
            Assert.AreEqual(1, book.FootnoteCount);
            Assert.Throws<InvalidOperationException>(() => book.AddFootnote(noteNumber, "AnotherNote"));
        }

        [Test]
        public void FindFootnoteTest()
        {
            int noteNumber = 1;
            string noteText = "Note";
            book.AddFootnote(noteNumber, noteText);
            book.AddFootnote(2, "Another Note");
            book.AddFootnote(3, "Another Note");

            string expectedOutput = $"Footnote #{noteNumber}: {noteText}";
            Assert.AreEqual(expectedOutput, book.FindFootnote(noteNumber));
            int invalidNoteNumber = 55;
            Assert.Throws<InvalidOperationException>(() => book.FindFootnote(invalidNoteNumber));
        }

        [Test]
        public void AlterFootnoteShouldChangeNoteText()
        {
            int noteNumber = 1;
            string noteText = "Note";
            book.AddFootnote(noteNumber, noteText);
            string oldNote = book.FindFootnote(noteNumber);
            string newText = "NewNote";
            book.AlterFootnote(noteNumber, newText);
            string newNote = book.FindFootnote(noteNumber);

            Assert.AreNotEqual(oldNote, newNote);
        }

        [Test]
        public void AlterFootnoteThrowWhenNoteNumberNotExist()
        {
            int noteNumber = 1;
            string noteText = "Note";
            book.AddFootnote(noteNumber, noteText);
            int notExistNumberNote = 55;
            Assert.Throws<InvalidOperationException>(() => book.AlterFootnote(notExistNumberNote, noteText));
        }
    }
}