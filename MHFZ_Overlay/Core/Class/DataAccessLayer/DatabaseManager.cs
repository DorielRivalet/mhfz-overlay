﻿using Dictionary;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data.SQLite;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Globalization;
using System.Windows.Documents;
using System.Windows.Markup;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Transactions;
using System.Collections;
using Octokit;
using System.Windows.Controls;
using System.Diagnostics;

// TODO: PascalCase for functions, camelCase for private fields, ALL_CAPS for constants
namespace MHFZ_Overlay
{
    // Singleton
    internal class DatabaseManager
    {
        private readonly string _connectionString;

        public readonly string dataSource = "Data Source="+Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MHFZ_Overlay\\MHFZ_Overlay.sqlite");

        private static DatabaseManager instance;

        private DatabaseManager()
        {
            // Private constructor to prevent external instantiation
            _connectionString = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MHFZ_Overlay\\MHFZ_Overlay.sqlite");
        }

        public static DatabaseManager GetInstance()
        {
            if (instance == null)
            {
                instance = new DatabaseManager();
            }

            return instance;
        }

        #region program time

        // Calculate the total time spent using the program
        public TimeSpan CalculateTotalTimeSpent()
        {
            TimeSpan totalTimeSpent = TimeSpan.Zero;

            using (var connection = new SQLiteConnection(dataSource))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT SUM(SessionDuration) FROM Session";
                    object result = command.ExecuteScalar();
                    if (result != DBNull.Value)
                    {
                        totalTimeSpent = TimeSpan.FromSeconds(Convert.ToInt32(result));
                    }
                }
            }

