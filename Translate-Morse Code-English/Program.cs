using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// International Morse Code rules:
// 1) The length of a short is one unit.
// 2) The length of a long is three units.
// 3) The space between parts of the same letter is one unit.
// 4) The space between letters is three units.
// 5) The space between words is seven units.
// This implementation does not pay attention to some punctuation symbols when doing translations.
// This implementation doesn't work with sound files
namespace Translate_Morse_Code_English
{
    class Program
    {
        static void Main(string[] args)
        {
            string testString = Translate.morseCodeToEnglish(Translate.englishToMorseCode("SOS SOS.  hello world!  This is a test"));
            Console.WriteLine("Converted the following string to Morse Code and back to English:");
            Console.WriteLine("SOS SOS.  hello world!  This is a test");
            Console.WriteLine("Returned: " + testString);
            Console.WriteLine();
            Console.WriteLine("Press a key to hear that message in Morse Code");
            List <MorseCodeSymbol> morseCodeData = Translate.englishToMorseCode("SOS SOS.  hello world!  This is a test");
            Console.ReadKey();
            Translate.playMorseCode(morseCodeData);
            Console.ReadKey();
        }
    }

    public enum MorseCodeSymbol { SHORT, LONG, SHORTBREAK, MEDIUMBREAK, LONGBREAK };

    public static class Translate
    {
       
        public static List<MorseCodeSymbol> englishToMorseCode(string message)
        {
            // set up the fields needed
            message = message.ToLower();
            List<MorseCodeSymbol> morseCodeFinalOutput = new List<MorseCodeSymbol>();
            List<MorseCodeSymbol> morseCodeCharacter = new List<MorseCodeSymbol>();

            // this field helps determine pause lengths
            bool isNewWord = false;

            foreach (char chr in message)
            {
                // If the key is in the dictionary do a conversion to morse code
                if (englishToMorseCodeLookup.TryGetValue(chr, out morseCodeCharacter))
                {
                    if (isNewWord)
                        morseCodeFinalOutput.Add(MorseCodeSymbol.LONGBREAK);
                    else
                        morseCodeFinalOutput.Add(MorseCodeSymbol.MEDIUMBREAK);

                    // quick solution; could be wrong:
                    morseCodeFinalOutput.AddRange(morseCodeCharacter);
                    isNewWord = false;
                }
                // Otherwise, don't do a conversion and set a boolean flag saying we've reached a new word
                else
                {
                    isNewWord = true;
                }
            }
            morseCodeFinalOutput.RemoveAt(0); // remove the medium break given in the first loop iteration
            morseCodeFinalOutput.Add(MorseCodeSymbol.MEDIUMBREAK); // may as well add a long break, will be useful later.
            return morseCodeFinalOutput;
        }

        public static string morseCodeToEnglish(List<MorseCodeSymbol> message)
        {
            // you know it is a new letter if there is a medium break or a long break
            StringBuilder outputMessage = new StringBuilder();

            // the symbols for a given character will go into this list
            string morseCodeCharacter = "";
            char chr;

            foreach (var morseCodeSymbol in message)
            {
                // if a medium or long break then we need are ready to determine the next letter
                if (morseCodeSymbol == MorseCodeSymbol.MEDIUMBREAK || morseCodeSymbol == MorseCodeSymbol.LONGBREAK)
                {
                    // try to determine the letter
                    if (morseCodeToEnglishLookup.TryGetValue(morseCodeCharacter, out chr))
                    {
                        // update the StringBuilder
                        outputMessage.Append(chr);
                    }
                    // failed to find the letter in a dictionary
                    else
                    {
                        // do nothing.  Might add something here later.
                    }
                    // Finally clear this string, because we've handled the character
                    morseCodeCharacter = "";
                }
                // We do this step if it isn't a medium or long break.
                else
                {
                    // We add the symbol to this temporary list and continue the loop; 
                    morseCodeCharacter = morseCodeCharacter + ", " + morseCodeSymbol.ToString();
                }
                // add a space if there is a long space
                if (morseCodeSymbol == MorseCodeSymbol.LONGBREAK)
                    outputMessage.Append(" ");
            }

            // Finished the morse code message
            return outputMessage.ToString(); ;
        }

