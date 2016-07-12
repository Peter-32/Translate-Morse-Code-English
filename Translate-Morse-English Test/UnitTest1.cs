using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Translate_Morse_Code_English;

namespace Translate_Morse_English_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Assemble
            string testString;

            // Act
            testString = Translate.morseCodeToEnglish(Translate.englishToMorseCode("hello world!  This is a test"));

            // Assert
            Assert.AreEqual(testString, "hello world! this is a test");

        }
    }
}
