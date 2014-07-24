using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Tasks
/// </summary>
public class Tasks
{

        public string id_tasks { get; set; }
        public string description { get; set; }
        public string priority { get; set; }
        public string duedate { get; set; }
        public string createdby { get; set; }
        public string createddate { get; set; }
        public string completeddate { get; set; }
        public string finalapprovaldate { get; set; }
        public string deleted { get; set; }
        public string assignedtoid { get; set; }
        public string assignedto { get; set; }

       /* public Tasks(string id_tasks, string description, string priority, string duedate, string createdby, string createddate, string completeddate, string finalapprovaldate, string deleted)
        {

            this.id_tasks = id_tasks;
            this.description = description;
            this.priority = priority;
            this.duedate = duedate;
            this.createdby = createdby;
            this.createddate = createddate;
            this.completeddate = completeddate;
            this.finalapprovaldate = finalapprovaldate;
            this.deleted = deleted;
        }

        public Tasks(string id_tasks, string description, string priority, string duedate, string createdby, string createddate )
        {

            this.id_tasks = id_tasks;
            this.description = description;
            this.priority = priority;
            this.duedate = duedate;
            this.createdby = createdby;
            this.createddate = createddate;

        }*/


}