using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ncepu.GraduationProject.Sis.Web.Models.General.Index;
using Ncepu.GraduationProject.Sis.Persistence;

namespace Ncepu.GraduationProject.Sis.Web.Controllers
{
    [Authorize]
    public class GeneralController : BaseController
    {
        //
        // GET: /General/

        public GeneralController()
        {
            ViewBag.Port = Port;
        }

        public ActionResult Index()
        {
            ViewBag.Protocol = Request.IsSecureConnection ? "wss" : "ws";

            IndexModel model = new IndexModel();
            model.GeneralInfo = new GeneralInfo();
#if NODB
            model.GeneralInfo.DailyGenerate = 62781.140M;
            model.GeneralInfo.TotalInstalledCapacity = 3600;
            model.GeneralInfo.MonthlyGenerate = 1976390.370M;
            model.GeneralInfo.YearlyGenerate = 10804267.356M;

            model.Unit = new Unit[4];
            model.Unit[0] = new Unit();
            model.Unit[0].Id = 1;
            model.Unit[0].CurrentLoad = 0M;
            model.Unit[0].InstalledCapacity = 800;
            model.Unit[0].Status = UnitStatus.Running;

            model.Unit[1] = new Unit();
            model.Unit[1].Id = 2;
            model.Unit[1].CurrentLoad = 0M;
            model.Unit[1].InstalledCapacity = 800;
            model.Unit[1].Status = UnitStatus.Stop;

            model.Unit[2] = new Unit();
            model.Unit[2].Id = 3;
            model.Unit[2].CurrentLoad = 0M;
            model.Unit[2].InstalledCapacity = 1000;
            model.Unit[2].Status = UnitStatus.Running;

            model.Unit[3] = new Unit();
            model.Unit[3].Id = 4;
            model.Unit[3].CurrentLoad = 0M;
            model.Unit[3].InstalledCapacity = 1000;
            model.Unit[3].Status = UnitStatus.Running;
#else
            
#endif
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Modal(QueryModel qm)
        {
            qm.Date_01 = DateTime.Now;

            qm.Unit = new Unit[4];
            //DbEntities db = new DbEntities();
            DbModelDataContext db = DbContextFactory.Create();
            var units = db.SIS_Unit;
            int i = 0;
            foreach (var item in units)
            {
                qm.Unit[i] = new Unit();
                qm.Unit[i].Id = item.UnitId;
                i++;
            }
            db.Dispose();
            return View(qm);
        }

        [HttpPost]
        public ActionResult Query(QueryModel qm)
        {
            QueryModel[] result;
            if (qm.Date_02.Year == 1)
            {
                result = new QueryModel[1];
                result[0] = qm;
                result = DoQuery(result, false);
                return View("Query", result.ToList());
            }
            else
            {
                result = new QueryModel[2];
                result[0] = qm;
                result[1]=new QueryModel();
                result[1].GeneralInfo = new GeneralInfo();
                result[1].Unit = new Unit[4];
                for (int i = 0; i < 4; i++)
                {
                    result[1].Unit[i] = new Unit();
                }
                result = DoQuery(result, true);
                return View("QueryCompare",result.ToList());
            }
        }

        private QueryModel[] DoQuery(QueryModel[] qm,bool isCompare)
        {

            DbModelDataContext db = DbContextFactory.Create();
            if (qm[0].GeneralInfo.IsTotalInstalledCapacityChecked)
            {
                qm[0].GeneralInfo.TotalInstalledCapacity = db.SIS_PlantSummary.First().InstalledCapacity;
            }
            if (qm[0].GeneralInfo.IsDailyGenerateChecked)
            {
                qm[0].GeneralInfo.DailyGenerate = db.SIS_TotalPowerGeneration_Daily
                    .Single(g => g.Timestamp.Year == qm[0].Date_01.Year
                    && g.Timestamp.Month == qm[0].Date_01.Month 
                    && g.Timestamp.Day == qm[0].Date_01.Day)
                    .PowerGenerate ?? 0;
                if (isCompare)
                {
                    qm[1].GeneralInfo.DailyGenerate = db.SIS_TotalPowerGeneration_Daily
                    .Single(g => g.Timestamp.Year == qm[0].Date_02.Year
                    && g.Timestamp.Month == qm[0].Date_02.Month
                    && g.Timestamp.Day == qm[0].Date_02.Day)
                    .PowerGenerate ?? 0;
                }
            }
            if (qm[0].GeneralInfo.IsMonthlyGenerateChecked)
            {
                var total = db.SIS_TotalPowerGeneration_Monthly
                    .Single(g => g.Timestamp.Year == qm[0].Date_01.Year && g.Timestamp.Month == qm[0].Date_01.Month)
                    .PowerGenerate;
                qm[0].GeneralInfo.MonthlyGenerate = total ?? 0;
                if (isCompare)
                {
                    var total2 = db.SIS_TotalPowerGeneration_Monthly
                    .Single(g => g.Timestamp.Year == qm[0].Date_02.Year && g.Timestamp.Month == qm[0].Date_02.Month)
                    .PowerGenerate;
                    qm[1].GeneralInfo.MonthlyGenerate = total2 ?? 0;
                }
            }
            if (qm[0].GeneralInfo.IsYearlyGenerateChecked)
            {
                var total = db.SIS_TotalPowerGeneration_Yearly.Single(g => g.Timestamp.Year == qm[0].Date_01.Year).PowerGenerate;
                qm[0].GeneralInfo.YearlyGenerate = total ?? 0;
                if (isCompare)
                {
                    var total2 = db.SIS_TotalPowerGeneration_Yearly.Single(g => g.Timestamp.Year == qm[0].Date_02.Year).PowerGenerate;
                    qm[1].GeneralInfo.YearlyGenerate = total2 ?? 0;

                }
            }

            List<int> chk_id = new List<int>();
            for (int i = 0; i < 4; i++)
            {
                if (qm[0].Unit[i].IsIdChecked)
                {
                    chk_id.Add(i + 1);
                    qm[0].Unit[i].Id = i + 1;
                    if (isCompare)
                    {
                        qm[1].Unit[i].Id = i+1;
                        qm[1].Unit[i].IsIdChecked = true;
                    }
                }
            }

          

            if (qm[0].GeneralInfo.IsHourlyTotalAvgLoadChecked)
            {
                qm[0].GeneralInfo.HourlyAvgTotalLoadList = new List<decimal>();
                var total = db.SIS_TotalPowerGeneration_Hourly.Where(g => g.Timestamp.Year == qm[0].Date_01.Year
                    && g.Timestamp.Month == qm[0].Date_01.Month && g.Timestamp.Day == qm[0].Date_01.Day);
                foreach (var item in total)
                {
                    qm[0].GeneralInfo.HourlyAvgTotalLoadList.Add(item.PowerGenerate ?? 0);
                }
                if (isCompare)
                {
                    qm[1].GeneralInfo.HourlyAvgTotalLoadList = new List<decimal>();
                    var total2 = db.SIS_TotalPowerGeneration_Hourly.Where(g => g.Timestamp.Year == qm[0].Date_02.Year
                        && g.Timestamp.Month == qm[0].Date_02.Month && g.Timestamp.Day == qm[0].Date_02.Day);
                    foreach (var item in total2)
                    {
                        qm[1].GeneralInfo.HourlyAvgTotalLoadList.Add(item.PowerGenerate ?? 0);
                    }
                }
            }

            if (qm[0].Unit[0].IsHourlyAvgLoadChecked)
            {

                for (int i = 0; i < chk_id.Count(); i++)
                {
                    int id = chk_id[i];
                    var generate = db.SIS_PowerGeneration_Hourly.Where(g => g.UnitId == id
                        && g.Timestamp.Year == qm[0].Date_01.Year
                        && g.Timestamp.Month == qm[0].Date_01.Month
                        && g.Timestamp.Day == qm[0].Date_01.Day
                        );
                    qm[0].Unit[id-1].HourlyAvgLoadList = new List<decimal>();
                    foreach (var item in generate)
                    {
                        qm[0].Unit[id - 1].HourlyAvgLoadList.Add(item.PowerGeneration ?? 0);
                    }
                }

                if (isCompare)
                {
                    for (int i = 0; i < chk_id.Count(); i++)
                    {
                        int id = chk_id[i];
                        var generate = db.SIS_PowerGeneration_Hourly.Where(g => g.UnitId == id
                            && g.Timestamp.Year == qm[0].Date_02.Year
                            && g.Timestamp.Month == qm[0].Date_02.Month
                            && g.Timestamp.Day == qm[0].Date_02.Day
                            );
                        qm[1].Unit[id - 1].HourlyAvgLoadList = new List<decimal>();
                        foreach (var item in generate)
                        {
                            qm[1].Unit[id - 1].HourlyAvgLoadList.Add(item.PowerGeneration ?? 0);
                        }
                    }
                }
            }
            db.Dispose();

            return qm;

        }
    }
}
