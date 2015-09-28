using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ncepu.GraduationProject.Sis.Web.Models.EnvironmentalIndex;
using Ncepu.GraduationProject.Sis.Persistence;


namespace Ncepu.GraduationProject.Sis.Web.Controllers
{
    [Authorize]
    public class EnvironmentalIndexController : BaseController
    {
        //
        // GET: /EnvironmentalIndex/

        public EnvironmentalIndexController()
        {

            ViewBag.Port = Port;
        }

        public ActionResult Index()
        {
            ViewBag.Protocol = Request.IsSecureConnection ? "wss" : "ws";
            return View();
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
            DbModelDataContext db = DbContextFactory.Create();
            DateTime date_01 = qm.Date_01;
            DateTime date_02 = qm.Date_02;
            IQueryable<SIS_EnvrionmentalIndex> raw1 = db.SIS_EnvrionmentalIndex.Where(e => e.Timestamp.Date == date_01.Date);
            IQueryable<SIS_EnvrionmentalIndex> raw2=null;
            qm.Data = new List<DataCollection>[2];
            bool isCompare=false;
            if (date_02.Year != 1)
            {
                isCompare = true;
                raw2 = db.SIS_EnvrionmentalIndex.Where(e => e.Timestamp.Date == date_02.Date);
            }
            
            DoQuery(raw1, qm, 0);
            if (isCompare)
            {
                DoQuery(raw2, qm, 1);
            }

            db.Dispose();
            if (isCompare)
            {
                return View("QueryCompare",qm);
            }
            else
            {
                return View("Query",qm);
            }
        }

        private void DoQuery(IQueryable<SIS_EnvrionmentalIndex> raw,QueryModel qm,int index)
        {

            qm.Data[index] = new List<DataCollection>();
            foreach (var item in raw)
            {
                DataCollection dc = new DataCollection();

                if (qm.IsDNPCChecked)
                {
                    dc.DNPC = item.DNPC ?? 0;
                }
                if (qm.IsDSPCChecked)
                {
                    dc.DSPC = item.DSPC ?? 0;
                }
                if (qm.IsSO2Checked)
                {
                    dc.SO2 = item.SO2 ?? 0;
                }
                if (qm.IsNOxChecked)
                {
                    dc.NOX = item.NOX ?? 0;
                }
                dc.Timestamp = item.Timestamp;
                qm.Data[index].Add(dc);
            }
        }

    }
}
