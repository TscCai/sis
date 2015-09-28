using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ncepu.GraduationProject.Sis.Web.Models.UnitIndex;
using Ncepu.GraduationProject.Sis.Persistence;

namespace Ncepu.GraduationProject.Sis.Web.Controllers
{
    [Authorize]
    public class UnitIndexController : BaseController
    {
        //
        // GET: /UnitIndex/

        public UnitIndexController()
        {
            ViewBag.Port = Port;
        }

        public ActionResult Index()
        {
            ViewBag.Protocol = Request.IsSecureConnection ? "wss" : "ws";

            IndexModel model = new IndexModel();
#if NODB
            const int uc = 4;
            model.BoilerInfos = new BoilerInfo[uc];
            model.TurbineInfos = new TurbineInfo[uc];
            model.MotorInfos = new MotorInfo[uc];
            for (int i = 0; i < uc; i++)
            {
                model.BoilerInfos[i] = new BoilerInfo();
                model.TurbineInfos[i] = new TurbineInfo();
                model.MotorInfos[i] = new MotorInfo();
                model.BoilerInfos[i].Id = (i + 1) + "#";
                model.TurbineInfos[i].Id = (i + 1) + "#";
                model.MotorInfos[i].Id = (i + 1) + "#";
            }
#endif

            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Modal()
        {
            QueryModel qm = new QueryModel();

            return View(qm);
        }

        [HttpPost]
        public ActionResult Query(QueryModel qm)
        {

            bool isCompare = false;
            if (qm.Date_02.Year != 1)
            {
                isCompare = true;
            }

            DoQuery(qm, isCompare);

            if (!isCompare)
            {
                return View("Query", qm);
            }
            else
            {
                return View("QueryCompare", qm);
            }
        }

        private void DoQuery(QueryModel qm, bool isCompare)
        {
            List<int> chk_id = new List<int>();
            int i = 0;
            foreach (var item in qm.Data)
            {
                if (item.IsChecked)
                {
                    chk_id.Add(i);
                    qm.Data[i].Id = i + 1;
                }
                i++;
            }

            DbModelDataContext db = DbContextFactory.Create();
            if (qm.IsBoilerChecked)
            {
                for (int j = 0; j < chk_id.Count(); j++)
                {
                    qm.Data[chk_id[j]].BoilerCollection = new List<SIS_BoilerIndex>[2];
                    if (!isCompare)
                    {
                        FillBoilerInfo(db, qm, chk_id[j], qm.Date_01);
                    }
                    else
                    {
                        FillBoilerInfo(db, qm, chk_id[j], qm.Date_01, qm.Date_02);
                    }
                }
            }

            if (qm.IsTurbineChecked)
            {
                for (int j = 0; j < chk_id.Count(); j++)
                {
                    qm.Data[chk_id[j]].TurbineCollection = new List<SIS_TurbineIndex>[2];
                    if (!isCompare)
                    {
                        FillTurbineInfo(db, qm, chk_id[j], qm.Date_01);
                    }
                    else
                    {
                        FillTurbineInfo(db, qm, chk_id[j], qm.Date_01, qm.Date_02);
                    }
                }
            }

            if (qm.IsMotorChecked)
            {
                for (int j = 0; j < chk_id.Count(); j++)
                {
                    qm.Data[chk_id[j]].MotorCollection = new List<SIS_MotorIndex>[2];
                    if (!isCompare)
                    {
                        FillMotorInfo(db, qm, chk_id[j], qm.Date_01);
                    }
                    else
                    {
                        FillMotorInfo(db, qm, chk_id[j], qm.Date_01, qm.Date_02);
                    }
                }
            }

        }

        private void FillBoilerInfo(DbModelDataContext db, QueryModel qm, int index, DateTime date)
        {
            var raw = db.SIS_BoilerIndex.Where(b => b.UnitId == index + 1 && b.Timestamp.Date == date);
            qm.Data[index].BoilerCollection[0] = raw.ToList();
        }

        private void FillBoilerInfo(DbModelDataContext db, QueryModel qm, int index, DateTime date_01, DateTime date_02)
        {
            var raw1 = db.SIS_BoilerIndex.Where(b => b.UnitId == index + 1 && b.Timestamp.Date == date_01);
            var raw2 = db.SIS_BoilerIndex.Where(b => b.UnitId == index + 1 && b.Timestamp.Date == date_02);
            qm.Data[index].BoilerCollection[0] = raw1.ToList();
            qm.Data[index].BoilerCollection[1] = raw2.ToList();

        }

        private void FillTurbineInfo(DbModelDataContext db, QueryModel qm, int index, DateTime date)
        {
            var raw = db.SIS_TurbineIndex.Where(b => b.UnitId == index + 1 && b.Timestamp.Date == date);
            qm.Data[index].TurbineCollection[0] = raw.ToList();
        }

        private void FillTurbineInfo(DbModelDataContext db, QueryModel qm, int index, DateTime date_01, DateTime date_02)
        {
            var raw1 = db.SIS_TurbineIndex.Where(b => b.UnitId == index + 1 && b.Timestamp.Date == date_01);
            var raw2 = db.SIS_TurbineIndex.Where(b => b.UnitId == index + 1 && b.Timestamp.Date == date_02);
            qm.Data[index].TurbineCollection[0] = raw1.ToList();
            qm.Data[index].TurbineCollection[1] = raw2.ToList();
        }

        private void FillMotorInfo(DbModelDataContext db, QueryModel qm, int index, DateTime date)
        {
            var raw = db.SIS_MotorIndex.Where(b => b.UnitId == index + 1 && b.Timestamp.Date == date);
            qm.Data[index].MotorCollection[0] = raw.ToList();
        }

        private void FillMotorInfo(DbModelDataContext db, QueryModel qm, int index, DateTime date_01, DateTime date_02)
        {
            var raw1 = db.SIS_MotorIndex.Where(b => b.UnitId == index + 1 && b.Timestamp.Date == date_01);
            var raw2 = db.SIS_MotorIndex.Where(b => b.UnitId == index + 1 && b.Timestamp.Date == date_02);
            qm.Data[index].MotorCollection[0] = raw1.ToList();
            qm.Data[index].MotorCollection[1] = raw2.ToList();

        }

    }
}
