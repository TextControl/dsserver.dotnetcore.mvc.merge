using DSServerMerge.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using TXTextControl.DocumentServices.DocumentProcessing;
using Newtonsoft.Json;

namespace DSServerMerge.Controllers {
	public class HomeController : Controller {
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger) {
			_logger = logger;
		}

		[HttpPost]
		public IActionResult Merge(Template template) {

			// private OAuth settings
			var os = new TXTextControl.DocumentServices.DocumentProcessing.OAuth.OAuthSettings(
					 "dsserver.EMRWLJAdPpZirYLDNAF0gP7QzQaB8OfH",
					 "OGC24l8BcjVpc90SHt0gHJhDjtYJP70a");

			// create a new DocumentProcessing object with OAuth settings
			DocumentProcessing s_dp = new DocumentProcessing("https://trial.dsserver.io", os);

			// load the merge data from JSON text file
			List<Report> journal = 
				JsonConvert.DeserializeObject<List<Report>>(System.IO.File.ReadAllText("App_Data/data.json"));

			// create a new MergeBody object with merge data and template
			MergeBody mergeBody = new MergeBody() {
				Template = Convert.FromBase64String(template.Document),
				MergeData = journal
			};

			// merge the document
			var results = s_dp.Merge(mergeBody, ReturnFormat.TX).Result;

			return Ok(results[0]);
		}

		public IActionResult Index() {

			// load the merge data from JSON text file
			List<Report> journal = 
				JsonConvert.DeserializeObject<List<Report>>(System.IO.File.ReadAllText("App_Data/data.json"));

			return View(journal);
		}

	}
}
