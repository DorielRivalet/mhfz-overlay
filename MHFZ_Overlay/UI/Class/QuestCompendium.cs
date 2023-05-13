﻿namespace MHFZ_Overlay.UI.Class
{
    //TODO: ORM
    public class QuestCompendium
    {
        public long MostCompletedQuestRuns { get; set; }
        public long MostCompletedQuestRunsAttempted { get; set; }
        public long MostCompletedQuestRunsQuestID { get; set; }
        public long MostAttemptedQuestRuns { get; set; }
        public long MostAttemptedQuestRunsCompleted { get; set; }
        public long MostAttemptedQuestRunsQuestID { get; set; }
        public long TotalQuestsCompleted { get; set; }
        public long TotalQuestsAttempted { get; set; }
        public double QuestCompletionTimeElapsedAverage { get; set; }
        public double QuestCompletionTimeElapsedMedian { get; set; }
        public long TotalTimeElapsedQuests { get; set; }
        public double TotalCartsInQuestsAverage { get; set; }
        public double TotalCartsInQuestsMedian { get; set; }
        public long MostCompletedQuestWithCarts { get; set; }
        public long MostCompletedQuestWithCartsQuestID { get; set; }
        public long TotalCartsInQuest { get; set; }
        public double TotalCartsInQuestAverage { get; set; }
        public double TotalCartsInQuestMedian { get; set; }
        public double QuestPartySizeAverage { get; set; }
        public double QuestPartySizeMedian { get; set; }
        public long QuestPartySizeMode { get; set; }
        public double PercentOfSoloQuests { get; set; }
        public double PercentOfGuildFood { get; set; }
        public double PercentOfDivaSkill { get; set; }
        public double PercentOfSkillFruit { get; set; }
        public long MostCommonDivaSkill { get; set; }
        public long MostCommonGuildFood { get; set; }
    }
}
