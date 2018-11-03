using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Mono.Data.Sqlite;
using System.Data;

public class Persistence{
	 
	public Persistence(){
	}

	public Game GetGame()
	{
		Game g = new Game ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "SELECT * FROM game";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			g.Id_game = reader.GetInt32(0);
			g.Name = reader.GetString (1);
			g.Route_file = reader.GetString (2);
			g.Date_done = reader.GetDateTime(3);
			g.P = this.GetPlayer (g.Id_game, 1);
			g.List_maps = this.ListMap (g.Id_game);
			g.List_pieces = this.ListPieces (g.Id_game);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return g;
	}

	public Player GetPlayer(int pIdGame, int pIdPlayer)
	{
		Player p = new Player();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format ("SELECT * FROM player where id_game = \"{0}\" and id_player = \"{1}\"", pIdGame, pIdPlayer);
		//string sqlQuery = "SELECT * FROM player where id_game = 1 and id_player = 1";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			p.Id_player = reader.GetInt32(1);
			p.Name = reader.GetString (2);
			p.Heart = this.GetHeart (pIdGame, pIdPlayer, 1);
			p.Ingot = this.GetIngot (pIdGame, pIdPlayer, 1);
			p.ListAchievements = this.ListAchievement (pIdGame, pIdPlayer);
			p.ListStateLevel = this.ListLevelState (pIdGame, pIdPlayer);
			p.ListBonus = this.ListBonus (pIdGame, pIdPlayer);
			p.ListRegMap = this.ListRegisterMap (pIdGame, pIdPlayer);
			p.List_message_group = this.ListMessageGroup (pIdGame, pIdPlayer);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return p;
	}

