using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Kids.LoggingHelper;

namespace Kids.EntitiesModel
{
    public class Game_DataProvider : BaseDataProvider
    {
        public static List<Game> GetGame(int? GameId = null, string GameName = null)
        {
            long PageCount;
            return GetGame(out PageCount, GameId, GameName);
        }

        public static List<Game> GetGame(out long PageCount, int? GameId = null, string GameName = null,
                                         int Currentpage = 1, int PageSize = DefaultPageSize)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {

                var q = from m in ctx.Games.Include("ScoreTypes")
                        where (!GameId.HasValue || m.GameId == GameId.Value) &&
                        (string.IsNullOrEmpty(GameName) || m.Name.Contains(GameName))
                        orderby m.GameId descending
                        select m;
                PageCount = q.LongCount();
                return q.Skip((Currentpage - 1) * PageSize).Take(PageSize).ToList();


            }
        }


        public static void SaveGame(Game game)
        {
            using (var ctx = new BMIKidsEntities(ConnectionString))
            {
                try
                {
                    if (game.ChangeTracker.State == ObjectState.Unchanged)
                        game.MarkAsModified();

                    ctx.Games.ApplyChanges(game);
                    ctx.SaveChanges();
                }
                catch (Exception ex)
                {
                    LogUtility.WriteEntryEventLog("Game_DataProvider_DataProvider", ex, EventLogEntryType.Information);
                    if (ex.InnerException != null)
                        throw ex.InnerException;
                    throw;
                }
            }
        }



    }
}
