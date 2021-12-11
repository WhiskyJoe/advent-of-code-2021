using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class DayFour : MonoBehaviour
{
	private class Card
	{
		public int CardSum
		{
			get
			{
				int sum = 0;

				for (int i = 0; i < entries.Count; i++)
				{
					for (int j = 0; j < entries[i].Count; j++)
					{
						if (!markedEntries.Contains(entries[i][j]))
						{
							sum += entries[i][j];
						}
					}
				}

				return sum;
			}
		}

		private List<List<int>> entries = new List<List<int>>();
		private List<int> markedEntries = new List<int>();

		public void AddRow(List<int> numbers)
		{
			List<int> rowEntry = new List<int>();
			for (int i = 0; i < numbers.Count; i++)
			{
				rowEntry.Add(numbers[i]);
			}

			entries.Add(rowEntry);
		}

		public bool CheckNumber(int number)
		{
			for (int i = 0; i < entries.Count; i++)
			{
				if (entries[i].Contains(number))
				{
					markedEntries.Add(number);
					return true;
				}
			}

			return false;
		}

		public bool HasBingo()
		{
			for (int i = 0; i < entries.Count; i++)
			{
				bool columnBingo = true;
				bool rowBingo = true;

				for (int j = 0; j < entries[i].Count; j++)
				{
					rowBingo &= markedEntries.Contains(entries[i][j]);
					columnBingo &= markedEntries.Contains(entries[j][i]);
				}

				if (rowBingo || columnBingo)
				{
					return true;
				}
			}

			return false;
		}
	}

	protected void Start()
	{
		PartOne();

		PartTwo();
	}

	private void PartOne()
	{
		string[] lines = File.ReadAllLines($"{Application.dataPath}/Day 4/Input.txt");

		string[] numbers = lines[0].Split(',');

		List<Card> bingoCards = new List<Card>();

		for (int i = 1; i < lines.Length; i++)
		{
			if (string.IsNullOrWhiteSpace(lines[i]))
			{
				bingoCards.Add(new Card());
				continue;
			}

			Card card = bingoCards[bingoCards.Count - 1];

			string[] entries = lines[i].Split(' ');
			List<int> rowNumbers = new List<int>();
			for (int j = 0; j < entries.Length; j++)
			{
				if (int.TryParse(entries[j], out int number))
				{
					rowNumbers.Add(number);
				}
			}

			card.AddRow(rowNumbers);
		}

		for (int i = 0; i < numbers.Length; i++)
		{
			if (int.TryParse(numbers[i], out int number))
			{
				for (int c = 0; c < bingoCards.Count; c++)
				{
					if (bingoCards[c].CheckNumber(number))
					{
						if (bingoCards[c].HasBingo())
						{
							int result = number * bingoCards[c].CardSum;

							Debug.Log($"Part 1 - Result: {result}");
							return;
						}
					}
				}
			}
		}
	}

	private void PartTwo()
	{
		string[] lines = File.ReadAllLines($"{Application.dataPath}/Day 4/Input.txt");

		string[] numbers = lines[0].Split(',');

		List<Card> bingoCards = new List<Card>();

		for (int i = 1; i < lines.Length; i++)
		{
			if (string.IsNullOrWhiteSpace(lines[i]))
			{
				bingoCards.Add(new Card());
				continue;
			}

			Card card = bingoCards[bingoCards.Count - 1];

			string[] entries = lines[i].Split(' ');
			List<int> rowNumbers = new List<int>();
			for (int j = 0; j < entries.Length; j++)
			{
				if (int.TryParse(entries[j], out int number))
				{
					rowNumbers.Add(number);
				}
			}

			card.AddRow(rowNumbers);
		}

		List<Card> completedCards = new List<Card>();
		int lastBingoNumber = 0;

		for (int i = 0; i < numbers.Length; i++)
		{
			if (int.TryParse(numbers[i], out int number))
			{
				for (int c = 0; c < bingoCards.Count; c++)
				{
					if (bingoCards[c].CheckNumber(number))
					{
						if (bingoCards[c].HasBingo())
						{
							if (bingoCards.Count - 1 == completedCards.Count)
							{
								lastBingoNumber = number;
							}

							if (!completedCards.Contains(bingoCards[c]))
							{
								completedCards.Add(bingoCards[c]);
							}

							if (bingoCards.Count == completedCards.Count)
							{
								int res = lastBingoNumber * bingoCards[c].CardSum;
								Debug.Log($"Part 2 - Result: {res}");
								return;
							}
						}
					}
				}
			}
		}

		Card lastCard = completedCards[completedCards.Count - 1];

		int result = lastBingoNumber * lastCard.CardSum;

		Debug.Log($"Part 2 - Result: {result}");
	}
}