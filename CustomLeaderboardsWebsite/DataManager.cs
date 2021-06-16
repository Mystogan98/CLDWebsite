using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomLeaderboardsWebsite
{	
	public enum Formatting
	{
		single,				// 1
		floatingPoint,		// 1.##
		percent				// 100.## %
	}

	public class DataStatic
	{
		public string name;
		public bool inverted;
		public string symbol;
		public Formatting formatting;

		public DataStatic(string name, bool inverted, string symbol, Formatting formatting)
		{
			this.name = name;
			this.inverted = inverted;
			this.symbol = symbol;
			this.formatting = formatting;
		}
	}

	public class DataManager
	{
		public static Dictionary<string, DataStatic> staticData = new Dictionary<string, DataStatic>()
		{
			{ "weightedPP", new DataStatic("Weighted PP", false, "pp", Formatting.floatingPoint) },
			{ "rawPP", new DataStatic("Raw PP", false, "pp", Formatting.floatingPoint) },
			{ "weightedAverageScorePercentage", new DataStatic("Weighted average score percentage", false, "", Formatting.percent) },
			{ "averageScorePercentage", new DataStatic("Average score percentage", false, "", Formatting.percent) },
			{ "weightedAverageRank", new DataStatic("Weighted average rank", true, "", Formatting.floatingPoint) },
			{ "averageRank", new DataStatic("Average rank", true, "", Formatting.floatingPoint) },
			{ "weightedAverageCountryRank", new DataStatic("Weighted average country rank", true, "", Formatting.floatingPoint) },
			{ "averageCountryRank", new DataStatic("Average country rank", true, "", Formatting.floatingPoint) },
			{ "topPP", new DataStatic("Top PP", false, "pp", Formatting.floatingPoint) },
			{ "highest96", new DataStatic("Highest 96+%", false, "pp", Formatting.floatingPoint) },
			{ "highest97", new DataStatic("Highest 97+%", false, "pp", Formatting.floatingPoint) },
			{ "highest98", new DataStatic("Highest 98+%", false, "pp", Formatting.floatingPoint) },
			{ "highest99", new DataStatic("Highest 99+%", false, "pp", Formatting.floatingPoint) },
			{ "nbOfRankedDiffPlayed", new DataStatic("Number of ranked difficulty played", false, "", Formatting.single) },
			{ "nbOf95", new DataStatic("Number of 95+%", false, "", Formatting.single) },
			{ "bestRank", new DataStatic("Best rank on any ranked difficulty", false, "", Formatting.single) },
			{ "nbOf325", new DataStatic("Number of 325+pp plays", false, "", Formatting.single) },
			{ "nbOfCountryFirst", new DataStatic("Number of country #1 place", false, "", Formatting.single) },
			{ "nbOfFirst", new DataStatic("Number of #1 place", false, "", Formatting.single) },
			{ "nbOfTop10", new DataStatic("Number of top 10", false, "", Formatting.single) },

			// country leaderboards
		};

		private static IMongoDatabase db;

		public static void ConnectToDB(string dbName)
		{
			System.Threading.Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-US");
			db = new MongoClient("mongodb://localhost").GetDatabase(dbName);
		}

		public static List<T> GetT<T>(string collectionName, FilterDefinition<T> filter) where T : class, new()
		{
			// Get from Database
			if(db != null)
			{
				try
				{
					if (filter == null)
						filter = Builders<T>.Filter.Empty;
					return db.GetCollection<T>(collectionName).Find(filter).ToList();
				} catch (Exception)
				{
					// Probably wrong category name, but I don't know how to show it
				}
			}
			
			return null;
		}
	}
}
