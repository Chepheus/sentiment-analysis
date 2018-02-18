using System;
using sentimentanalysis.Core;
using System.Collections.Generic;
using sentimentanalysis.Core.Database.Entity;
using sentimentanalysis.Core.Analysis.Service;

namespace sentimentanalysis.Core.Analysis
{
    public class InputPostAnalisator
    {
        protected SentimentCoeficientCalculator sentimentCoeficientCalculator;
        protected ToLemmasConverter toLemmaConverter;

        public InputPostAnalisator(ToLemmasConverter toLemmaConverter, 
                                   SentimentCoeficientCalculator sentimentCoeficientCalculator)
        {
            this.toLemmaConverter = toLemmaConverter;
            this.sentimentCoeficientCalculator = sentimentCoeficientCalculator;
        }

        public Dictionary<Post, float> GetPosts(List<Post> posts, bool isDebug = false)
        {
            Dictionary<Post, float> estimatedPosts = new Dictionary<Post, float>();
            foreach (Post post in posts)
            {
                string[] words = post.Title.Split(' ');
                var a = getPostCoeficient(words, isDebug);
                estimatedPosts.Add(post, a);
                if (isDebug)
                {
                    Console.WriteLine("{0}: {1}", post.Title, a);
                }
            }

            return estimatedPosts;
        }

        private float getPostCoeficient(string[] words, bool isDebug = false)
        {
            float postCoeficient = 0f;
            int estimatedWordsCount = 0;

            foreach (string word in words)
			{
                string lemmatizedWord = toLemmaConverter.ToLemma(word);
                float coeficient = getWordCoeficient(lemmatizedWord);
                postCoeficient += coeficient;

                if (Math.Abs(coeficient) > .000001)
                {
                    if (isDebug)
                    {
                        Console.WriteLine("{0}: {1}", lemmatizedWord, coeficient);
                    }
                    
                    estimatedWordsCount++;
                }
			}

            if (0 == estimatedWordsCount)
            {
                return 0;
            }

            return postCoeficient / estimatedWordsCount;
        }

        private float getWordCoeficient(string word)
        {
            // wordCount/posts
			float positiveCoeficient = sentimentCoeficientCalculator.GetCoeficient(word, true);
			float negativeCoeficient = sentimentCoeficientCalculator.GetCoeficient(word, false);

            return positiveCoeficient - negativeCoeficient;
        }
    }
}
