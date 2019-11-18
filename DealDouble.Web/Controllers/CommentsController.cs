using DealDouble.Entities;
using DealDouble.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using DealDouble.Services;

namespace DealDouble.Web.Controllers
{
    public class CommentsController : Controller
    {
        CommentsServices commentsService = new CommentsServices();

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
                    comment.Rating = model.Rating;
                    comment.EntityId = model.EntityId;
                    comment.RecordId = model.RecordId;
                    comment.UserId = User.Identity.GetUserId();
                    comment.Timestamp = DateTime.Now;

                    var commentResult = commentsService.LeaveComment(comment);

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

            model.Comments = commentsService.GetComments(entityId, recordId);

            return PartialView(model);
        }

        [HttpPost]
        public JsonResult DeleteComment(int entityId, int recordId)
        {
            JsonResult result = new JsonResult();

            var deleteResult = commentsService.DeleteEntityComment(entityId, recordId);

            if (deleteResult)
                result.Data = new { success = true };
            else
                result.Data = new { success = false, message = "Oops, something weent wrong." };

            return result;
        }
    }
}