﻿using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using UnityEngine;
using System.Collections;



public static class Mongo
{	

	private static MongoClient client;
	private static MongoDatabase db;
	private static MongoServer server;
	private static MongoCollection<Time> times;
	public static double second;
	public static int level;
	public static DbCallback callback;


	public static void Open(){
		client = new MongoClient("mongodb://qianmo:chuchu@ds159200.mlab.com:59200/game");
		server = client.GetServer();
		server.Connect ();
		db = server.GetDatabase ("game");
		times = db.GetCollection<Time> ("times");
	}


	public static void Add(){
		Open ();
		int count = times.AsQueryable<Time> ().Count (a => a.time >= second && a.level == level);
		int total = times.AsQueryable<Time> ().Count (a => a.level == level);
		times.Insert (new Time{ time = second, level=level});
		try{
			callback((float)count / (float)total);
		}catch{
		}
		Close ();

	}

	public static void Close(){
		server.Disconnect ();
	}

}

