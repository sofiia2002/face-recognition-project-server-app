using System;
using System.Net;
using System.Threading.Tasks;
using FaceRecognitionServer.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using FaceRecognitionServer.Interfaces.Repositories;

namespace FaceRecognitionServer.Web.Controllers
{
    public class FaceRecognitionController : Controller
    {
        private readonly IFaceAPIClient faceClient;
        protected readonly NLog.ILogger Logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public FaceRecognitionController(IFaceAPIClient faceClient)
        {
            this.faceClient = faceClient;
        }

        public IActionResult Create()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var personLiitViewModel = new PersonListViewModel
            {
                List = await faceClient.GetAllAsync()
            };

            return View(personLiitViewModel);
        }

        public IActionResult Recognize()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Detect(string imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl))
            {
                return BadRequest("URL is required");
            }
            try
            {
                var imgdata = new WebClient().DownloadData(imageUrl);
                return Json(await faceClient.RecognizeAsync(imgdata));
            }
            catch(WebException)
            {
                return BadRequest("Error accessing image, please try another");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Error recognizing");
            }

            return UnprocessableEntity("Please try again later");
        }

        [HttpPost]
        public async Task<IActionResult> Create(PersonViewModel personViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var imgdata = new WebClient().DownloadData(personViewModel.ImageUrl);
                    await faceClient.CreatePersonAsync(personViewModel.Name, personViewModel.Description, imgdata);
                    TempData["Message"] = "Success";
                    return RedirectToAction("Create");
                }
                catch(WebException)
                {
                    ModelState.AddModelError("", "Error accessing image, please try another");
                }
                catch(Exception ex)
                {
                    Logger.Error(ex, "Error creating person");
                    ModelState.AddModelError("", "Please try again later");
                }
            }

            return View();
        }
    }
}
