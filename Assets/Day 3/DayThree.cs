using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DayThree : MonoBehaviour
{
	protected void Start()
	{
		PartOne();

		PartTwo();
	}

	private void PartOne()
	{
		string[] lines = File.ReadAllLines($"{Application.dataPath}/Day 3/Input.txt");

		int lineLength = lines[0].Length;
		char[,] charList = new char[lines.Length, lineLength];

		for (int i = 0; i < lines.Length; i++)
		{
			for (int c = 0; c < lines[i].Length; c++)
			{
				charList[i, c] = lines[i][c];
			}
		}

		List<int> significantResult = new List<int>();

		for (int i = 0; i < lineLength; i++)
		{
			int zeroCount = 0;
			int oneCount = 0;

			for (int j = 0; j < lines.Length; j++)
			{
				int charResult = (int)char.GetNumericValue(charList[j, i]);

				if (charResult == 0)
				{
					zeroCount++;
				}
				else
				{
					oneCount++;
				}
			}

			if (zeroCount > oneCount)
			{
				significantResult.Add(0);
			}
			else
			{
				significantResult.Add(1);
			}
		}

		string gammaString = string.Empty;
		string epsilonString = string.Empty;

		for (int i = 0; i < significantResult.Count; i++)
		{
			gammaString += significantResult[i];

			epsilonString += significantResult[i] == 0 ? 1 : 0;
		}

		int gamma = Convert.ToInt32(gammaString, 2);
		int epsilon = Convert.ToInt32(epsilonString, 2);

		int result = gamma * epsilon;

		Debug.Log($"Part 1 - Result: {result}");
	}

	private void PartTwo()
	{
		string[] lines = File.ReadAllLines($"{Application.dataPath}/Day 3/Input.txt");

		int oxygenRating = CalculateRating(new List<string>(lines), 0, '1', true);
		int co2ScrubberRating = CalculateRating(new List<string>(lines), 0, '0', false);

		int result = oxygenRating * co2ScrubberRating;

		Debug.Log($"Part 2 - Result: {result}");
	}

	private int CalculateRating(List<string> entries, int searchIndex, char keepValue, bool oxygen)
	{
		List<string> oneEntries = new List<string>();
		List<string> zeroEntries = new List<string>();

		for (int i = 0; i < entries.Count; i++)
		{
			if (entries[i][searchIndex] == '0')
			{
				zeroEntries.Add(entries[i]);
			}
			else
			{
				oneEntries.Add(entries[i]);
			}
		}

		var combinedEntries = new List<string>();
		combinedEntries.AddRange(zeroEntries);
		combinedEntries.AddRange(oneEntries);

		if (combinedEntries.Count == 1)
		{
			return Convert.ToInt32(combinedEntries[0], 2);
		}
		else if (combinedEntries.Count == 2)
		{
			for (int i = 0; i < combinedEntries.Count; i++)
			{
				if (combinedEntries[i][searchIndex] == keepValue)
				{
					return Convert.ToInt32(combinedEntries[i], 2);
				}
			}
		}

		if (zeroEntries.Count == oneEntries.Count)
		{
			List<string> remaining = new List<string>();
			for (int i = 0; i < combinedEntries.Count; i++)
			{
				if (combinedEntries[i][searchIndex] == keepValue)
				{
					remaining.Add(combinedEntries[i]);
				}
			}

			return CalculateRating(remaining, searchIndex + 1, keepValue, oxygen);
		}

		if (oxygen)
		{
			if (oneEntries.Count > zeroEntries.Count)
			{
				return CalculateRating(oneEntries, searchIndex + 1, keepValue, oxygen);
			}
			else
			{
				return CalculateRating(zeroEntries, searchIndex + 1, keepValue, oxygen);
			}
		}
		else
		{
			if (oneEntries.Count < zeroEntries.Count)
			{
				return CalculateRating(oneEntries, searchIndex + 1, keepValue, oxygen);
			}
			else
			{
				return CalculateRating(zeroEntries, searchIndex + 1, keepValue, oxygen);
			}
		}
	}
}