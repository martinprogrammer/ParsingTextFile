﻿using ParsingTextFile.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParsingTextFile.Logic.Trier
{
    public static class TrierMain
    {
        public static List<WordCount> GetTrier(BookToParse book)
        {
            List<WordCount> Words = new List<WordCount>();
            TrieNode root = new TrieNode(null, '?');
            Dictionary<DataReader, Thread> readers = new Dictionary<DataReader, Thread>();


            DataReader new_reader = new DataReader(book.Path, ref root);
            Thread new_thread = new Thread(new_reader.ThreadRun);
            readers.Add(new_reader, new_thread);
            new_thread.Start();

            foreach (Thread t in readers.Values) t.Join();

            DateTime stop_at = DateTime.Now;
            
           

            //List<TrieNode> top10_nodes = new List<TrieNode> { root, root, root, root, root, root, root, root, root, root };
            List<TrieNode> top10_nodes = new List<TrieNode>();
            for(int i = 0; i< 10000; i++)
            {
                top10_nodes.Add(root);
            }
            int distinct_word_count = 0;
            int total_word_count = 0;
            root.GetTopCounts(ref top10_nodes, ref distinct_word_count, ref total_word_count);
            top10_nodes.Reverse();
            foreach (TrieNode node in top10_nodes)
            {
                Words.Add(new WordCount { Word = node.ToString(), Count = node.m_word_count });
            }

            return Words;
        }
    }

    

    public class DataReader
    {
        static int LOOP_COUNT = 1;
        private TrieNode m_root;
        private string m_path;

        public DataReader(string path, ref TrieNode root)
        {
            m_root = root;
            m_path = path;
        }

        public void ThreadRun()
        {
            for (int i = 0; i < LOOP_COUNT; i++) // fake large data set buy parsing smaller file multiple times
            {
                using (FileStream fstream = new FileStream(m_path, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader sreader = new StreamReader(fstream))
                    {
                        string line;
                        while ((line = sreader.ReadLine()) != null)
                        {
                            string[] chunks = line.Split(null);
                            foreach (string chunk in chunks)
                            {
                                m_root.AddWord(chunk.Trim());
                            }
                        }
                    }
                }
            }
        }
    }

    
    public class TrieNode : IComparable<TrieNode>
    {
        private char m_char;
        public int m_word_count;
        private TrieNode m_parent = null;
        private ConcurrentDictionary<char, TrieNode> m_children = null;

        public TrieNode(TrieNode parent, char c)
        {
            m_char = c;
            m_word_count = 0;
            m_parent = parent;
            m_children = new ConcurrentDictionary<char, TrieNode>();
        }

        public void AddWord(string word, int index = 0)
        {
            if (index < word.Length)
            {
                char key = word[index];
                if (char.IsLetter(key)) // should do that during parsing but we're just playing here! right?
                {
                    if (!m_children.ContainsKey(key))
                    {
                        m_children.TryAdd(key, new TrieNode(this, key));
                    }
                    m_children[key].AddWord(word, index + 1);
                }
                else
                {
                    // not a letter! retry with next char
                    AddWord(word, index + 1);
                }
            }
            else
            {
                if (m_parent != null) // empty words should never be counted
                {
                    lock (this)
                    {
                        m_word_count++;
                    }
                }
            }
        }

        public int GetCount(string word, int index = 0)
        {
            if (index < word.Length)
            {
                char key = word[index];
                if (!m_children.ContainsKey(key))
                {
                    return -1;
                }
                return m_children[key].GetCount(word, index + 1);
            }
            else
            {
                return m_word_count;
            }
        }

        public void GetTopCounts(ref List<TrieNode> most_counted, ref int distinct_word_count, ref int total_word_count)
        {
            if (m_word_count > 0)
            {
                distinct_word_count++;
                total_word_count += m_word_count;
            }
            if (m_word_count > most_counted[0].m_word_count)
            {
                most_counted[0] = this;
                most_counted.Sort();
            }
            foreach (char key in m_children.Keys)
            {
                m_children[key].GetTopCounts(ref most_counted, ref distinct_word_count, ref total_word_count);
            }
        }

        public override string ToString()
        {
            if (m_parent == null) return "";
            else return m_parent.ToString() + m_char;
        }

        public int CompareTo(TrieNode other)
        {
            return this.m_word_count.CompareTo(other.m_word_count);
        }
    }
}