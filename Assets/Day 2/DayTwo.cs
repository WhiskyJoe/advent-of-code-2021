using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class DayTwo : MonoBehaviour
{
	protected void Start()
	{
		PartOne();

		PartTwo();
	}

	private void PartOne()
	{
		string[] lines = File.ReadAllLines($"{Application.dataPath}/Day 2/Input.txt");

		Vector2Int position = new Vector2Int();

		for (int i = 0; i < lines.Length; i++)
		{
			string[] splitLine = lines[i].Split(' ');
			string heading = splitLine[0];
			int amount = int.Parse(splitLine[1]);

			switch (heading)
			{
				case "forward":
					{
						position.x += amount;
					}
					break;

				case "down":
					{
						position.y += amount;
					}
					break;

				case "up":
					{
						position.y -= amount;
					}
					break;
			}
		}

		int result = position.x * position.y;

		Debug.Log($"Part 1 - Result: {result}");
	}

	private void PartTwo()
	{
		string[] lines = File.ReadAllLines($"{Application.dataPath}/Day 2/Input.txt");

		Vector2Int position = new Vector2Int();
		int aim = 0;

		for (int i = 0; i < lines.Length; i++)
		{
			string[] splitLine = lines[i].Split(' ');
			string heading = splitLine[0];
			int amount = int.Parse(splitLine[1]);

			switch (heading)
			{
				case "forward":
					{
						position.x += amount;
						position.y += aim * amount;
					}
					break;

				case "down":
					{
						aim += amount;
					}
					break;

				case "up":
					{
						aim -= amount;
					}
					break;
			}
		}

		int result = position.x * position.y;

		Debug.Log($"Part 2 - Result: {result}");
	}
}