            return totalTimeSpent;
        }

        #endregion

        #region database

        public void SetupLocalDatabase(DataLoader dataLoader)
        {

            if (!File.Exists(_connectionString))
            {
                SQLiteConnection.CreateFile(_connectionString);
            }

            using (var conn = new SQLiteConnection(dataSource))
            {
                conn.Open();

                // Do something with the connection
                CreateDatabaseTables(conn, dataLoader);
                CreateDatabaseIndexes(conn);
                CreateDatabaseTriggers(conn);
            }
        }

        // Calculate the finalTimeDisplay value in the "mm:ss.mm" format
        //string finalTimeDisplay = TimeSpan.FromSeconds(timeLeft / 30.0).ToString();

        //// Insert the TimeLeft value into the FinalTimeValue field and the finalTimeDisplay value into the FinalTimeString field of the Quests table
        //string sql = "INSERT INTO Quests (QuestID, FinalTimeValue, FinalTimeString) VALUES (@QuestID, @FinalTimeValue, @FinalTimeString)";
        //using (SQLiteCommand cmd1 = new SQLiteCommand(sql, conn))
        //{
        //    cmd1.Parameters.AddWithValue("@QuestID", 1);
        //    cmd1.Parameters.AddWithValue("@FinalTimeValue", timeLeft);
        //    cmd1.Parameters.AddWithValue("@FinalTimeString", finalTimeDisplay.ToString("mm\\:ss\\.ff"));
        //    cmd1.ExecuteNonQuery();
        //}

        //sql = "SELECT FinalTimeValue, FinalTimeString FROM Quests WHERE QuestID = @QuestID ORDER BY FinalTimeValue ASC";
        //using (SQLiteCommand cmd1 = new SQLiteCommand(sql, conn))
        //{
        //    cmd1.Parameters.AddWithValue("@QuestID", 1);
        //    using (SQLiteDataReader reader = cmd1.ExecuteReader())
        //    {
        //        while (reader.Read())
        //        {
        //            int finalTimeValue = reader.GetInt32(0);
        //            string finalTimeString = reader.GetString(1);
        //            // Do something with the finalTimeValue and finalTimeString values
        //        }
        //    }
        //}

        public void InsertQuestData(string connectionString, DataLoader dataLoader)
        {
            if (!dataLoader.model.questCleared)
                return;

            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                var model = dataLoader.model;
                Settings s = (Settings)System.Windows.Application.Current.TryFindResource("Settings");
                string sql;

                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Insert data into the Quests table
                        sql = @"INSERT INTO Quests (
                        QuestID, AreaID, FinalTimeValue, FinalTimeDisplay, ObjectiveImage, ObjectiveTypeID, ObjectiveQuantity, StarGrade, RankNameID, ObjectiveName, Date
                        ) VALUES (@QuestID, @AreaID, @FinalTimeValue, @FinalTimeDisplay, @ObjectiveImage, @ObjectiveTypeID, @ObjectiveQuantity, @StarGrade, @RankNameID, @ObjectiveName, @Date)";

                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            int questID = model.QuestID();
                            int areaID = model.AreaID();
                            int timeLeft = model.TimeInt(); // Example value of the TimeLeft variable
                            int finalTimeValue = timeLeft;
                            // Calculate the elapsed time of the quest
                            string finalTimeDisplay = dataLoader.GetQuestTimeCompletion();
                            // Convert the elapsed time to a DateTime object
                            DateTime endTime = DateTime.ParseExact(finalTimeDisplay, @"mm\:ss\.ff", CultureInfo.InvariantCulture);
                            string objectiveImage;
                            //Gathering/etc
                            if ((dataLoader.model.ObjectiveType() == 0x0 || dataLoader.model.ObjectiveType() == 0x02 || dataLoader.model.ObjectiveType() == 0x1002) && (dataLoader.model.QuestID() != 23527 && dataLoader.model.QuestID() != 23628 && dataLoader.model.QuestID() != 21731 && dataLoader.model.QuestID() != 21749 && dataLoader.model.QuestID() != 21746 && dataLoader.model.QuestID() != 21750))
                            {
                                objectiveImage = MainWindow.GetAreaIconFromID(dataLoader.model.AreaID());
                            }
                            //Tenrou Sky Corridor areas
                            else if (dataLoader.model.AreaID() == 391 || dataLoader.model.AreaID() == 392 || dataLoader.model.AreaID() == 394 || dataLoader.model.AreaID() == 415 || dataLoader.model.AreaID() == 416)
                            {
                                objectiveImage = MainWindow.GetAreaIconFromID(dataLoader.model.AreaID());

                            }
                            //Duremudira Doors
                            else if (dataLoader.model.AreaID() == 399 || dataLoader.model.AreaID() == 414)
                            {
                                objectiveImage = MainWindow.GetAreaIconFromID(dataLoader.model.AreaID());
                            }
                            //Duremudira Arena
                            else if (dataLoader.model.AreaID() == 398)
                            {
                                objectiveImage = dataLoader.model.getMonsterIcon(dataLoader.model.LargeMonster1ID());
                            }
                            //Hunter's Road Base Camp
                            else if (dataLoader.model.AreaID() == 459)
                            {
                                objectiveImage = MainWindow.GetAreaIconFromID(dataLoader.model.AreaID());
                            }
                            //Raviente
                            else if (dataLoader.model.AreaID() == 309 || (dataLoader.model.AreaID() >= 311 && dataLoader.model.AreaID() <= 321) || (dataLoader.model.AreaID() >= 417 && dataLoader.model.AreaID() <= 422) || dataLoader.model.AreaID() == 437 || (dataLoader.model.AreaID() >= 440 && dataLoader.model.AreaID() <= 444))
                            {
                                objectiveImage = dataLoader.model.getMonsterIcon(dataLoader.model.LargeMonster1ID());
                            }
                            else
                            {
                                objectiveImage = dataLoader.model.getMonsterIcon(dataLoader.model.LargeMonster1ID());
                            }

                            int objectiveTypeID = model.ObjectiveType();

                            string objectiveName;
                            if ((model.ObjectiveType() == 0x0 || model.ObjectiveType() == 0x02 || model.ObjectiveType() == 0x1002 || model.ObjectiveType() == 0x10) && (model.QuestID() != 23527 && model.QuestID() != 23628 && model.QuestID() != 21731 && model.QuestID() != 21749 && model.QuestID() != 21746 && model.QuestID() != 21750))
                                objectiveName = model.GetObjective1Name(model.Objective1ID(), true);
                            else
                                objectiveName = model.GetRealMonsterName(model.CurrentMonster1Icon, true);

                            string rankName = model.GetRankNameFromID(model.RankBand(), true);
                            int objectiveQuantity = model.Objective1Quantity();
                            int starGrade = model.StarGrades();

                            if ((model.ObjectiveType() == 0x0 || model.ObjectiveType() == 0x02 || model.ObjectiveType() == 0x1002 || model.ObjectiveType() == 0x10) && (model.QuestID() != 23527 && model.QuestID() != 23628 && model.QuestID() != 21731 && model.QuestID() != 21749 && model.QuestID() != 21746 && model.QuestID() != 21750))
                                objectiveName = model.GetObjective1Name(model.Objective1ID(), true);
                            else
                                objectiveName = model.GetRealMonsterName(model.CurrentMonster1Icon, true);

                            DateTime date = DateTime.Now;

                            //                    --Insert data into the ZenithSkills table
                            //INSERT INTO ZenithSkills(ZenithSkill1, ZenithSkill2, ZenithSkill3, ZenithSkill4, ZenithSkill5, ZenithSkill6)
                            //VALUES(zenithSkillsID, zenithSkillsID, zenithSkillsID, zenithSkillsID, zenithSkillsID, zenithSkillsID);

                            //                    --Get the ZenithSkillsID that was generated
                            //                    SELECT LAST_INSERT_ROWID() as ZenithSkillsID;

                            cmd.Parameters.AddWithValue("@QuestID", questID);
                            cmd.Parameters.AddWithValue("@AreaID", areaID);
                            cmd.Parameters.AddWithValue("@FinalTimeValue", finalTimeValue);
                            cmd.Parameters.AddWithValue("@FinalTimeDisplay", finalTimeDisplay);
                            cmd.Parameters.AddWithValue("@ObjectiveImage", objectiveImage);
                            cmd.Parameters.AddWithValue("@ObjectiveTypeID", objectiveTypeID);
                            cmd.Parameters.AddWithValue("@ObjectiveQuantity", objectiveQuantity);
                            cmd.Parameters.AddWithValue("@StarGrade", starGrade);
                            cmd.Parameters.AddWithValue("@RankNameID", rankName);
                            cmd.Parameters.AddWithValue("@ObjectiveName", objectiveName);
                            cmd.Parameters.AddWithValue("@Date", date);
                            cmd.ExecuteNonQuery();
                        }

                        sql = "SELECT LAST_INSERT_ROWID()";
                        int runID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            runID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insert data into the Players table
                        sql = "INSERT INTO Players (PlayerName, GuildName, Gender) VALUES (@PlayerName, @GuildName, @Gender)";
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            string playerName = s.HunterName;
                            string guildName = s.GuildName;
                            string gender = s.GenderExport;

                            cmd.Parameters.AddWithValue("@PlayerName", playerName);
                            cmd.Parameters.AddWithValue("@GuildName", guildName);
                            cmd.Parameters.AddWithValue("@Gender", gender);
                            cmd.ExecuteNonQuery();
                        }

                        // Get the ID of the last inserted row in the Players table
                        sql = "SELECT LAST_INSERT_ROWID()";
                        int playerID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            playerID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        // Insert data into the ZenithSkills table
                        sql = "INSERT INTO ZenithSkills (RunID, ZenithSkill1ID, ZenithSkill2ID, ZenithSkill3ID, ZenithSkill4ID, ZenithSkill5ID, ZenithSkill6ID, ZenithSkill7ID) VALUES (@RunID, @ZenithSkill1ID, @ZenithSkill2ID, @ZenithSkill3ID, @ZenithSkill4ID, @ZenithSkill5ID, @ZenithSkill6ID, @ZenithSkill7ID)";
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            int zenithSkill1ID = model.ZenithSkill1();
                            int zenithSkill2ID = model.ZenithSkill2();
                            int zenithSkill3ID = model.ZenithSkill3();
                            int zenithSkill4ID = model.ZenithSkill4();
                            int zenithSkill5ID = model.ZenithSkill5();
                            int zenithSkill6ID = model.ZenithSkill6();
                            int zenithSkill7ID = model.ZenithSkill7();

                            cmd.Parameters.AddWithValue("@RunID", runID);
                            cmd.Parameters.AddWithValue("@ZenithSkill1ID", zenithSkill1ID);
                            cmd.Parameters.AddWithValue("@ZenithSkill2ID", zenithSkill2ID);
                            cmd.Parameters.AddWithValue("@ZenithSkill3ID", zenithSkill3ID);
                            cmd.Parameters.AddWithValue("@ZenithSkill4ID", zenithSkill4ID);
                            cmd.Parameters.AddWithValue("@ZenithSkill5ID", zenithSkill5ID);
                            cmd.Parameters.AddWithValue("@ZenithSkill6ID", zenithSkill6ID);
                            cmd.Parameters.AddWithValue("@ZenithSkill7ID", zenithSkill7ID);
                            cmd.ExecuteNonQuery();
                        }

                        sql = "SELECT LAST_INSERT_ROWID()";
                        int zenithSkillsID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            zenithSkillsID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        sql = "INSERT INTO AutomaticSkills (RunID, AutomaticSkill1ID, AutomaticSkill2ID, AutomaticSkill3ID, AutomaticSkill4ID, AutomaticSkill5ID, AutomaticSkill6ID) VALUES (@RunID, @AutomaticSkill1ID, @AutomaticSkill2ID, @AutomaticSkill3ID, @AutomaticSkill4ID, @AutomaticSkill5ID, @AutomaticSkill6ID)";
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            int automaticSkill1ID = model.AutomaticSkillWeapon();
                            int automaticSkill2ID = model.AutomaticSkillHead();
                            int automaticSkill3ID = model.AutomaticSkillChest();
                            int automaticSkill4ID = model.AutomaticSkillArms();
                            int automaticSkill5ID = model.AutomaticSkillWaist();
                            int automaticSkill6ID = model.AutomaticSkillLegs();

                            cmd.Parameters.AddWithValue("@RunID", runID);
                            cmd.Parameters.AddWithValue("@AutomaticSkill1ID", automaticSkill1ID);
                            cmd.Parameters.AddWithValue("@AutomaticSkill2ID", automaticSkill2ID);
                            cmd.Parameters.AddWithValue("@AutomaticSkill3ID", automaticSkill3ID);
                            cmd.Parameters.AddWithValue("@AutomaticSkill4ID", automaticSkill4ID);
                            cmd.Parameters.AddWithValue("@AutomaticSkill5ID", automaticSkill5ID);
                            cmd.Parameters.AddWithValue("@AutomaticSkill6ID", automaticSkill6ID);
                            cmd.ExecuteNonQuery();
                        }

                        sql = "SELECT LAST_INSERT_ROWID()";
                        int automaticSkillsID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            automaticSkillsID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        sql = "INSERT INTO ActiveSkills (RunID, ActiveSkill1ID, ActiveSkill2ID, ActiveSkill3ID, ActiveSkill4ID, ActiveSkill5ID, ActiveSkill6ID, ActiveSkill7ID, ActiveSkill8ID, ActiveSkill9ID, ActiveSkill10ID, ActiveSkill11ID, ActiveSkill12ID,ActiveSkill13ID,ActiveSkill14ID,ActiveSkill15ID,ActiveSkill16ID,ActiveSkill17ID,ActiveSkill18ID,ActiveSkill19ID) VALUES (@RunID, @ActiveSkill1ID, @ActiveSkill2ID, @ActiveSkill3ID, @ActiveSkill4ID, @ActiveSkill5ID, @ActiveSkill6ID, @ActiveSkill7ID, @ActiveSkill8ID, @ActiveSkill9ID, @ActiveSkill10ID, @ActiveSkill11ID, @ActiveSkill12ID, @ActiveSkill13ID, @ActiveSkill14ID, @ActiveSkill15ID, @ActiveSkill16ID, @ActiveSkill17ID, @ActiveSkill18ID, @ActiveSkill19ID)";
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            int activeSkill1ID = model.ArmorSkill1();
                            int activeSkill2ID = model.ArmorSkill2();
                            int activeSkill3ID = model.ArmorSkill3();
                            int activeSkill4ID = model.ArmorSkill4();
                            int activeSkill5ID = model.ArmorSkill5();
                            int activeSkill6ID = model.ArmorSkill6();
                            int activeSkill7ID = model.ArmorSkill7();
                            int activeSkill8ID = model.ArmorSkill8();
                            int activeSkill9ID = model.ArmorSkill9();
                            int activeSkill10ID = model.ArmorSkill10();
                            int activeSkill11ID = model.ArmorSkill11();
                            int activeSkill12ID = model.ArmorSkill12();
                            int activeSkill13ID = model.ArmorSkill13();
                            int activeSkill14ID = model.ArmorSkill14();
                            int activeSkill15ID = model.ArmorSkill15();
                            int activeSkill16ID = model.ArmorSkill16();
                            int activeSkill17ID = model.ArmorSkill17();
                            int activeSkill18ID = model.ArmorSkill18();
                            int activeSkill19ID = model.ArmorSkill19();

                            cmd.Parameters.AddWithValue("@RunID", runID);
                            cmd.Parameters.AddWithValue("@ActiveSkill1ID", activeSkill1ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill2ID", activeSkill2ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill3ID", activeSkill3ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill4ID", activeSkill4ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill5ID", activeSkill5ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill6ID", activeSkill6ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill7ID", activeSkill7ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill8ID", activeSkill8ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill9ID", activeSkill9ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill10ID", activeSkill10ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill11ID", activeSkill11ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill12ID", activeSkill12ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill13ID", activeSkill13ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill14ID", activeSkill14ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill15ID", activeSkill15ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill16ID", activeSkill16ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill17ID", activeSkill17ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill18ID", activeSkill18ID);
                            cmd.Parameters.AddWithValue("@ActiveSkill19ID", activeSkill19ID);
                            cmd.ExecuteNonQuery();
                        }

                        sql = "SELECT LAST_INSERT_ROWID()";
                        int activeSkillsID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            activeSkillsID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        sql = "INSERT INTO CaravanSkills (RunID, CaravanSkill1ID, CaravanSkill2ID, CaravanSkill3ID) VALUES (@RunID, @CaravanSkill1ID, @CaravanSkill2ID, @CaravanSkill3ID)";
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            int caravanSkill1ID = model.CaravanSkill1();
                            int caravanSkill2ID = model.CaravanSkill2();
                            int caravanSkill3ID = model.CaravanSkill3();

                            cmd.Parameters.AddWithValue("@RunID", runID);
                            cmd.Parameters.AddWithValue("@CaravanSkill1ID", caravanSkill1ID);
                            cmd.Parameters.AddWithValue("@CaravanSkill2ID", caravanSkill2ID);
                            cmd.Parameters.AddWithValue("@CaravanSkill3ID", caravanSkill3ID);
                            cmd.ExecuteNonQuery();
                        }

                        sql = "SELECT LAST_INSERT_ROWID()";
                        int caravanSkillsID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            caravanSkillsID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        sql = "INSERT INTO StyleRankSkills (RunID, StyleRankSkill1ID, StyleRankSkill2ID) VALUES (@RunID, @StyleRankSkill1ID, @StyleRankSkill2ID)";
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            int styleRankSkill1ID = model.StyleRank1();
                            int styleRankSkill2ID = model.StyleRank2();

                            cmd.Parameters.AddWithValue("@RunID", runID);
                            cmd.Parameters.AddWithValue("@StyleRankSkill1ID", styleRankSkill1ID);
                            cmd.Parameters.AddWithValue("@StyleRankSkill2ID", styleRankSkill2ID);
                            cmd.ExecuteNonQuery();
                        }

                        sql = "SELECT LAST_INSERT_ROWID()";
                        int styleRankSkillsID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            styleRankSkillsID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        sql = @"INSERT INTO PlayerInventory (
                            RunID,
                            Item1ID , 
                            Item1Quantity ,
                            Item2ID , 
                            Item2Quantity ,
                            Item3ID , 
                            Item3Quantity ,
                            Item4ID , 
                            Item4Quantity ,
                            Item5ID , 
                            Item5Quantity ,
                            Item6ID , 
                            Item6Quantity ,
                            Item7ID , 
                            Item7Quantity ,
                            Item8ID , 
                            Item8Quantity ,
                            Item9ID , 
                            Item9Quantity ,
                            Item10ID , 
                            Item10Quantity ,
                            Item11ID , 
                            Item11Quantity ,
                            Item12ID , 
                            Item12Quantity ,
                            Item13ID , 
                            Item13Quantity ,
                            Item14ID , 
                            Item14Quantity ,
                            Item15ID , 
                            Item15Quantity ,
                            Item16ID , 
                            Item16Quantity ,
                            Item17ID , 
                            Item17Quantity ,
                            Item18ID , 
                            Item18Quantity ,
                            Item19ID , 
                            Item19Quantity ,
                            Item20ID , 
                            Item20Quantity )
                            VALUES (
                            @RunID,
                            @Item1ID , 
                            @Item1Quantity ,
                            @Item2ID , 
                            @Item2Quantity ,
                            @Item3ID , 
                            @Item3Quantity ,
                            @Item4ID , 
                            @Item4Quantity ,
                            @Item5ID , 
                            @Item5Quantity ,
                            @Item6ID , 
                            @Item6Quantity ,
                            @Item7ID , 
                            @Item7Quantity ,
                            @Item8ID , 
                            @Item8Quantity ,
                            @Item9ID , 
                            @Item9Quantity ,
                            @Item10ID , 
                            @Item10Quantity ,
                            @Item11ID , 
                            @Item11Quantity ,
                            @Item12ID , 
                            @Item12Quantity ,
                            @Item13ID , 
                            @Item13Quantity ,
                            @Item14ID , 
                            @Item14Quantity ,
                            @Item15ID , 
                            @Item15Quantity ,
                            @Item16ID , 
                            @Item16Quantity ,
                            @Item17ID , 
                            @Item17Quantity ,
                            @Item18ID , 
                            @Item18Quantity ,
                            @Item19ID , 
                            @Item19Quantity ,
                            @Item20ID , 
                            @Item20Quantity )";

                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            int item1ID = model.PouchItem1IDAtQuestStart;
                            int item1Quantity = model.PouchItem1QuantityAtQuestStart;
                            int item2ID = model.PouchItem2IDAtQuestStart;
                            int item2Quantity = model.PouchItem2QuantityAtQuestStart;
                            int item3ID = model.PouchItem3IDAtQuestStart;
                            int item3Quantity = model.PouchItem3QuantityAtQuestStart;
                            int item4ID = model.PouchItem4IDAtQuestStart;
                            int item4Quantity = model.PouchItem4QuantityAtQuestStart;
                            int item5ID = model.PouchItem5IDAtQuestStart;
                            int item5Quantity = model.PouchItem5QuantityAtQuestStart;
                            int item6ID = model.PouchItem6IDAtQuestStart;
                            int item6Quantity = model.PouchItem6QuantityAtQuestStart;
                            int item7ID = model.PouchItem7IDAtQuestStart;
                            int item7Quantity = model.PouchItem7QuantityAtQuestStart;
                            int item8ID = model.PouchItem8IDAtQuestStart;
                            int item8Quantity = model.PouchItem8QuantityAtQuestStart;
                            int item9ID = model.PouchItem9IDAtQuestStart;
                            int item9Quantity = model.PouchItem9QuantityAtQuestStart;
                            int item10ID = model.PouchItem10IDAtQuestStart;
                            int item10Quantity = model.PouchItem10QuantityAtQuestStart;
                            int item11ID = model.PouchItem11IDAtQuestStart;
                            int item11Quantity = model.PouchItem11QuantityAtQuestStart;
                            int item12ID = model.PouchItem12IDAtQuestStart;
                            int item12Quantity = model.PouchItem12QuantityAtQuestStart;
                            int item13ID = model.PouchItem13IDAtQuestStart;
                            int item13Quantity = model.PouchItem13QuantityAtQuestStart;
                            int item14ID = model.PouchItem14IDAtQuestStart;
                            int item14Quantity = model.PouchItem14QuantityAtQuestStart;
                            int item15ID = model.PouchItem15IDAtQuestStart;
                            int item15Quantity = model.PouchItem15QuantityAtQuestStart;
                            int item16ID = model.PouchItem16IDAtQuestStart;
                            int item16Quantity = model.PouchItem16QuantityAtQuestStart;
                            int item17ID = model.PouchItem17IDAtQuestStart;
                            int item17Quantity = model.PouchItem17QuantityAtQuestStart;
                            int item18ID = model.PouchItem18IDAtQuestStart;
                            int item18Quantity = model.PouchItem18QuantityAtQuestStart;
                            int item19ID = model.PouchItem19IDAtQuestStart;
                            int item19Quantity = model.PouchItem19QuantityAtQuestStart;
                            int item20ID = model.PouchItem20IDAtQuestStart;
                            int item20Quantity = model.PouchItem20QuantityAtQuestStart;

                            cmd.Parameters.AddWithValue("@RunID", runID);
                            cmd.Parameters.AddWithValue("@Item1ID", item1ID);
                            cmd.Parameters.AddWithValue("@Item1Quantity", item1Quantity);
                            cmd.Parameters.AddWithValue("@Item2ID", item2ID);
                            cmd.Parameters.AddWithValue("@Item2Quantity", item2Quantity);
                            cmd.Parameters.AddWithValue("@Item3ID", item3ID);
                            cmd.Parameters.AddWithValue("@Item3Quantity", item3Quantity);
                            cmd.Parameters.AddWithValue("@Item4ID", item4ID);
                            cmd.Parameters.AddWithValue("@Item4Quantity", item4Quantity);
                            cmd.Parameters.AddWithValue("@Item5ID", item5ID);
                            cmd.Parameters.AddWithValue("@Item5Quantity", item5Quantity);
                            cmd.Parameters.AddWithValue("@Item6ID", item6ID);
                            cmd.Parameters.AddWithValue("@Item6Quantity", item6Quantity);
                            cmd.Parameters.AddWithValue("@Item7ID", item7ID);
                            cmd.Parameters.AddWithValue("@Item7Quantity", item7Quantity);
                            cmd.Parameters.AddWithValue("@Item8ID", item8ID);
                            cmd.Parameters.AddWithValue("@Item8Quantity", item8Quantity);
                            cmd.Parameters.AddWithValue("@Item9ID", item9ID);
                            cmd.Parameters.AddWithValue("@Item9Quantity", item9Quantity);
                            cmd.Parameters.AddWithValue("@Item10ID", item10ID);
                            cmd.Parameters.AddWithValue("@Item10Quantity", item10Quantity);
                            cmd.Parameters.AddWithValue("@Item11ID", item11ID);
                            cmd.Parameters.AddWithValue("@Item11Quantity", item11Quantity);
                            cmd.Parameters.AddWithValue("@Item12ID", item12ID);
                            cmd.Parameters.AddWithValue("@Item12Quantity", item12Quantity);
                            cmd.Parameters.AddWithValue("@Item13ID", item13ID);
                            cmd.Parameters.AddWithValue("@Item13Quantity", item13Quantity);
                            cmd.Parameters.AddWithValue("@Item14ID", item14ID);
                            cmd.Parameters.AddWithValue("@Item14Quantity", item14Quantity);
                            cmd.Parameters.AddWithValue("@Item15ID", item15ID);
                            cmd.Parameters.AddWithValue("@Item15Quantity", item15Quantity);
                            cmd.Parameters.AddWithValue("@Item16ID", item16ID);
                            cmd.Parameters.AddWithValue("@Item16Quantity", item16Quantity);
                            cmd.Parameters.AddWithValue("@Item17ID", item17ID);
                            cmd.Parameters.AddWithValue("@Item17Quantity", item17Quantity);
                            cmd.Parameters.AddWithValue("@Item18ID", item18ID);
                            cmd.Parameters.AddWithValue("@Item18Quantity", item18Quantity);
                            cmd.Parameters.AddWithValue("@Item19ID", item19ID);
                            cmd.Parameters.AddWithValue("@Item19Quantity", item19Quantity);
                            cmd.Parameters.AddWithValue("@Item20ID", item20ID);
                            cmd.Parameters.AddWithValue("@Item20Quantity", item20Quantity);
                            cmd.ExecuteNonQuery();
                        }

                        sql = "SELECT LAST_INSERT_ROWID()";
                        int playerInventoryID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            playerInventoryID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        sql = @"INSERT INTO AmmoPouch (
                            RunID,
                            Item1ID , 
                            Item1Quantity ,
                            Item2ID , 
                            Item2Quantity ,
                            Item3ID , 
                            Item3Quantity ,
                            Item4ID , 
                            Item4Quantity ,
                            Item5ID , 
                            Item5Quantity ,
                            Item6ID , 
                            Item6Quantity ,
                            Item7ID , 
                            Item7Quantity ,
                            Item8ID , 
                            Item8Quantity ,
                            Item9ID , 
                            Item9Quantity ,
                            Item10ID , 
                            Item10Quantity
                            )
                            VALUES (
                            @RunID,
                            @Item1ID , 
                            @Item1Quantity ,
                            @Item2ID , 
                            @Item2Quantity ,
                            @Item3ID , 
                            @Item3Quantity ,
                            @Item4ID , 
                            @Item4Quantity ,
                            @Item5ID , 
                            @Item5Quantity ,
                            @Item6ID , 
                            @Item6Quantity ,
                            @Item7ID , 
                            @Item7Quantity ,
                            @Item8ID , 
                            @Item8Quantity ,
                            @Item9ID , 
                            @Item9Quantity ,
                            @Item10ID , 
                            @Item10Quantity)";

                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            int item1ID = model.AmmoPouchItem1IDAtQuestStart;
                            int item1Quantity = model.AmmoPouchItem1QuantityAtQuestStart;
                            int item2ID = model.AmmoPouchItem2IDAtQuestStart;
                            int item2Quantity = model.AmmoPouchItem2QuantityAtQuestStart;
                            int item3ID = model.AmmoPouchItem3IDAtQuestStart;
                            int item3Quantity = model.AmmoPouchItem3QuantityAtQuestStart;
                            int item4ID = model.AmmoPouchItem4IDAtQuestStart;
                            int item4Quantity = model.AmmoPouchItem4QuantityAtQuestStart;
                            int item5ID = model.AmmoPouchItem5IDAtQuestStart;
                            int item5Quantity = model.AmmoPouchItem5QuantityAtQuestStart;
                            int item6ID = model.AmmoPouchItem6IDAtQuestStart;
                            int item6Quantity = model.AmmoPouchItem6QuantityAtQuestStart;
                            int item7ID = model.AmmoPouchItem7IDAtQuestStart;
                            int item7Quantity = model.AmmoPouchItem7QuantityAtQuestStart;
                            int item8ID = model.AmmoPouchItem8IDAtQuestStart;
                            int item8Quantity = model.AmmoPouchItem8QuantityAtQuestStart;
                            int item9ID = model.AmmoPouchItem9IDAtQuestStart;
                            int item9Quantity = model.AmmoPouchItem9QuantityAtQuestStart;
                            int item10ID = model.AmmoPouchItem10IDAtQuestStart;
                            int item10Quantity = model.AmmoPouchItem10QuantityAtQuestStart;

                            cmd.Parameters.AddWithValue("@RunID", runID);
                            cmd.Parameters.AddWithValue("@Item1ID", item1ID);
                            cmd.Parameters.AddWithValue("@Item1Quantity", item1Quantity);
                            cmd.Parameters.AddWithValue("@Item2ID", item2ID);
                            cmd.Parameters.AddWithValue("@Item2Quantity", item2Quantity);
                            cmd.Parameters.AddWithValue("@Item3ID", item3ID);
                            cmd.Parameters.AddWithValue("@Item3Quantity", item3Quantity);
                            cmd.Parameters.AddWithValue("@Item4ID", item4ID);
                            cmd.Parameters.AddWithValue("@Item4Quantity", item4Quantity);
                            cmd.Parameters.AddWithValue("@Item5ID", item5ID);
                            cmd.Parameters.AddWithValue("@Item5Quantity", item5Quantity);
                            cmd.Parameters.AddWithValue("@Item6ID", item6ID);
                            cmd.Parameters.AddWithValue("@Item6Quantity", item6Quantity);
                            cmd.Parameters.AddWithValue("@Item7ID", item7ID);
                            cmd.Parameters.AddWithValue("@Item7Quantity", item7Quantity);
                            cmd.Parameters.AddWithValue("@Item8ID", item8ID);
                            cmd.Parameters.AddWithValue("@Item8Quantity", item8Quantity);
                            cmd.Parameters.AddWithValue("@Item9ID", item9ID);
                            cmd.Parameters.AddWithValue("@Item9Quantity", item9Quantity);
                            cmd.Parameters.AddWithValue("@Item10ID", item10ID);
                            cmd.Parameters.AddWithValue("@Item10Quantity", item10Quantity);
                            cmd.ExecuteNonQuery();
                        }

                        sql = "SELECT LAST_INSERT_ROWID()";
                        int ammoPouchID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            ammoPouchID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        sql = @"INSERT INTO RoadDureSkills (
                        RunID, 
                        RoadDureSkill1ID,
                        RoadDureSkill1Level,
                        RoadDureSkill2ID,
                        RoadDureSkill2Level,
                        RoadDureSkill3ID,
                        RoadDureSkill3Level,
                        RoadDureSkill4ID,
                        RoadDureSkill4Level,
                        RoadDureSkill5ID,
                        RoadDureSkill5Level,
                        RoadDureSkill6ID,
                        RoadDureSkill6Level,
                        RoadDureSkill7ID,
                        RoadDureSkill7Level,
                        RoadDureSkill8ID,
                        RoadDureSkill8Level,
                        RoadDureSkill9ID,
                        RoadDureSkill9Level,
                        RoadDureSkill10ID,
                        RoadDureSkill10Level,
                        RoadDureSkill11ID,
                        RoadDureSkill11Level,
                        RoadDureSkill12ID,
                        RoadDureSkill12Level,
                        RoadDureSkill13ID,
                        RoadDureSkill13Level,
                        RoadDureSkill14ID,
                        RoadDureSkill14Level,
                        RoadDureSkill15ID,
                        RoadDureSkill15Level,
                        RoadDureSkill16ID,
                        RoadDureSkill16Level
                        ) VALUES (
                        @RunID, 
                        @RoadDureSkill1ID,
                        @RoadDureSkill1Level,
                        @RoadDureSkill2ID,
                        @RoadDureSkill2Level,
                        @RoadDureSkill3ID,
                        @RoadDureSkill3Level,
                        @RoadDureSkill4ID,
                        @RoadDureSkill4Level,
                        @RoadDureSkill5ID,
                        @RoadDureSkill5Level,
                        @RoadDureSkill6ID,
                        @RoadDureSkill6Level,
                        @RoadDureSkill7ID,
                        @RoadDureSkill7Level,
                        @RoadDureSkill8ID,
                        @RoadDureSkill8Level,
                        @RoadDureSkill9ID,
                        @RoadDureSkill9Level,
                        @RoadDureSkill10ID,
                        @RoadDureSkill10Level,
                        @RoadDureSkill11ID,
                        @RoadDureSkill11Level,
                        @RoadDureSkill12ID,
                        @RoadDureSkill12Level,
                        @RoadDureSkill13ID,
                        @RoadDureSkill13Level,
                        @RoadDureSkill14ID,
                        @RoadDureSkill14Level,
                        @RoadDureSkill15ID,
                        @RoadDureSkill15Level,
                        @RoadDureSkill16ID,
                        @RoadDureSkill16Level
                        )";

                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            int roadDureSkill1ID = model.RoadDureSkill1Name();
                            int roadDureSkill2ID = model.RoadDureSkill2Name();
                            int roadDureSkill3ID = model.RoadDureSkill3Name();
                            int roadDureSkill4ID = model.RoadDureSkill4Name();
                            int roadDureSkill5ID = model.RoadDureSkill5Name();
                            int roadDureSkill6ID = model.RoadDureSkill6Name();
                            int roadDureSkill7ID = model.RoadDureSkill7Name();
                            int roadDureSkill8ID = model.RoadDureSkill8Name();
                            int roadDureSkill9ID = model.RoadDureSkill9Name();
                            int roadDureSkill10ID = model.RoadDureSkill10Name();
                            int roadDureSkill11ID = model.RoadDureSkill11Name();
                            int roadDureSkill12ID = model.RoadDureSkill12Name();
                            int roadDureSkill13ID = model.RoadDureSkill13Name();
                            int roadDureSkill14ID = model.RoadDureSkill14Name();
                            int roadDureSkill15ID = model.RoadDureSkill15Name();
                            int roadDureSkill16ID = model.RoadDureSkill16Name();

                            int roadDureSkill1Level = model.RoadDureSkill1Level();
                            int roadDureSkill2Level = model.RoadDureSkill2Level();
                            int roadDureSkill3Level = model.RoadDureSkill3Level();
                            int roadDureSkill4Level = model.RoadDureSkill4Level();
                            int roadDureSkill5Level = model.RoadDureSkill5Level();
                            int roadDureSkill6Level = model.RoadDureSkill6Level();
                            int roadDureSkill7Level = model.RoadDureSkill7Level();
                            int roadDureSkill8Level = model.RoadDureSkill8Level();
                            int roadDureSkill9Level = model.RoadDureSkill9Level();
                            int roadDureSkill10Level = model.RoadDureSkill10Level();
                            int roadDureSkill11Level = model.RoadDureSkill11Level();
                            int roadDureSkill12Level = model.RoadDureSkill12Level();
                            int roadDureSkill13Level = model.RoadDureSkill13Level();
                            int roadDureSkill14Level = model.RoadDureSkill14Level();
                            int roadDureSkill15Level = model.RoadDureSkill15Level();
                            int roadDureSkill16Level = model.RoadDureSkill16Level();

                            cmd.Parameters.AddWithValue("@RunID", runID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill1ID", roadDureSkill1ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill2ID", roadDureSkill2ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill3ID", roadDureSkill3ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill4ID", roadDureSkill4ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill5ID", roadDureSkill5ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill6ID", roadDureSkill6ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill7ID", roadDureSkill7ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill8ID", roadDureSkill8ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill9ID", roadDureSkill9ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill10ID", roadDureSkill10ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill11ID", roadDureSkill11ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill12ID", roadDureSkill12ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill13ID", roadDureSkill13ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill14ID", roadDureSkill14ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill15ID", roadDureSkill15ID);
                            cmd.Parameters.AddWithValue("@RoadDureSkill16ID", roadDureSkill16ID);

                            cmd.Parameters.AddWithValue("@RoadDureSkill1Level", roadDureSkill1Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill2Level", roadDureSkill2Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill3Level", roadDureSkill3Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill4Level", roadDureSkill4Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill5Level", roadDureSkill5Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill6Level", roadDureSkill6Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill7Level", roadDureSkill7Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill8Level", roadDureSkill8Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill9Level", roadDureSkill9Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill10Level", roadDureSkill10Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill11Level", roadDureSkill11Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill12Level", roadDureSkill12Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill13Level", roadDureSkill13Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill14Level", roadDureSkill14Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill15Level", roadDureSkill15Level);
                            cmd.Parameters.AddWithValue("@RoadDureSkill16Level", roadDureSkill16Level);
                            cmd.ExecuteNonQuery();
                        }

                        sql = "SELECT LAST_INSERT_ROWID()";
                        int roadDureSkillsID;
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                        {
                            roadDureSkillsID = Convert.ToInt32(cmd.ExecuteScalar());
                        }

                        string gearName = s.GearDescriptionExport;
                        if (gearName == "" || gearName == null)
                            gearName = "Unnamed";

                        int weaponTypeID = model.WeaponType();
                        int weaponClassID = weaponTypeID;
                        int weaponID = model.BlademasterWeaponID();//ranged and melee are the same afaik
                        string weaponSlot1 = model.GetDecoName(model.WeaponDeco1ID(), 1);// no sigils in database ig
                        string weaponSlot2 = model.GetDecoName(model.WeaponDeco2ID(), 2);
                        string weaponSlot3 = model.GetDecoName(model.WeaponDeco3ID(), 3);
                        int headID = model.ArmorHeadID();
                        int headSlot1 = model.ArmorHeadDeco1ID();
                        int headSlot2 = model.ArmorHeadDeco2ID();
                        int headSlot3 = model.ArmorHeadDeco3ID();
                        int chestID = model.ArmorChestID();
                        int chestSlot1 = model.ArmorChestDeco1ID();
                        int chestSlot2 = model.ArmorChestDeco2ID();
                        int chestSlot3 = model.ArmorChestDeco3ID();
                        int armsID = model.ArmorArmsID();
                        int armsSlot1 = model.ArmorArmsDeco1ID();
                        int armsSlot2 = model.ArmorArmsDeco2ID();
                        int armsSlot3 = model.ArmorArmsDeco3ID();
                        int waistID = model.ArmorWaistID();
                        int waistSlot1 = model.ArmorWaistDeco1ID();
                        int waistSlot2 = model.ArmorWaistDeco2ID();
                        int waistSlot3 = model.ArmorWaistDeco3ID();
                        int legsID = model.ArmorLegsID();
                        int legsSlot1 = model.ArmorLegsDeco1ID();
                        int legsSlot2 = model.ArmorLegsDeco2ID();
                        int legsSlot3 = model.ArmorLegsDeco3ID();
                        int cuffSlot1 = model.Cuff1ID();
                        int cuffSlot2 = model.Cuff2ID();
                        string questName = model.GetQuestNameFromID(model.QuestID());
                        int styleID = model.WeaponStyle();
                        int weaponIconID = weaponTypeID;
                        int divaSkillID = model.DivaSkill();
                        int guildFoodID = model.GuildFoodSkill();
                        int poogieItemID = model.PoogieItemUseID();

                        int? blademasterWeaponID = null;
                        int? gunnerWeaponID = null;

                        //Check the WeaponTypeID and insert the corresponding weapon ID
                        switch (weaponTypeID)
                        {
                            case 0:
                            case 2:
                            case 3:
                            case 4:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                            case 11:
                            case 12:
                            case 13:
                                blademasterWeaponID = model.BlademasterWeaponID();
                                break;
                            case 1:
                            case 5:
                            case 10:
                                gunnerWeaponID = model.GunnerWeaponID();
                                break;
                        }

                        string insertSql = @"INSERT INTO PlayerGear (
                        --PlayerGearID INTEGER PRIMARY KEY AUTOINCREMENT,
                        RunID, --INTEGER NOT NULL, 
                        PlayerID, --INTEGER NOT NULL,
                        GearName, --TEXT NOT NULL,
                        StyleID, --INTEGER NOT NULL CHECK (StyleID >= 0),
                        WeaponIconID, --INTEGER NOT NULL,
                        WeaponClassID, --INTEGER NOT NULL,
                        WeaponTypeID, --INTEGER NOT NULL CHECK (WeaponTypeID >= 0),
                        BlademasterWeaponID, --INTEGER,
                        GunnerWeaponID, --INTEGER,
                        WeaponSlot1, --TEXT NOT NULL,
                        WeaponSlot2, --TEXT NOT NULL,
                        WeaponSlot3, --TEXT NOT NULL,
                        HeadID, --INTEGER NOT NULL CHECK (HeadID >= 0), 
                        HeadSlot1ID, --INTEGER NOT NULL CHECK (HeadSlot1ID >= 0),
                        HeadSlot2ID, --INTEGER NOT NULL CHECK (HeadSlot2ID >= 0),
                        HeadSlot3ID, --INTEGER NOT NULL CHECK (HeadSlot3ID >= 0),
                        ChestID, --INTEGER NOT NULL CHECK (ChestID >= 0),
                        ChestSlot1ID, --INTEGER NOT NULL CHECK (ChestSlot1ID >= 0),
                        ChestSlot2ID,-- INTEGER NOT NULL CHECK (ChestSlot2ID >= 0),
                        ChestSlot3ID,-- INTEGER NOT NULL CHECK (ChestSlot3ID >= 0),
                        ArmsID,-- INTEGER NOT NULL CHECK (ArmsID >= 0),
                        ArmsSlot1ID,-- INTEGER NOT NULL CHECK (ArmsSlot1ID >= 0),
                        ArmsSlot2ID,-- INTEGER NOT NULL CHECK (ArmsSlot2ID >= 0),
                        ArmsSlot3ID,-- INTEGER NOT NULL CHECK (ArmsSlot3ID >= 0),
                        WaistID,-- INTEGER NOT NULL CHECK (WaistID >= 0),
                        WaistSlot1ID,-- INTEGER NOT NULL CHECK (WaistSlot1ID >= 0),
                        WaistSlot2ID,-- INTEGER NOT NULL CHECK (WaistSlot2ID >= 0),
                        WaistSlot3ID,-- INTEGER NOT NULL CHECK (WaistSlot3ID >= 0),
                        LegsID,-- INTEGER NOT NULL CHECK (LegsID >= 0),
                        LegsSlot1ID,-- INTEGER NOT NULL CHECK (LegsSlot1ID >= 0),
                        LegsSlot2ID,-- INTEGER NOT NULL CHECK (LegsSlot2ID >= 0),
                        LegsSlot3ID,-- INTEGER NOT NULL CHECK (LegsSlot3ID >= 0),
                        Cuff1ID,-- INTEGER NOT NULL CHECK (Cuff1ID >= 0),
                        Cuff2ID,-- INTEGER NOT NULL CHECK (Cuff2ID >= 0),
                        ZenithSkillsID,-- INTEGER NOT NULL,
                        AutomaticSkillsID,-- INTEGER NOT NULL,
                        ActiveSkillsID,-- INTEGER NOT NULL,
                        CaravanSkillsID,-- INTEGER NOT NULL,
                        DivaSkillID,-- INTEGER NOT NULL,
                        GuildFoodID,-- INTEGER NOT NULL,
                        StyleRankSkillsID,-- INTEGER NOT NULL,
                        PlayerInventoryID,-- INTEGER NOT NULL,
                        AmmoPouchID,-- INTEGER NOT NULL,
                        PoogieItemID,-- INTEGER NOT NULL,
                        RoadDureSkillsID-- INTEGER NOT NULL,
                            ) VALUES (
                        --PlayerGearID INTEGER PRIMARY KEY AUTOINCREMENT,
                        @RunID, --INTEGER NOT NULL, 
                        @PlayerID, --INTEGER NOT NULL,
                        @GearName, --TEXT NOT NULL,
                        @StyleID, --INTEGER NOT NULL CHECK (StyleID >= 0),
                        @WeaponIconID, --INTEGER NOT NULL,
                        @WeaponClassID, --INTEGER NOT NULL,
                        @WeaponTypeID, --INTEGER NOT NULL CHECK (WeaponTypeID >= 0),
                        @BlademasterWeaponID, --INTEGER,
                        @GunnerWeaponID, --INTEGER,
                        @WeaponSlot1, --TEXT NOT NULL,
                        @WeaponSlot2, --TEXT NOT NULL,
                        @WeaponSlot3, --TEXT NOT NULL,
                        @HeadID, --INTEGER NOT NULL CHECK (HeadID >= 0), 
                        @HeadSlot1ID, --INTEGER NOT NULL CHECK (HeadSlot1ID >= 0),
                        @HeadSlot2ID, --INTEGER NOT NULL CHECK (HeadSlot2ID >= 0),
                        @HeadSlot3ID, --INTEGER NOT NULL CHECK (HeadSlot3ID >= 0),
                        @ChestID, --INTEGER NOT NULL CHECK (ChestID >= 0),
                        @ChestSlot1ID, --INTEGER NOT NULL CHECK (ChestSlot1ID >= 0),
                        @ChestSlot2ID,-- INTEGER NOT NULL CHECK (ChestSlot2ID >= 0),
                        @ChestSlot3ID,-- INTEGER NOT NULL CHECK (ChestSlot3ID >= 0),
                        @ArmsID,-- INTEGER NOT NULL CHECK (ArmsID >= 0),
                        @ArmsSlot1ID,-- INTEGER NOT NULL CHECK (ArmsSlot1ID >= 0),
                        @ArmsSlot2ID,-- INTEGER NOT NULL CHECK (ArmsSlot2ID >= 0),
                        @ArmsSlot3ID,-- INTEGER NOT NULL CHECK (ArmsSlot3ID >= 0),
                        @WaistID,-- INTEGER NOT NULL CHECK (WaistID >= 0),
                        @WaistSlot1ID,-- INTEGER NOT NULL CHECK (WaistSlot1ID >= 0),
                        @WaistSlot2ID,-- INTEGER NOT NULL CHECK (WaistSlot2ID >= 0),
                        @WaistSlot3ID,-- INTEGER NOT NULL CHECK (WaistSlot3ID >= 0),
                        @LegsID,-- INTEGER NOT NULL CHECK (LegsID >= 0),
                        @LegsSlot1ID,-- INTEGER NOT NULL CHECK (LegsSlot1ID >= 0),
                        @LegsSlot2ID,-- INTEGER NOT NULL CHECK (LegsSlot2ID >= 0),
                        @LegsSlot3ID,-- INTEGER NOT NULL CHECK (LegsSlot3ID >= 0),
                        @Cuff1ID,-- INTEGER NOT NULL CHECK (Cuff1ID >= 0),
                        @Cuff2ID,-- INTEGER NOT NULL CHECK (Cuff2ID >= 0),
                        @ZenithSkillsID,-- INTEGER NOT NULL,
                        @AutomaticSkillsID,-- INTEGER NOT NULL,
                        @ActiveSkillsID,-- INTEGER NOT NULL,
                        @CaravanSkillsID,-- INTEGER NOT NULL,
                        @DivaSkillID,-- INTEGER NOT NULL,
                        @GuildFoodID,-- INTEGER NOT NULL,
                        @StyleRankSkillsID,-- INTEGER NOT NULL,
                        @PlayerInventoryID,-- INTEGER NOT NULL,
                        @AmmoPouchID,-- INTEGER NOT NULL,
                        @PoogieItemID,-- INTEGER NOT NULL,
                        @RoadDureSkillsID-- INTEGER NOT NULL,
                        )";

                        using (SQLiteCommand cmd = new SQLiteCommand(insertSql, conn))
                        {
                            cmd.Parameters.AddWithValue("@RunID", runID);
                            cmd.Parameters.AddWithValue("@PlayerID", playerID);
                            cmd.Parameters.AddWithValue("@GearName", gearName);
                            cmd.Parameters.AddWithValue("@StyleID", styleID);
                            cmd.Parameters.AddWithValue("@WeaponIconID", weaponIconID);
                            cmd.Parameters.AddWithValue("@WeaponClassID", weaponClassID);
                            cmd.Parameters.AddWithValue("@WeaponTypeID", weaponTypeID);
                            if (blademasterWeaponID == null)
                            {
                                cmd.Parameters.AddWithValue("@BlademasterWeaponID", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@BlademasterWeaponID", blademasterWeaponID);
                            }
                            if (gunnerWeaponID == null)
                            {
                                cmd.Parameters.AddWithValue("@GunnerWeaponID", DBNull.Value);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@GunnerWeaponID", gunnerWeaponID);
                            }
                            cmd.Parameters.AddWithValue("@WeaponSlot1", weaponSlot1);
                            cmd.Parameters.AddWithValue("@WeaponSlot2", weaponSlot2);
                            cmd.Parameters.AddWithValue("@WeaponSlot3", weaponSlot3);
                            cmd.Parameters.AddWithValue("@HeadID", headID);
                            cmd.Parameters.AddWithValue("@HeadSlot1ID", headSlot1);
                            cmd.Parameters.AddWithValue("@HeadSlot2ID", headSlot2);
                            cmd.Parameters.AddWithValue("@HeadSlot3ID", headSlot3);
                            cmd.Parameters.AddWithValue("@ChestID", chestID);
                            cmd.Parameters.AddWithValue("@ChestSlot1ID", chestSlot1);
                            cmd.Parameters.AddWithValue("@ChestSlot2ID", chestSlot2);
                            cmd.Parameters.AddWithValue("@ChestSlot3ID", chestSlot3);
                            cmd.Parameters.AddWithValue("@ArmsID", armsID);
                            cmd.Parameters.AddWithValue("@ArmsSlot1ID", armsSlot1);
                            cmd.Parameters.AddWithValue("@ArmsSlot2ID", armsSlot2);
                            cmd.Parameters.AddWithValue("@ArmsSlot3ID", armsSlot3);
                            cmd.Parameters.AddWithValue("@WaistID", waistID);
                            cmd.Parameters.AddWithValue("@WaistSlot1ID", waistSlot1);
                            cmd.Parameters.AddWithValue("@WaistSlot2ID", waistSlot2);
                            cmd.Parameters.AddWithValue("@WaistSlot3ID", waistSlot3);
                            cmd.Parameters.AddWithValue("@LegsID", legsID);
                            cmd.Parameters.AddWithValue("@LegsSlot1ID", legsSlot1);
                            cmd.Parameters.AddWithValue("@LegsSlot2ID", legsSlot2);
                            cmd.Parameters.AddWithValue("@LegsSlot3ID", legsSlot3);
                            cmd.Parameters.AddWithValue("@Cuff1ID", cuffSlot1);
                            cmd.Parameters.AddWithValue("@Cuff2ID", cuffSlot2);
                            cmd.Parameters.AddWithValue("@ZenithSkillsID", zenithSkillsID);
                            cmd.Parameters.AddWithValue("@AutomaticSkillsID", automaticSkillsID);
                            cmd.Parameters.AddWithValue("@ActiveSkillsID", activeSkillsID);
                            cmd.Parameters.AddWithValue("@CaravanSkillsID", caravanSkillsID);
                            cmd.Parameters.AddWithValue("@DivaSkillID", divaSkillID);
                            cmd.Parameters.AddWithValue("@GuildFoodID", guildFoodID);
                            cmd.Parameters.AddWithValue("@StyleRankSkillsID", styleRankSkillsID);
                            cmd.Parameters.AddWithValue("@PlayerInventoryID", playerInventoryID);
                            cmd.Parameters.AddWithValue("@AmmoPouchID", ammoPouchID);
                            cmd.Parameters.AddWithValue("@PoogieItemID", poogieItemID);
                            cmd.Parameters.AddWithValue("@RoadDureSkillsID", roadDureSkillsID);

                            // Execute the stored procedure
                            cmd.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (SQLiteException ex)
                    {
                        if (transaction != null)
                            transaction.Rollback();
                        // Handle a SQL exception
                        MessageBox.Show("An error occurred while accessing the database: " + ex.SqlState+"\n\n"+ex.HelpLink+"\n\n"+ex.ResultCode+"\n\n"+ex.ErrorCode+"\n\n"+ex.Source+"\n\n"+ex.StackTrace+"\n\n"+ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (IOException ex)
                    {
                        if (transaction != null)
                            transaction.Rollback();
                        // Handle an I/O exception
                        MessageBox.Show("An error occurred while accessing a file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    catch (ArgumentException ex)
                    {
                        if (transaction != null)
                            transaction.Rollback();
                        MessageBox.Show("ArgumentException " + ex.Message + " " + ex.ParamName);
                    }
                    catch (Exception ex)
                    {
                        HandleError(transaction, ex);
                    }
                }
            }
        }

        private void CreateDatabaseTriggers(SQLiteConnection conn)
        {
            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    // goodbye world
                    // Commit the transaction
                    //transaction.Commit();
                }
                catch (Exception ex)
                {
                    HandleError(transaction, ex);
                }
            }
        }

        private void CreateDatabaseIndexes(SQLiteConnection conn)
        {
            List<string> createIndexSqlStatements = new List<string>
            {
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_activeskills_runid ON ActiveSkills(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allarmorskills_armorskillid ON AllArmorSkills(ArmorSkillID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allcaravanskills_caravanskillid ON AllCaravanSkills(CaravanSkillID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allroaddureskills_roaddureskillid ON AllRoadDureSkills(RoadDureSkillID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allstylerankskills_stylerankskillid ON AllStyleRankSkills(StyleRankSkillID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allzenithskills_zenithskillid ON AllZenithSkills(ZenithSkillID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_ammopouch_runid ON AmmoPouch(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_area_areaid ON Area(AreaID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_automaticskills_runid ON AutomaticSkills(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_caravanskills_runid ON CaravanSkills(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_item_itemid ON Item(ItemID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_objectivetype_objectivetypeid ON ObjectiveType(ObjectiveTypeID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_playergear_runid ON PlayerGear(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_playerinventory_runid ON PlayerInventory(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_players_playerid ON Players(PlayerID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_questname_questnameid ON QuestName(QuestNameID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_quests_runid ON Quests(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_rankname_ranknameid ON RankName(RankNameID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_roaddureskills_runid ON RoadDureSkills(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_stylerankskills_runid ON StyleRankSkills(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_weapontype_weapontypeid ON WeaponType(WeaponTypeID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_zenithskills_runid ON ZenithSkills(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allblademasterweapons_blademasterweaponid ON AllBlademasterWeapons(BlademasterWeaponID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allgunnerweapons_gunnerweaponid ON AllGunnerWeapons(GunnerWeaponID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allheadpieces_headpieceid ON AllHeadPieces(HeadPieceID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allchestpieces_chestpieceid ON AllChestPieces(ChestPieceID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allarmspieces_armspieceid ON AllArmsPieces(ArmsPieceID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_allwaistpieces_waistpieceid ON AllWaistPieces(WaistPieceID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_alllegspieces_legspieceid ON AllLegsPieces(LegsPieceID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_questevents_runid ON QuestEvents(RunID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_weaponclass_weaponclassid ON WeaponClass(WeaponClassID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_weaponicon_weaponiconid ON WeaponIcon(WeaponIconID)",
                "CREATE UNIQUE INDEX IF NOT EXISTS idx_weaponstyles_styleid ON WeaponStyles(StyleID)"

            };

            using (var transaction = conn.BeginTransaction())
            {
                try
                {
                    foreach (string createIndexSql in createIndexSqlStatements)
                    {
                        using (var cmd = new SQLiteCommand(createIndexSql, conn))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    HandleError(transaction, ex);
                }
            }
        }

        private void HandleError(SQLiteTransaction? transaction, Exception ex)
        {
            // Roll back the transaction
            if (transaction != null)
                transaction.Rollback();

            // Handle the exception and show an error message to the user
            MessageBox.Show("An error occurred: " + ex.Message +"\n\n" + ex.StackTrace + "\n\n" +ex.Source+"\n\n"+ex.Data.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        #region session time

        public void StoreSessionTime(MainWindow window)
        {
            try
            {
                var model = window.DataLoader.model;
                DateTime ProgramEnd = DateTime.Now;
                DateTime ProgramStart = window.ProgramStart;
                TimeSpan duration = ProgramEnd - ProgramStart;
                int sessionDuration = (int)duration.TotalSeconds;

                // Connect to the database
                string dbFilePath = _connectionString;
                string connectionString = "Data Source=" + dbFilePath;
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Begin a transaction
                    using (SQLiteTransaction transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Create the INSERT command
                            string insertSql = "INSERT INTO Session (StartTime, EndTime, SessionDuration) VALUES (@startTime, @endTime, @sessionDuration)";
                            using (SQLiteCommand insertCommand = new SQLiteCommand(insertSql, connection))
                            {
                                insertCommand.Parameters.AddWithValue("@startTime", ProgramStart);
                                insertCommand.Parameters.AddWithValue("@endTime", ProgramEnd);
                                insertCommand.Parameters.AddWithValue("@sessionDuration", sessionDuration);
                                // Execute the INSERT statement
                                insertCommand.ExecuteNonQuery();
                            }
                            // Commit the transaction
                            transaction.Commit();
                        }
                        catch (Exception ex)
                        {
                            HandleError(transaction, ex);
                        }
                    }
                }
            }
            catch (SQLiteException ex)
            {
                // Handle a SQL exception
                MessageBox.Show("An error occurred while accessing the database: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (IOException ex)
            {
                // Handle an I/O exception
                MessageBox.Show("An error occurred while accessing a file: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                // Handle any other exception
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        #endregion

        private readonly List<string> _validTableNames = new List<string> {
            "RankName", 
            "ObjectiveType", 
            "QuestName", 
            "WeaponType", 
            "Item", 
            "Area", 
            "AllZenithSkills",
            "AllArmorSkills",
            "AllCaravanSkills", 
            "AllStyleRankSkills", 
            "AllRoadDureSkills",
            "AllBlademasterWeapons",
            "AllGunnerWeapons",
            "AllHeadPieces",
            "AllChestPieces",
            "AllArmsPieces",
            "AllWaistPieces",
            "AllLegsPieces", 
            "WeaponClass", 
            "WeaponIcon",
            "WeaponStyles",
            "AllDivaSkills"};

        private void InsertDictionaryDataIntoTable(IReadOnlyDictionary<int, string> dictionary, string tableName, string idColumn, string valueColumn, SQLiteConnection conn)
        {
            // Start a transaction
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    // Validate the input table name
                    if (!_validTableNames.Contains(tableName))
                    {
                        throw new ArgumentException($"Invalid table name: {tableName}");
                    }

                    // Validate the input parameters
                    if (dictionary == null || dictionary.Count == 0)
                    {
                        throw new ArgumentException($"Invalid dictionary: {dictionary}");
                    }

                    if (string.IsNullOrEmpty(tableName) || string.IsNullOrEmpty(idColumn) || string.IsNullOrEmpty(valueColumn))
                    {
                        throw new ArgumentException("Invalid table name, id column, or value column");
                    }
                    if (conn == null)
                    {
                        throw new ArgumentException("Invalid connection");
                    }
                    if (conn.State != ConnectionState.Open)
                    {
                        throw new InvalidOperationException("Connection is not open");
                    }

                    // Create a command that will be used to insert multiple rows in a batch
                    using (var cmd = new SQLiteCommand(conn))
                    {
                        // Set the command text to insert a single row
                        cmd.CommandText = $"INSERT OR REPLACE INTO {tableName} ({idColumn}, {valueColumn}) VALUES (@id, @value)";

                        // Create a parameter for the value to be inserted
                        var valueParam = cmd.CreateParameter();
                        valueParam.ParameterName = "@value";
                        cmd.Parameters.Add(valueParam);

                        // Create a parameter for the ID to be inserted
                        var idParam = cmd.CreateParameter();
                        idParam.ParameterName = "@id";
                        cmd.Parameters.Add(idParam);

                        // Insert each row in the dictionary
                        foreach (var pair in dictionary)
                        {
                            // Set the values of the parameters
                            idParam.Value = pair.Key;
                            valueParam.Value = pair.Value;

                            // Execute the command to insert the row
                            cmd.ExecuteNonQuery();
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }
                } 
                catch (Exception ex)
                {
                    HandleError(transaction, ex); 
                }
            }
        }

        public void UpdateYoutubeLink(string youtubeId, int runId, SQLiteConnection conn)
        {
            try
            {
                // Start a transaction
                using (SQLiteTransaction transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Execute the query
                        string updateSql = "UPDATE Quests SET YoutubeID = @youtubeId WHERE RunID = @runId";
                        using (SQLiteCommand cmd = new SQLiteCommand(updateSql, conn))
                        {
                            // Add the parameters for the placeholders in the SQL query
                            cmd.Parameters.AddWithValue("@youtubeId", youtubeId);
                            cmd.Parameters.AddWithValue("@runId", runId);

                            // Execute the update statement
                            int rowsAffected = cmd.ExecuteNonQuery();

                            // Check if the update was successful
                            if (rowsAffected > 0)
                            {
                                // The update was successful
                                MessageBox.Show("YoutubeID updated successfully");
                            }
                            else
                            {
                                // The update was not successful
                                MessageBox.Show("Error updating YoutubeID");
                            }
                        }

                        // Commit the transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Handle the error
                        HandleError(transaction, ex);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that may occur when starting the transaction
                HandleError(null, ex);
            }
        }

        private void CreateDatabaseTables(SQLiteConnection conn, DataLoader dataLoader)
        {
            using (SQLiteTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    var model = dataLoader.model;

                    // Create table to store program usage time
                    string sql = @"CREATE TABLE IF NOT EXISTS Session (
                    SessionID INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime DATETIME NOT NULL,
                    EndTime DATETIME NOT NULL,
                    SessionDuration INTEGER NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    //Create the Quests table
                    sql = @"CREATE TABLE IF NOT EXISTS Quests 
                    (RunID INTEGER PRIMARY KEY AUTOINCREMENT, 
                    QuestID INTEGER NOT NULL CHECK (QuestID >= 0), 
                    AreaID INTEGER NOT NULL CHECK (AreaID >= 0), 
                    FinalTimeValue INTEGER NOT NULL,
                    FinalTimeDisplay TEXT NOT NULL, 
                    ObjectiveImage TEXT NOT NULL,
                    ObjectiveTypeID INTEGER NOT NULL CHECK (ObjectiveTypeID >= 0), 
                    ObjectiveQuantity INTEGER NOT NULL, 
                    StarGrade INTEGER NOT NULL, 
                    RankNameID INTEGER NOT NULL CHECK (RankNameID >= 0), 
                    ObjectiveName TEXT NOT NULL, 
                    Date DATETIME NOT NULL,
                    YoutubeID TEXT DEFAULT 'dQw4w9WgXcQ', -- default value for YoutubeID is a Rick Roll video
                    -- DpsData TEXT NOT NULL,
                    FOREIGN KEY(QuestID) REFERENCES QuestName(QuestNameID),
                    FOREIGN KEY(AreaID) REFERENCES Area(AreaID),
                    FOREIGN KEY(ObjectiveTypeID) REFERENCES ObjectiveType(ObjectiveTypeID),
                    FOREIGN KEY(RankNameID) REFERENCES RankName(RankNameID)
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    /*
                     * Here's an example of how you could store the dps data as a JSON string in a single column in the "Quests" table:

                        1. Add a new column to the "Quests" table to store the dps data as a JSON string. You could use a data type like "TEXT" or "BLOB" for this column.

                        2. When a quest finishes, generate an array or list of dps values recorded during the quest run.

                        3. Use a JSON serialization library (such as Newtonsoft.Json in C#) to convert the array or list of dps values into a JSON string.

                        4. Insert the JSON string into the new column in the "Quests" table, along with the other quest run data (such as the QuestID, AreaID, etc.).

                        To extract the dps data and plot it in the chart later, you would do the following:

                        1. Retrieve the JSON string from the "Quests" table for the specific quest run you want to display.

                        2. Use a JSON parsing library (such as Newtonsoft.Json in C#) to deserialize the JSON string into an array or list of dps values.

                        3. Use the dps values and a charting library (such as OxyPlot or LiveCharts) to plot the dps data in a chart.

                        Note that this approach assumes that the dps data is recorded and stored consistently for each quest run. You may need to do additional processing or validation on the data to ensure that it is in a suitable format for charting.
                     */

                    /*
                     Quest Events log example:

                        00:00.00 Start at zone X
                        00:10.00 Changed to zone X
                        00:15.00 First hit towards monster
                        00:20.00 Maximum attack buff obtained is now 2850
                        00:25.00 Reached 67 Hits towards monster
                        00:27.00 Maximum attack buff obtained is now 3060
                        00:30.00 Hit by monster
                        00:40.00 Changed to zone X
                        00:50.00 Carted at zone X
                        01:10.33 Changed to zone X (Basecamp ig)
                        02:00.30 Monster is now at 90% HP
                        ...
                        35:34.27 Monster is now at 10% HP
                        40:00.00 Completed Quest
                     */

                    sql = @"CREATE TABLE IF NOT EXISTS QuestEvents(
                      EventID INTEGER PRIMARY KEY AUTOINCREMENT,
                      RunID INTEGER NOT NULL, -- foreign key to the Quests table
                      EventType TEXT NOT NULL, -- type of event, e.g. Start, Hit, Cart, etc.
                      TimeValue INTEGER NOT NULL,
                      TimeDisplay TEXT NOT NULL, 
                      EventDetails TEXT NOT NULL, -- data for the event, e.g. zone X, 67 Hits. 
                      FOREIGN KEY(RunID) REFERENCES Quests(RunID)
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    //Create the RankNames table
                    sql = @"CREATE TABLE IF NOT EXISTS RankName
                    (RankNameID INTEGER PRIMARY KEY, 
                    RankNameName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.RanksBandsList.RankBandsID, "RankName", "RankNameID", "RankNameName", conn);

                    //Create the ObjectiveTypes table
                    sql = @"CREATE TABLE IF NOT EXISTS ObjectiveType
                    (ObjectiveTypeID INTEGER PRIMARY KEY, 
                    ObjectiveTypeName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.ObjectiveTypeList.ObjectiveTypeID, "ObjectiveType", "ObjectiveTypeID", "ObjectiveTypeName", conn);

                    //Create the QuestNames table
                    sql = @"CREATE TABLE IF NOT EXISTS QuestName
                    (QuestNameID INTEGER PRIMARY KEY, 
                    QuestNameName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Quests.QuestIDs, "QuestName", "QuestNameID", "QuestNameName", conn);

                    // Create the Players table
                    //do an UPDATE when inserting quests. since its just local player?
                    sql = @"
                    CREATE TABLE IF NOT EXISTS Players (
                    PlayerID INTEGER PRIMARY KEY AUTOINCREMENT, 
                    PlayerName TEXT NOT NULL,
                    GuildName TEXT NOT NULL,
                    Gender TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Create the WeaponTypes table
                    sql = @"CREATE TABLE IF NOT EXISTS WeaponType (
                    WeaponTypeID INTEGER PRIMARY KEY, 
                    WeaponTypeName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(WeaponTypes.WeaponTypeID, "WeaponType", "WeaponTypeID", "WeaponTypeName", conn);

                    // Create the Item table
                    sql = @"CREATE TABLE IF NOT EXISTS Item (
                    ItemID INTEGER PRIMARY KEY, 
                    ItemName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Items.ItemIDs, "Item", "ItemID", "ItemName", conn);

                    // Create the Area table
                    sql = @"CREATE TABLE IF NOT EXISTS Area (
                    AreaID INTEGER PRIMARY KEY,
                    AreaName TEXT NOT NULL,
                    AreaIcon TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    // Prepare the SQL statement
                    sql = "INSERT OR REPLACE INTO Area (AreaID, AreaIcon, AreaName) VALUES (@AreaID, @AreaIcon, @AreaName)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        // Add the parameter placeholders
                        cmd.Parameters.Add("@AreaID", DbType.Int32);
                        cmd.Parameters.Add("@AreaIcon", DbType.String);
                        cmd.Parameters.Add("@AreaName", DbType.String);

                        // Iterate through the list of areas
                        foreach (KeyValuePair<List<int>, string> kvp in AreaIconDictionary.AreaIconID)
                        {
                            List<int> areaIDs = kvp.Key;

                            foreach (int areaID in areaIDs)
                            {
                                string areaIcon = kvp.Value;
                                string areaName = model.GetAreaName(areaID);

                                // Set the parameter values
                                cmd.Parameters["@AreaID"].Value = areaID;
                                cmd.Parameters["@AreaIcon"].Value = areaIcon;
                                cmd.Parameters["@AreaName"].Value = areaName;

                                // Execute the SQL statement
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    // Create the PlayerGear table
                    sql = @"CREATE TABLE IF NOT EXISTS PlayerGear (
                    PlayerGearID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RunID INTEGER NOT NULL, 
                    PlayerID INTEGER NOT NULL,
                    GearName TEXT NOT NULL,
                    StyleID INTEGER NOT NULL CHECK (StyleID >= 0),
                    WeaponIconID INTEGER NOT NULL,
                    WeaponClassID INTEGER NOT NULL,
                    WeaponTypeID INTEGER NOT NULL CHECK (WeaponTypeID >= 0),
                    BlademasterWeaponID INTEGER,
                    GunnerWeaponID INTEGER,
                    WeaponSlot1 TEXT NOT NULL,
                    WeaponSlot2 TEXT NOT NULL,
                    WeaponSlot3 TEXT NOT NULL,
                    HeadID INTEGER NOT NULL CHECK (HeadID >= 0), 
                    HeadSlot1ID INTEGER NOT NULL CHECK (HeadSlot1ID >= 0),
                    HeadSlot2ID INTEGER NOT NULL CHECK (HeadSlot2ID >= 0),
                    HeadSlot3ID INTEGER NOT NULL CHECK (HeadSlot3ID >= 0),
                    ChestID INTEGER NOT NULL CHECK (ChestID >= 0),
                    ChestSlot1ID INTEGER NOT NULL CHECK (ChestSlot1ID >= 0),
                    ChestSlot2ID INTEGER NOT NULL CHECK (ChestSlot2ID >= 0),
                    ChestSlot3ID INTEGER NOT NULL CHECK (ChestSlot3ID >= 0),
                    ArmsID INTEGER NOT NULL CHECK (ArmsID >= 0),
                    ArmsSlot1ID INTEGER NOT NULL CHECK (ArmsSlot1ID >= 0),
                    ArmsSlot2ID INTEGER NOT NULL CHECK (ArmsSlot2ID >= 0),
                    ArmsSlot3ID INTEGER NOT NULL CHECK (ArmsSlot3ID >= 0),
                    WaistID INTEGER NOT NULL CHECK (WaistID >= 0),
                    WaistSlot1ID INTEGER NOT NULL CHECK (WaistSlot1ID >= 0),
                    WaistSlot2ID INTEGER NOT NULL CHECK (WaistSlot2ID >= 0),
                    WaistSlot3ID INTEGER NOT NULL CHECK (WaistSlot3ID >= 0),
                    LegsID INTEGER NOT NULL CHECK (LegsID >= 0),
                    LegsSlot1ID INTEGER NOT NULL CHECK (LegsSlot1ID >= 0),
                    LegsSlot2ID INTEGER NOT NULL CHECK (LegsSlot2ID >= 0),
                    LegsSlot3ID INTEGER NOT NULL CHECK (LegsSlot3ID >= 0),
                    Cuff1ID INTEGER NOT NULL CHECK (Cuff1ID >= 0),
                    Cuff2ID INTEGER NOT NULL CHECK (Cuff2ID >= 0),
                    ZenithSkillsID INTEGER NOT NULL,
                    AutomaticSkillsID INTEGER NOT NULL,
                    ActiveSkillsID INTEGER NOT NULL,
                    CaravanSkillsID INTEGER NOT NULL,
                    DivaSkillID INTEGER NOT NULL,
                    GuildFoodID INTEGER NOT NULL,
                    StyleRankSkillsID INTEGER NOT NULL,
                    PlayerInventoryID INTEGER NOT NULL,
                    AmmoPouchID INTEGER NOT NULL,
                    PoogieItemID INTEGER NOT NULL,
                    RoadDureSkillsID INTEGER NOT NULL,
                    FOREIGN KEY(RunID) REFERENCES Quests(RunID),
                    FOREIGN KEY(PlayerID) REFERENCES Players(PlayerID),
                    FOREIGN KEY(StyleID) REFERENCES WeaponStyles(StyleID),
                    FOREIGN KEY(WeaponIconID) REFERENCES WeaponIcon(WeaponIconID),
                    FOREIGN KEY(WeaponClassID) REFERENCES WeaponClass(WeaponClassID),
                    FOREIGN KEY(WeaponTypeID) REFERENCES WeaponType(WeaponTypeID),
                    FOREIGN KEY(BlademasterWeaponID) REFERENCES AllBlademasterWeapons(BlademasterWeaponID),
                    FOREIGN KEY(GunnerWeaponID) REFERENCES AllGunnerWeapons(GunnerWeaponID),
                    CHECK 
                    (
                        (BlademasterWeaponID IS NOT NULL AND GunnerWeaponID IS NULL) OR 
                        (BlademasterWeaponID IS NULL AND GunnerWeaponID IS NOT NULL)
                    )
                    FOREIGN KEY(HeadID) REFERENCES AllHeadPieces(HeadPieceID),
                    FOREIGN KEY(ChestID) REFERENCES AllChestPieces(ChestPieceID),
                    FOREIGN KEY(ArmsID) REFERENCES AllArmsPieces(ArmsPieceID),
                    FOREIGN KEY(WaistID) REFERENCES AllWaistPieces(WaistPieceID),
                    FOREIGN KEY(LegsID) REFERENCES AllLegsPieces(LegsPieceID),
                    FOREIGN KEY(Cuff1ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Cuff2ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(ZenithSkillsID) REFERENCES ZenithSkills(ZenithSkillsID),
                    FOREIGN KEY(AutomaticSkillsID) REFERENCES AutomaticSkills(AutomaticSkillsID),
                    FOREIGN KEY(ActiveSkillsID) REFERENCES ActiveSkills(ActiveSkillsID),
                    FOREIGN KEY(CaravanSkillsID) REFERENCES CaravanSkills(CaravanSkillsID),
                    FOREIGN KEY(DivaSkillID) REFERENCES AllDivaSkills(DivaSkillID),
                    FOREIGN KEY(GuildFoodID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(StyleRankSkillsID) REFERENCES StyleRankSkills(StyleRankSkillsID),
                    FOREIGN KEY(PlayerInventoryID) REFERENCES PlayerInventory(PlayerInventoryID),
                    FOREIGN KEY(AmmoPouchID) REFERENCES AmmoPouch(AmmoPouchID),
                    FOREIGN KEY(PoogieItemID) REFERENCES Item(ItemID),
                    FOREIGN KEY(RoadDureSkillsID) REFERENCES RoadDureSkills(RoadDureSkillsID)
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = @"CREATE TABLE IF NOT EXISTS AllDivaSkills (
                      DivaSkillID INTEGER PRIMARY KEY,
                      DivaSkillName TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.DivaSkillList.DivaSkillID, "AllDivaSkills", "DivaSkillID", "DivaSkillName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS WeaponStyles (
                      StyleID INTEGER PRIMARY KEY,
                      StyleName TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.WeaponStyles.WeaponStyleID, "WeaponStyles", "StyleID", "StyleName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS WeaponIcon (
                      WeaponIconID INTEGER PRIMARY KEY,
                      WeaponIconLink TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.WeaponIconsDictionary.WeaponIconID, "WeaponIcon", "WeaponIconID", "WeaponIconLink", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS WeaponClass (
                      WeaponClassID INTEGER PRIMARY KEY,
                      WeaponClassName TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.WeaponClass.WeaponClassID, "WeaponClass", "WeaponClassID", "WeaponClassName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS AllBlademasterWeapons (
                      BlademasterWeaponID INTEGER PRIMARY KEY,
                      BlademasterWeaponName TEXT NOT NULL
                    )"; 
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.BlademasterWeapons.BlademasterWeaponIDs, "AllBlademasterWeapons", "BlademasterWeaponID", "BlademasterWeaponName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS AllGunnerWeapons (
                      GunnerWeaponID INTEGER PRIMARY KEY,
                      GunnerWeaponName TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.GunnerWeapons.GunnerWeaponIDs, "AllGunnerWeapons", "GunnerWeaponID", "GunnerWeaponName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS AllHeadPieces (
                      HeadPieceID INTEGER PRIMARY KEY,
                      HeadPieceName TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.ArmorHeads.ArmorHeadIDs, "AllHeadPieces", "HeadPieceID", "HeadPieceName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS AllChestPieces (
                      ChestPieceID INTEGER PRIMARY KEY,
                      ChestPieceName TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.ArmorChests.ArmorChestIDs, "AllChestPieces", "ChestPieceID", "ChestPieceName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS AllArmsPieces (
                      ArmsPieceID INTEGER PRIMARY KEY,
                      ArmsPieceName TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.ArmorArms.ArmorArmIDs, "AllArmsPieces", "ArmsPieceID", "ArmsPieceName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS AllWaistPieces (
                      WaistPieceID INTEGER PRIMARY KEY,
                      WaistPieceName TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.ArmorWaists.ArmorWaistIDs, "AllWaistPieces", "WaistPieceID", "WaistPieceName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS AllLegsPieces (
                      LegsPieceID INTEGER PRIMARY KEY,
                      LegsPieceName TEXT NOT NULL
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.ArmorLegs.ArmorLegIDs, "AllLegsPieces", "LegsPieceID", "LegsPieceName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS ZenithSkills(
                    ZenithSkillsID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RunID INTEGER NOT NULL,
                    ZenithSkill1ID INTEGER NOT NULL CHECK (ZenithSkill1ID >= 0),
                    ZenithSkill2ID INTEGER NOT NULL CHECK (ZenithSkill2ID >= 0),
                    ZenithSkill3ID INTEGER NOT NULL CHECK (ZenithSkill3ID >= 0),
                    ZenithSkill4ID INTEGER NOT NULL CHECK (ZenithSkill4ID >= 0),
                    ZenithSkill5ID INTEGER NOT NULL CHECK (ZenithSkill5ID >= 0),
                    ZenithSkill6ID INTEGER NOT NULL CHECK (ZenithSkill6ID >= 0),
                    ZenithSkill7ID INTEGER NOT NULL CHECK (ZenithSkill7ID >= 0),
                    FOREIGN KEY(RunID) REFERENCES Quests(RunID)
                    FOREIGN KEY(ZenithSkill1ID) REFERENCES AllZenithSkills(ZenithSkillID),
                    FOREIGN KEY(ZenithSkill2ID) REFERENCES AllZenithSkills(ZenithSkillID),
                    FOREIGN KEY(ZenithSkill3ID) REFERENCES AllZenithSkills(ZenithSkillID),
                    FOREIGN KEY(ZenithSkill4ID) REFERENCES AllZenithSkills(ZenithSkillID),
                    FOREIGN KEY(ZenithSkill5ID) REFERENCES AllZenithSkills(ZenithSkillID),
                    FOREIGN KEY(ZenithSkill6ID) REFERENCES AllZenithSkills(ZenithSkillID),
                    FOREIGN KEY(ZenithSkill7ID) REFERENCES AllZenithSkills(ZenithSkillID))";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = @"CREATE TABLE IF NOT EXISTS AllZenithSkills(
                    ZenithSkillID INTEGER PRIMARY KEY,
                    ZenithSkillName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.ZenithSkillList.ZenithSkillID, "AllZenithSkills", "ZenithSkillID", "ZenithSkillName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS AutomaticSkills(
                    AutomaticSkillsID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RunID INTEGER NOT NULL,
                    AutomaticSkill1ID INTEGER NOT NULL CHECK (AutomaticSkill1ID >= 0),
                    AutomaticSkill2ID INTEGER NOT NULL CHECK (AutomaticSkill2ID >= 0),
                    AutomaticSkill3ID INTEGER NOT NULL CHECK (AutomaticSkill3ID >= 0),
                    AutomaticSkill4ID INTEGER NOT NULL CHECK (AutomaticSkill4ID >= 0),
                    AutomaticSkill5ID INTEGER NOT NULL CHECK (AutomaticSkill5ID >= 0),
                    AutomaticSkill6ID INTEGER NOT NULL CHECK (AutomaticSkill6ID >= 0),
                    FOREIGN KEY(RunID) REFERENCES Quests(RunID),
                    FOREIGN KEY(AutomaticSkill1ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(AutomaticSkill2ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(AutomaticSkill3ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(AutomaticSkill4ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(AutomaticSkill5ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(AutomaticSkill6ID) REFERENCES AllArmorSkills(ArmorSkillID))";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = @"CREATE TABLE IF NOT EXISTS AllArmorSkills(
                    ArmorSkillID INTEGER PRIMARY KEY,
                    ArmorSkillName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.ArmorSkillList.ArmorSkillID, "AllArmorSkills", "ArmorSkillID", "ArmorSkillName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS ActiveSkills(
                    ActiveSkillsID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RunID INTEGER NOT NULL,
                    ActiveSkill1ID INTEGER NOT NULL CHECK (ActiveSkill1ID >= 0),
                    ActiveSkill2ID INTEGER NOT NULL CHECK (ActiveSkill2ID >= 0),
                    ActiveSkill3ID INTEGER NOT NULL CHECK (ActiveSkill3ID >= 0),
                    ActiveSkill4ID INTEGER NOT NULL CHECK (ActiveSkill4ID >= 0),
                    ActiveSkill5ID INTEGER NOT NULL CHECK (ActiveSkill5ID >= 0),
                    ActiveSkill6ID INTEGER NOT NULL CHECK (ActiveSkill6ID >= 0),
                    ActiveSkill7ID INTEGER NOT NULL CHECK (ActiveSkill7ID >= 0),
                    ActiveSkill8ID INTEGER NOT NULL CHECK (ActiveSkill8ID >= 0),
                    ActiveSkill9ID INTEGER NOT NULL CHECK (ActiveSkill9ID >= 0),
                    ActiveSkill10ID INTEGER NOT NULL CHECK (ActiveSkill10ID >= 0),
                    ActiveSkill11ID INTEGER NOT NULL CHECK (ActiveSkill11ID >= 0),
                    ActiveSkill12ID INTEGER NOT NULL CHECK (ActiveSkill12ID >= 0),
                    ActiveSkill13ID INTEGER NOT NULL CHECK (ActiveSkill13ID >= 0),
                    ActiveSkill14ID INTEGER NOT NULL CHECK (ActiveSkill14ID >= 0),
                    ActiveSkill15ID INTEGER NOT NULL CHECK (ActiveSkill15ID >= 0),
                    ActiveSkill16ID INTEGER NOT NULL CHECK (ActiveSkill16ID >= 0),
                    ActiveSkill17ID INTEGER NOT NULL CHECK (ActiveSkill17ID >= 0),
                    ActiveSkill18ID INTEGER NOT NULL CHECK (ActiveSkill18ID >= 0),
                    ActiveSkill19ID INTEGER NOT NULL CHECK (ActiveSkill19ID >= 0),
                    FOREIGN KEY(RunID) REFERENCES Quests(RunID)
                    FOREIGN KEY(ActiveSkill1ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill2ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill3ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill4ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill5ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill6ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill7ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill8ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill9ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill10ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill11ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill12ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill13ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill14ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill15ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill16ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill17ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill18ID) REFERENCES AllArmorSkills(ArmorSkillID),
                    FOREIGN KEY(ActiveSkill19ID) REFERENCES AllArmorSkills(ArmorSkillID)
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = @"CREATE TABLE IF NOT EXISTS CaravanSkills(
                    CaravanSkillsID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RunID INTEGER NOT NULL,
                    CaravanSkill1ID INTEGER NOT NULL CHECK (CaravanSkill1ID >= 0),
                    CaravanSkill2ID INTEGER NOT NULL CHECK (CaravanSkill2ID >= 0),
                    CaravanSkill3ID INTEGER NOT NULL CHECK (CaravanSkill3ID >= 0),
                    FOREIGN KEY(RunID) REFERENCES Quests(RunID)
                    FOREIGN KEY(CaravanSkill1ID) REFERENCES AllCaravanSkills(CaravanSkillID),
                    FOREIGN KEY(CaravanSkill2ID) REFERENCES AllCaravanSkills(CaravanSkillID),
                    FOREIGN KEY(CaravanSkill3ID) REFERENCES AllCaravanSkills(CaravanSkillID)
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = @"CREATE TABLE IF NOT EXISTS AllCaravanSkills(
                    CaravanSkillID INTEGER PRIMARY KEY,
                    CaravanSkillName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.CaravanSkillList.CaravanSkillID, "AllCaravanSkills", "CaravanSkillID", "CaravanSkillName", conn);

                    sql = @"CREATE TABLE IF NOT EXISTS StyleRankSkills(
                    StyleRankSkillsID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RunID INTEGER NOT NULL,
                    StyleRankSkill1ID INTEGER NOT NULL CHECK (StyleRankSkill1ID >= 0),
                    StyleRankSkill2ID INTEGER NOT NULL CHECK (StyleRankSkill2ID >= 0),
                    FOREIGN KEY(RunID) REFERENCES Quests(RunID),
                    FOREIGN KEY(StyleRankSkill1ID) REFERENCES AllStyleRankSkills(StyleRankSkillID),
                    FOREIGN KEY(StyleRankSkill2ID) REFERENCES AllStyleRankSkills(StyleRankSkillID)
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = @"CREATE TABLE IF NOT EXISTS AllStyleRankSkills(
                    StyleRankSkillID INTEGER PRIMARY KEY,
                    StyleRankSkillName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.StyleRankSkillList.StyleRankSkillID, "AllStyleRankSkills", "StyleRankSkillID", "StyleRankSkillName", conn);

                    // Create the PlayerInventory table
                    sql = @"CREATE TABLE IF NOT EXISTS PlayerInventory (
                    PlayerInventoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RunID INTEGER NOT NULL,
                    Item1ID INTEGER NOT NULL CHECK (Item1ID >= 0), 
                    Item1Quantity INTEGER NOT NULL,
                    Item2ID INTEGER NOT NULL CHECK (Item2ID >= 0), 
                    Item2Quantity INTEGER NOT NULL,
                    Item3ID INTEGER NOT NULL CHECK (Item3ID >= 0), 
                    Item3Quantity INTEGER NOT NULL,
                    Item4ID INTEGER NOT NULL CHECK (Item4ID >= 0), 
                    Item4Quantity INTEGER NOT NULL,
                    Item5ID INTEGER NOT NULL CHECK (Item5ID >= 0), 
                    Item5Quantity INTEGER NOT NULL,
                    Item6ID INTEGER NOT NULL CHECK (Item6ID >= 0), 
                    Item6Quantity INTEGER NOT NULL,
                    Item7ID INTEGER NOT NULL CHECK (Item7ID >= 0), 
                    Item7Quantity INTEGER NOT NULL,
                    Item8ID INTEGER NOT NULL CHECK (Item8ID >= 0), 
                    Item8Quantity INTEGER NOT NULL,
                    Item9ID INTEGER NOT NULL CHECK (Item9ID >= 0), 
                    Item9Quantity INTEGER NOT NULL,
                    Item10ID INTEGER NOT NULL CHECK (Item10ID >= 0), 
                    Item10Quantity INTEGER NOT NULL,
                    Item11ID INTEGER NOT NULL CHECK (Item11ID >= 0), 
                    Item11Quantity INTEGER NOT NULL,
                    Item12ID INTEGER NOT NULL CHECK (Item12ID >= 0), 
                    Item12Quantity INTEGER NOT NULL,
                    Item13ID INTEGER NOT NULL CHECK (Item13ID >= 0), 
                    Item13Quantity INTEGER NOT NULL,
                    Item14ID INTEGER NOT NULL CHECK (Item14ID >= 0), 
                    Item14Quantity INTEGER NOT NULL,
                    Item15ID INTEGER NOT NULL CHECK (Item15ID >= 0), 
                    Item15Quantity INTEGER NOT NULL,
                    Item16ID INTEGER NOT NULL CHECK (Item16ID >= 0), 
                    Item16Quantity INTEGER NOT NULL,
                    Item17ID INTEGER NOT NULL CHECK (Item17ID >= 0), 
                    Item17Quantity INTEGER NOT NULL,
                    Item18ID INTEGER NOT NULL CHECK (Item18ID >= 0), 
                    Item18Quantity INTEGER NOT NULL,
                    Item19ID INTEGER NOT NULL CHECK (Item19ID >= 0), 
                    Item19Quantity INTEGER NOT NULL,
                    Item20ID INTEGER NOT NULL CHECK (Item20ID >= 0), 
                    Item20Quantity INTEGER NOT NULL,
                    FOREIGN KEY(RunID) REFERENCES Quests(RunID),
                    FOREIGN KEY(Item1ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item2ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item3ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item4ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item5ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item6ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item7ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item8ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item9ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item10ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item11ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item12ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item13ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item14ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item15ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item16ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item17ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item18ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item19ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item20ID) REFERENCES Item(ItemID)
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = @"CREATE TABLE IF NOT EXISTS AmmoPouch (
                    AmmoPouchID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RunID INTEGER NOT NULL,
                    Item1ID INTEGER NOT NULL CHECK (Item1ID >= 0), 
                    Item1Quantity INTEGER NOT NULL,
                    Item2ID INTEGER NOT NULL CHECK (Item2ID >= 0), 
                    Item2Quantity INTEGER NOT NULL,
                    Item3ID INTEGER NOT NULL CHECK (Item3ID >= 0), 
                    Item3Quantity INTEGER NOT NULL,
                    Item4ID INTEGER NOT NULL CHECK (Item4ID >= 0), 
                    Item4Quantity INTEGER NOT NULL,
                    Item5ID INTEGER NOT NULL CHECK (Item5ID >= 0), 
                    Item5Quantity INTEGER NOT NULL,
                    Item6ID INTEGER NOT NULL CHECK (Item6ID >= 0), 
                    Item6Quantity INTEGER NOT NULL,
                    Item7ID INTEGER NOT NULL CHECK (Item7ID >= 0), 
                    Item7Quantity INTEGER NOT NULL,
                    Item8ID INTEGER NOT NULL CHECK (Item8ID >= 0), 
                    Item8Quantity INTEGER NOT NULL,
                    Item9ID INTEGER NOT NULL CHECK (Item9ID >= 0), 
                    Item9Quantity INTEGER NOT NULL,
                    Item10ID INTEGER NOT NULL CHECK (Item10ID >= 0), 
                    Item10Quantity INTEGER NOT NULL,
                    FOREIGN KEY(RunID) REFERENCES Quests(RunID),
                    FOREIGN KEY(Item1ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item2ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item3ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item4ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item5ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item6ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item7ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item8ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item9ID) REFERENCES Item(ItemID),
                    FOREIGN KEY(Item10ID) REFERENCES Item(ItemID)
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = @"CREATE TABLE IF NOT EXISTS RoadDureSkills (
                    RoadDureSkillsID INTEGER PRIMARY KEY AUTOINCREMENT,
                    RunID INTEGER NOT NULL,
                    RoadDureSkill1ID INTEGER NOT NULL CHECK (RoadDureSkill1ID >= 0), 
                    RoadDureSkill1Level INTEGER NOT NULL,
                    RoadDureSkill2ID INTEGER NOT NULL CHECK (RoadDureSkill2ID >= 0), 
                    RoadDureSkill2Level INTEGER NOT NULL,
                    RoadDureSkill3ID INTEGER NOT NULL CHECK (RoadDureSkill3ID >= 0), 
                    RoadDureSkill3Level INTEGER NOT NULL,
                    RoadDureSkill4ID INTEGER NOT NULL CHECK (RoadDureSkill4ID >= 0), 
                    RoadDureSkill4Level INTEGER NOT NULL,
                    RoadDureSkill5ID INTEGER NOT NULL CHECK (RoadDureSkill5ID >= 0), 
                    RoadDureSkill5Level INTEGER NOT NULL,
                    RoadDureSkill6ID INTEGER NOT NULL CHECK (RoadDureSkill6ID >= 0), 
                    RoadDureSkill6Level INTEGER NOT NULL,
                    RoadDureSkill7ID INTEGER NOT NULL CHECK (RoadDureSkill7ID >= 0), 
                    RoadDureSkill7Level INTEGER NOT NULL,
                    RoadDureSkill8ID INTEGER NOT NULL CHECK (RoadDureSkill8ID >= 0), 
                    RoadDureSkill8Level INTEGER NOT NULL,
                    RoadDureSkill9ID INTEGER NOT NULL CHECK (RoadDureSkill9ID >= 0), 
                    RoadDureSkill9Level INTEGER NOT NULL,
                    RoadDureSkill10ID INTEGER NOT NULL CHECK (RoadDureSkill10ID >= 0), 
                    RoadDureSkill10Level INTEGER NOT NULL,
                    RoadDureSkill11ID INTEGER NOT NULL CHECK (RoadDureSkill11ID >= 0), 
                    RoadDureSkill11Level INTEGER NOT NULL,
                    RoadDureSkill12ID INTEGER NOT NULL CHECK (RoadDureSkill12ID >= 0), 
                    RoadDureSkill12Level INTEGER NOT NULL,
                    RoadDureSkill13ID INTEGER NOT NULL CHECK (RoadDureSkill13ID >= 0), 
                    RoadDureSkill13Level INTEGER NOT NULL,
                    RoadDureSkill14ID INTEGER NOT NULL CHECK (RoadDureSkill14ID >= 0), 
                    RoadDureSkill14Level INTEGER NOT NULL,
                    RoadDureSkill15ID INTEGER NOT NULL CHECK (RoadDureSkill15ID >= 0), 
                    RoadDureSkill15Level INTEGER NOT NULL,
                    RoadDureSkill16ID INTEGER NOT NULL CHECK (RoadDureSkill16ID >= 0), 
                    RoadDureSkill16Level INTEGER NOT NULL,
                    FOREIGN KEY(RunID) REFERENCES Quests(RunID)
                    FOREIGN KEY(RoadDureSkill1ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill2ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill3ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill4ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill5ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill6ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill7ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill8ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill9ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill10ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill11ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill12ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill13ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill14ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill15ID) REFERENCES AllRoadDureSkills(RoadDureSkillID),
                    FOREIGN KEY(RoadDureSkill16ID) REFERENCES AllRoadDureSkills(RoadDureSkillID)
                    )";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    sql = @"CREATE TABLE IF NOT EXISTS AllRoadDureSkills(
                    RoadDureSkillID INTEGER PRIMARY KEY,
                    RoadDureSkillName TEXT NOT NULL)";
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    InsertDictionaryDataIntoTable(Dictionary.RoadDureSkills.RoadDureSkillIDs, "AllRoadDureSkills", "RoadDureSkillID", "RoadDureSkillName", conn);

                    // Commit the transaction
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    HandleError(transaction, ex);
                }
            }
        }

        //i would first insert into the quest table,
        //then the tables referencing
        //playergear, then the playergear table
        // TODO
        void InsertQuestIntoDatabase(SQLiteConnection conn, DataLoader dataLoader)
        {
            var model = dataLoader.model;
            // Insert a new quest into the Quests table
            string sql = @"INSERT INTO Quests (
            QuestID, 
            QuestName, 
            EndTime, 
            ObjectiveQuantity, 
            ObjectiveName, 
            Gear, 
            Weapon, 
            Date) 
            VALUES (
            @questID, 
            @questName, 
            @endTime, 
            @objectiveType, 
            @objectiveQuantity, 
            @starGrade, 
            @rankName, 
            @objectiveName, 
            @date)";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);

            cmd.Parameters.AddWithValue("@questName", model.GetQuestNameFromID(model.QuestID()));
            cmd.Parameters.AddWithValue("@endTime", "");
            cmd.Parameters.AddWithValue("@objectiveType", "");
            cmd.Parameters.AddWithValue("@objectiveQuantity", "");
            cmd.Parameters.AddWithValue("@starGrade", "");
            cmd.Parameters.AddWithValue("@rankName", "");
            cmd.Parameters.AddWithValue("@objectiveName", model.GetObjectiveNameFromID(model.Objective1ID()));
            cmd.Parameters.AddWithValue("@date", DateTime.Now);
            cmd.ExecuteNonQuery();

            // Check if the player has already been inserted into the Players table
            sql = "SELECT PlayerID FROM Players WHERE PlayerName = @playerName";
            cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@playerName", "Doriel");
            object result = cmd.ExecuteScalar();

            int playerID;

            // If the player has not been inserted, insert the player into the Players table
            if (result == null)
            {
                sql = "INSERT INTO Players (PlayerName) VALUES (@playerName)";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@playerName", "Doriel");
                cmd.ExecuteNonQuery();

                // Get the PlayerID of the inserted player
                sql = "SELECT PlayerID FROM Players WHERE PlayerName = @playerName";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@playerName", "Doriel");
                playerID = (int)cmd.ExecuteScalar();

            }
            else
            {
                // Get the PlayerID of the player that was retrieved from the database
                playerID = (int)result;
            }

            // Check if the helmet, chestplate, and weapon have already been inserted into the Gear table
            sql = "SELECT GearID FROM Gear WHERE GearType = @gearType AND Rarity = @rarity AND OtherInfo = @otherInfo";
            cmd = new SQLiteCommand(sql, conn);
            cmd.Parameters.AddWithValue("@gearType", "Helmet");
            cmd.Parameters.AddWithValue("@rarity", 3);
            cmd.Parameters.AddWithValue("@otherInfo", "Alisys ZP Head");
            result = cmd.ExecuteScalar();

            // If the gear has not been inserted, insert it into the Gear table
            if (result == null)
            {
                sql = "INSERT INTO Gear (GearType, Rarity, OtherInfo) VALUES (@gearType, @rarity, @otherInfo)";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@gearType", "Helmet");
                cmd.Parameters.AddWithValue("@rarity", 3);
                cmd.Parameters.AddWithValue("@otherInfo", "Alisys ZP Head");
                cmd.ExecuteNonQuery();

                // Retrieve the ID of the newly inserted gear
                //int gearID = (int)cmd.LastInsertedId;

                // Insert data into the PlayerGear table
                sql = "INSERT INTO PlayerGear (PlayerID, RunID, GearID) VALUES (@playerID, @runID, @gearID)";
                cmd = new SQLiteCommand(sql, conn);
                cmd.Parameters.AddWithValue("@playerID", playerID);
                cmd.Parameters.AddWithValue("@runID", "");
                cmd.Parameters.AddWithValue("@gearID", "");
                cmd.ExecuteNonQuery();

                // Close the database connection
                conn.Close();

                return;
            }

            // Close the database connection
            conn.Close();
        }

        // TODO
        void RetreiveQuestsFromDatabase()
        {
            SQLiteConnection conn = new SQLiteConnection(dataSource);
            
            conn.Open();

            // Create the Quests table
            string sql = "CREATE TABLE IF NOT EXISTS Quests (RunID INTEGER PRIMARY KEY AUTOINCREMENT, QuestID INTEGER, QuestName TEXT, EndTime DATETIME, ObjectiveType TEXT, ObjectiveQuantity INTEGER, StarGrade INTEGER, RankName TEXT, ObjectiveName TEXT, Date DATETIME)";
            SQLiteCommand cmd = new SQLiteCommand(sql, conn);
            cmd.ExecuteNonQuery();

            // Retrieve all quests from the Quests table
            sql = "SELECT * FROM Quests";
            cmd = new SQLiteCommand(sql, conn);
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                // Print the quest data to the console
                Console.WriteLine("Quest: " + reader["QuestName"].ToString());
                Console.WriteLine("End Time: " + reader["EndTime"].ToString());
                Console.WriteLine("Monster: " + reader["Monster"].ToString());
                Console.WriteLine("Gear: " + reader["Gear"].ToString());
                Console.WriteLine();
            }

            // Close the database connection
            conn.Close();
        }

        #endregion
    }
}
