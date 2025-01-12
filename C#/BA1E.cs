﻿using System;
using System.Collections.Generic;

namespace BA1E
{
    class Program
    {
        //A solution to a ROSALIND bioinformatics problem.
        //Problem Title: Find Patterns Forming Clumps in a String
        //Rosalind ID: BA1E
        //URL: http://rosalind.info/problems/ba1e/
        static void Main(string[] args)
        {
            string kmer(string text, int i, int k)
            {
                //substring of text from i-th position for the next k letters
                return text.Substring(i, k);
            }

            Dictionary<string, int> kmersfrequency(string text, int k)
            {
                Dictionary<string, int> D = new Dictionary<string, int>();
                for (int i = 0; i < text.Length - k + 1; i++)
                {
                    string tmp = kmer(text, i, k);
                    try
                    {
                        D[tmp] = D[tmp] + 1;
                    }
                    catch (KeyNotFoundException) //ne postoji taj kljuc, tj. prvi put se pojavljuje ta rijec u tekstu
                    {

                        D[tmp] = 1;
                    }
                }
                return D;
            }
            
            List<string> kmerfrequencyatleast(Dictionary<string,int> dictionaryoffrequency,int t)
            {
                //search for kmers with frequency at least t
                List<string> clumpsinwindow = new List<string>();
                foreach (string x in dictionaryoffrequency.Keys)
                {
                    if (dictionaryoffrequency[x]>=t)
                    {
                        clumpsinwindow.Add(x);
                    }
                }
                return clumpsinwindow;

            }

            List<string> Lwindows(string text,int L)
            {
                //list of all L-windows in text
                List<string> windows = new List<string>();
                for (int i = 0; i < text.Length-L+1; i++)
                {
                    windows.Add(kmer(text, i, L));
                }
                return windows;

            }
            HashSet<string> clump(string text,int k,int L,int t)
            {
                //set of (k,L,t)-clumps in text
                HashSet<string> c = new HashSet<string>();
                foreach (string w in Lwindows(text,L))
                {
                    foreach (string x in kmerfrequencyatleast(kmersfrequency(w,k),t))
                    {
                        c.Add(x);
                    }
                }
                return c;

            }


            string x = "CGGACTCGACAGATGTGAAGAAATGTGAAGACTGAGTGAAGAGAAGAGGAAACACGACACGACATTGCGACATAATGTACGAATGTAATGTGCCTATGGC\n5 75 4";
            string[] inlines = x.Split();
            string text = inlines[0];
            int k = int.Parse(inlines[1]);
            int L = int.Parse(inlines[2]);
            int t = int.Parse(inlines[3]);

            HashSet<string> res = clump(text, k, L, t);
            foreach (string r in res)
            {
                Console.WriteLine(r);
            }


        }
    }
}