	public Heart GetHeart(int pIdGame, int pIdPlayer, int pIdHeart)
	{
		Heart h = new Heart();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM heart where id_game = \"{0}\" and id_player = \"{1}\" and id_heart = \"{2}\"",pIdGame, pIdPlayer, pIdHeart);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			h.Id_heart = reader.GetInt32(2);
			h.Count_lifes = reader.GetInt32 (3);
			h.Minutes = reader.GetInt32 (4);
			h.Seconds = reader.GetInt32 (5);
			h.Minutes_per_life =  reader.GetInt32 (6);
			h.IsInfinite = this.GetBoolean(reader.GetInt32 (7));
			h.Time_infinite = reader.GetDateTime (8);
			h.List_next_life = this.ListNextLife (pIdGame, pIdPlayer, pIdHeart);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
		return h;
	
	}

	public List<NextLife> ListNextLife(int pIdGame, int pIdPlayer, int pIdHeart)
	{
		List<NextLife> list_next = new List<NextLife>();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM next_life where id_game = \"{0}\" and id_player = \"{1}\" and id_heart = \"{2}\"",pIdGame, pIdPlayer, pIdHeart);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			NextLife n = new NextLife ();
			n.Id_next_life = reader.GetInt32 (3);
			n.Date_next_life = reader.GetDateTime(4);
			list_next.Add(n);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_next;

	}
		
	public Ingot GetIngot(int pIdGame, int pIdPlayer, int pIdIngot)
	{
		Ingot i = new Ingot ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM ingot where id_game = \"{0}\" and id_player = \"{1}\" and id_ingot = \"{2}\"",pIdGame, pIdPlayer, pIdIngot);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			i.Id_ingot = reader.GetInt32(2);
			i.Coin_count = reader.GetInt32 (3);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return i;

	}

	public List<Bonus> ListBonus(int pIdGame, int pIdPlayer)
	{
		List<Bonus> list_bonus = new List<Bonus>();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM bonus where id_game = \"{0}\" and id_player = \"{1}\"",pIdGame, pIdPlayer);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			Bonus b = new Bonus ();

			b.Id_bonus = reader.GetInt32(2);
			b.BonusType = this.GetBonusType (reader.GetInt32 (3));
			b.File = reader.GetString(4);
			b.Count_bonus = reader.GetInt32(5);

			list_bonus.Add (b);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_bonus;
	}

	public BonusType GetBonusType(int pIdType)
	{
		BonusType t = new BonusType ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM bonus_type where id_bonus_type = \"{0}\"",pIdType);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			t.Id_type_bonus = reader.GetInt32(0);
			t.Name = reader.GetString(1);
			t.Description = reader.GetString(2);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return t;

	}

	public List<MessageGroup> ListMessageGroup(int pIdGame, int pIdPlayer)
	{
		List<MessageGroup> list_message_group = new List<MessageGroup> ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM message_group where id_game = \"{0}\" and id_player = \"{1}\"", pIdGame, pIdPlayer);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			MessageGroup mg = new MessageGroup();
			mg.Id_message_group = reader.GetInt32(2);
			mg.Name = reader.GetString(3);
			mg.MessageList = this.ListMessages (pIdGame, pIdPlayer, reader.GetInt32 (2));
			list_message_group.Add (mg);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_message_group;
	}

	public List<Message> ListMessages(int pIdGame, int pIdPlayer, int pIdMessGroup)
	{
		//MessageGroup mg = new MessageGroup();
		List<Message> list_message = new List<Message>();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM message where id_game = \"{0}\" and id_player = \"{1}\" and id_message_group = \"{2}\"",pIdGame,pIdPlayer,pIdMessGroup);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			Message m = new Message();
			m.Id_message = reader.GetInt32(3);
			m.Title = reader.GetString(4);
			m.Description = reader.GetString (5);
			m.File = reader.GetString (6);
			m.DoneDate = reader.GetDateTime (7);
			m.UntilDate = reader.GetDateTime (8);
			list_message.Add (m);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_message;
	}

	public List<RegisterMap> ListRegisterMap(int pIdGame, int pIdPlayer)
	{
		List<RegisterMap> list_register = new List<RegisterMap>();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM map_register where id_game = \"{0}\" and id_player = \"{1}\"",pIdGame, pIdPlayer);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			RegisterMap r = new RegisterMap();
			r.Id_register_map = reader.GetInt32(2);
			r.Map = this.GetMap (pIdGame,reader.GetInt32 (3),-2);
			r.Score_map = reader.GetInt32(4);
			r.ListRegLevel = this.ListRegisterLevel (pIdGame, pIdPlayer, r.Id_register_map);
			list_register.Add (r);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_register;
	}

	public List<RegisterLevel> ListRegisterLevel(int pIdGame, int pIdPlayer, int pIdRegisterMap)
	{
		List<RegisterLevel> list_register = new List<RegisterLevel>();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM level_register where id_game = \"{0}\" and id_player = \"{1}\" and id_map_register = \"{2}\"",pIdGame,pIdPlayer,pIdRegisterMap);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			RegisterLevel r = new RegisterLevel();
			r.Id_register_level = reader.GetInt32(3);
			r.Level = this.GetLevel(pIdGame, reader.GetInt32(4), reader.GetInt32(5));
			r.Score = reader.GetInt32(6);
			r.Count_stars = reader.GetInt32(7);
			list_register.Add (r);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_register;
	}

	public List<LevelState> ListLevelState(int pIdGame, int pIdPlayer)
	{
		List<LevelState> list_state = new List<LevelState>();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM level_state where id_game = \"{0}\" and id_player = \"{1}\"",pIdGame,pIdPlayer);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			LevelState ls = new LevelState();
			ls.Id_state= reader.GetInt32(2);
			ls.Level = this.GetLevel (pIdGame,reader.GetInt32 (3), reader.GetInt32 (4));
			ls.Num_state = reader.GetInt32(5);
			ls.Times_lost = reader.GetInt32 (6);
			list_state.Add (ls);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_state;
	}

	public List<Achievement> ListAchievement(int pIdGame, int pIdPlayer)
	{
		List<Achievement> list_ach = new List<Achievement>();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM achievement where id_game = \"{0}\" and id_player = \"{1}\"", pIdGame, pIdPlayer);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read())
		{
			Achievement a = new Achievement();
			a.Id_achievement = reader.GetInt32(2);
			a.Image_file = reader.GetString(3);
			a.Title = reader.GetString(4);
			a.Description = reader.GetString (5);
			a.Count = reader.GetInt32 (6);
			a.Max_count = reader.GetInt32 (7);
			a.Percentaje = reader.GetDouble (8);
			a.Prize = reader.GetInt32 (9);
			a.Width = reader.GetDouble (10);
			a.Height = reader.GetDouble (11);
			a.Loc_x = reader.GetDouble (12);
			a.Is_earned = (Boolean)reader["is_earned"];
			list_ach.Add (a);
		}
		reader.Close();
		reader = null;
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_ach;
	}

	public Map GetMap(int pIdGame, int pIdMap, int pIndexLevel)
	{
		Map m = new Map();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM map where id_game = \"{0}\" and id_map = \"{1}\"",pIdGame,pIdMap);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			m.Id_map = reader.GetInt32 (1);
			m.Name = reader.GetString (2);
			m.Description = reader.GetString (3);
			m.File = reader.GetString (4);
			if (pIndexLevel < 0) 
			{
				if (pIndexLevel == -1) 
				{
					m.ListLevel = this.ListLevel (pIdGame, pIdMap);
				}
			} 
			else
			{
				m.ListLevel.Add(GetLevel(pIdGame, pIdMap, pIndexLevel));
			}
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return m;
	}

	public List<Map> ListMap(int pIdGame)
	{
		List<Map> list_map = new List<Map>();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM map where id_game = \"{0}\"",pIdGame);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			Map m = new Map();
			m.Id_map = reader.GetInt32 (1);
			m.Name = reader.GetString (2);
			m.Description = reader.GetString (3);
			m.File = reader.GetString (4);
			m.ListLevel = this.ListLevel(pIdGame, m.Id_map);
			list_map.Add(m);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_map;
	}

	public Level GetLevel(int pIdGame, int pIdMap, int pIdLevel)
	{
		Level l = new Level ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM level where id_game = \"{0}\" and id_map = \"{1}\" and id_level = \"{2}\"", pIdGame, pIdMap, pIdLevel);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			l.Id_level = reader.GetInt32(2);
			l.Name = reader.GetString (3);
			l.File = reader.GetString(4);
			l.File_osc = reader.GetString(5);
			l.X = (float)((double)reader ["x"]);
			l.Y = (float)((double)reader ["y"]);
			l.X_exp = (float)((double)reader ["x_exp"]);
			l.Y_exp = (float)((double)reader ["y_exp"]);
			l.X_frame = (float)((double)reader ["x_frame"]);
			l.Y_frame =(float)((double)reader ["y_frame"]);
			l.Board_list = this.ListBoard (pIdGame, pIdMap, reader.GetInt32 (2));
			l.Level_score_list = this.ListLevelScore(pIdGame, pIdMap, reader.GetInt32 (2));
			l.Objective_list = this.ListObjective(pIdGame, pIdMap, reader.GetInt32 (2));
			l.Conditionating_list = this.ListConditionating(pIdGame,pIdMap,reader.GetInt32 (2));
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return l;
	}

	public List<Level> ListLevel(int pIdGame, int pIdMap)
	{
		List<Level> list_level = new List<Level> ();
	
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM level where id_game = \"{0}\" and id_map = \"{1}\"", pIdGame, pIdMap);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			Level l = new Level();
			l.Id_level = reader.GetInt32(2);
			l.Name = reader.GetString (3);
			l.File = reader.GetString(4);
			l.File_osc = reader.GetString(5);
			l.X = (float)((double)reader ["x"]);
			l.Y = (float)((double)reader ["y"]);
			l.X_exp = (float)((double)reader ["x_exp"]);
			l.Y_exp = (float)((double)reader ["y_exp"]);
			l.X_frame = (float)((double)reader ["x_frame"]);
			l.Y_frame =(float)((double)reader ["y_frame"]);
			l.Board_list = this.ListBoard (pIdGame, pIdMap, reader.GetInt32 (2));
			l.Level_score_list = this.ListLevelScore(pIdGame, pIdMap, reader.GetInt32 (2));
			l.Objective_list = this.ListObjective(pIdGame, pIdMap, reader.GetInt32 (2));
			l.Conditionating_list = this.ListConditionating(pIdGame,pIdMap,reader.GetInt32 (2));
			list_level.Add (l);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_level;
	}
		
	public List<Objective> ListObjective(int pIdGame, int pIdMap, int pIdLevel)
	{
		List<Objective> list_objective = new List<Objective> ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM objective where id_game = \"{0}\" and id_map = \"{1}\" and id_level = \"{2}\"",pIdGame,pIdMap,pIdLevel);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			Objective o = new Objective ();
			o.Id_objective = reader.GetInt32 (3);
			o.Type_obj = this.GetTypeObjective(reader.GetInt32 (4));
			o.Top_value = reader.GetInt32(5);
			list_objective.Add (o);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_objective;
	}

	public ObjectiveType GetTypeObjective(int pIdObjectiveType)
	{
		ObjectiveType ob = new ObjectiveType();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM objective_type where id_objective_type = \"{0}\"",pIdObjectiveType);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			ob.Id_type_objective = reader.GetInt32 (0);
			ob.Description = reader.GetString (1);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return ob;
	}

	public List<Conditionating> ListConditionating(int pIdGame, int pIdMap, int pIdLevel)
	{
		List<Conditionating> list_cond = new List<Conditionating> ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM conditionating where id_game = \"{0}\" and id_map = \"{1}\" and id_level = \"{2}\"",pIdGame,pIdMap,pIdLevel);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			Conditionating c = new Conditionating ();
			c.Id_conditionating = reader.GetInt32 (3);
			c.Cond_type = this.GetTypeConditionating (reader.GetInt32 (4));
			c.Top_value = reader.GetInt32(5);
			c.Is_infinitive = this.GetBoolean(reader.GetInt32(6));
			list_cond.Add (c);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_cond;
	}

	public ConditionatingType GetTypeConditionating(int pIdCondType)
	{
		ConditionatingType cond = new ConditionatingType();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand(); 
		string sqlQuery = String.Format("SELECT * FROM conditionating_type where id_conditionating_type = \"{0}\"",pIdCondType);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			cond.Id_type_conditionating= reader.GetInt32 (0);
			cond.Description = reader.GetString (1);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return cond;
	}

	public List<LevelScore> ListLevelScore(int pIdGame, int pIdMap, int pIdLevel)
	{
		List<LevelScore> list_level_score = new List<LevelScore> ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM score_level where id_game = \"{0}\" and id_map = \"{1}\" and id_level = \"{2}\"",pIdGame,pIdMap,pIdLevel);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			LevelScore l = new LevelScore ();
			l.Id_level_score = reader.GetInt32 (3);
			l.Star = this.GetStar (reader.GetInt32 (4));
			l.Score_level= reader.GetInt32(5);
			l.Loc_star = (float)((double)reader["loc_star"]);
			l.Bar_value = (float)((double)reader["bar_value"]);
			list_level_score.Add (l);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_level_score;
	}

	public Star GetStar(int pIdStar)
	{
		Star s = new Star();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM star where id_star= \"{0}\"",pIdStar);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			s.Id_star = reader.GetInt32 (0);
			s.Name = reader.GetString (1);
			s.File = reader.GetString (2);
			s.Grade = reader.GetInt32 (3);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return s;
	}


	public List<Board> ListBoard(int pIdGame, int pIdMap, int pIdLevel)
	{
		List<Board> list_board = new List<Board> ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection)new SqliteConnection (conn);
		dbconn.Open (); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand ();
		string sqlQuery = String.Format("SELECT * FROM board where id_game = \"{0}\" and id_map = \"{1}\" and id_level = \"{2}\"",pIdGame, pIdMap, pIdLevel);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader ();
		while (reader.Read ()) {
			Board b = new Board ();
			b.Id_board = reader.GetInt32 (3);
			b.Board_type = this.GetBoardType (reader.GetInt32 (4));
			b.File = reader.GetString (5);
			list_board.Add (b);
		}
		dbcmd.Dispose ();
		dbcmd = null;
		dbconn.Close ();
		dbconn = null;

		return list_board;
	}

	public BoardType GetBoardType(int pIdBoardType)
	{
		BoardType bt = new BoardType();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM board_type where id_board_type = \"{0}\"",pIdBoardType);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			bt.Id_board_type = reader.GetInt32 (0);
			bt.Description = reader.GetString (1);
			bt.Square_list = this.ListSquare (pIdBoardType);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return bt;
	}

	public List<Square> ListSquare(int pIdBoardType)
	{
		List<Square> list_square = new List<Square>();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM square where id_board_type = \"{0}\"",pIdBoardType);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			Square s = new Square ();
			s.Id_square = reader.GetInt32 (1);
			s.Square_type = this.GetSquareType (reader.GetInt32 (2));
			s.Coordinate_x_border = (float)((double)reader["coordinate_x_border"]);
			s.Coordinate_y_border = (float)((double)reader["coordinate_y_border"]);
			s.Coordinate_x_center = (float)((double)reader["coordinate_x_center"]);
			s.Coordinate_y_center = (float)((double)reader["coordinate_y_center"]);
			s.Location_x = reader.GetInt32 (7);
			s.Location_y = reader.GetInt32 (8);
			s.IsOcupated = false;
			s.Game_square = null;
			list_square.Add (s);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return list_square;
	}

	public SquareType GetSquareType(int pIdSquareType)
	{
		SquareType s = new SquareType();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM square_type where id_square_type = \"{0}\"",pIdSquareType);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			s.Id_type_square = reader.GetInt32 (0);
			s.Name = reader.GetString (1);
			s.Description = reader.GetString (2);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return s;
	}

	public List<Piece> ListPieces(int pIdGame)
	{
		List<Piece> listPieces = new List<Piece> ();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM piece where id_game = \"{0}\"",pIdGame);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{  
			Piece p = new Piece();
			p.Id_piece = reader.GetInt32 (1);
			p.Model = this.GetModel (reader.GetInt32 (2));
			p.Piece_number = reader.GetInt32(3);
			p.Piece_score = reader.GetInt32 (4);
			p.File = reader.GetString (5);
			listPieces.Add (p);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return listPieces;
	}

	public Model GetModel(int pIdModel)
	{
		Model m = new Model();

		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = String.Format("SELECT * FROM model where id_model = \"{0}\"",pIdModel);
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			m.Id_model = reader.GetInt32 (0);
			m.Description = reader.GetString (1);
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;

		return m;
	}

	public int GetNextNextLifeIndex()
	{
		int lastIndex = 0;
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.
		IDbConnection dbconn;
		dbconn = (IDbConnection) new SqliteConnection(conn);
		dbconn.Open(); //Open connection to the database.
		IDbCommand dbcmd = dbconn.CreateCommand();
		string sqlQuery = "select ifnull(max(id_next_life),0) from next_life";
		dbcmd.CommandText = sqlQuery;
		IDataReader reader = dbcmd.ExecuteReader();
		while (reader.Read ()) 
		{
			lastIndex = reader.GetInt32 (0) + 1;
		}
		dbcmd.Dispose();
		dbcmd = null;
		dbconn.Close();
		dbconn = null;
		return lastIndex;
	}

	public void UpdatePlayer(int pIdGame, Player p)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("UPDATE player SET id_game = \"{0}\", id_player = \"{1}\", name = \"{2}\" WHERE id_game = \"{0}\" AND id_player = \"{1}\"",pIdGame,p.Id_player,p.Name);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void UpdateBonus(int pIdGame, int pIdPlayer, Bonus b)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("UPDATE bonus SET id_game = \"{0}\", id_player = \"{1}\", id_bonus = \"{2}\", id_type_bonus = \"{3}\", file = \"{4}\", count_bonus = \"{5}\" WHERE id_game = \"{0}\" AND  id_player = \"{1}\" AND id_bonus = \"{2}\"",pIdGame,pIdPlayer,b.Id_bonus,b.BonusType.Id_type_bonus,b.File,b.Count_bonus);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}
	
	}

	public void UpdateIngot(int pIdGame, int pIdPlayer, Ingot i)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("UPDATE ingot SET id_game = \"{0}\", id_player = \"{1}\", id_ingot = \"{2}\", count_coin = \"{3}\" WHERE id_game = \"{0}\" AND id_player = \"{1}\" AND  id_ingot = \"{2}\"",pIdGame,pIdPlayer,i.Id_ingot,i.Coin_count);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void UpdateHeart(int pIdGame, int pIdPlayer,Heart h)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("UPDATE heart SET id_game = \"{0}\", id_player = \"{1}\", id_heart = \"{2}\", count_life = \"{3}\", minutes = \"{4}\", seconds = \"{5}\", minutes_per_life = \"{6}\", is_infinitive = \"{7}\", time_infinitive = \"{8}\" WHERE id_game = \"{0}\" AND id_player = \"{1}\" AND id_heart = \"{0}\"", pIdGame, pIdPlayer,h.Id_heart, h.Count_lifes, h.Minutes, h.Seconds, h.Minutes_per_life, this.GetInteger(h.IsInfinite), h.Time_infinite.ToString("yyyy-MM-dd HH:mm:ss"));
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}
	
	}

	public void InsertNextLife(int pIdGame, int pIdPlayer, int pIdHeart, NextLife n)
	{
		n.Id_next_life = this.GetNextNextLifeIndex ();
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("INSERT INTO next_life (id_game, id_player,id_heart,id_next_life,date_next_life) VALUES (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\")", pIdGame, pIdPlayer, pIdHeart, n.Id_next_life, n.Date_next_life.ToString("yyyy-MM-dd HH:mm:ss"));
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void DeleteNextLife(int pIdGame, int pIdPlayer, int pIdHeart, NextLife n)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("DELETE FROM next_life WHERE id_game = \"{0}\" AND  id_player = \"{1}\" AND  id_heart = \"{2}\" AND id_next_life = \"{3}\"", pIdGame, pIdPlayer, pIdHeart, n.Id_next_life);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void DeleteAllNextLife(int pIdGame, int pIdPlayer, int pIdHeart)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("DELETE FROM next_life WHERE id_game = \"{0}\" AND  id_player = \"{1}\" AND  id_heart = \"{2}\"", pIdGame, pIdPlayer, pIdHeart);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void InsertRegMap(int pIdGame, int pIdPlayer, RegisterMap r)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("INSERT INTO map_register (id_game,id_player,id_map_register,id_map,score_map) VALUES (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\")", pIdGame, pIdPlayer, r.Id_register_map, r.Id_register_map, r.Score_map);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void UpdateRegMap(int pIdGame, int pIdPlayer,RegisterMap r)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("UPDATE map_register SET id_game = \"{0}\", id_player = \"{1}\", id_map_register = \"{2}\", id_map = \"{3}\", score_map = \"{4}\" WHERE id_game =\"{0}\" AND id_player = \"{1}\" AND  id_map_register = \"{2}\"", pIdGame, pIdPlayer, r.Id_register_map, r.Map.Id_map, r.Score_map);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void InsertRegLevel(int pIdGame, int pIdPlayer, int pIdRegMap, int pIdMap, RegisterLevel r)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("INSERT INTO level_register (id_game, id_player, id_map_register,  id_level_register, id_map, id_level, score, count_star) VALUES (\"{0}\",\"{1}\",\"{2}\",\"{3}\",\"{4}\",\"{5}\",\"{6}\",\"{7}\")", pIdGame, pIdPlayer, pIdRegMap ,r.Id_register_level, pIdMap, r.Level.Id_level, r.Score,r.Count_stars);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void UpdateAchievement(int pIdGame, int pIdPlayer,Achievement ach)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("UPDATE achievement SET id_game = \"{0}\", id_player = \"{1}\", id_achievement = \"{2}\", image_file = \"{3}\", title = \"{4}\", description = \"{5}\", count = \"{6}\", max_count = \"{7}\", percentaje = \"{8}\", prize = \"{9}\", width = \"{10}\", heigth = \"{11}\", loc_x = \"{12}\", is_earned = \"{13}\" WHERE id_game = \"{0}\" AND  id_player = \"{1}\" AND  id_achievement = \"{2}\"", pIdGame, pIdPlayer, ach.Id_achievement, ach.Image_file, ach.Title, ach.Description, ach.Count, ach.Max_count, ach.Percentaje, ach.Prize, ach.Width, ach.Height, ach.Loc_x, ach.Is_earned);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void UpdateLevelState(int pIdGame, int pIdPlayer, int pIdMap ,LevelState l)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("UPDATE level_state SET id_game = \"{0}\", id_player = \"{1}\", id_state = \"{2}\", id_map = \"{3}\", id_level = \"{4}\", state_num = \"{5}\",time_lost = \"{6}\" WHERE id_game = \"{0}\" AND  id_player = \"{1}\" AND  id_state = \"{2}\";", pIdGame, pIdPlayer, l.Id_state, pIdMap ,l.Level.Id_level, l.Num_state, l.Times_lost);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}
	
	public void DeleteAllRegMap(int pIdGame, int pIdPlayer)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("DELETE FROM map_register WHERE id_game = \"{0}\" AND  id_player = \"{1}\" ", pIdGame, pIdPlayer);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}

	public void DeleteAllRegLevel(int pIdGame, int pIdPlayer, int pIdMap)
	{
		string conn = "URI=file:" + Application.streamingAssetsPath + "/AdvScrabble.db"; //Path to database.

		using(IDbConnection dbconn = new SqliteConnection(conn))
		{
			dbconn.Open ();

			using (IDbCommand dbcmd = dbconn.CreateCommand ())
			{
				string sqlQuery = String.Format ("DELETE FROM level_register WHERE id_game = \"{0}\" AND  id_player = \"{1}\" AND id_map = \"{2}\"", pIdGame, pIdPlayer, pIdMap);
				dbcmd.CommandText = sqlQuery;
				dbcmd.ExecuteScalar ();
				dbconn.Close();
			}

		}

	}
		
	public void UpdateListAchievements(int pIdGame, int pIdPlayer, List<Achievement> pListAch)
	{
		foreach (Achievement ach in pListAch)
		{
			this.UpdateAchievement (pIdGame, pIdPlayer, ach);
		}
	}

	public void UpdateListBonus(int pIdGame, int pIdPlayer, List<Bonus> pListBonus)
	{
		foreach (Bonus b in pListBonus)
		{
			this.UpdateBonus (pIdGame, pIdPlayer, b);
		}
	
	}

	public bool GetBoolean(int pNumberValue)
	{
		bool value = false;

		if(pNumberValue == 1)
		{
			value = true;
		}
		return value;
	}

	public int GetInteger(bool pBooleanValue)
	{
		int value = 0;

		if (pBooleanValue == true) 
		{
			value = 1;
		}

		return value;
	}

}
