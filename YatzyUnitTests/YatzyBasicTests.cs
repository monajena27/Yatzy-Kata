using System.Collections.Generic;
using Xunit;
using YatzyKata;
using Moq;

namespace YatzyUnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void TestIfKeepNumPutsNumbersSpecifiedByUserInAnArray()
        {
            //write a test for 1 index
            List<int> player1DiceList = new List<int>() {3, 4, 5, 5, 2};
            List<int> player2DiceList = new List<int>() {5, 2, 3, 2, 1};
            var consoleActionsMock = new Mock<IConsole>();
            consoleActionsMock.Setup(s => s.ReadLine())
                .Returns("1,2,3"); //fake readline
            string[] expected = {"1", "2", "3"};
            YatzyGame player = new YatzyGame(consoleActionsMock.Object, new Rng(), player1DiceList, player2DiceList);
            string[] result = player.GetIndexesUserWantsToKeep();
            Assert.Equal(expected, result);
        }
        
        
        [Fact]
        public void TestIfGetIndexesToKeepReturnsKeepNumAsInt()
        {
            List<int> player1DiceList = new List<int>() {3, 4, 5, 5, 2};
            List<int> player2DiceList = new List<int>() {5, 2, 3, 2, 1};
            string[] eachNumToKeep = {"1", "2", "3"};
            int[] expected = {1,2,3};
            YatzyGame player = new YatzyGame(new ConsoleActions(), new Rng(), player1DiceList, player2DiceList);
            List<int> result = player.ConvertUserStringToInt(eachNumToKeep);
            Assert.Equal(expected, result);
        }
        

        [Fact]
        public void TestToSeeIfCalculateSumReturnsSum()
        {
            List<int> player1DiceList = new List<int>() {3, 4, 5, 5, 2};
            List<int> player2DiceList = new List<int>() {5, 2, 3, 2, 1};
            int expected = 19;
            YatzyGame player = new YatzyGame(new ConsoleActions(), new Rng(), player1DiceList, player2DiceList);
            int result = player.CalculateSum(player1DiceList);
            Assert.Equal(expected, result);
        }


        [Fact]
        public void TestIfCreateListRollsDiceForIndexesNotSpecifiedByUser()
        {
            List<int> player1DiceList = new List<int>() {3, 4, 5, 5, 2};
            List<int> player2DiceList = new List<int>() {5, 2, 3, 2, 1};
            var rngMock = new Mock<IRandom>();
            rngMock.SetupSequence(s => s.Next())
                .Returns(4) //what random num should .Next() return 
                .Returns(5)
                .Returns(3);
            
            List<int> fiveNumbers = new List<int>() {3, 6, 2, 1, 5};
            List<int> keepIndexes = new List<int>() {3, 5};
            List<int> expected = new List<int>() {4, 5, 2, 3, 5};
            
            YatzyGame player = new YatzyGame(new ConsoleActions(), rngMock.Object, player1DiceList, player2DiceList); //pass in Rng Object
            List<int> result = player.Reroll(fiveNumbers, keepIndexes);
            Assert.Equal(expected, result);
            
        }
    }


}