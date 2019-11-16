using DealDouble.Entities;
using DealDouble.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DealDouble.Web.ViewModels;
using Microsoft.AspNet.Identity;

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

        [HttpPost]
        public JsonResult LeaveComment(CommentViewModel model)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (User.Identity.IsAuthenticated)
            {
                if (ModelState.IsValid)
                {
                    var comment = new Comment();

                    comment.Body = model.Body;
                    comment.EntityId = model.EntityId;
                    comment.RecordId = model.RecordId;
                    comment.UserId = User.Identity.GetUserId();
                    comment.Timestamp = DateTime.Now;

                    var commentResult = service.LeaveComment(comment);

                    result.Data = new { success = true };
                }
                else
                {
                    result.Data = new { success = false, message = "Invalid input.", isSwitchUrl = false };
                }

            }
            else
            {
                result.Data = new { success = false, message = "you've to login in first.", isSwitchUrl = true };
            }

            return result;
        }

        public PartialViewResult EntityComments(int entityId, int recordId)
        {
            var model = new CommentsViewModel();

            model.Comments = service.GetComments(entityId, recordId);

            return PartialView(model);
        }
    }
}