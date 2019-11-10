﻿using DealDouble.Entities;
using DealDouble.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DealDouble.Web.Controllers
{
    public class SharedController : Controller
    {
        SharedService service = new SharedService();

        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult json = new JsonResult();

            var picturesJSON = new List<object>();

            var pictures = Request.Files;

            for (int i = 0; i < pictures.Count; i++)
            {
                var picture = pictures[i];

                //create picName, and path(url) 
                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var path = Server.MapPath("~/Content/images/") + fileName;

                var picUrl = "/Content/images/" + fileName;
                //upload pic into images file
                picture.SaveAs(path);

                //save pic url in db
                var newPic = new Picture();
                newPic.Url = picUrl;

                int picId = service.SavePicture(newPic);

                picturesJSON.Add(new { id = picId, url = picUrl });
            }

            json.Data = picturesJSON;

            return json;
        }
    }
}