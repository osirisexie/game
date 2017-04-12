using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using UnityEngine;



public static class Mongo
{	

	private static MongoClient client;
	private static MongoDatabase db;
	private static MongoServer server;
	private static MongoCollection<Time> times;


	public static void Open(){
		client = new MongoClient("mongodb://qianmo:chuchu@ds159200.mlab.com:59200/game");
		server = client.GetServer();
		server.Connect ();
		db = server.GetDatabase ("game");
		times = db.GetCollection<Time> ("times");
	}

	public static float Add(int second)
	{
		int count = times.AsQueryable<Time> ().Count (a => a.time >= second);
		long total = times.Count ();
		times.Insert (new Time{ time = second });
		return (float)count/(float)total;
	}

	public static void Close(){
		server.Disconnect ();
	}

}

