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
// This implementation does not pay attention to most punctuation symbols when doing translations.
namespace Translate_Morse_Code_English
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    internal enum MorseCodeSymbol { SHORT, LONG, SHORTBREAK, MEDIUMBREAK, LONGBREAK };

    static class Translate
    {
       
        static List<MorseCodeSymbol> englishToMorseCode(string message)
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
            return morseCodeFinalOutput;
        }

        static string morseCodeToEnglish(List<MorseCodeSymbol> message)
        {
            // you know it is a new letter if there is a medium break or a long break
            StringBuilder outputMessage = new StringBuilder();
            //.append to add data ???????

            // the symbols for a given character will go into this list
            List<MorseCodeSymbol> morseCodeCharacter = new List<MorseCodeSymbol>();
            char chr;

            foreach (var morseCodeSymbol in message)
            {
                if (morseCodeSymbol == MorseCodeSymbol.MEDIUMBREAK || morseCodeSymbol == MorseCodeSymbol.LONGBREAK)
                {
                    if (morseCodeToEnglishLookup.TryGetValue(morseCodeCharacter, out chr))
                    {
                        outputMessage.Append(chr);
                    }
                    else
                    {
                        // do nothing; something strange happened
                    }

                    morseCodeCharacter.Clear();
                }
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

        static Dictionary<List<MorseCodeSymbol>, char> morseCodeToEnglishLookup = new Dictionary<List<MorseCodeSymbol>, char>
        {
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'a' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'b' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'c' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'd' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT }, 'e' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'f' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'g' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'h' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'i' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'j' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'k' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'l' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'm' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'n' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'o' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'p' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'q' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'r' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 's' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG }, 't' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'u' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'v' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'w' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'x' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, 'y' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, 'z' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, '1' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, '2' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, '3' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, '4' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, '5' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, '6' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, '7' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, '8' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, '9' },
            { new List<MorseCodeSymbol> {  MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, '0' }
            { new List<MorseCodeSymbol> { MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, '\'' },
            { new List<MorseCodeSymbol> { MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, '-' },
            { new List<MorseCodeSymbol> { MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG }, '.' },
            { new List<MorseCodeSymbol> { MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, '?' },
            { new List<MorseCodeSymbol> { MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.LONG, MorseCodeSymbol.SHORTBREAK, MorseCodeSymbol.SHORT }, '!' }
        };
    }

}
