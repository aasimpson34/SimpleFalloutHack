using System;
using System.Linq;

namespace FalloutHackingMiniGame
{
    class Program
    {
        //Word Reader reads the words from a file
        static WordReader m_words;

        static void Main(string[] args)
        {
            Boolean shouldPlay = true;
            do
            {
                //  1.  Choose difficulty (easy = 1, medium = 2, hard = 3)
                //1.1 Output a nice message
                Console.WriteLine("Choose a difficulty to hack the console: 1 = easy, 2 = medium, 3= hard");

                //1.2 read the users input and make sure it is between 1 and 3.
                int difficulty = getValue(1, 3);

                //  2.  open enable.txt and get the difficulty words.
                //      easy = 4 letters. medium = 5 letters. hard = 6 letters
                m_words = new WordReader(difficulty);



                //  5.  start loop for the number of attempts left or the word has been found
                //  5.1 DO WHILE ( selected word position NOT EQUAL to word to hack) AND (number of tries is LESS THAN 4)
                int attempts_left = 0;
                Random random = new Random();
                char[] selected_word = m_words.words.ElementAt(random.Next(0, 7)).ToCharArray();
                int word_to_try = 0;
                int letters_found = 0;

                do
                {
                    // 5.1.1 Prompt the user to enter the position of the word
                    Console.WriteLine("Enter the position of the word to hack");
                    for (int x = 0; x < 8; x++)
                    {
                        Console.WriteLine((x + 1) + ". " + m_words.words.ElementAt(x));
                    }

                    //5.1.2 Get the input:
                    word_to_try = getValue(1, 8);
          
                    //5.1.3 Gets the users words that they selected and converts it to a char array
                    char[] user_word = m_words.words.ElementAt(word_to_try - 1).ToCharArray();
                    letters_found = 0;
                    for (int x = 0; x < user_word.Length; x++)
                    {
                        //5.1.3.1 check how many letters match
                        if (user_word[x] == selected_word[x])
                        {
                            letters_found++;
                        }
                    }

                    // 5.1.4 Output how many letters are correct
                    Console.WriteLine();
                    Console.WriteLine(letters_found + "/" + (difficulty + 3) + " letterscorrect.");
                    Console.WriteLine();
                    Console.WriteLine();

                    // 5.1.5 Increment the number of tries
                    attempts_left++;

                } while (attempts_left < 4 && letters_found != difficulty + 3);

                //  6.  display a relevant message to the current state
                if (letters_found != difficulty + 3)
                {
                    Console.WriteLine("YOU ARE LOCKED OUT.");
                }
                else if (attempts_left >= 4)
                {
                    Console.WriteLine("YOU HAVE SUCEEDED");
                }

                //7. Ask the user if they want to try hack again
                Console.WriteLine("Would you like to play again? 1= YES, 0 = NO");

                //7.1 Get the value of the input between 0 and 1
                int isPlaying = getValue(0, 1);

                //7.2 if is playing != 1 then we are not playing again
                if (isPlaying != 1)
                {
                    shouldPlay = false;
                }

                Console.Clear();
            } while (shouldPlay);
        }

    /*
     * Gets the value of the user input between two values 
     */
        public static int getValue(int from, int to)
        {
            //1. sets the value so it is not within scope
            int value = from - 1;

            //2. Sets whether the value is an int to false 
            Boolean isInt = false;

            //3. Loop while value is out of bounds and it is an integer
            do
            {
                string input = Console.ReadLine();
                isInt = int.TryParse(input, out value);
            } while (!(value >= from && value <= to) || !isInt);

            return value;
        }
    }
}
