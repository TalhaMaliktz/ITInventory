using System;
using System.IO;
using System.Text;
using System.Data;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;


using Korzh.EasyQuery.Db;
using Korzh.EasyQuery.Services;
using Korzh.EasyQuery.Services.Db;
using Korzh.EasyQuery.EF;
using Korzh.EasyQuery.Mvc;
using Korzh.Utils.Db;
using ITInventory.Models;
using Korzh.EasyQuery;

namespace ITInventory.Controllers
{
    public class AdvancedSearchController : Controller {
        private EqServiceProviderDb eqService;

        public AdvancedSearchController() {

            var context = new ApplicationDbContext();
            context.Database.Initialize(false);
            var connection = context.Database.Connection;

            eqService = new EqServiceProviderDb();
			eqService.StoreModelInSession = true;
			eqService.StoreQueryInSession = true;
            eqService.Connection = connection;
            
            eqService.ModelLoader = (model, modelName) => {
                model.Clear();
                model.LoadFromDBContext(context); 
            };

           
            eqService.SessionGetter = key => Session[key];
            eqService.SessionSetter = (key, value) => Session[key] = value;


        }

		public ActionResult Index() {
            return View("AdvancedSearch");
        }


        #region EasyQuery actions

        /// <summary>
        /// Gets the model by its name
        /// </summary>
        /// <param name="modelName">The name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetModel(string modelName) {
           var  Model = new DbModel();
           Model.LoadFromContext(typeof(ApplicationDbContext), DataModel.ContextLoadingOptions.JoinUsingPrimitiveTypes | DataModel.ContextLoadingOptions.ScanOnlyQueryable);
           return Json(Model.SaveToDictionary());
            
            //var model = eqService.GetModel(modelName);
            //return Json(model.SaveToDictionary());
        }

        /// <summary>
        /// This action returns a custom list by different list request options (list name).
        /// </summary>
        /// <param name="options">List request options - an instance of <see cref="ListRequestOptions"/> type.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetList(ListRequestOptions options) {
            return Json(eqService.GetList(options));
        }

        /// <summary>
        /// Gets the query by its name
        /// </summary>
        /// <param name="queryName">The name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetQuery(string queryName) {
            var query = eqService.GetQuery(queryName);
            return Json(query.SaveToDictionary());
        }

        /// <summary>
        /// Saves the query.
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query .</param>
        /// <param name="queryName">Query name.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SaveQuery(string queryJson, string queryName) {
            eqService.SaveQueryDict(queryJson.ToDictionary(), queryName);
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("result", "OK");
            return Json(dict);
        }

        /// <summary>
        /// It's called when it's necessary to synchronize query on client side with its server-side copy.
        /// Additionally this action can be used to return a generated SQL statement (or several statements) as JSON string
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query .</param>
        /// <param name="optionsJson">The additional parameters which can be passed to this method to adjust query statement generation.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SyncQuery(string queryJson, string optionsJson) {
            var query = eqService.SyncQueryDict(queryJson.ToDictionary());

            var statement = eqService.BuildQuery(query, optionsJson.ToDictionary());
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("statement", statement);
            return Json(dict);
        }


        /// <summary>
        /// Executes the query passed as JSON string and returns the result record set (again as JSON).
        /// </summary>
        /// <param name="queryJson">The JSON representation of the query.</param>
        /// <param name="optionsJson">Different options in JSON format</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ExecuteQuery(string queryJson, string optionsJson) {
            var query = eqService.LoadQueryDict(queryJson.ToDictionary());

            //query.Options.SelectTop = "1000";

            var statement = eqService.BuildQuery(query, optionsJson.ToDictionary());
            var resultSet = eqService.GetDataSetBySql(statement);


            var resultSetDict = eqService.DataSetToDictionary(resultSet, optionsJson.ToDictionary());
            Dictionary<string, object> dict = new Dictionary<string, object>();
            dict.Add("statement", statement);
            dict.Add("resultSet", resultSetDict);
            return Json(dict);
        }


        [HttpGet]
        public JsonResult GetQueryList(string modelName) {
            var queries = eqService.GetQueryList(modelName);

            return Json(queries, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public FileResult GetCurrentQuery() {
            var query = eqService.GetQuery();
            FileContentResult result = new FileContentResult(Encoding.UTF8.GetBytes(query.SaveToString()), "Content-disposition: attachment;");
            result.FileDownloadName = "CurrentQuery.xml";
            return result;
        }

        private void ErrorResponse(string msg) {
            Response.StatusCode = 400;
            Response.Write(msg);
            Response.Output.Flush();
        }
		
        [HttpPost]
        public ActionResult LoadQueryFromFile(HttpPostedFileBase queryFile) {  
            if (queryFile != null && queryFile.ContentLength > 0)  
                try {
                    var query = eqService.GetQuery();
                    query.LoadFromStream(queryFile.InputStream);
                    eqService.SyncQuery(query);
                }  
                catch (Exception ex){  
                    TempData["Message"] = "ERROR:" + ex.Message.ToString();  
                }  
            else{  
                TempData["Message"] = "You have not specified a file.";  
            }

            return RedirectToAction("Index");
        }
		
        [HttpGet]
        public void ExportToFileExcel() {
            Response.Clear();

            var query = eqService.GetQuery();

            if (!query.IsEmpty) {
                var sql = eqService.BuildQuery(query);
                eqService.Paging.Enabled = false;
                DataSet dataset = eqService.GetDataSetBySql(sql);
                if (dataset != null) {
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.AddHeader("Content-Disposition",
                        string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode("report.xls")));
                    DbExport.ExportToExcelHtml(dataset, Response.Output, ExcelFormats.Default);
                }
                else
                    ErrorResponse("Empty dataset");
            }
            else
                ErrorResponse("Empty query");
            
        }

        [HttpGet]
        public void ExportToFileCsv() {
            Response.Clear();
            var query = eqService.GetQuery();

            if (!query.IsEmpty) {
                var sql = eqService.BuildQuery(query);
                eqService.Paging.Enabled = false;
                DataSet dataset = eqService.GetDataSetBySql(sql);
                if (dataset != null) {
                    Response.ContentType = "text/csv";
                    Response.AddHeader("Content-Disposition",
                        string.Format("attachment; filename=\"{0}\"", HttpUtility.UrlEncode("report.csv")));
                    DbExport.ExportToCsv(dataset, Response.Output, CsvFormats.Default);
                }
                else
                    ErrorResponse("Empty dataset");
            }
            else
                ErrorResponse("Empty query");

        }

        #endregion

    }

}
