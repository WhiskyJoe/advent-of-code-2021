using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SonarSweep : MonoBehaviour
{
	protected void Start()
	{
		PartOne();

		PartTwo();
	}

	private void PartOne()
	{
		string[] lines = File.ReadAllLines($"{Application.dataPath}/Day 1/Input.txt");

		int increaseAmount = 0;

		for (int i = 1; i < lines.Length; i++)
		{
			int currentInput = int.Parse(lines[i]);
			int previousInput = int.Parse(lines[i - 1]);

			if (currentInput > previousInput)
			{
				increaseAmount++;
			}
		}

		Debug.Log($"Part 1 - Result: {increaseAmount}");
	}

	private void PartTwo()
	{
		string[] lines = File.ReadAllLines($"{Application.dataPath}/Day 1/Input.txt");

		int increaseAmount = 0;

		for (int i = 1; i < lines.Length; i++)
		{
			int currentInput = GetSum(3, i, lines);
			int previousInput = GetSum(3, i - 1, lines);

			if (currentInput != -1 && previousInput != -1 && currentInput > previousInput)
			{
				increaseAmount++;
			}
		}

		Debug.Log($"Part 2 - Result: {increaseAmount}");
	}

	private int GetSum(int sumAmount, int startIndex, string[] lines)
	{
		if (startIndex + sumAmount > lines.Length)
		{
			return -1;
		}

		int sum = 0;

		for (int i = startIndex; i < startIndex + sumAmount; i++)
		{
			int currentInput = int.Parse(lines[i]);
			sum += currentInput;
		}

		return sum;
	}
}