using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeebackApplication.DAL
{
    public class FeedbackInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<FeedbackContext>
    {
        protected override void Seed(FeedbackContext context)
        {
            base.Seed(context);
        }
    }
}