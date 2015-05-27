using System.IO;
using NUnit.Framework;
using UglyTrivia;

namespace Trivia.Tests
{
    [TestFixture]
    public class GameTest
    {
        [Test]
        public void Test1() {
        var outputStringWriter = new StringWriter { NewLine = "\n" };
        Game aGame = new Game(outputStringWriter);
        aGame.add("Chet");
        aGame.wrongAnswer();
        Assert.AreEqual(outputStringWriter.ToString(), "Chet was added\n" +
                "They are player number 1\n" +
                "Question was incorrectly answered\n" +
                "Chet was sent to the penalty box\n");
    }
    }
}


