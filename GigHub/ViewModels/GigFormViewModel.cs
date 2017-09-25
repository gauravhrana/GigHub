using GigHub.Models;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using GigHub.Controllers;
using System.Web.Mvc;
using System.Linq.Expressions;

namespace GigHub.ViewModels
{
    public class GigFormViewModel
    {

        public int Id { get; set; }

        [Required]
        public string Venue { get; set; }

        [Required]
        [FutureDate]
        public string Date { get; set; }

        [Required]
        [ValidTime]
        public string Time { get; set; }

        [Required]
        public byte Genre { get; set; }

        public IEnumerable<Genre> Genres { get; set; }

        public string Heading { get; set; }

        public string Action
        {
            get
            {

                Expression<Func<GigsController, ActionResult>> create = 
                    (c => c.Create(this));

                Expression < Func <GigsController, ActionResult>> update = 
                    (c => c.Update(this));

                var action = (Id != 0) ? update : create;
                return (action.Body as MethodCallExpression).Method.Name;
            }
        }

        public DateTime GetDateTime()
        {
            return DateTime.Parse(string.Format("{0} {1}", Date, Time));
        }
    }
}