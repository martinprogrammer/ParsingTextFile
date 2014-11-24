using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ParsingTextFile
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Counting words...");
            DateTime start_at = DateTime.Now;
            TrieNode root = new TrieNode(null, '?');
            Dictionary<DataReader, Thread> readers = new Dictionary<DataReader, Thread>();

            if (args.Length == 0)
            {
                args = new string[] { @"~..\..\..\..\Books\MobyDick.txt"
                                       };
            }

            if (args.Length > 0)
            {
                foreach (string path in args)
                {
                    DataReader new_reader = new DataReader(path, ref root);
                    Thread new_thread = new Thread(new_reader.ThreadRun);
                    readers.Add(new_reader, new_thread);
                    new_thread.Start();
                }
            }

            foreach (Thread t in readers.Values) t.Join();

            DateTime stop_at = DateTime.Now;
            Console.WriteLine("Input data processed in {0} secs", new TimeSpan(stop_at.Ticks - start_at.Ticks).TotalSeconds);
            Console.WriteLine();
            Console.WriteLine("Most commonly found words:");

            List<TrieNode> top10_nodes = new List<TrieNode> { root, root, root, root, root, root, root, root, root, root };
            int distinct_word_count = 0;
            int total_word_count = 0;
            root.GetTopCounts(ref top10_nodes, ref distinct_word_count, ref total_word_count);
            top10_nodes.Reverse();
            foreach (TrieNode node in top10_nodes)
           
            {
                Console.WriteLine("{0} - {1} times", node.ToString(), node.m_word_count);
            }

            Console.WriteLine();
            Console.WriteLine("{0} words counted", total_word_count);
            Console.WriteLine("{0} distinct words found", distinct_word_count);
            Console.WriteLine();
            Console.WriteLine("done.");
            Console.Read();
        }
    }
}
