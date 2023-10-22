using System;
using System.Diagnostics;
using System.Text;

namespace Homework1
{
    internal class Program
    {
        static string text;
        public static int wordCount = 0;
        public static void AverageWordLenght()
        {
            var sb = new StringBuilder();
            foreach (var letter in text)
            {
                if (Char.IsLetter(letter))
                {
                    sb.Append(letter);
                }

            }
            Console.WriteLine($"The average word length is: {sb.Length / wordCount}");
        }
        public static void RemoveSpecialCharacters()
        {
            text = text.Replace("\n", " ");
            var sb = new StringBuilder();
            foreach (char c in text)
            {
                if ((c >= 'А' && c <= 'Я') || (c >= 'а' && c <= 'я') || c == ' ')
                {
                    sb.Append(c);
                }
            }
            text = sb.ToString();
        }

        public static void WordCount()
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            int count = 0;
            foreach (var item in words)
            {
                if (item.Length >= 3)
                {
                    count++;
                }
            }
            wordCount = count;
            Console.WriteLine($"The word count is: {count}");
        }
        public static void ShortestWord()
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string shortestWord = words[0];
            foreach (var item in words)
            {
                if (item.Length >= 3 && item.Length < shortestWord.Length)
                {
                    shortestWord = item;
                }
            }
            Console.WriteLine($"The shortest word is: {shortestWord}");
        }
        public static void LongestWord()
        {
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            string longestWord = words[0];
            foreach (var item in words)
            {
                if (item.Length >= 3 && item.Length > longestWord.Length)
                {
                    longestWord = item;
                }
            }
            Console.WriteLine($"The longest word is: {longestWord}");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("How is your book file named?");
            string filename = Console.ReadLine();
            text = File.ReadAllText(filename + ".txt");

            //Average elapsed time of the non-multithread program: 303,6ms
            //RemoveSpecialCharacters();
            //WordCount();
            //ShortestWord();
            //LongestWord();
            //AverageWordLenght();
            //LeastCommonWord();
            //MostCommonWord();

            //Average elapsed time of the multithread program: 233,8ms 
            RemoveSpecialCharacters();
            Thread t2 = new Thread(WordCount);
            t2.Start();
            Thread t3 = new Thread(ShortestWord);
            t3.Start();
            Thread t4 = new Thread(LongestWord);
            t4.Start();
            Thread t5 = new Thread(AverageWordLenght);
            t2.Join();
            t5.Start();
            Thread t6 = new Thread(LeastCommonWord);
            t6.Start();
            Thread t7 = new Thread(MostCommonWord);
            t7.Start();
            t3.Join();
            t4.Join();
            t5.Join();
            t6.Join();
            t7.Join();
        }

        private static void MostCommonWord()
        {
            string[] mwords = new string[5];
            int myNumber = int.MinValue;
            string myWord = "";
            text = text.ToLower();
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> wordcount = new Dictionary<string, int>();
            foreach (string item in words)
            {
                if (item.Length >= 3)
                {
                    if (!wordcount.ContainsKey(item))
                    {
                        wordcount.Add(item, 1);
                    }
                    else
                    {
                        wordcount[item]++;
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                foreach (var item in wordcount)
                {
                    if (item.Value > myNumber)
                    {
                        myWord = item.Key;
                        myNumber = item.Value;
                    }
                }
                myNumber = int.MinValue;
                mwords[i] = myWord;
                wordcount.Remove(myWord);
            }
            Console.WriteLine($"The five most common words are: {String.Join(", ", mwords)}");

        }

        private static void LeastCommonWord()
        {
            string[] lwords = new string[5];
            int myNumber = int.MaxValue;
            string myWord = "";
            text = text.ToLower();
            string[] words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, int> wordcount = new Dictionary<string, int>();
            foreach (string item in words)
            {
                if (item.Length >= 3)
                {
                    if (!wordcount.ContainsKey(item))
                    {
                        wordcount.Add(item, 1);
                    }
                    else
                    {
                        wordcount[item]++;
                    }
                }
            }

            for (int i = 0; i < 5; i++)
            {
                foreach (var item in wordcount)
                {
                    if (item.Value < myNumber)
                    {
                        myWord = item.Key;
                        myNumber = item.Value;
                    }
                }
                myNumber = int.MaxValue;
                lwords[i] = myWord;
                wordcount.Remove(myWord);
            }
            Console.WriteLine($"The five least common words are: {String.Join(", ", lwords)}");
        }


    }

}