        public static void playMorseCode(List<MorseCodeSymbol> message)
        {
            int freq = 500;
            int duration = 90;  // one unit

            foreach (MorseCodeSymbol symbol in message)
            {
                
                if (symbol == MorseCodeSymbol.SHORT)
                    Console.Beep(freq, duration);
                if (symbol == MorseCodeSymbol.LONG)
                    Console.Beep(freq, duration * 3);
                if (symbol == MorseCodeSymbol.SHORTBREAK)
                    System.Threading.Thread.Sleep(duration);
                if (symbol == MorseCodeSymbol.MEDIUMBREAK)
                    System.Threading.Thread.Sleep(duration * 3);
                if (symbol == MorseCodeSymbol.LONGBREAK)
                    System.Threading.Thread.Sleep(duration * 7);
            }
        }

        static Dictionary<char, List<MorseCodeSymbol>> englishToMorseCodeLookup = new Dictionary<char, List<MorseCodeSymbol>>
        {
            { 'a', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'b', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'c', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'd', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'e', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT } },
            { 'f', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'g', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'h', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'i', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'j', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'k', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'l', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'm', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'n', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'o', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'p', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 'q', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'r', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 's', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { 't', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG } },
            { 'u', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'v', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'w', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'x', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'y', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { 'z', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { '1', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { '2', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { '3', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { '4', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { '5', new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { '6', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { '7', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { '8', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { '9', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { '0', new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { '\'', new List<MorseCodeSymbol> { MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { '-', new List<MorseCodeSymbol> { MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { '.', new List<MorseCodeSymbol> { MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG } },
            { '?', new List<MorseCodeSymbol> { MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } },
            { '!', new List<MorseCodeSymbol> { MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT } }
        };

        static Dictionary<string, char> morseCodeToEnglishLookup = new Dictionary<string, char>
        {
            { ", SHORT, SHORTBREAK, LONG", 'a' },
            { ", LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT", 'b' },
            { ", LONG, SHORTBREAK, SHORT, SHORTBREAK, LONG, SHORTBREAK, SHORT", 'c' },
            { ", LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT", 'd' },
            { ", SHORT", 'e' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, LONG, SHORTBREAK, SHORT", 'f' },
            { ", LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT", 'g' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT", 'h' },
            { ", SHORT, SHORTBREAK, SHORT", 'i' },
            { ", SHORT, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, LONG", 'j' },
            { ", LONG, SHORTBREAK, SHORT, SHORTBREAK, LONG", 'k' },
            { ", SHORT, SHORTBREAK, LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT", 'l' },
            { ", LONG, SHORTBREAK, LONG", 'm' },
            { ", LONG, SHORTBREAK, SHORT", 'n' },
            { ", LONG, SHORTBREAK, LONG, SHORTBREAK, LONG", 'o' },
            { ", SHORT, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT", 'p' },
            { ", LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT, SHORTBREAK, LONG", 'q' },
            { ", SHORT, SHORTBREAK, LONG, SHORTBREAK, SHORT", 'r' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT", 's' },
            { ", LONG", 't' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, LONG", 'u' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, LONG", 'v' },
            { ", SHORT, SHORTBREAK, LONG, SHORTBREAK, LONG", 'w' },
            { ", LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, LONG", 'x' },
            { ", LONG, SHORTBREAK, SHORT, SHORTBREAK, LONG, SHORTBREAK, LONG", 'y' },
            { ", LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT", 'z' },
            { ", SHORT, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, LONG", '1' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, LONG", '2' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, LONG, SHORTBREAK, LONG", '3' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, LONG", '4' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT", '5' },
            { ", LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT", '6' },
            { ", LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT", '7' },
            { ", LONG, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT", '8' },
            { ", LONG, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT", '9' },
            { ", LONG, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, LONG", '0' },
            { ", SHORT, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT", '\'' },
            { ", LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, SHORT, SHORTBREAK, LONG", '-' },
            { ", SHORT, SHORTBREAK, LONG, SHORTBREAK, SHORT, SHORTBREAK, LONG, SHORTBREAK, SHORT, SHORTBREAK, LONG", '.' },
            { ", SHORT, SHORTBREAK, SHORT, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT, SHORTBREAK, SHORT", '?' },
            { ", LONG, SHORTBREAK, LONG, SHORTBREAK, LONG, SHORTBREAK, SHORT", '!' }
        };
    }

}
