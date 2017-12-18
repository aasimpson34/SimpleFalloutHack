using System;
using System.Collections.Generic;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FalloutHackingMiniGame
{
    class WordReader
    {

        public List<string> words = new List<string>();

        public WordReader(int difficulty)
        {
            //1. Sets the difficulty and the word length the reader will find
            int word_length = difficulty + 3;
            int currentWordCount = 0;

            //1.X (ADDED AFTER) A word is only selected at a 10% chance
            Random random_generator = new Random();

            //2. Read the file enable.txt from the directory location in the bin folder
            string filePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Data\enable1.txt");
            while (currentWordCount < 8)
            {
                using (var mappedFile1 = MemoryMappedFile.CreateFromFile(filePath))
                {
                    using (Stream mmStream = mappedFile1.CreateViewStream())
                    {
                        using (StreamReader sr = new StreamReader(mmStream, ASCIIEncoding.ASCII))
                        {
                            //2.1 Loop until the end of the stream
                            while (!sr.EndOfStream)
                            {

                                //2.1.1 Read the line and ensure it is split into a word 
                                var line = sr.ReadLine();
                                var lineWords = line.Split(' ');

                                //2.1.2 If the word is the same length as the difficulty then we increment the current word count and add it to the list
                                //      that we are about to use
                                if (lineWords[0].Length == word_length)
                                {
                                    if(random_generator.Next(0,5000*difficulty) < 5)
                                    {

                                    
                                        currentWordCount++;
                                        words.Add(lineWords[0]);
                                        if (currentWordCount > 7)
                                        {
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
