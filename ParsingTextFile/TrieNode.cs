using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParsingTextFile
{
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

        public void AddWord(string word, int index=0)
        {
            if (index < word.Length)
            {
                char key = word[index];
                if(char.IsLetter(key))
                {
                    if(!m_children.ContainsKey(key))
                    {
                        m_children.TryAdd(key, new TrieNode(this, key));
                    }
                    m_children[key].AddWord(word, index+1);
                }
                else{
                    AddWord(word, index+1);
                }
            }
            else
            {
                if(m_parent!=null)
                {
                    lock(this)
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
